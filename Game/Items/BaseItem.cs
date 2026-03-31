using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using MohawkGame2D;

public class BaseItem
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public string Name { get; protected set; } = "";
    public string RoomName { get; protected set; } = string.Empty;
    public bool InRoom { get; protected set; } = false;
    public bool InInvetory { get; protected set; } = false;
    public string Description { get; protected set; } = "";
    public int ID { get; protected set; } = 0;
    public bool CanPickup { get; protected set; } = true;
    public float PickupRadius { get; protected set; } = 10f;

    public Texture2D InventoryTexture;
    public string InventoryTextureLocation = "../../../Assets/Textures/Inv_Pistol.png";
    public Texture2D HoldingTexture;
    public string HoldingTextureLocation = string.Empty;

    public void Setup(BaseRoom SpawnRoom, Vector2 position) 
    {
        RoomName = $"{SpawnRoom}";
        Position = position;
        CustomSetup();
        InventoryTexture = Graphics.LoadTexture(InventoryTextureLocation);
        //HoldingTexture = Graphics.LoadTexture(HoldingTextureLocation);
    }

    public virtual void CustomSetup() 
    {

    }

    public bool PickUp() 
    {
        if(!CanPickup)
            return false;

        InInvetory = true;
        return true;
    }

    public virtual void NewRoom(BaseRoom NewRoom)
    {
        if (InInvetory)
            return;
        if (NewRoom == null) 
            return;

        if (RoomName == $"{NewRoom}")
        {
            InRoom = true;
            return;
        }
        else
        {
            InRoom = false;
            return;
        }

    }

    /// <summary>
    ///     For useing the item's main function. Can be overided.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public virtual void UseItem(Vector2 position, Vector2 direction) 
    {
        Console.WriteLine("Hello!");
    }

    /// <summary>
    ///     For useing the item's secentry function. Can be overided.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public virtual void UseItemSpacl(Vector2 position, Vector2 direction) 
    {
        Console.WriteLine("AHHHHH!");

    }

    /// <summary>
    ///     Used for rendering the item at the inventory location.
    /// </summary>
    public virtual void RenderInv(Vector2 position) 
    {
        Graphics.Draw(InventoryTexture, position);
    }

    /// <summary>
    ///     Used for rendering the holding item
    /// </summary>
    /// <param name="rotation"></param>
    /// <param name="position"></param>
    public virtual void RenderHolding(float rotation, Vector2 position) 
    {
        Position = position;
        Graphics.Rotation = rotation;
        Graphics.Draw(HoldingTexture, position);
    }

    /// <summary>
    ///     Used for rendering the item on the map. 
    /// </summary>
    public virtual void Render()
    {
        if (InRoom)
        {
            Graphics.Rotation = 0f;
            Vector2 Offset = new Vector2(InventoryTexture.Width / 2, InventoryTexture.Height / 2);
            Graphics.Draw(InventoryTexture, Position - Offset);
        }
    }
}
