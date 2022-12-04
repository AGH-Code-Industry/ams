using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideDoor : MonoBehaviour, IMechanism
{
    Animator doorAnim;


    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();
    }


    public bool Activate(IInteractable activator)
    {
        if (!doorAnim.GetBool("open"))
        {
            doorAnim.SetBool("open", true);
            return true;
        }
        return false;
    }

}
