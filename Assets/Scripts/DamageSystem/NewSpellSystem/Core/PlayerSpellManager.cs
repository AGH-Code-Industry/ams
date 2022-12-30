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

        KeyValuePair<KeyCode, SpellType> queuedSpell;

        bool primaryCasting = false;
        bool secondaryCasting = false;

        private void Start()
        {
            primarySpellsDict = new Dictionary<KeyCode, SpellType>();
            secondarySpellsDict = new Dictionary<KeyCode, SpellType>();
            for(int i=0; i<primarySpells.Length && i<primaryBinds.Length; i++)
            {
                primarySpellsDict.Add(primaryBinds[i], primarySpells[i].GetComponent<SpellType>());
            }

            for (int i = 0; i < secondarySpells.Length && i < secondaryBinds.Length; i++)
            {
                secondarySpellsDict.Add(secondaryBinds[i], secondarySpells[i].GetComponent<SpellType>());
            }
        }
        private void Update()
        {
            UsePrimarySpell();
            UseSecondarySpell();
        }

        void UsePrimarySpell()
        {
            if (canCast)
            {
                foreach (KeyValuePair<KeyCode, SpellType> entry in primarySpellsDict) {
                    if (Input.GetKeyDown(entry.Key))
                    {
                        if (!primaryCasting)
                        {
                            primaryCasting = true;
                            StartCoroutine(primaryCooldown(entry.Value, entry.Key));
                        }
                        else {
                            queuedSpell = new KeyValuePair<KeyCode, SpellType>(entry.Key, entry.Value);
                        }
                    }
                }
            }
        }

        IEnumerator primaryCooldown(SpellType spell, KeyCode key)
        {
            while (primaryCasting)
            {
                yield return new WaitForSeconds(spell.GetCooldown());
                spell.Cast(spellOrigin);

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
        }

        void UseSecondarySpell()
        {
            if (canCast && !secondaryCasting) {
                foreach (KeyValuePair<KeyCode, SpellType> entry in secondarySpellsDict)
                {
                    if (Input.GetKeyDown(entry.Key))
                    {
                        secondaryCasting = true;
                        SecondaryCast(entry.Value);
                    }
                }
            }
        }

        void SecondaryCast(SpellType spell) 
        {
            StartCoroutine(DelayedCast(spell.GetCastTime(), spell));
        }

        IEnumerator DelayedCast(float delay, SpellType spell)
        {
            Debug.Log("Casting " + spell);
            yield return new WaitForSeconds(delay);
            spell.Cast(spellOrigin);
            StartCoroutine(Cooldown(spell.GetCooldown()));
        }

        IEnumerator Cooldown(float cooldown) {
            Debug.Log("Spell Cooldown: " + cooldown);
            yield return new WaitForSeconds(cooldown);
            secondaryCasting = false;
            Debug.Log("Can cast again");
        }
    }

}