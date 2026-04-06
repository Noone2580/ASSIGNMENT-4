using System;
using System.Numerics;
using MohawkGame2D;

public class BaseDoor
{
    public Vector2 Position = Vector2.Zero;
    public Vector2 EndPosition = Vector2.Zero;
    public Vector2 ExitGridPosition = Vector2.Zero;
    public int RoomCode = 0;

    public Vector2 SpriteOffset = Vector2.Zero;
    public Vector2 NewSpriteOffset = Vector2.Zero;
    public float Rotation = 0f;

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
        switch (RoomCode)
        {
            case 0:
                Draw.FillColor = Color.DarkGray;
                break;
            case 1:
                Draw.FillColor = Color.Gray;
                break;
            case 2:
                Draw.FillColor = Color.Blue;
                break;
            case 3:
                Draw.FillColor = Color.Red;
                break;
        }
        Draw.Circle(Position, 50);
    }
}
