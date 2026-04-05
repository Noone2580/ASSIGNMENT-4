using System;
using System.Numerics;
using MohawkGame2D;

public class Key : BaseKeys
{
    public override void CustomSetup()
    {
        base.CustomSetup();

        InventorySpriteLocation = new Vector2(0, 72 * 2);
        RoomCode = 1;
    }
}