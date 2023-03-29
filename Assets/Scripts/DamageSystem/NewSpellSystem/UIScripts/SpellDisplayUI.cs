using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DamageSystem.NewSpellSystem.Core;

namespace DamageSystem.NewSpellSystem.UI {
    public class SpellDisplayUI : MonoBehaviour
    {
        //Spelltype - spell reference, bool - if the spell is currently active
        private Dictionary<SpellType, bool> primarySpells;
        //Spelltype - spell reference, float - the cooldown of the spell
        private Dictionary<SpellType, float> secondarySpells;

        //Binds for spells to display bind tooltip and detect if that key is being pressed (to reflect it on the UI)
        private List<KeyCode> primaryBinds;
        private List<KeyCode> secondaryBinds;

        //Update bind tooltips for spells
        public void UpdatePrimaryBinds(List<KeyCode> binds)
        {
            primaryBinds = binds;
        }
        public void UpdateSecondaryBinds(List<KeyCode> binds)
        {
            secondaryBinds = binds;
        }

        //Update UI elements (for things like cooldowns etc.)
        public void UpdateUI(PlayerSpellManager playerSpellManager)
        {
            secondarySpells = playerSpellManager.secondarySpellCooldowns;
            //TODO - update primarySpells dict
            //primarySpells[playerSpellManager.]
        }

        //TODO - implement ui elements and update them 
    }
}
