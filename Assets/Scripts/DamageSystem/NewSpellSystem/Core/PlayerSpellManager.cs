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

        public List<Spell> primarySpells;
        public List<Spell> secondarySpells;

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
            
            while (primarySpells.Count < maxPrimarySpells)
                primarySpells.Add(null);

            while (secondarySpells.Count < maxSecondarySpells)
                secondarySpells.Add(null);


            for (int i = 0; primarySpells[i] && i < InputManager.primaryCastActions.Count; i++) {
                primarySpellActions.Add(primarySpells[i], InputManager.primaryCastActions[i]);
            }

            for (int i = 0; secondarySpells[i] && i < InputManager.secondaryCastActions.Count; i++) {
                secondarySpellActions.Add(secondarySpells[i], InputManager.secondaryCastActions[i]);
                secondarySpellCooldowns.Add(secondarySpells[i], Time.time);
            }


            foreach(var action in InputManager.primaryCastActions)
            {
                action.started += _ => OnPrimarySpellDown(action);
                action.canceled += _ => OnPrimarySpellUp();
            }

            foreach(var action in InputManager.secondaryCastActions)
                action.started += _ => OnSecondarySpellDown(action);
        }

        void Update() {
            TryCastPrimarySpell();
            TryCastSecondarySpell();
        }

        void TryCastPrimarySpell() {
            if (canCast && activePrimarySpell && primarySpellActions[activePrimarySpell].IsPressed() && activeSpellCooldown < Time.time) {
                activePrimarySpell.Cast(spellOrigin);
                activeSpellCooldown = Time.time + activePrimarySpell.GetCooldown();
            }
        }

        void TryCastSecondarySpell() {
            if (canCast && queuedSecondarySpell && secondaryCasting && secondaryCastTime < Time.time) { 
                queuedSecondarySpell.Cast(spellOrigin);
                secondaryCasting = false;
                secondarySpellCooldowns[queuedSecondarySpell] = Time.time + queuedSecondarySpell.GetCooldown();
                queuedSecondarySpell = null;
            }
        }

        void OnPrimarySpellDown(InputAction action) {
            if (canCast && primarySpellActions.ContainsValue(action))
            {
                Spell spell = primarySpells[InputManager.primaryCastActions.IndexOf(action)];
                if (primaryCasting)
                {
                    queuedPrimarySpell = spell;
                }
                else
                {
                    primaryCasting = true;
                    activePrimarySpell = spell;
                }
            }
        }

        void OnPrimarySpellUp() {
            if (activePrimarySpell)
            {
                primaryCasting = false;
                activePrimarySpell.StopCast();
                if (queuedPrimarySpell && queuedPrimarySpell != activePrimarySpell)
                {
                    primaryCasting = true;
                    activePrimarySpell = queuedPrimarySpell;
                }
                else
                    queuedPrimarySpell = null;
            }
        }

        void OnSecondarySpellDown(InputAction action) {
            if (canCast && secondarySpellActions.ContainsValue(action))
            {
                Spell spell = secondarySpells[InputManager.secondaryCastActions.IndexOf(action)];
                if (!secondaryCasting && secondarySpellCooldowns[spell] < Time.time)
                {
                    secondaryCasting = true;
                    queuedSecondarySpell = spell;
                    secondaryCastTime = Time.time + spell.GetCastTime();
                }
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
                canCast = false;
                RemoveSpellFromPlayer(spellToRemove);
                return AssignSpellToPlayer(spellToAdd);
            }
        }

        private void RemoveSpellFromPlayer(Spell spell)
        {
            if(spell.isPrimarySpell())
            {
                primarySpells[primarySpells.IndexOf(spell)] = null;
                primarySpellActions.Remove(spell);
            }
            else if(spell.isSecondarySpell())
            {
                secondarySpells[secondarySpells.IndexOf(spell)] = null;
                secondarySpellActions.Remove(spell);
            }
        }

        private bool AssignSpellToPlayer(Spell spell)
        {
            if (spell.isPrimarySpell())
            {
                int i = 0;
                foreach (var item in primarySpells)
                {
                    if (!item)
                    {
                        // Add the spell
                        primarySpells[i] = spell;
                        primarySpellActions.Add(spell, InputManager.primaryCastActions[i]);
                        spell.transform.parent = primarySpellParent.transform;
                            
                        // Set the position of the physical spell object (so the casted spell starts from the correct position)
                        spell.transform.localPosition = spellOrigin.transform.localPosition;

                        activePrimarySpell = null;
                        queuedPrimarySpell = null;
                        canCast = true;
                        return true;
                    }
                    i++;
                }
            }
            else if (spell.isSecondarySpell())
            {
                int i = 0;
                foreach (var item in secondarySpells)
                {
                    if (!item)
                    {
                        // Add the spell
                        secondarySpells[i] = spell;
                        secondarySpellActions.Add(spell, InputManager.secondaryCastActions[i]);
                        secondarySpellCooldowns.Add(spell, Time.time);
                        spell.transform.parent = secondarySpellParent.transform;

                        // Set the position of the physical spell object (so the casted spell starts from the correct position)
                        spell.transform.localPosition = spellOrigin.transform.localPosition;

                        canCast = true;
                        return true;
                    }
                    i++;
                }
            }
            canCast = true;
            return false;
        }

    }
}