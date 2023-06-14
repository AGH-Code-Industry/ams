using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySliderAnimator : MonoBehaviour
{
    public GameObject PanelMenu;
    public GameObject AnimatorButton;

    public void ShowHideMenu()
    {
        if (PanelMenu == null || AnimatorButton == null) { return; }

        Animator animator = PanelMenu.GetComponent<Animator>();
        if (animator != null )
        {
            bool isActive = animator.GetBool("show");
            animator.SetBool("show", !isActive);
            AnimatorButton.transform.Rotate(0, 0, 180f);
        }
    }
}
