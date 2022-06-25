using UnityEngine;
using UnityEngine.EventSystems;

public class DragEndDropHendler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 _startPos;
    private InventoryManager _im;
    public InventoryCell cell;
    private void Start()
    {
        _im = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        _startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cell.currentWeapon != null)
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _startPos;
        cell.DropItem();
    }
}
