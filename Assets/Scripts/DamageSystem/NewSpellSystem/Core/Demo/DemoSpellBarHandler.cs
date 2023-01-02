using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DamageSystem.NewSpellSystem.Core
{
    public class DemoSpellBarHandler : MonoBehaviour
    {

        public GameObject player;
        public Image background;
        PlayerSpellManager playerSpellManager;
        public SpellType spellToTrack;
        Slider cooldownBar;

        float trackedCooldown;
        Color defaultColor;


        // Start is called before the first frame update
        void Start()
        {
            playerSpellManager = player.GetComponent<PlayerSpellManager>();
            cooldownBar = GetComponent<Slider>();
            cooldownBar.maxValue = spellToTrack.GetCooldown();
            cooldownBar.minValue = 0f;
            defaultColor = background.color;
        }

        // Update is called once per frame
        void Update()
        {
            trackedCooldown = playerSpellManager.spellCooldowns[spellToTrack];
            cooldownBar.value = Mathf.Clamp(trackedCooldown - Time.time, cooldownBar.minValue, cooldownBar.maxValue);
            if (cooldownBar.value == 0f)
            {
                background.color = Color.green;
            }
            else {
                background.color = defaultColor;
            }
        }
    }
}