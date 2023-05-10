using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesInteraction : MonoBehaviour, IInteractable
{
    private Outline outline;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Use()
    {
        // Kod do wykonania po interakcji z Notes
    }

    public void Outline(bool enabled)
    {
        outline.enabled = enabled;
    }

    private void OnMouseEnter()
    {
        outline.OutlineWidth = 10f;
    }

    private void OnMouseExit()
    {
        outline.OutlineWidth = 0f;
    }
}