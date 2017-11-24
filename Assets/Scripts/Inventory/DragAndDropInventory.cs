﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropInventory : MonoBehaviour
{
    #region Variables
    [Header("Inventory")]
    //the toggle for showing our inventory
    public bool showInv;
    public List<Item> inventory = new List<Item>();
    public int slotX, slotY;
    private Rect inventorySize;
    [Header("Dragging")]
    public bool dragging;
    public Item draggedItem;
    public int draggedFrom; //the slot we took the item from
    public GameObject droppedItem;
    [Header("Tool Tip")]
    public int toolTipItem;
    public bool showToolTip;
    private Rect toolTipRect;
    private float scrW;
    private float scrH;
    #endregion
    #region Clamp to screen
    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }
    #endregion
    #region AddItem
    public void AddItem(int iD)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Name == null)
            {
                inventory[i] = ItemGen.CreateItem(iD);
                Debug.Log("Added Item: " + inventory[i].Name);
                return;
            }
        }
    }
    #endregion
    #region Drop Item
    public void DropItem(int iD)
    {
        droppedItem = Resources.Load("Prefabs/" + ItemGen.CreateItem(iD).Mesh) as GameObject;
        Instantiate(droppedItem, transform.position + transform.forward * 3, Quaternion.identity);
        return;
    }
    #endregion
    #region DrawItem
    void DrawItem(int windowID)
    {
        GUI.DrawTexture(new Rect(0, 0, scrW, scrH), draggedItem.Icon);
    }
    #endregion
    #region ToolTip
      #region ToolTipContent
        private string ToolTipText(int iD)
        {
        string toolTipText =
        "Name: " + inventory[iD].Name +
        "\nDescription: " + inventory[iD].Description +
        "\nType: " + inventory[iD].Type +
        "\nID: " + inventory[iD].ID;
        return toolTipText;
        }
      #endregion
      #region ToolTipWindow
        void DrawToolTip(int windowID)
        {
            GUI.Box(new Rect(0,0,scrW*2,scrH*3), ToolTipText(toolTipItem));
        }
      #endregion
    #endregion
    #region Drag Inventory
    void InventoryDrag(int windowID)
    {
        GUI.Box(new Rect(0, 0.25f * scrH, 6 * scrW, 0.5f * scrH), "Banner");
        GUI.Box(new Rect(0, 4.25f * scrH, 6 * scrW, 0.5f * scrH), "Gold n EXP");
        showToolTip = false;
        #region Nested For Loop
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(scrW*0.25f + x*(scrW*0.5f),scrH*0.25f + y *(scrH*0.5f),0.5f*scrW,0.5f*scrH);
                GUI.Box(slotLocation,"");
                #region Pickup Item
                if (e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !dragging && inventory[i].Name != null && !Input.GetKey(KeyCode.LeftShift))
                {
                    draggedItem = inventory[i];//we pick up item
                    inventory[i] = new Item();//the slot item is now blank
                    dragging = true;//we are holding an item
                    draggedFrom = i;//this is so we know where the item came from
                    Debug.Log("Dragging: "+ draggedItem.Name);
                }
                #endregion
                #region Swap Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && dragging && inventory[i].Name != null)
                {
                    Debug.Log("Swapping: " + draggedItem.Name + " :With: " + inventory[i].Name);

                    inventory[draggedFrom] = inventory[i];//our pick up slot now has our other item
                    inventory[i] = draggedItem;//the slot we are dropping the Item is now filled with our drag item
                    draggedItem = new Item();//the item we use to be dragging is empty
                    dragging = false;//we are not holding an item

                }
                #endregion
                #region Place Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && dragging && inventory[i].Name == null)
                {
                    Debug.Log("Place: " + draggedItem.Name + " :Into: " + i);

                    inventory[i] = draggedItem;//the slot we are dropping the Item is now filled with our drag item
                    draggedItem = new Item();//the item we use to be dragging is empty
                    dragging = false;//we are not holding an item

                }
                #endregion
                #region Return Item
                if (e.button == 0 && e.type == EventType.MouseUp && i == ((slotX*slotY)-1) && dragging)
                {
                    Debug.Log("Return: " + draggedItem.Name + " :Into: " + draggedFrom);

                    inventory[draggedFrom] = draggedItem;//the slot we are dropping the Item is now filled with our drag item
                    draggedItem = new Item();//the item we use to be dragging is empty
                    dragging = false;//we are not holding an item

                }
                #endregion
                #region Draw Item Icon
                if(inventory[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, inventory[i].Icon);
                    #region Set ToolTip on Mouse Hover
                    if(slotLocation.Contains(e.mousePosition)&& !dragging && showInv)
                    {
                        toolTipItem = i;
                        showToolTip = true;
                    }
                    #endregion
                }
                #endregion
                i++;
            }
        }
        #endregion
        #region Drag Window
        GUI.DragWindow(new Rect(0*scrW,0*scrH,6*scrW,0.5f*scrH));//Top Drag
        GUI.DragWindow(new Rect(0*scrW, 0.5f*scrH, 0.25f*scrW, 3.5f*scrH));//Left Drag
        GUI.DragWindow(new Rect(5.5f*scrW, 0.5f*scrH, 0.25f*scrW, 3.5f*scrH));//Right Drag
        GUI.DragWindow(new Rect(0*scrW, 4*scrH, 0.25f*scrW, 3.5f*scrH));//Bottom Drag
        #endregion
    }
    #endregion
    #region Start
    void Start()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
        inventorySize = new Rect(scrW,scrH,6*scrW,4.5f*scrH);
        for (int i = 0; i < (slotX * slotY); i++)
        {
            inventory.Add(new Item());
        }
        AddItem(0);
        AddItem(0);
        AddItem(1);
        AddItem(102);
        AddItem(206);
    }
    #endregion
    #region Update
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            showInv = !showInv;
        }   
    }
    #endregion
    #region OnGUI
  void OnGUI()
    {
        Event e = Event.current;
        #region Draw Inventory if showInv is true
        if(showInv)
        {
            inventorySize = ClampToScreen(GUI.Window(1,inventorySize,InventoryDrag,"My Inventory"));
        }
        #endregion
        #region Draw ToolTip
        if(showToolTip && showInv)
        {
            toolTipRect = new Rect(e.mousePosition.x +0.01f, e.mousePosition.y + 0.01f, scrW * 2,scrH * 3);
            GUI.Window(15, toolTipRect, DrawItem, "");
        }
        #endregion
        #region Drop Item is not showInv and mouse is up
        if(e.button == 0 && e.type == EventType.MouseUp && dragging)
        {
            DropItem(draggedItem.ID);
            Debug.Log("Dropped: " + draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region Incase inventory closes drop dragged item
        if (e.button == 0 && e.type == EventType.MouseUp && dragging && !showInv)
        {
            DropItem(draggedItem.ID);
            Debug.Log("Dropped: " + draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region Draw Item on Mouse
        if(dragging)
        {
            if(draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y +0.125f, scrW *0.5f, scrH * 0.5f);
                GUI.Window(2, mouseLocation, DrawItem, "");
            }
        }
        #endregion
    }
    #endregion

}
/*
 Mouse Appear and Disapear
 Lock Cam
 Lock Movement

Show ToolTip??
Draw Drag Icons To Fit

Move Slots
     
     */