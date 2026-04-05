using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using MohawkGame2D;

public class Kinfe : BaseWeapon
{
    public override void CustomSetup()
    {
        base.CustomSetup();
        FireRate = .3f;
        InventorySpriteLocation = new Vector2(0, 0);
        Damage = 10;
        Range = 80;
    }

    public override void UseItem(Vector2 position, Vector2 direction)
    {
        if (GetGame == null || Owner == null) return;

        if (IsTimerDone(0))
        {
            SetTimer(0, FireRate);
            position += direction * 5;
            GetGame.DamageAllInRadiusButSelf(Owner, position, Range, Damage, direction * 300f);
        }
    }
}
