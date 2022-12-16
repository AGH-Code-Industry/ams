using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log(anim);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) {
            anim.SetBool("isWalking", true);
        };

        if (Input.GetKey(KeyCode.LeftShift)) {
            anim.SetBool("isRunning", true);
        };

        if(!Input.GetKey("w")) {
            anim.SetBool("isWalking", false);
        }

        if(!Input.GetKey(KeyCode.LeftShift)) {
            anim.SetBool("isRunning", false);
        }
    }
}
