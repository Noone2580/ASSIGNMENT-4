using MohawkGame2D;
using System;
using System.Buffers.Text;
using System.Numerics;

public class BaseAI : BaseCharacter
{
    public BaseCharacter Target;
    public string RoomName { get; protected set; } = "";
    public bool InRoom { get; protected set; } = true;

    protected Vector2 EnterRoomDoor = Vector2.Zero;
    protected Vector2 ExitRoomDoor = Vector2.Zero;

    public override void CustomSetup()
    {
        base.CustomSetup();
        RoomName = $"{GetGame.CurrentRoom}";
    }

    public virtual void NewRoom(Vector2 EnterDoorPos, Vector2 ExitDoorPos)
    {
        EnterRoomDoor = EnterDoorPos;
        ExitRoomDoor = ExitDoorPos;

        if (RoomName != $"{GetGame.CurrentRoom}")
        {
            InRoom = false;
            SetTimer(0,2);
        }
        else 
        {
            InRoom = true;
            SetTimer(0,0f);
        }
    }

    public BasePlayer GetClosetPlayer()
    {
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


}
