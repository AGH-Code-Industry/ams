using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DamageSystem.NewSpellSystem.Core;

namespace DamageSystem.NewSpellSystem.UI {
    public class SpellDisplayUI : MonoBehaviour
    {

        //Spelltype - spell reference, float - the cooldown of the spell
        private Dictionary<SpellType, float> secondaryCooldowns;
        //The currently casted primary spell
        private KeyValuePair<KeyCode, SpellType> activePrimarySpell;

        //Binds for spells to display bind tooltip and detect if that key is being pressed (to reflect it on the UI)
        private Dictionary<KeyCode, SpellType> primaryBinds;
        private Dictionary<KeyCode, SpellType> secondaryBinds;

        //dicts to remember which spell is assigned to which UI element
        private Dictionary<SpellType, Image> primarySpells;
        private Dictionary<SpellType, Slider> secondarySpells;

        //Reference to the main Spell Manager class (on player)
        public PlayerSpellManager spellManager;

        //UI elemnt lists to assign in inspector (probably up to change idk)
        public List<Image> primaryUI;
        public List<Slider> secondaryUI;

        //bool to check if spell UI has been initialized (the first time update spell list has been executed)
        private bool initialized = false;


        private void Update()
        {
            UpdateUI();
        }

        //Method to update the UI elements with (potentially) new spells. Called only when there is a change
        //to either binds or equipped spells (for example if we get one).
        public void UpdateSpellList()
        {
            primaryBinds = spellManager.primaryKeybindSpellPairs;
            secondaryBinds = spellManager.secondaryKeybindSpellPairs;
            primarySpells = new Dictionary<SpellType, Image>();
            secondarySpells = new Dictionary<SpellType, Slider>();
            int i = 0;
            foreach(SpellType spell in primaryBinds.Values)
            {
                primarySpells.Add(spell, primaryUI[i]);
                i++;
            }
            i = 0;
            foreach(SpellType spell in secondaryBinds.Values)
            {
                secondarySpells.Add(spell, secondaryUI[i]);
                //This should probably be moved somewhere else (in case we want to add modifiers on cooldowns for whatever reason)
                secondaryUI[i].maxValue = spell.GetCooldown();
                i++;
            }
            secondaryCooldowns = spellManager.secondarySpellColldownDict;
            activePrimarySpell = spellManager.primaryActiveSpell;
            initialized = true;
        }

        //Method that updates the UI (visualising spell cooldowns, which spell is currently being casted)
        private void UpdateUI()
        {
            if (initialized)
            {
                activePrimarySpell = spellManager.primaryActiveSpell;
                secondaryCooldowns = spellManager.secondarySpellColldownDict;

                foreach (SpellType spell in primarySpells.Keys)
                {
                    //replace this solution for something more robust 
                    if (activePrimarySpell.Value == spell && Input.GetKey(activePrimarySpell.Key))
                        primarySpells[spell].color = new Color(0, 0, 0, 0.2f);
                    else
                        primarySpells[spell].color = new Color(1, 1, 1, 1);
                }
                foreach (SpellType spell in secondarySpells.Keys)
                {
                    secondarySpells[spell].value = Mathf.Clamp(secondaryCooldowns[spell] - Time.time, secondarySpells[spell].minValue, secondarySpells[spell].maxValue);
                }
            }
        }
    }
}
