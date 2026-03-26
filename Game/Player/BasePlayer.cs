using System;
using System.Numerics;
using MohawkGame2D;

public class BasePlayer : BaseCharacter
{
    public override void Render()
    {

        Direction = Vector2.Normalize( Input.GetMousePosition() - Position);

        base.Render();
    }
}
