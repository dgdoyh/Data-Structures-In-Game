using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private ItemUIManager[] itemSlots;


    #region Event Subscriptions
    private void Start()
    {
        InventoryManager.Singleton.OnInventoryUpdated += UpdateUI;
    }

    private void OnDisable()
    {
        InventoryManager.Singleton.OnInventoryUpdated -= UpdateUI;
    }
    #endregion

    private void UpdateUI(LinkedList<Item> inventory)
    {
        int slotIndex = 0;

        foreach (Item i in inventory)
        {
            itemSlots[slotIndex].AddItemUI(i);
            slotIndex++;
        }
    }
}
