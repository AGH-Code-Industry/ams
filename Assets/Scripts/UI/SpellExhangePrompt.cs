using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DamageSystem.NewSpellSystem.Core.UI
{
    public class SpellExhangePrompt : MonoBehaviour
    {
        // Buttons for selecting which spell to replace
        public List<Button> optionButtons = new List<Button>();
        // Button for cancelling adding of the spell
        public Button cancelButton;
        public GameObject exchangePrompt;

        // State of the prompt, used for determining which spell was chosen, if the selection was made
        int state = -1;

        private void Start()
        {
            cancelButton.onClick.AddListener(delegate { Cancel(); });
            foreach(var button in optionButtons)
            {
                button.gameObject.SetActive(false);
            }
        }

        public async Task<bool> SpellExchange(List<Spell> currentSpells, Spell newSpell, PlayerSpellManager player)
        {
            state = -1;

            exchangePrompt.SetActive(true);

            // Initialize buttons that we want to show
            for (int i=0; i<currentSpells.Count; i++)
            {
                var a = i;
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].onClick.AddListener(delegate { this.SetSpellExchangeValue(a); });
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentSpells[i].name;
            }


            while(state==-1)
            {
                await Task.Delay(200);
            }

            foreach(var button in optionButtons)
            {
                button.onClick.RemoveAllListeners();
                button.GetComponentInChildren<TextMeshProUGUI>().text = "null";
                button.gameObject.SetActive(false);
            }
            exchangePrompt.SetActive(false);
            if (state == -2)
                return false;
            else
                return  player.AddSpell(newSpell, currentSpells[state]);
        }


        public void Cancel()
        {
            state = -2;
        }

        public void SetSpellExchangeValue(int value)
        {
            Debug.Log(value);
            state = value;
        }
    }
}
