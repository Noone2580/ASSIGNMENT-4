using MohawkGame2D;
using System.Numerics;

public class StartingRoom : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "Start";
        MapPosition = new Vector2(0, 4);

        Doors = new BaseDoor[2];
        ConectedRooms = new BaseRoom[2];

        AddDoor(0,
            new Vector2(Window.Width, Window.Height / 2),
            new Vector2(100, Window.Height / 2),
            new Room_1_4());

        AddDoor(1,
            new Vector2(120, Window.Height),
            new Vector2(120, 100),
            new Room_0_5());
    }
}