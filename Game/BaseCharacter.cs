using System;
using System.Numerics;
using MohawkGame2D;

public class BaseCharacter
{
    float MovementSpeed = 300f;

    public Vector2 Position = Vector2.Zero;
    public Vector2 Velocity = Vector2.Zero;
    public float Rotation = 0f;

    Texture2D BodyTexture;
    string BodyTextureLocation = "../../../Assets/Textures/Dude.png";
    public Vector2 SpriteOffest = new Vector2(0);

    public void Setup()
    {
        BodyTexture = Graphics.LoadTexture(BodyTextureLocation);
    }

    public virtual void Move(Vector2 Mag)
    {
        Mag = Vector2.Normalize(Mag);

        Position += Mag * MovementSpeed * Time.DeltaTime;
    }

    public virtual void Render()
    {
        // Body
        Draw.FillColor = Color.Red;
        Draw.Circle(Position, 20);

        Vector2 Dir = Vector2.Normalize(Input.GetMousePosition() - Position);
        Rotation = float.RadiansToDegrees(MathF.Atan2(Dir.X, Dir.Y) * -1f) + 90f; // DON'T TOUCH!!!!


        // Noise
        Draw.FillColor = Color.Blue;
        Draw.Circle(Position + (Dir * 30), 10);

        //Console.WriteLine($"Cos:{COS} Sin:{SIN}");
        //Console.WriteLine($"Cos:{Dir.X} Sin:{Dir.Y}");
        Console.WriteLine($"Row:{Rotation}");

        Graphics.Rotation = Rotation;
        Graphics.Draw(BodyTexture,  + Position);
    }

}
