
using UnityEngine;
public class Item
{
    #region Private Var
    private int _idNum;
    private string _name;
    private int _value;
    private string _description;
    private Texture2D _icon;
    private string _mesh;
    private ItemType _type;
}
#endregion
public void ItemConstructor(int itemId, string itemName, Texture2D itemIcon, ItemType itemType)
{
    _idNum = itemId;
    _name = itemName;
    _icon = itemIcon;
    _type = itemType;
}

#region public var
public interface ID
{

    #endregion
    public enum ItemType
    {
        Food,
        Weapon,
        Apparel,
        Crafting,
        Quest,
        Money,
        Ingredients,
        Portions,
        Scrolls,
    }
}
