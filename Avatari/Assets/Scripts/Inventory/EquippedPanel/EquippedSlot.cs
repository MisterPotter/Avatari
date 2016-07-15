using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquippedSlot : MonoBehaviour, IPointerDownHandler {

    /*
     *  An empty slot has no current item it represents and will loose
     *  a lot of interactability. 
     */
    public bool empty { get { return item == null; } }

    public Item item { get; set; }

    public void OnPointerDown(PointerEventData eventData) {
        if(item == null) {
            Debug.Log("Item isn't set");
            return;
        }
        Debug.Log(this.item.itemDescription);
    }
}
