using System.Numerics;
using MohawkGame2D;

/// <summary>
///     This Class has the base functions and variables that all rooms inhairint form.
/// </summary>
public class BaseRoom
{
    public BaseDoor[] Doors = new BaseDoor[4];

    public float LeftWallCal;
    public float RightWallCal;
    public float TopWallCal;
    public float BottomWallCal;

    public Texture2D RoomTexture;
    public Texture2D RoomLightTexture;
    public string RoomTextureLocation = "../../../Assets/Textures/T_Floor.png";
    public string[] RoomLightTextureLocation = ["../../../Assets/Textures/T_Floor_Lighting_A.png",
        "../../../Assets/Textures/T_Floor_Lighting_1.png",
        "../../../Assets/Textures/T_Floor_Lighting_2.png",
        "../../../Assets/Textures/T_Floor_Lighting_3.png",
        "../../../Assets/Textures/T_Floor_Lighting_4.png",
        "../../../Assets/Textures/T_Floor_Lighting_5.png",
        "../../../Assets/Textures/T_Floor_Lighting_6.png"
        ];
    protected int LightIndex = 2;

    public Game? GetGame;

    public string RoomName { get; protected set; } = "";
    public int RoomCode { get; protected set; } = 0;
    public bool IsBossRoom { get; protected set; } = false;
    public Vector2 GridPosition = Vector2.Zero;
    public int ThisRoomIndex = 0;
    public RoomGrid? Grid;

    /// <summary>
    ///     Sets Up the base variables and loads textures.
    /// </summary>
    public void Setup(Game game, Vector2 gridPosition, int roomCode)
    {
        GetGame = game;
        Grid = GetGame.Grid;
        GridPosition = gridPosition;
        RoomCode = roomCode;

        LeftWallCal = 50;
        RightWallCal = Window.Width - 50;
        TopWallCal = 50;
        BottomWallCal = Window.Height - 50;

        ThisRoomIndex = Grid.GetIndexAtGrid(GridPosition);

        CustomSetup();

        RoomTexture = Graphics.LoadTexture(RoomTextureLocation);
        RoomLightTexture = Graphics.LoadTexture(RoomLightTextureLocation[LightIndex]);

        if (GridPosition != GetGame.StartGrid)
            for (int i = 0; i < Random.Integer(0, 10); i++)
            {
                int EE = Random.Integer(0, 4);
                switch (EE)
                {
                    case 0:
                        GetGame.AddEnemy(new Zombie(), GridPosition, Random.Vector2(new Vector2(RightWallCal, BottomWallCal), new Vector2(LeftWallCal, TopWallCal)));
                        break;
                    case 1:
                        GetGame.AddEnemy(new Z_Tank(), GridPosition, Random.Vector2(new Vector2(RightWallCal, BottomWallCal), new Vector2(LeftWallCal, TopWallCal)));
                        break;
                    case 2:
                        GetGame.AddEnemy(new Spiter(), GridPosition, Random.Vector2(new Vector2(RightWallCal, BottomWallCal), new Vector2(LeftWallCal, TopWallCal)));
                        break;
                    case 3:
                        GetGame.AddEnemy(new Cultist(), GridPosition, Random.Vector2(new Vector2(RightWallCal, BottomWallCal), new Vector2(LeftWallCal, TopWallCal)));
                        break;
                }
            }
    }

    /// <summary>
    ///     Can be Overided to Setup Custom Variables.
    /// </summary>
    public virtual void CustomSetup()
    {

    }

    public void AddDoor(int index, Vector2 position, Vector2 endPosition, Vector2 connectedRoom)
    {
        //Console.WriteLine(GridPosition + connectedRoom);
        if (Grid.GetRoomAtGrid(GridPosition + connectedRoom) == Vector4.Zero)
            return;
        Doors[index] = new BaseDoor();
        Doors[index].Position = position;
        Doors[index].EndPosition = endPosition;
        Doors[index].ExitGridPosition = GridPosition + connectedRoom;

        Doors[index].RoomCode = ((int)Grid.GetRoomAtGrid(GridPosition + connectedRoom).W);
        Doors[index].Setup();
    }

    public bool CheckIfPlayerInDoor()
    {
        if (GetGame == null)
            return false;

        BasePlayer[] Players = GetGame.GetAllPlayers();
        bool Reset = true;

        for (int i = 0; i < Doors.Length; i++)
        {
            if (Doors[i] != null)
            {

                if (GetGame.CanUseDoor)
                {
                    // Checks to see if the Door has a lock on it
                    if (Doors[i].RoomCode > 0)
                    {
                        // Goes through all Players and checks if they are in the Door way and have the right Key
                        for (int j = 0; j < Players.Length; j++)
                        {
                            if (Vector2.Distance(Players[j].Position, Doors[i].Position) <= 100)
                            {
                                int RoomIndex = Grid.GetIndexAtGrid(Doors[i].ExitGridPosition);
                                Vector4 RoomVec = Grid.GetRoomAtIndex(RoomIndex);

                                if (Players[j].Items[Players[j].InventoryIndex] != null && Players[j].Items[Players[j].InventoryIndex].RoomCode == Doors[i].RoomCode)
                                {
                                    // Unlock Door
                                    if (RoomVec.Z == 11)
                                    {
                                        RoomVec.W--;
                                        Grid.SetRoomAtIndex(RoomIndex, RoomVec);
                                        RoomVec = Grid.GetRoomAtIndex(RoomIndex);
                                        GetGame.StartDialogue($"You take one lock off. {RoomVec.W}", 1.5f);
                                        Doors[i].RoomCode = (int)RoomVec.W;
                                        Reset = false;
                                        return true;
                                    }
                                    else
                                    {
                                        Graphics.UnloadTexture(RoomTexture);
                                        Graphics.UnloadTexture(RoomLightTexture);
                                        RoomVec.W = 0;
                                        Grid.SetRoomAtIndex(RoomIndex, RoomVec);
                                        GetGame.StartDialogue("It unlocks", 1f);
                                        GetGame.EnterNewRoom(Doors[i].ExitGridPosition, Doors[i].EndPosition, Doors[i].Position);
                                    }
                                    Reset = false;
                                    return true;
                                }

                                else
                                    GetGame.StartDialogue($"It's locked. Key level {Doors[i].RoomCode} needed", 1f);
                                return false;
                            }
                        }
                    }
                    else if (Vector2.Distance(Players[0].Position, Doors[i].Position) <= 80)
                    {
                        Graphics.UnloadTexture(RoomTexture);
                        Graphics.UnloadTexture(RoomLightTexture);
                        GetGame.EnterNewRoom(Doors[i].ExitGridPosition, Doors[i].EndPosition, Doors[i].Position);
                        Reset = false;
                        return true;
                    }

                }
                else if (Vector2.Distance(Players[0].Position, Doors[i].Position) >= 80)
                {
                    GetGame.CanUseDoor = Reset;
                }
            }
        }
        return false;
    }


    public bool CheckIfInDoor()
    {
        if (GetGame == null)
            return false;

        BasePlayer[] Players = GetGame.GetAllPlayers();
        bool Reset = true;

        for (int i = 0; i < Doors.Length; i++)
        {
            if (Doors[i] != null)
            {
                if (GetGame.CanUseDoor)
                {
                    if (Vector2.Distance(Players[0].Position, Doors[i].Position) <= 80)
                    {
                        Reset = false;
                        return true;
                    }
                }

                else if (Vector2.Distance(Players[0].Position, Doors[i].Position) >= 80)
                {
                    GetGame.CanUseDoor = Reset;
                }
            }
        }

        return false;
    }

    public virtual void Render()
    {
        Graphics.Draw(RoomTexture, 0, 0);

        for (int i = 0; i < Doors.Length; i++)
        {
            if (Doors[i] != null)
                Doors[i].Render();
        }

        CheckIfInDoor();

        if (RoomName != "")
            Text.Draw(RoomName, Window.Width / 2, 40);
    }
}
