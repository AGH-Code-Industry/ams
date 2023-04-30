using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DamageSystem.NewSpellSystem.Core
{
    public class PlayerSpellManager : MonoBehaviour
    {
        public bool canCast = true;
        [SerializeField] Transform spellOrigin;

        [SerializeField] int maxPrimarySpells = 3;
        [SerializeField] int maxSecondarySpells = 3;

        [SerializeField] GameObject primarySpellParent;
        [SerializeField] GameObject secondarySpellParent;

        [HideInInspector]
        public Dictionary<Spell, float> secondarySpellCooldowns = new Dictionary<Spell, float>();
        
        Dictionary<Spell, InputAction> primarySpellActions = new Dictionary<Spell, InputAction>();
        Dictionary<Spell, InputAction> secondarySpellActions = new Dictionary<Spell, InputAction>();

        List<Spell> primarySpells;
        List<Spell> secondarySpells;

        Spell activePrimarySpell;
        Spell queuedPrimarySpell;

        [HideInInspector]
        public Spell queuedSecondarySpell;
        [HideInInspector]
        public float secondaryCastTime = 0f;
        float activeSpellCooldown = 0f;

        bool primaryCasting = false;
        bool secondaryCasting = false;

        void Start() {
            primarySpells = new List<Spell>(primarySpellParent.GetComponentsInChildren<Spell>());
            secondarySpells = new List<Spell>(secondarySpellParent.GetComponentsInChildren<Spell>());

            for (int i = 0; i < primarySpells.Count && i < InputManager.primaryCastActions.Count; i++) {
                primarySpellActions.Add(primarySpells[i], InputManager.primaryCastActions[i]);
            }

            for (int i = 0; i < secondarySpells.Count && i < InputManager.secondaryCastActions.Count; i++) {
                secondarySpellActions.Add(secondarySpells[i], InputManager.secondaryCastActions[i]);
                secondarySpellCooldowns.Add(secondarySpells[i], Time.time);
            }

            foreach (var (spell, action) in primarySpellActions) {
                action.started += _ => OnPrimarySpellDown(spell);
                action.canceled += _ => OnPrimarySpellUp();
            }

            foreach (var (spell, action) in secondarySpellActions) {
                action.started += _ => OnSecondarySpellDown(spell);
            }
        }

        void Update() {
            TryCastPrimarySpell();
            TryCastSecondarySpell();
        }

        void TryCastPrimarySpell() {
            if (activePrimarySpell && primarySpellActions[activePrimarySpell].IsPressed() && activeSpellCooldown < Time.time) {
                activePrimarySpell.Cast(spellOrigin);
                activeSpellCooldown = Time.time + activePrimarySpell.GetCooldown();
            }
        }

        void TryCastSecondarySpell() {
            if (queuedSecondarySpell && secondaryCasting && secondaryCastTime < Time.time) { 
                queuedSecondarySpell.Cast(spellOrigin);
                secondaryCasting = false;
                secondarySpellCooldowns[queuedSecondarySpell] = Time.time + queuedSecondarySpell.GetCooldown();
                queuedSecondarySpell = null;
            }
        }

        void OnPrimarySpellDown(Spell spell) {
            if (!canCast) return;
            if (primaryCasting) {
                queuedPrimarySpell = spell;
            } else {
                primaryCasting = true;
                activePrimarySpell = spell;
            }
        }

        void OnPrimarySpellUp() {
            primaryCasting = false;
            activePrimarySpell.StopCast();
            if (queuedPrimarySpell && queuedPrimarySpell != activePrimarySpell) {
                primaryCasting = true;
                activePrimarySpell = queuedPrimarySpell;
            } else {
                queuedPrimarySpell = null;
            }
        }

        void OnSecondarySpellDown(Spell spell) {
            if (!canCast) return;
            if (!secondaryCasting && secondarySpellCooldowns[spell] < Time.time) {
                secondaryCasting = true;
                queuedSecondarySpell = spell;
                secondaryCastTime = Time.time + spell.GetCastTime();
            }
        }

        // Adding a spell to the Player
        // If spellToRemove is null, the spell will be added if there is room for it
        // If there is a spellToRemove specified, it will be replaced with the spellToAdd
        public bool AddSpell(Spell spellToAdd, Spell? spellToRemove) {
            if (!spellToRemove)
                return AssignSpellToPlayer(spellToAdd);
            else
            {
                RemoveSpellFromPlayer(spellToRemove);
                return AssignSpellToPlayer(spellToAdd);
            }
        }

        private void RemoveSpellFromPlayer(Spell spell)
        {
            if(spell.isPrimarySpell())
            {
                primarySpells.Insert(primarySpells.IndexOf(spell), null);
                primarySpellActions.Remove(spell);
            }
            else if(spell.isSecondarySpell())
            {
                secondarySpells.Insert(secondarySpells.IndexOf(spell), null);
                secondarySpellActions.Remove(spell);
            }
        }

        // Method that adds the spell if it finds a "null" element in the spell list (could be a pretty dumb method)
        private bool AssignSpellToPlayer(Spell spell)
        {
            if (spell.isPrimarySpell())
            {
                if (primarySpells.Count < maxPrimarySpells)
                {
                    primarySpells.Add(spell);
                    primarySpellActions.Add(spell, InputManager.primaryCastActions[InputManager.primaryCastActions.Count - 1]);
                    spell.transform.parent = primarySpellParent.transform;
                    return true;
                }
                else
                {
                    int i = 0;
                    foreach (var item in primarySpells)
                    {
                        i++;
                        if (!item)
                        {
                            // Add the spell
                            primarySpells.Insert(i, spell);
                            primarySpellActions.Add(spell, InputManager.primaryCastActions[i]);
                            spell.transform.parent = primarySpellParent.transform;
                            return true;
                        }
                    }
                }
                return false;
            }
            else if (spell.isSecondarySpell())
            {
                if (secondarySpells.Count < maxSecondarySpells)
                {
                    secondarySpells.Add(spell);
                    secondarySpellActions.Add(spell, InputManager.secondaryCastActions[InputManager.secondaryCastActions.Count - 1]);
                    spell.transform.parent = secondarySpellParent.transform;
                    return true;
                }
                else
                {
                    int i = 0;
                    foreach (var item in secondarySpells)
                    {
                        i++;
                        if (!item)
                        {
                            // Add the spell
                            secondarySpells.Insert(i, spell);
                            secondarySpellActions.Add(spell, InputManager.secondaryCastActions[i]);
                            spell.transform.parent = secondarySpellParent.transform;
                            return true;
                        }
                    }
                }
                return false;
            }
            return false;
        }
    }
}