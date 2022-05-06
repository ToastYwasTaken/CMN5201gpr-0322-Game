using Assets.Scripts.Player;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemContainer : MonoBehaviour
{
    [SerializeField] Item _item;
    public Item Item { get { return _item; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_item == null) return;
        PlayerController2 pContrl = collision.gameObject.GetComponent<PlayerController2>();
        if (pContrl != null)
            if(pContrl.Inventory.PickupItem(_item))
                Destroy(gameObject);
        //if usable > pContrl.stats.Changestats()
    }
}
