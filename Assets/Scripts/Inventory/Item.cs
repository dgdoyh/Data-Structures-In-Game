using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int itemID;
    public int ItemID { get => itemID; private set => itemID = value; }
    public string ItemName { get; private set; }
    public int Quantity { get; set; } = 1;



    private void Start()
    {
        ItemName = this.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {          
            InventoryManager.Singleton.AddItem(this);

            this.gameObject.SetActive(false);
        }
    }
}
