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
    // Spawn position:
    Vector2 Start = new Vector2(Window.Width / 2, Window.Height / 2);

    // Game Vars
    BasePlayer[] Players = new BasePlayer[1];
    BaseEnemy[] Enemies = new BaseEnemy[30];
    BaseItem[] Items = new BaseItem[25];
    BaseProjectile[] Projectiles = new BaseProjectile[200];
    public Boss? TheBoss;
    public float[] Timers { get; protected set; } = new float[200];


    // Text and dialogue
    public TextBoxDialogue GetDialoguePersonally = new TextBoxDialogue();
    string CurrentText = "";

    // Rooms and Grid Vars
    public RoomGrid Grid { get; protected set; } = new RoomGrid();
    public BaseRoom? CurrentRoom;
    public bool CanUseDoor = true;


    /// <summary>
    ///     Setup runs once before the game loop begins.
    /// </summary>
    public void Setup()
    {
        // Setup window
        Window.SetTitle("TEST");
        Window.SetSize(1100, 900);
        Window.TargetFPS = 60;

        // Remove outlines
        Draw.LineColor = Color.Clear;

        Grid.Setup(this);

        Start = new Vector2(Window.Width / 2, Window.Height / 2);

        // Where the player starts
        Grid.CurrentRoomPosition = new Vector2(1, 5);

        CurrentRoom = Grid.GetRoomClassAtGrid(Grid.CurrentRoomPosition);
        CurrentRoom.Setup(this, Grid.CurrentRoomPosition, 0);

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new BasePlayer();
            Players[i].Setup(this);
            Players[i].Position = Start;
        }

        // Debug Romove SOON!
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

    public void SpawnBoss()
    {
        TheBoss = new Boss();
        TheBoss.GridPosition = Grid.CurrentRoomPosition;
        TheBoss.InRoom = true;
        TheBoss.Position = new Vector2(Window.Width / 2, 200);
        TheBoss.Setup(this);
    }

    public int AddEnemy(BaseEnemy item, Vector2 gridPosition, Vector2 position)
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i] == null)
            {
                Enemies[i] = item;
                Enemies[i].Setup(this);
                Enemies[i].GridPosition = gridPosition;
                Enemies[i].Position = position;
                if (Grid.CurrentRoomPosition == gridPosition)
                    Enemies[i].InRoom = true;
                return i;
            }
        }

        return -1;
    }
    public void RemoveEnemy(int Index)
    {
        Enemies[Index] = null;
    }

    public int AddItem(BaseItem item, Vector2 gridPosition, Vector2 position)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
            {
                Items[i] = item;
                Items[i].Setup(this, gridPosition, position, i);
                Items[i].NewRoom();
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

    public bool TryOpenDoor()
    {
        if (CurrentRoom == null)
            return false;
        bool can = CurrentRoom.CheckIfPlayerInDoor();

        return can;
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
                        Items[i].NewRoom();
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
            if (self != null && Enemies[i] != null && self != Enemies[i] && Enemies[i].InRoom)
            {
                if (Vector2.Distance(Enemies[i].Position, position) - Enemies[i].HitBoxSize <= radius)
                {
                    Hit = true;
                    Enemies[i].TakeDamage(damage, force);
                }
            }
        }

        if (TheBoss != null && TheBoss.InRoom && TheBoss != self)
        {
            if (Vector2.Distance(TheBoss.Position, position) - TheBoss.HitBoxSize <= radius)
            {
                TheBoss.TakeDamage(damage, force);
                Hit = true;
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
            if (Enemies[i] != null && Enemies[i].InRoom)
            {
                if (Vector2.Distance(Enemies[i].Position, position) - Enemies[i].HitBoxSize <= radius)
                {
                    Enemies[i].TakeDamage(damage, force);
                    Hit = true;
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
                }
            }
        }
        if (TheBoss != null && TheBoss.InRoom)
        {
            if (Vector2.Distance(TheBoss.Position, position) - TheBoss.HitBoxSize <= radius)
            {
                TheBoss.TakeDamage(damage, force);
                Hit = true;
            }
        }

        return Hit;
    }

    public void StartDialogue(string Text, float Time)
    {
        CurrentText = Text;
        SetTimer(0, Time);
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
            if (Enemies[i] != null)
                AiPositions[i] = Enemies[i].Position;
        }
        return AiPositions;
    }

    public void TextBoxRender()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i] != null)
                Enemies[i].RenderNoUpdate();
        }

        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null)
                Players[i].RenderNoUpdate();
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
                Projectiles[i].RenderNoUpdate();
            }
        }

        // Draw player Hud on top
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null)
                Players[i].DrawHud();
        }
        TextBox.Write(CurrentText);
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

        if (!IsTimerDone(0))
        {
            TextBoxRender();
            return;
        }


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

        if (TheBoss != null)
            TheBoss.Render();

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

    }
}