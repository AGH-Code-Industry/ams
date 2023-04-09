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

    public void SetItemStack(ItemStack stack) {
        iconImage.sprite = stack.item.icon;
        nameText.text = stack.item.name;
        countText.text = stack.count.ToString();
        descriptionText.text = stack.item.description;
    }

    private void Update() {
        if (gameObject.activeInHierarchy) {
            transform.position = new Vector3(
                Input.mousePosition.x + mouseMargin.x, 
                Input.mousePosition.y + mouseMargin.y, 
                0);
        }
    }
}