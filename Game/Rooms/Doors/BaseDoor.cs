using System;
using System.Numerics;
using MohawkGame2D;

public class BaseDoor
{
    public Vector2 Position = Vector2.Zero;
    public Vector2 EndPosition = Vector2.Zero;
    public float Rotation = 0f;

    public Vector2 SpriteOffset = Vector2.Zero;
    public Vector2 NewSpriteOffset = Vector2.Zero;


    public void Setup() 
    {
        SetRotation(Rotation);
    }

    public void SetRotation(float rotation)
    {
        Rotation = rotation;
        float RotationAngle = float.DegreesToRadians(rotation);

        // Rotates the Body's offset Sprite poition
        NewSpriteOffset.X = (SpriteOffset.X * MathF.Cos(RotationAngle)) - (SpriteOffset.Y * MathF.Sin(RotationAngle));
        NewSpriteOffset.Y = (SpriteOffset.Y * MathF.Cos(RotationAngle)) + (SpriteOffset.X * MathF.Sin(RotationAngle));

    }

    public void Render()
    {
        Draw.FillColor = Color.Green;
        Draw.Circle(Position, 50);
    }
}
