using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using MohawkGame2D;

public class BaseItem
{
    // Game Vars
    public Game? GetGame;
    public float[] Timers { get; protected set; } = new float[200];
    public BasePlayer? OwnerAsPlayer;
    public BaseCharacter? Owner;

    // Item Vars
    public Vector2 Position = Vector2.Zero;
    public string Name = "";
    public bool InInvetory = false;
    public string Description = "";
    public int ID = 0;
    public bool CanPickup = true;
    public float PickupRadius = 50f;

    // Room Vars
    public int RoomCode { get; set; } = 0;
    public Vector2 GridPosition = Vector2.Zero;
    public bool InRoom = false;

    // Textures
    public Texture2D InventoryTexture;
    string InventoryTextureLocation = "../../../Assets/Textures/Items.png";
    public Vector2 InventorySpriteLocation = Vector2.Zero;
    public Texture2D HoldingTexture;
    public string HoldingTextureLocation = string.Empty;

    


    public void Setup(Game game, Vector2 GridSpawn, Vector2 position, int index) 
    {
        GetGame = game;
        GridPosition = GridSpawn;
        Position = position;
        ID = index;
        CustomSetup();
        InventoryTexture = Graphics.LoadTexture(InventoryTextureLocation);
        //HoldingTexture = Graphics.LoadTexture(HoldingTextureLocation);
    }

    public virtual void CustomSetup() 
    {

    }

    public bool PickUp() 
    {
        if(!CanPickup || InInvetory)
            return false;

        GetGame.StartDialogue(GetGame.GetDialoguePersonally.PlayerText[0], 1f);
        InInvetory = true;
        return true;
    }

    public void Drop(Vector2 position, Vector2 gridPosition) 
    {
        GetGame.AddItem(this, gridPosition, position);
        Position = position;
        GridPosition = gridPosition;
        InInvetory = false;
        CanPickup = true;
    }

    public virtual void NewRoom()
    {
        if (InInvetory)
        {
            return;
        }

        if (GridPosition == GetGame.Grid.CurrentRoomPosition)
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
    ///     For useing the item's main function evey frame. Can be overided.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public virtual void UseItemFrame(Vector2 position, Vector2 direction)
    {
    }
    /// <summary>
    ///     For useing the item's main function. Can be overided.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public virtual void UseItem(Vector2 position, Vector2 direction) 
    {
    }

    /// <summary>
    ///     stop useing the item's main function. Can be overided.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public virtual void StopUsingItem(Vector2 position, Vector2 direction) 
    {
    }

    /// <summary>
    ///     For useing the item's secentry function. Can be overided.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public virtual void UseItemSpacl(Vector2 position, Vector2 direction) 
    {

    }

    /// <summary>
    ///     Sets a Timer on a index and takes time.
    /// </summary>
    public void SetTimer(int TimerIndex, float setTime) // Sets a new timer
    {
        Timers[TimerIndex] = setTime + Time.SecondsElapsed;
    }

    /// <summary>
    ///     Checks if a Timer at a index is done.
    ///     Returns a bool.
    /// </summary>
    public bool IsTimerDone(int TimerIndex)
    {
        if (Time.SecondsElapsed >= Timers[TimerIndex])
        {
            Timers[TimerIndex] = 0;
            return true;
        }
        else
            return false;
    }

    /// <summary>
    ///     Used for rendering the item at the inventory location.
    /// </summary>
    public virtual void RenderInv(Vector2 position) 
    {
        Graphics.DrawSubset(InventoryTexture, position, InventorySpriteLocation, new Vector2(72f, 72f));
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
        //Graphics.DrawSubset(InventoryTexture, position, InventorySpriteLocation, new Vector2(72f, 72f));
        Graphics.Rotation = 0;
    }

    /// <summary>
    ///     Used for rendering the item on the map. 
    /// </summary>
    public virtual void Render()
    {
        if (InRoom)
        {
            Graphics.Rotation = 0f;
            Vector2 Offset = new Vector2(72 / 2, 72 / 2);
            Graphics.DrawSubset(InventoryTexture, Position - Offset, InventorySpriteLocation, new Vector2(72f,72f));
        }
    }
}
