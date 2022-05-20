using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class ItemContainer : MonoBehaviour
{
    [SerializeField] Item _item;
    [SerializeField] SpriteRenderer _spriteRenderer;
    public Item Item { get { return _item; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_item == null) return;
        PlayerController2 pContrl = collision.gameObject.GetComponent<PlayerController2>();
        if (pContrl != null)
            if(RefLib.sInventory.PickupItem(_item, _spriteRenderer.sprite))
                Destroy(gameObject);
        //if usable > pContrl.stats.Changestats()
    }
}

public enum eItemType
{
    ALL,
    WEAPON,
    CHIP
}
