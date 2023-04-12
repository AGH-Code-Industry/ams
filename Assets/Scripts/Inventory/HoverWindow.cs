using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverWindow : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Vector2 mouseMargin = new Vector2(8, -8);
    public ItemStack stack;

    public void Show(ItemStack stack) {
        gameObject.SetActive(true);
        SetItemStack(stack);
        UpdatePosition();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void Update() {
        if (gameObject.activeInHierarchy) {
            UpdatePosition();
            UpdateCount();
        }
    }

    private void UpdatePosition() {
        transform.position = new Vector3(
            Input.mousePosition.x + mouseMargin.x, 
            Input.mousePosition.y + mouseMargin.y, 
            0);
    }

    private void SetItemStack(ItemStack stack) {
        this.stack = stack;
        iconImage.sprite = stack.item.icon;
        nameText.text = stack.item.name;
        countText.text = stack.count.ToString();
        descriptionText.text = stack.item.description;
    }

    private void UpdateCount() {
        countText.text = stack.count.ToString();
    }
}