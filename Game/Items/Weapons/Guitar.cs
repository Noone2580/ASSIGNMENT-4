using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using MohawkGame2D;

public class Guitar : BaseWeapon
{
    public override void CustomSetup()
    {
        base.CustomSetup();
        FireRate = .7f;
        InventorySpriteLocation = new Vector2(72*2, 0);
        Damage = 35;
        Range = 150;
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
    public override void UseItemSpacl(Vector2 position, Vector2 direction)
    {
    }
}
