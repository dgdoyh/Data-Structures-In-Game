using UnityEngine;
using TMPro;


public class ItemUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemQuantity;

    [SerializeField] private Item myItem;

    #region Event Subscriptions
    private void Start()
    {
        InventoryManager.Singleton.OnItemRunOut += EmptySlot;
    }

    private void OnDisable()
    {
        InventoryManager.Singleton.OnItemRunOut -= EmptySlot;
    }
    #endregion

    public void AddItemUI(Item item)
    {
        myItem = item;

        this.itemName.text = item.ItemName;
        this.itemQuantity.text = item.Quantity.ToString();
    }

    public void UseItem()
    {
        if (myItem == null)
        {
            Debug.Log("Item slot is empty.");
        }
        else
        {
            Debug.Log("Used " + myItem.ItemName);

            InventoryManager.Singleton.RemoveItem(myItem);
        }       
    }

    public void EmptySlot()
    {
        myItem = null; 

        this.itemName.text = "";
        this.itemQuantity.text = "";
    }
}
