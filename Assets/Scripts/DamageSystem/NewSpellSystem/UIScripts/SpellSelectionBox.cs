using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DamageSystem.NewSpellSystem.Core;

namespace DamageSystem.NewSpellSystem.UI
{
    public class SpellSelectionBox : MonoBehaviour
    {
        // Bool to see whether we're currently expected to make a selection
        bool selectionActive = false;

        // Dict of currently equipped spells (need List<KeyCode> because there is no other way to access the DICT using the SpellType)
        Dictionary<KeyCode, SpellType> selections = new Dictionary<KeyCode, SpellType>();
        List<KeyCode> binds;

        // Spell that we want to add
        SpellType toAdd;
        // Reference to the player manager that we want to replace spell in
        PlayerSpellManager manager;

        void Start()
        {
            //disable the object when starting the game
            gameObject.SetActive(false);
        }

        // Function for creating a new choice box
        public void SpellSelection(PlayerSpellManager spellManager, SpellType newSpell, Dictionary<KeyCode, SpellType> choices, List<KeyCode> _binds)
        {
            manager = spellManager;
            toAdd = newSpell;
            selections = choices;
            binds = _binds;
            gameObject.SetActive(true);
            selectionActive = true;
        }

        // Function that is invoked, when the player selects "the first" spell in list to be replaced
        public void Selection0()
        {
            if (selectionActive)
            {
                manager.AddSpell(toAdd.gameObject, selections[binds[0]], binds[0]);
            }
            Deactivate();
        }

        // Function that is invoked, when the player selects "the second" spell in list to be replaced
        public void Selection1()
        {
            if (selectionActive)
            {
                manager.AddSpell(toAdd.gameObject, selections[binds[1]], binds[1]);
            }
            Deactivate();
        }

        // Function that is invoked, when the player selects "the third" spell in list to be replaced
        public void Selection2()
        {
            if (selectionActive)
            {
                manager.AddSpell(toAdd.gameObject, selections[binds[2]], binds[2]);
            }
            Deactivate();
        }

        // Function that is invoked, when the player selects "cance" (they don't want to replace any spells)
        public void CancelSpellSelection()
        {
            Deactivate();
        }


        // Function for deactivating the SpellSelection UI element
        void Deactivate()
        {
            selectionActive = false;
            gameObject.SetActive(false);
            toAdd = null;
            selections = null;
        }
    }
}
