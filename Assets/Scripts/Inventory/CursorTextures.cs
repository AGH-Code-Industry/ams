using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTextures : MonoBehaviour
{
    public Texture2D cursorDefault;
    public Texture2D cursorPointer;
    public static CursorTextures instance;

    private void Awake() {
        instance = this;
    }
}
