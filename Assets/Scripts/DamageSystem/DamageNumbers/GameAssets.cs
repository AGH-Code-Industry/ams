using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.DamageNumbers
{
    public class GameAssets : MonoBehaviour
    {
        private static GameAssets _i;

        public static GameAssets i
        {
            get
            {
                if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
                return _i;
            }
        }

        public Transform damagePopup;


    }
}
