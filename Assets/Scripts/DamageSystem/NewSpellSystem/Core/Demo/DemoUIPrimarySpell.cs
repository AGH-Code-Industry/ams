using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DamageSystem.NewSpellSystem.Core
{
    public class DemoUIPrimarySpell : MonoBehaviour
    {
        public KeyCode spellBind;
        Image spellImage;
        public Color inactive;
        public Color active;

        private void Start()
        {
            spellImage = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(spellBind))
            {
                spellImage.color = active;
            }
            else {
                spellImage.color = inactive;
            }
        }
    }
}
