using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.Core
{
    public class PlayerSpellManager : MonoBehaviour
    {
        public bool canCast = true;
        public Transform spellOrigin;

        public KeyCode[] primaryBinds = { KeyCode.Mouse0, KeyCode.Mouse1 };
        public KeyCode[] secondaryBinds = {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3};

        public GameObject[] primarySpells;
        public GameObject[] secondarySpells;

        Dictionary<KeyCode, SpellType> primarySpellsDict;
        Dictionary<KeyCode, SpellType> secondarySpellsDict;
        
        [HideInInspector]
        public Dictionary<SpellType, float> spellCooldowns;

        KeyValuePair<KeyCode, SpellType> activePrimarySpell;
        KeyValuePair<KeyCode, SpellType> queuedPrimarySpell;
        
        [HideInInspector]
        public SpellType queuedSecondarySpell;
        [HideInInspector]
        public float secondaryCastTime = 0f;

        float activeSpellCooldown = 0f;

        bool primaryCasting = false;
        bool secondaryCasting = false;

        private void Start()
        {
            primarySpellsDict = new Dictionary<KeyCode, SpellType>();
            secondarySpellsDict = new Dictionary<KeyCode, SpellType>();
            spellCooldowns = new Dictionary<SpellType, float>();
            Debug.Log("PRIMARY SPELLS:");
            for(int i=0; i<primarySpells.Length && i<primaryBinds.Length; i++)
            {
                primarySpellsDict.Add(primaryBinds[i], primarySpells[i].GetComponent<SpellType>());
                Debug.Log(primaryBinds[i] + " - " + primarySpells[i].name);
            }

            Debug.Log("SECONDARY SPELLS:");
            for (int i = 0; i < secondarySpells.Length && i < secondaryBinds.Length; i++)
            {
                secondarySpellsDict.Add(secondaryBinds[i], secondarySpells[i].GetComponent<SpellType>());
                spellCooldowns.Add(secondarySpells[i].GetComponent<SpellType>(), Time.time);
                Debug.Log(secondaryBinds[i] + " - " + secondarySpells[i].name);
            }
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
                    if (Input.GetKeyDown(entry.Key) && spellCooldowns[entry.Value] < Time.time)
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
                spellCooldowns[spell] = Time.time + spell.GetCooldown();
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
    }

}