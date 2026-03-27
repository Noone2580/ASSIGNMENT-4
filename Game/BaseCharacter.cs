using System;
using System.Numerics;
using System.Timers;
using MohawkGame2D;


/// <summary>
///     This Class is used for Movement, Collision, and Sprite Rotations/Offsets
/// </summary>
public class BaseCharacter
{
    // Game Vars
    float[] Timers = new float[200];
    Game GetGame;

    // User Vars
    float MovementSpeed = 120f;
    public Vector2 Position = Vector2.Zero;
    public Vector2 Velocity = Vector2.Zero;
    public float Rotation = 0f;
    public float VelRotation = 0f;
    public Vector2 Direction = Vector2.Zero;
    Vector2 LastDirection = Vector2.Zero;
    public float HitBoxSize = 15f;
    public float Grip = 5f;

    // Body Sprites and Offsets
    public Texture2D BodyTexture;
    public string BodyTextureLocation = "../../../Assets/Textures/Dude.png";
    public Vector2 BodySpriteOffset = new Vector2(63f, -63f);
    Vector2 NewBodySpriteOffset = Vector2.Zero;

    // Legs Sprites and Offsets
    public Texture2D LegsTexture;
    public string LegsTextureLocation = "../../../Assets/Textures/Dude_Legs.png";
    public Vector2 LegsSpriteOffset = new Vector2(63f, -63f);
    Vector2 NewLegsSpriteOffset = Vector2.Zero;

    /// <summary>
    ///     Sets Up the base variables and loads textures.
    /// </summary>
    public void Setup(Game game)
    {
        GetGame = game;
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

        Velocity += (Mag * MovementSpeed) / 2;

    }

    /// <summary>
    ///     For Updateing Pawn and sprite rotation / offsets.
    /// </summary>
    public virtual float UpdateRotation()
    {
        float RotationAngle = MathF.Atan2(Direction.X, Direction.Y) * -1f; // Gets an angle form Direction
        Rotation = float.RadiansToDegrees(RotationAngle) + 90f; // Turns that angle into Degrees and adds 90 Degrees

        float VelRotationAngle = MathF.Atan2(Velocity.X, Velocity.Y) * -1f; // Gets an angle form Velocity
        VelRotation = float.RadiansToDegrees(VelRotationAngle) + 90f; // Turns that angle into Degrees and adds 90 Degrees

        // Rotates the Body's offset Sprite poition
        NewBodySpriteOffset.X = (BodySpriteOffset.X * MathF.Cos(RotationAngle)) - (BodySpriteOffset.Y * MathF.Sin(RotationAngle));
        NewBodySpriteOffset.Y = (BodySpriteOffset.Y * MathF.Cos(RotationAngle)) + (BodySpriteOffset.X * MathF.Sin(RotationAngle));

        // Rotates the Legs's offset Sprite poition
        NewLegsSpriteOffset.X = (LegsSpriteOffset.X * MathF.Cos(VelRotationAngle)) - (LegsSpriteOffset.Y * MathF.Sin(VelRotationAngle));
        NewLegsSpriteOffset.Y = (LegsSpriteOffset.Y * MathF.Cos(VelRotationAngle)) + (LegsSpriteOffset.X * MathF.Sin(VelRotationAngle));

        return Rotation;
    }

    public virtual void CheckForCal()
    {
        float[] RoomCal = GetGame.GetRoomCal();

        for (int i = 0; i < RoomCal.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (Position.X - HitBoxSize < RoomCal[i])
                    {
                        Velocity.X = 0;
                        Position.X = RoomCal[i] + HitBoxSize;
                    }
                    break;
                case 1:
                    if (Position.X + HitBoxSize > RoomCal[i])
                    {
                        Velocity.X = 0;
                        Position.X = RoomCal[i] - HitBoxSize;
                    }
                    break;
                case 2:
                    if (Position.Y - HitBoxSize < RoomCal[i])
                    {
                        Velocity.Y = 0;
                        Position.Y = RoomCal[i] + HitBoxSize;
                    }
                    break;
                case 3:
                    if (Position.Y + HitBoxSize > RoomCal[i])
                    {
                        Velocity.Y = 0;
                        Position.Y = RoomCal[i] - HitBoxSize;
                    }
                    break;
            }
        }
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

        CheckForCal();
        UpdateRotation();

        // Sprite
        // Body
        Graphics.Rotation = VelRotation;
        Graphics.Draw(LegsTexture, NewLegsSpriteOffset + Position);

        //Legs
        Graphics.Rotation = Rotation;
        Graphics.Draw(BodyTexture, NewBodySpriteOffset + Position);

    }
}
