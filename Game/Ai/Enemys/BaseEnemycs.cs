using System;
using System.Numerics;
using MohawkGame2D;

public class BaseEnemy : BaseAI
{

    public float AttackDamage = 1;
    public float AttackCooldown = .7f;
    public float AttackRange = 20f;
    public float TargetSlowdown = .8f;

    public override void CustomSetup()
    {
        base.CustomSetup();
    }

    public override void Die()
    {
        int IN = 0;
        for (int i = 0; i < GetGame.GetAllAis().Length; i++)
        {
            if (GetGame.GetAllAis()[i] == this)
                IN = i;
        }

        GetGame.RemoveEnemy(IN);
    }

    public void TryEnterRoom()
    {
        if (IsTimerDone(0))
        {
            Velocity = Vector2.Zero;
            Position = EnterRoomDoor;
            InRoom = true;
            GridPosition = GetGame.Grid.CurrentRoomPosition;
        }
    }

    public override void RenderNoUpdate()
    {
        if (InRoom)
        {
            base.RenderNoUpdate();
        }
    }

    public override void Render()
    {
        base.Render();
    }
}
