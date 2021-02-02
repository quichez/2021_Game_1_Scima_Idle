using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Equipment Item { get; protected set; }
    public Image Icon => transform.GetChild(0).GetComponent<Image>();
    public static GameObject ItemBeingDragged;

    private Transform startParent => transform;
    private Vector3 startPosition => transform.position;

    protected TooltipTrigger _tooltipTrigger => GetComponent<TooltipTrigger>();

    public void SetItem(Equipment item)
    {
        _tooltipTrigger.EnableToolTip(false);
        Item = item;            
        Icon.sprite = item.Icon;
        Icon.color = item.Color;

        if (Item.GetType() == typeof(Equipment) && Item.ID != -1)
        {
            _tooltipTrigger.EnableToolTip(true);
            _tooltipTrigger.SetToolTipText(Item.Grade.ToString() + " " + Item.Title, ((Equipment)Item).StatsToString());
        }
    }

    public void RemoveItem()
    {
        _tooltipTrigger.EnableToolTip(false);
        Item = new Equipment();
        Icon.sprite = null;
        Icon.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item != null && Item.ID != -1)
        {
            ItemBeingDragged = Icon.gameObject;
            Icon.GetComponent<CanvasGroup>().blocksRaycasts = false;
            Icon.GetComponent<Canvas>().sortingOrder = 2;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ItemBeingDragged)
            ItemBeingDragged.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ItemBeingDragged)
        {
            Icon.GetComponent<CanvasGroup>().blocksRaycasts = false;
            Icon.GetComponent<Canvas>().sortingOrder = 1;
            if (ItemBeingDragged.transform.parent == startParent)
                ItemBeingDragged.transform.position = startPosition;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(ItemBeingDragged)
        {
            InventorySlot temp = ItemBeingDragged.transform.parent.GetComponent<InventorySlot>();

            if (temp != this)
            {
                if (GetType() == temp.GetType() && GetType() == typeof(InventorySlot))
                {
                    Player.Instance.SwapItems(temp, this);
                }
                else
                {
                    if(GetType() == typeof(EquipmentSlot))
                    {
                        if(temp.GetType() == typeof(InventorySlot))                
                            Player.Instance.EquipItem(temp, (EquipmentSlot)this);
                    }

                    if(GetType() == typeof(InventorySlot))
                    {
                        if (temp.GetType() == typeof(EquipmentSlot))
                        {
                            Player.Instance.SwapEquip((EquipmentSlot)temp, this);
                        }
                    }
                    if(GetType() == typeof(ItemDeleteSlot))
                    {
                        if (temp.GetType() == typeof(InventorySlot))
                            Player.Instance.RemoveItem(temp);
                        else if (temp.GetType() == typeof(EquipmentSlot))
                            Player.Instance.DeleteEquippedItem((EquipmentSlot) temp);

                    }
                }
            }
            
            temp.transform.GetChild(0).position = temp.startPosition;

            ItemBeingDragged.GetComponent<CanvasGroup>().blocksRaycasts = true;
            ItemBeingDragged.GetComponent<Canvas>().sortingOrder = 1;

            ItemBeingDragged = null;
        }
    }
}
