using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DamageSystem.NewSpellSystem.Core
{
    public class DemoCastBarHandler : MonoBehaviour
    {

        public GameObject player;
        PlayerSpellManager playerSpellManager;
        SpellType spellToTrack;
        Slider castBar;
        public TextMeshProUGUI spellText;

        float trackedCastTime;
        GameObject[] children;

        // Start is called before the first frame update
        void Start()
        {
            playerSpellManager = player.GetComponent<PlayerSpellManager>();
            castBar = GetComponent<Slider>();
            castBar.minValue = 0f;
            AssignChildren();
            SetChildrenActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (playerSpellManager.queuedSecondarySpell)
            {
                SetChildrenActive(true);
                spellToTrack = playerSpellManager.queuedSecondarySpell;
                castBar.maxValue = spellToTrack.GetCastTime();
                trackedCastTime = playerSpellManager.secondaryCastTime;
                castBar.value = Mathf.Clamp(trackedCastTime - Time.time, castBar.minValue, castBar.maxValue);
                spellText.text = playerSpellManager.queuedSecondarySpell.name;
            }
            else {
                SetChildrenActive(false);
            }
        }

        void SetChildrenActive(bool active)
        {
            for (int i = 0; i < children.Length; i++)
            {
               children[i].SetActive(active);
            }
        }

        void AssignChildren()
        { 
            children = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                children[i] = transform.GetChild(i).gameObject;
            }
        }
    }
}
