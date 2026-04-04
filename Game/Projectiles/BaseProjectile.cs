using System;
using System.Numerics;
using MohawkGame2D;

public class BaseProjectile
{
    Game? GetGame;
    public BaseCharacter? Owner;

    public Vector2 Velocity = Vector2.Zero;
    public Vector2 Position = Vector2.Zero;
    public float HitRange { get; protected set; } = 10f;
    public float Damage { get; protected set; } = 10f;
    public float HitFroce { get; protected set; } = 100f;
    Vector2 RoomPostion = Vector2.Zero;

    public int ProIndex = 0;

    public void Setup(Game game, BaseCharacter owner,float damage, Vector2 Spawnposition, Vector2 Speed)
    {
        GetGame = game;
        Owner = owner;
        Damage = damage;
        Position = Spawnposition;
        Velocity = Speed;

        ProIndex = GetGame.AddProjectile(this);
    }

    public virtual bool CheckForCal()
    {
        if (GetGame == null)
        { return false; }

        float[] RoomCal = GetGame.GetRoomCal();

        for (int i = 0; i < RoomCal.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (Position.X - HitRange < RoomCal[i])
                    {
                        return true;
                    }
                    break;
                case 1:
                    if (Position.X + HitRange > RoomCal[i])
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (Position.Y - HitRange < RoomCal[i])
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (Position.Y + HitRange > RoomCal[i])
                    {
                        return true;
                    }
                    break;
            }
        }
        return false;
    }

    public virtual void RenderNoUpdate() 
    {
        Draw.FillColor = Color.Red;
        Draw.Circle(Position, HitRange);
    }

    public virtual void Render()
    {
        bool Dam = GetGame.DamageAllInRadiusButSelf(Owner,Position, HitRange, Damage, Vector2.Normalize(Velocity) * HitFroce);

        if (CheckForCal() || Dam )
        {
            GetGame.RemoveProjectile(ProIndex);
            return;
        }

        Position += Velocity * Time.DeltaTime;

        Draw.FillColor = Color.Red;
        Draw.Circle(Position, HitRange);
    }
}
