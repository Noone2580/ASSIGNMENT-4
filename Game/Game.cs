// Include the namespaces (code libraries) you need below.
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Threading;

// The namespace your code is in.
namespace MohawkGame2D;

/// <summary>
///     Your game code goes inside this class!
/// </summary>
public class Game
{
    // Place your variables here:
    Vector2 Start = new Vector2(Window.Width / 2, Window.Height / 2);

    BasePlayer[] Players = new BasePlayer[1];
    BaseEnemy[] Enemies = new BaseEnemy[4];
    BaseItem[] Items = new BaseItem[0];
    BaseProjectile[] Projectiles = new BaseProjectile[200];

    public BaseRoom? CurrentRoom;
    TextBoxDialogue GetDialoguePersonally = new TextBoxDialogue();

    public bool CanUseDoor = true;
    public RoomGrid Grid { get; protected set; } = new RoomGrid();

    /// <summary>
    ///     Setup runs once before the game loop begins.
    /// </summary>
    public void Setup()
    {
        // Set up window
        Window.SetTitle("TEST");
        Window.SetSize(1100, 900);
        Window.TargetFPS = 60;

        // Remove outlines
        Draw.LineColor = Color.Clear;

        Grid.Setup();

        Start = new Vector2(Window.Width / 2, Window.Height / 2);

        // Where the player starts
        Grid.CurrentRoomPosition = new Vector2(1,5);

        CurrentRoom = Grid.GetRoomClassAtGrid(Grid.CurrentRoomPosition);
        CurrentRoom.Setup(this, Grid.CurrentRoomPosition,0);

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new BasePlayer();
            Players[i].Setup(this);
            Players[i].Position = Start;
        }

        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i] = new Zombie();
            Enemies[i].Setup(this);
            Enemies[i].Position += Start;

        }

        // Debug Romove SOON!
        Items = new BaseItem[3];
        Items[0] = new BaseWeapon();
        Items[0].Setup(this, Grid.CurrentRoomPosition, Start);
        Items[0].NewRoom(CurrentRoom);

        Items[1] = new Keycard();
        Items[1].Setup(this, Grid.CurrentRoomPosition, Start + new Vector2(100, 0));
        Items[1].NewRoom(CurrentRoom);

        Items[2] = new Key();
        Items[2].Setup(this, Grid.CurrentRoomPosition, Start + new Vector2(200, 0));
        Items[2].NewRoom(CurrentRoom);
    }

    public float[] GetRoomCal()
    {
        float[] RoomCal = new float[4];
        for (int i = 0; i < RoomCal.Length; i++)
        {
            switch (i)
            {
                case 0:
                    RoomCal[i] = CurrentRoom.LeftWallCal;
                    break;
                case 1:
                    RoomCal[i] = CurrentRoom.RightWallCal;
                    break;
                case 2:
                    RoomCal[i] = CurrentRoom.TopWallCal;
                    break;
                case 3:
                    RoomCal[i] = CurrentRoom.BottomWallCal;
                    break;
            }
        }

        return RoomCal;
    }

    public int AddItem(BaseItem item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
            {
                Items[i] = item;
                return i;
            }
        }

        return -1;
    }
    public void RemoveItem(int Index)
    {
        Items[Index] = null;
    }

    public int AddProjectile(BaseProjectile projectile)
    {
        for (int i = 0; i < Projectiles.Length; i++)
        {
            if (Projectiles[i] == null)
            {
                Projectiles[i] = projectile;
                return i;
            }
        }

        return -1;
    }
    public void RemoveProjectile(int Index)
    {
        Projectiles[Index] = null;
    }

    public BaseItem PickupItem(Vector2 position)
    {
        float DIS = 9999999999f;
        BaseItem CloseItem = null;
        int Index = 0;

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null)
            {
                if (Vector2.Distance(position, Items[i].Position) <= DIS && Vector2.Distance(position, Items[i].Position) <= Items[i].PickupRadius && Items[i].InRoom && Items[i].CanPickup)
                {
                    DIS = Vector2.Distance(position, Items[i].Position);
                    CloseItem = Items[i];
                    Index = i;
                }
            }
        }

        if (CloseItem != null)
        {
            Items[Index] = null;
            CloseItem.PickUp();
            return CloseItem;
        }

        return CloseItem;
    }

    public void EnterNewRoom(Vector2 NewRoom, Vector2 EnterDoorPosition, Vector2 ExitDoorPostion)
    {
        if (NewRoom != Vector2.Zero)
        {
            if (Grid.GetRoomAtGrid(NewRoom) != Vector4.Zero)
            {
                Vector4 GridPostion = Grid.GetRoomAtGrid(NewRoom);

                CanUseDoor = false;
                CurrentRoom = Grid.GetRoomClassAtGrid(NewRoom);
                CurrentRoom.Setup(this, new Vector2(GridPostion.X, GridPostion.Y), ((int)GridPostion.W));
                Grid.CurrentRoomPosition = NewRoom;

                for (int i = 0; i < Players.Length; i++)
                {
                    if (Players[i] != null)
                    {
                        Players[i].Velocity = Vector2.Zero;
                        Players[i].Position = EnterDoorPosition;
                    }
                }
                for (int i = 0; i < Enemies.Length; i++)
                {
                    if (Enemies[i] != null)
                        Enemies[i].NewRoom(EnterDoorPosition, ExitDoorPostion);
                }
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i] != null)
                        Items[i].NewRoom(CurrentRoom);
                }
            }
        }
        else
            return;
    }


    /// <summary>
    ///    Used for Player malee damage.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <param name="damage"></param>
    /// <param name="force"></param>
    public bool DamageAllInRadiusButSelf(BaseCharacter self, Vector2 position, float radius, float damage, Vector2 force)
    {
        bool Hit = false;

        for (int i = 0; i < Players.Length; i++)
        {
            if (self != null && Players[i] != null && self != Players[i])
            {
                if (Vector2.Distance(Players[i].Position, position) - Players[i].HitBoxSize <= radius)
                {
                    Hit = true;
                    Players[i].TakeDamage(damage, force);
                }
            }
        }

        for (int i = 0; i < Enemies.Length; i++)
        {
            if (self != null && Enemies[i] != null && self != Enemies[i])
            {
                if (Vector2.Distance(Enemies[i].Position, position) - Enemies[i].HitBoxSize <= radius)
                {
                    Hit = true;
                    Enemies[i].TakeDamage(damage, force);
                }
            }
        }

        return Hit;
    }


    /// <summary>
    ///    Used for Player malee damage.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <param name="damage"></param>
    /// <param name="force"></param>
    public bool DamageAllInRadius(Vector2 position, float radius, float damage, Vector2 force)
    {
        bool Hit = false;

        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i] != null)
            {
                if (Vector2.Distance(Enemies[i].Position, position) - Enemies[i].HitBoxSize <= radius)
                {
                    Enemies[i].TakeDamage(damage, force);
                    Hit = true;
                    Console.WriteLine("gggggggg");
                }
            }
        }

        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null)
            {
                if (Vector2.Distance(Players[i].Position, position) - Players[i].HitBoxSize <= radius)
                {
                    Players[i].TakeDamage(damage, force);
                    Hit = true;
                    Console.WriteLine("gggggggg");
                }
            }
        }
        return Hit;

    }



    public BasePlayer[] GetAllPlayers()
    {
        return Players;
    }
    public BaseAI[] GetAllAis()
    {
        return Enemies;
    }

    public Vector2[] GetAllPlayerPositions()
    {
        Vector2[] PlayerPositions = new Vector2[Players.Length];
        for (int i = 0; i < Players.Length; i++)
        {
            PlayerPositions[i] = Players[i].Position;
        }
        return PlayerPositions;
    }
    public Vector2[] GetAllAiPositions()
    {
        Vector2[] AiPositions = new Vector2[Enemies.Length];
        for (int i = 0; i < Enemies.Length; i++)
        {
            AiPositions[i] = Enemies[i].Position;
        }
        return AiPositions;
    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        // Reset background
        Window.ClearBackground(Color.OffWhite);

        //Graphics.Tint = new Color(255/2);
        Graphics.Rotation = 0;

        CurrentRoom.Render();

        // For keeping the text Readable
        Text.Kerning = 2; 



        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i] != null)
                Enemies[i].Render();
        }

        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null)
                Players[i].Render();
        }

        // Draws shaows
        Graphics.Rotation = 0;
        Graphics.Draw(CurrentRoom.RoomLightTexture, Vector2.Zero);

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null)
                Items[i].Render();
        }

        for (int i = 0; i < Projectiles.Length; ++i)
        {
            if (Projectiles[i] != null)
            {
                Projectiles[i].Render();
            }
        }

        // Draw player Hud on top
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null)
                Players[i].DrawHud();
        }

        // THIS IS FOR TESTING THE TEXT BOX
        //Text.Kerning = 2;

        //TextBox.Write(GetDialoguePersonally.BossRaphText[0]);
    }
}