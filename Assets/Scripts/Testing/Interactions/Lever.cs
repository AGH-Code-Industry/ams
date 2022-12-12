using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public GameObject mechanism;
    Animator levelAnim;
    IMechanism _mech;
    Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        levelAnim = GetComponent<Animator>();
        _mech = mechanism.GetComponent<IMechanism>();
        outline = GetComponentInParent<Outline>();
    }


    public void Use()
    {
        if (!levelAnim.GetBool("activate"))
        {
            levelAnim.SetBool("activate", true);
            _mech.Activate(this);
        }
        else
        {
            levelAnim.SetBool("activate", false);
            _mech.Activate(this);
        }
    }

    public void Outline(bool enabled)
    {
        outline.enabled = enabled;
    }
}
