using MohawkGame2D;
using System;
using System.Buffers.Text;
using System.Numerics;

public class BaseAI : BaseCharacter
{
    public bool IsReady = false;
    public BaseCharacter? Target;
    public Vector2 GridPosition = Vector2.Zero;
    public bool InRoom { get; set; } = true;

    protected Vector2 EnterRoomDoor = Vector2.Zero;
    protected Vector2 ExitRoomDoor = Vector2.Zero;

    public override void CustomSetup()
    {
        base.CustomSetup();

        if (GetGame == null)
        { return; }

        IsReady = true;
    }

    public virtual void NewRoom(Vector2 EnterDoorPos, Vector2 ExitDoorPos)
    {
        if (GetGame == null || !IsReady)
        { return; }

        EnterRoomDoor = EnterDoorPos;
        ExitRoomDoor = ExitDoorPos;

        if (GridPosition != GetGame.Grid.CurrentRoomPosition)
        {
            InRoom = false;

            float time = Vector2.Distance(Position, EnterRoomDoor) / MovementSpeed * .3f;
            SetTimer(0, time);
        }
        else
        {
            InRoom = true;
            SetTimer(0, 0f);
        }
    }

    public BasePlayer GetClosetPlayer()
    {
        if (GetGame == null)
        { return null; }

        Vector2[] PlayerPos = GetGame.GetAllPlayerPositions();
        float Dis = 99999999999f;
        int Index = 0;

        for (int i = 0; i < PlayerPos.Length; i++)
        {
            if (Vector2.Distance(Position, PlayerPos[i]) <= Dis)
            {
                Dis = Vector2.Distance(Position, PlayerPos[i]);
                Index = i;
            }
        }

        return GetGame.GetAllPlayers()[Index];
    }
    public BaseAI GetClosetAI()
    {
        if (GetGame == null)
        { return null; }

        Vector2[] AiPos = GetGame.GetAllAiPositions();
        float Dis = 99999999999f;
        int Index = 0;

        for (int i = 0; i < AiPos.Length; i++)
        {
            if (GetGame.GetAllAis()[i] != this)
                if (Vector2.Distance(Position, AiPos[i]) <= Dis)
                {
                    Dis = Vector2.Distance(Position, AiPos[i]);
                    Index = i;
                }
        }

        return GetGame.GetAllAis()[Index];
    }

    public override void Die() 
    {
    }
}
