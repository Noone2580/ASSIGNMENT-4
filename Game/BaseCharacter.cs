using System;
using System.Numerics;
using MohawkGame2D;

public class BaseCharacter
{
    float MovementSpeed = 300f;

    public Vector2 Position = Vector2.Zero;
    public Vector2 Velocity = Vector2.Zero;
    public float Rotation = 0f;
    public Vector2 Direction = Vector2.Zero;

    Texture2D BodyTexture;
    string BodyTextureLocation = "../../../Assets/Textures/Dude.png";
    public Vector2 SpriteOffest = new Vector2(-63f);
    Vector2 NewSpriteOffest = Vector2.Zero;

    public void Setup()
    {
        BodyTexture = Graphics.LoadTexture(BodyTextureLocation);
    }

    public virtual void Move(Vector2 Mag)
    {
        Mag = Vector2.Normalize(Mag);

        Position += Mag * MovementSpeed * Time.DeltaTime;
    }

    public virtual float UpdateRotation()
    {
        Direction = Vector2.Normalize(Input.GetMousePosition() - Position);// TEST REMOVE SOON

        Rotation = float.RadiansToDegrees(MathF.Atan2(Direction.X, Direction.Y) * -1f) + 90f; // DON'T TOUCH!!!!

        NewSpriteOffest = (SpriteOffest * Rotation);


        return Rotation;
    }

    public virtual void Render()
    {
        UpdateRotation();

        // Body
        Draw.FillColor = Color.Red;
        Draw.Circle(Position, 20);

        // Rotation Debug
        Console.WriteLine($"Row:{Rotation}");

        // Sprite
        Graphics.Rotation = Rotation;
        Graphics.Draw(BodyTexture, NewSpriteOffest + Position);

        // Noise
        Draw.FillColor = Color.Blue;
        Draw.Circle(Position + (Direction * 30), 10);
    }

}
