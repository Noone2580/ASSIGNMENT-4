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

        if (MohawkGame2D.Random.Bool()) 
        {
            int ammo = MohawkGame2D.Random.Integer(0,2);
            switch (ammo) 
            {
                case 0:
                    GetGame.AddItem(new Ammo_Pistol(), GridPosition, Position);
                    break;
                case 1:
                    GetGame.AddItem(new Ammo_Shotgun(), GridPosition, Position);
                    break;
                case 2:
                    GetGame.AddItem(new Ammo_AssulitRifle(), GridPosition, Position);
                    break;
            }
        }

        GetGame.RemoveEnemy(IN);
    }

    public void TryEnterRoom()
    {
        if (IsTimerDone(0) && IsReady)
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
