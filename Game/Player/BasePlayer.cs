using System;
using System.Numerics;
using MohawkGame2D;

public class BasePlayer : BaseCharacter
{
    public override void Render()
    {

        Direction = Vector2.Normalize( Input.GetMousePosition() - Position);

        if (Input.IsMouseButtonPressed(MouseInput.Left)) 
        {

            GetGame.DamageAllInRadius(this,Position,100f,1f,Direction * 600f);
        }

        base.Render();
        Text.Draw($"{HP}", new Vector2( Position.X, Position.Y + 10 ));
    }
}
