using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebilPlayer : MonoBehaviour
{
    public void OnDead(){
        Destroy(gameObject);
    }
}
