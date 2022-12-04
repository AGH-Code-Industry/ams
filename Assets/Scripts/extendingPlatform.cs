using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extendingPlatform : MonoBehaviour, IMechanism
{
    Animator platformAnim;

    // Start is called before the first frame update
    void Start()
    {
        platformAnim = GetComponent<Animator>();
    }
    public bool Activate(IInteractable activator)
    {
        if (!platformAnim.GetBool("activate"))
        {
            platformAnim.SetBool("activate", true);
            return true;
        }
        else
        {
            platformAnim.SetBool("activate", false);
            return true;
        }
    }
}
