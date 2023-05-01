using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.NewSpellSystem.Core;
using DamageSystem.NewSpellSystem.Core.UI;

public class PickingUpItems : MonoBehaviour
{
    private GameObject itemToPickUp;
    public EQ_CanvasController controller;
    public List<GameObject> Inventory = new List<GameObject>();
    public PlayerSpellManager player;
    public SpellExhangePrompt spellExchangePrompt;

    // Made a small change to support the new Input system (Konrad)
    private void Awake()
    {
        InputManager.actions.Player.Interact.started += _ => PickUpAction();
    }


    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Pickable"))
        {
            controller.OpenMessagePanel();
            itemToPickUp = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Pickable"))
        {
            controller.CloseMessagePanel();
            itemToPickUp = null;
        }
    }

    // The new method that is called whenever the Interaction button is pressed
    private async void PickUpAction()
    {
        if (itemToPickUp != null)
        {
            if (itemToPickUp.GetComponentInChildren<DamageSystem.NewSpellSystem.Core.Spell>())
            {
                DamageSystem.NewSpellSystem.Core.Spell spell = itemToPickUp.GetComponentInChildren<DamageSystem.NewSpellSystem.Core.Spell>();
                if (!player.AddSpell(spell, null))
                {
                    if (spell.isPrimarySpell())
                    {
                        if (await spellExchangePrompt.SpellExchange(player.primarySpells, spell, player))
                        {
                            Destroy(itemToPickUp);
                            controller.CloseMessagePanel();
                            itemToPickUp = null;
                        }
                    }
                    else if (spell.isSecondarySpell())
                    {
                        if (await spellExchangePrompt.SpellExchange(player.secondarySpells, spell, player))
                        {
                            Destroy(itemToPickUp);
                            controller.CloseMessagePanel();
                            itemToPickUp = null;
                        }
                    }
                }
            }
            else
            {
                Destroy(itemToPickUp);
                controller.CloseMessagePanel();
                Inventory.Add(itemToPickUp);
                itemToPickUp = null;
            }
        }
    }
}
