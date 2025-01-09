using System;
using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public static LinkedList<Item> inventory = new LinkedList<Item>();

    public event Action<LinkedList<Item>> OnInventoryUpdated;
    public event Action OnItemRunOut;

    public void AddItem(Item newItem)
    {
        // If the same item is already in the inventory, just increase the quantity of the item
        LinkedListNode<Item> currItem = inventory.First;

        while (currItem != null)
        {
            if (currItem.Value.ItemID == newItem.ItemID)
            {
                currItem.Value.Quantity++;
                OnInventoryUpdated?.Invoke(inventory);

                return;
            }

            currItem = currItem.Next;
        }

        inventory.AddLast(newItem);

        OnInventoryUpdated?.Invoke(inventory);
    }

    public void RemoveItem(Item item)
    {
        item.Quantity--;

        if (item.Quantity <= 0)
        {
            LinkedListNode<Item> itemToRemove = FindItemByID(item.ItemID);
            inventory.Remove(itemToRemove);

            OnItemRunOut?.Invoke();

        }

        OnInventoryUpdated.Invoke(inventory);
    }

    public LinkedListNode<Item> FindItemByID(int id)
    {
        LinkedListNode<Item> currItem = inventory.First;

        while (currItem != null)
        {
            if (currItem.Value.ItemID == id)
            {
                return currItem;
            }

            currItem = currItem.Next;
        }

        return null;
    }
}
