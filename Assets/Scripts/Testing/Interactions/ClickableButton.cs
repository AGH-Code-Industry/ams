using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ClickableButton : MonoBehaviour, IInteractable
{
    public GameObject mechanism;

    public IMechanism _mech;

    bool pressed = false;

    Animator buttonAnim;
    BoxCollider buttonTrigger;
    Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        _mech = mechanism.GetComponent<IMechanism>();
        buttonAnim = GetComponent<Animator>();
        buttonTrigger = GetComponent<BoxCollider>();
        outline = GetComponentInParent<Outline>();
    }

    public void Use()
    {
        if (!pressed)
        {
            if (_mech.Activate(this))
            {
                pressed = true;
                buttonAnim.SetBool("pressed", true);
                buttonTrigger.enabled = false;
            }
        }
    }

    public void Outline(bool enabled)
    {
        outline.enabled = enabled;
    }
}
