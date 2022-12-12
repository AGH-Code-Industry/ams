using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Inventory))]
public class playerInteract : MonoBehaviour
{
    public int MaxHealth = 100;
    private int PlayerHealth;
    public int StartingHealth;
    public HealthBar healthBar;

    public int MaxMana = 100;
    private int PlayerMana;
    public int StartingMana;
    public HealthBar manaBar;

    public GameObject interactTooltip;
    IInteractable interaction;
    IConsumable pickup;
    bool canInteract = false;

    IEquipment equipment;
    bool canObtain = false;
    Inventory inventory;

    public TMPro.TextMeshProUGUI messageLog;

    private void Start()
    {
        PlayerHealth = StartingHealth;
        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(StartingHealth);

        PlayerMana = StartingMana;
        manaBar.SetMaxHealth(MaxMana);
        manaBar.SetHealth(StartingMana);


        messageLog.text = "";
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && Input.GetKeyDown(KeyCode.E))
        {
            interaction.Use();
            interactTooltip.SetActive(false);
            interaction.Outline(false);
        }else if(canObtain && Input.GetKeyDown(KeyCode.E))
        {
            equipmentItem temp = equipment.Collect();
            interactTooltip.SetActive(false);
            PushToInventory(temp);
            canObtain = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IInteractable>(out interaction))
        { 
            canInteract = true;
            interactTooltip.SetActive(true);
            interaction.Outline(true);
        }else if(other.gameObject.TryGetComponent<IConsumable>(out pickup))
        {
            pickUp temp = pickup.Consume();
            Heal(temp);
        }else if(other.gameObject.TryGetComponent<IEquipment>(out equipment))
        {
            if (inventory.canPickUp())
            {
                canObtain = true;
                interactTooltip.SetActive(true);
            }
            equipment.Outline(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<IInteractable>(out interaction))
        {
            canInteract = false;
            interaction.Outline(false);
            interaction = null;
            interactTooltip.SetActive(false);
        }else if(other.TryGetComponent<IEquipment>(out equipment))
        {
            canObtain = false;
            equipment.Outline(false);
            equipment = null;
            interactTooltip.SetActive(false);
        }
    }

    private void Heal(pickUp consumable)
    {

        PlayerHealth +=  consumable.healValue;
        PlayerMana += consumable.manaValue;
        if (PlayerHealth > MaxHealth)
            PlayerHealth = MaxHealth;

        if (PlayerMana > MaxMana)
            PlayerMana = MaxMana;
        messageLog.text += "\nPicked up: " + consumable.name;
        healthBar.SetHealth(PlayerHealth);
        manaBar.SetHealth(PlayerMana);
    }

    private void PushToInventory(equipmentItem item)
    {
        messageLog.text += "\nObtained: " + item.name;
        inventory.InsertItem(item);
    }
}
