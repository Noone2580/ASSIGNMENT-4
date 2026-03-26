using System;
using System.Numerics;
using System.Timers;
using MohawkGame2D;


/// <summary>
///     This Class is used for Movement, Collision, and Sprite Rotations/Offsets
/// </summary>
public class BaseCharacter
{
    //
    float[] Timers = new float[200];

    // User Vars
    float MovementSpeed = 150f;
    public Vector2 Position = Vector2.Zero;
    public Vector2 Velocity = Vector2.Zero;
    public float Rotation = 0f;
    public float VelRotation = 0f;
    public Vector2 Direction = Vector2.Zero;
    Vector2 LastDirection = Vector2.Zero;
    public float Grip = 10f;

    // Body Sprites and Offsets
    public Texture2D BodyTexture;
    public string BodyTextureLocation = "../../../Assets/Textures/Dude.png";
    public Vector2 BodySpriteOffest = new Vector2(63f,-63f);
    Vector2 NewBodySpriteOffest = Vector2.Zero;

    // Legs Sprites and Offsets
    public Texture2D LegsTexture;
    public string LegsTextureLocation = "../../../Assets/Textures/Dude_Legs.png";
    public Vector2 LegsSpriteOffest = new Vector2(63f,-63f);
    Vector2 NewLegsSpriteOffest = Vector2.Zero;

    /// <summary>
    ///     Sets Up the base variables and loads textures.
    /// </summary>
    public void Setup()
    {
        BodyTexture = Graphics.LoadTexture(BodyTextureLocation);
        LegsTexture = Graphics.LoadTexture(LegsTextureLocation);
        CustomSetup();
    }

    /// <summary>
    ///     Can be Overided to Setup Custom Variables.
    /// </summary>
    public virtual void CustomSetup() 
    {

    }

    /// <summary>
    ///     Sets a Timer on a index and takes time.
    /// </summary>
    public void SetTimer(int TimerIndex, float setTime) // Sets a new timer
    {
        if (Timers[TimerIndex] <= 0)
        {
            Timers[TimerIndex] = setTime + Time.SecondsElapsed;
        }
    }

    /// <summary>
    ///     Checks if a Timer at a index is done.
    ///     Returns a bool.
    /// </summary>
    public bool IsTimerDone(int TimerIndex) 
    {
        if (Time.SecondsElapsed >= Timers[TimerIndex])
        {
            Timers[TimerIndex] = 0;
            return true;
        }
        else
            return false;
    }

    /// <summary>
    ///     Moves the pawn in a direction
    /// </summary>
    public virtual void Move(Vector2 Mag)
    {
        Mag = Vector2.Normalize(Mag);

        Velocity += (Mag * MovementSpeed);

    }

    /// <summary>
    ///     For Updateing Pawn and sprite rotation / offsets.
    /// </summary>
    public virtual float UpdateRotation()
    {
        float RotationAngle =  MathF.Atan2(Direction.X, Direction.Y) * -1f; // Gets an angle form Direction
        Rotation = float.RadiansToDegrees(RotationAngle) + 90f; // Turns that angle into Degrees and adds 90 Degrees

        float VelRotationAngle = MathF.Atan2(Velocity.X, Velocity.Y) * -1f; // Gets an angle form Velocity
        VelRotation = float.RadiansToDegrees(VelRotationAngle) + 90f; // Turns that angle into Degrees and adds 90 Degrees

        // Rotates the Body's offset Sprite poition
        NewBodySpriteOffest.X = (BodySpriteOffest.X * MathF.Cos(RotationAngle)) - (BodySpriteOffest.Y * MathF.Sin(RotationAngle)) ;
        NewBodySpriteOffest.Y = (BodySpriteOffest.Y * MathF.Cos(RotationAngle)) + (BodySpriteOffest.X * MathF.Sin(RotationAngle)) ;

        // Rotates the Legs's offset Sprite poition
        NewLegsSpriteOffest.X = (LegsSpriteOffest.X * MathF.Cos(VelRotationAngle)) - (LegsSpriteOffest.Y * MathF.Sin(VelRotationAngle)) ;
        NewLegsSpriteOffest.Y = (LegsSpriteOffest.Y * MathF.Cos(VelRotationAngle)) + (LegsSpriteOffest.X * MathF.Sin(VelRotationAngle)) ;

        return Rotation;
    }

    /// <summary>
    ///     Renders the pawn to the screen. Can be Overided
    /// </summary>
    public virtual void Render()
    {
        // Update Poition
        Position += Velocity * Time.DeltaTime;
        
        // Update Velocity
        Velocity -= Velocity * Grip * Time.DeltaTime;

        UpdateRotation();

        // Sprite
        // Body
        Graphics.Rotation = VelRotation;
        Graphics.Draw(LegsTexture, NewLegsSpriteOffest + Position);

        //Legs
        Graphics.Rotation = Rotation;
        Graphics.Draw(BodyTexture, NewBodySpriteOffest + Position);

    }
}
