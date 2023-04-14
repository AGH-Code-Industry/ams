using UnityEngine;

public class Chest : MonoBehaviour
{
    public RandomItem<Transform> lootTable;

    public Transform itemHolder;

    Animator animator;

    bool wasInvoked = false;
    bool wasPicked = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Chest")
                {
                    if (IsOpen())
                    {
                        if (wasPicked == true)
                        {
                            animator.SetTrigger("close");
                            HideItem();
                        }
                    }
                    else
                    {
                        animator.SetTrigger("open");
                    }
                }
                if (hit.transform.tag == "Item")
                {
                    Debug.Log("Picing up item");
                    foreach (Transform child in itemHolder)
                    {
                        Destroy(child.gameObject);
                    }
                    wasPicked = true;
                }
            }
        }
    }

    bool IsOpen()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("ChestOpen");
    }

    void HideItem()
    {
            itemHolder.localScale = Vector3.zero;
            itemHolder.gameObject.SetActive(false);

            foreach (Transform child in itemHolder)
            {
                Destroy(child.gameObject);
            }
    }

    void ShowItem()
    {
        if (wasInvoked == false)        // invoking function only once
        {
            Transform item = lootTable.GetRandom();
            Instantiate(item, itemHolder);
            itemHolder.gameObject.SetActive(true);
            foreach (Transform child in itemHolder)
            {
                child.gameObject.tag = "Item";
                child.gameObject.AddComponent<MeshCollider>();
            }
            wasInvoked = true;
        }
    }
}
