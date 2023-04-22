using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.NewSpellSystem.UI;

namespace DamageSystem.NewSpellSystem.Core
{
    public class PlayerSpellManager : MonoBehaviour
    {
        public bool canCast = true;
        public Transform spellOrigin;

        //Editable binds in inspector (maybe add [Serialize Field] and make it private?)
        public List<KeyCode> primaryBinds = new List<KeyCode>{ KeyCode.Mouse0, KeyCode.Mouse1 };
        public List<KeyCode> secondaryBinds = new List<KeyCode>{KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3};

        //Lists of spells attached to player
        public List<GameObject> primarySpells = new List<GameObject>();
        public List<GameObject> secondarySpells = new List<GameObject>();

        //The amount of spells that you can carry at one time
        public int maxPrimarySpells = 3;
        public int maxSecondarySpells = 3;

        //Parents under which new spells will be attached
        public GameObject primarySpellParent;
        public GameObject secondarySpellParent;

        Dictionary<KeyCode, SpellType> primarySpellsDict;
        Dictionary<KeyCode, SpellType> secondarySpellsDict;
        
        [HideInInspector]
        public Dictionary<SpellType, float> secondarySpellCooldowns;

        KeyValuePair<KeyCode, SpellType> activePrimarySpell;
        KeyValuePair<KeyCode, SpellType> queuedPrimarySpell;
        
        [HideInInspector]
        public SpellType queuedSecondarySpell;
        [HideInInspector]
        public float secondaryCastTime = 0f;

        float activeSpellCooldown = 0f;

        bool primaryCasting = false;
        bool secondaryCasting = false;

        //attributes used in UI elements
        public Dictionary<KeyCode, SpellType> primaryKeybindSpellPairs { get => primarySpellsDict; }
        public Dictionary<KeyCode, SpellType> secondaryKeybindSpellPairs { get => secondarySpellsDict; }
        public Dictionary<SpellType, float> secondarySpellColldownDict { get => secondarySpellCooldowns; }
        public KeyValuePair<KeyCode, SpellType> primaryActiveSpell { get => activePrimarySpell; }

        //Reference to the UI script
        [SerializeField]
        private SpellDisplayUI spellUI;

        //Reference to the UI spell selection box
        [SerializeField]
        private SpellSelectionBox selectionBox;

        private void Start()
        {
            primarySpellsDict = new Dictionary<KeyCode, SpellType>();
            secondarySpellsDict = new Dictionary<KeyCode, SpellType>();
            secondarySpellCooldowns = new Dictionary<SpellType, float>();
            Debug.Log("PRIMARY SPELLS:");
            for(int i=0; i<primarySpells.Count && i<primaryBinds.Count; i++)
            {
                primarySpellsDict.Add(primaryBinds[i], primarySpells[i].GetComponent<SpellType>());
                Debug.Log(primaryBinds[i] + " - " + primarySpells[i].name);
            }

            Debug.Log("SECONDARY SPELLS:");
            for (int i = 0; i < secondarySpells.Count && i < secondaryBinds.Count; i++)
            {
                secondarySpellsDict.Add(secondaryBinds[i], secondarySpells[i].GetComponent<SpellType>());
                secondarySpellCooldowns.Add(secondarySpells[i].GetComponent<SpellType>(), Time.time);
                Debug.Log(secondaryBinds[i] + " - " + secondarySpells[i].name);
            }
            //Call this method to update the spell UI
            spellUI.UpdateSpellList();
        }
        private void Update()
        {
            AssignActivePrimarySpell();
            UsePrimarySpell(activePrimarySpell.Value, activePrimarySpell.Key);
            QueueNextSecondarySpell();
            UseSecondarySpell(queuedSecondarySpell);
        }

        void AssignActivePrimarySpell()
        {
            if (canCast)
            {
                foreach (KeyValuePair<KeyCode, SpellType> entry in primarySpellsDict) {
                    if (Input.GetKeyDown(entry.Key))
                    {
                        if (!primaryCasting)
                        {
                            primaryCasting = true;
                            //StartCoroutine(primaryCooldown(entry.Value, entry.Key));
                            //newPrimaryCooldown(entry.Value, entry.Key);
                            activePrimarySpell = new KeyValuePair<KeyCode, SpellType>(entry.Key, entry.Value);
                        }
                        else {
                            queuedPrimarySpell = new KeyValuePair<KeyCode, SpellType>(entry.Key, entry.Value);
                        }
                    }
                }
            }
        }
        /*
         * Stara Kurutynka do starej implementacji castowania spelli
        IEnumerator primaryCooldown(SpellType spell, KeyCode key)
        {
            while (primaryCasting)
            {
                spell.Cast(spellOrigin);
                yield return new WaitForSeconds(spell.GetCooldown());

                if (!Input.GetKey(key))
                {
                    primaryCasting = false;
                    spell.StopCast();
                   
                }
            }
            if (queuedSpell.Value && queuedSpell.Value != spell)
            {
                primaryCasting = true;
                StartCoroutine(primaryCooldown(queuedSpell.Value, queuedSpell.Key));
            }
            else
            {
                queuedSpell = new KeyValuePair<KeyCode, SpellType>(KeyCode.None, null);
            }
        }*/

        void UsePrimarySpell(SpellType spell, KeyCode key) {
            if (Input.GetKey(key) && Time.time > activeSpellCooldown)
            {
                spell.Cast(spellOrigin);
                activeSpellCooldown = Time.time + spell.GetCooldown();
            }
            else if (Input.GetKeyUp(key))
            {
                primaryCasting = false;
                spell.StopCast();

                if (queuedPrimarySpell.Value && queuedPrimarySpell.Value != spell)
                {
                    primaryCasting = true;
                    activePrimarySpell = new KeyValuePair<KeyCode, SpellType>(queuedPrimarySpell.Key, queuedPrimarySpell.Value);
                }
                else
                {
                    queuedPrimarySpell = new KeyValuePair<KeyCode, SpellType>(KeyCode.None, null);
                }
            }
        }

        void QueueNextSecondarySpell()
        {
            if (canCast && !secondaryCasting) {
                foreach (KeyValuePair<KeyCode, SpellType> entry in secondarySpellsDict)
                {
                    if (Input.GetKeyDown(entry.Key) && secondarySpellCooldowns[entry.Value] < Time.time)
                    {
                        secondaryCasting = true;
                        //SecondaryCast(entry.Value);
                        queuedSecondarySpell = entry.Value;
                        secondaryCastTime = Time.time + entry.Value.GetCastTime();
                        Debug.Log("Casting " + entry.Value.name);
                    }
                }
            }
        }

        void UseSecondarySpell(SpellType spell) 
        {
            //StartCoroutine(DelayedCast(spell.GetCastTime(), spell));
            if (spell && secondaryCasting && secondaryCastTime < Time.time)
            {
                spell.Cast(spellOrigin);
                secondaryCasting = false;
                queuedSecondarySpell = null; //Is it necessary?
                secondarySpellCooldowns[spell] = Time.time + spell.GetCooldown();
            }
        }

        //Stara implementacja castTime

        //IEnumerator DelayedCast(float delay, SpellType spell)
        //{
        //    Debug.Log("Casting " + spell);
        //    yield return new WaitForSeconds(delay);
        //    spell.Cast(spellOrigin);
        //    secondaryCasting = false;
        //    spellCooldowns[spell] = Time.time + spell.GetCooldown();
        //}

        /*
         * Stara implementacja cooldownów
        IEnumerator Cooldown(float cooldown) {
            Debug.Log("Spell Cooldown: " + cooldown);
            yield return new WaitForSeconds(cooldown);
            secondaryCasting = false;
            Debug.Log("Can cast again");
        }*/

        //Initial implementation of adding spells to the player, returns true for now, but the ret value is used by the SpellBook
        public bool AddSpell(GameObject spell, SpellType ?toReplace, KeyCode replaceBind)
        {
            if (spell.GetComponent<SpellType>())
            {
                //Replace this with instantiating a spell under the player or something
                SpellType addedSpell = spell.GetComponent<SpellType>();
                //Check whether the spell should be added to the primary spell list or the secondary spell list
                if (addedSpell.isPrimarySpell())
                {
                    //Add the spell to primary Spells if there's room for that
                    if(primarySpells.Count < maxPrimarySpells)
                    {
                        // Adding the spell to the primarySpells List
                        primarySpells.Add(spell);
                        // Adding the spell to the primarySpellsDict, using the first free bind in the list
                        primarySpellsDict.Add(primaryBinds[primarySpells.Count - 1], addedSpell);
                        //Changing parent to the player
                        spell.transform.parent = primarySpellParent.transform;
                        Debug.Log("Spell added to primary spells! - " + spell.name);
                        return true;
                    // Check whether this call of the function provides what spell is to be replaced
                    }else if (toReplace)
                    {
                        // Remove the old spell from its list and add the new one
                        primarySpells.Remove(toReplace.gameObject);
                        primarySpells.Add(spell);

                        // Change the spell that is connected with the bind (replaceBind is the bind of the spell we want to replace)
                        primarySpellsDict[replaceBind] = addedSpell;
                        
                        // Change the parent of the added spell
                        spell.transform.parent = primarySpellParent.transform;

                        // Destroy the old spell
                        Destroy(toReplace.gameObject);
                        Debug.Log("Replaced spell " + toReplace.name + " with " + spell.name);
                        return true;
                    }
                    else
                    {
                        // Invoke the spell selection prompt
                        selectionBox.SpellSelection(this, addedSpell, primarySpellsDict, primaryBinds);
                        return true;
                    }
                }else if (addedSpell.isSecondarySpell())
                {
                    if(secondarySpells.Count < maxSecondarySpells)
                    {
                        // Adding the spell to the secondarySpells List
                        secondarySpells.Add(spell);
                        // Adding the spell to the secondarySpellsDict, using the first free bind in the list
                        secondarySpellsDict.Add(secondaryBinds[primarySpells.Count - 1], addedSpell);
                        //Changing parent to the player
                        spell.transform.parent = secondarySpellParent.transform;
                        Debug.Log("Spell added to secondary spells! - " + spell.name);
                        return true;
                    }
                    else if (toReplace)
                    {
                        // Remove the old spell from its list and add the new one
                        secondarySpells.Remove(toReplace.gameObject);
                        secondarySpells.Add(spell);

                        // Change the spell that is connected with the bind (replaceBind is the bind of the spell we want to replace)
                        secondarySpellsDict[replaceBind] = addedSpell;

                        // Change the parent of the added spell
                        spell.transform.parent = secondarySpellParent.transform;

                        // Destroy the old spell
                        Destroy(toReplace.gameObject);
                        Debug.Log("Replaced spell " + toReplace.name + " with " + spell.name);
                    }
                    else
                    {
                        // Invoke the spell selection prompt
                        selectionBox.SpellSelection(this, addedSpell, secondarySpellsDict, secondaryBinds);
                        return true;
                    }
                }
            }
            return false;
        }
    }

   

}