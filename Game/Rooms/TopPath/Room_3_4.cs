using MohawkGame2D;
using System.Numerics;

public class Room_3_4 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "3";
        MapPosition = new Vector2(3, 4);
        RoomCode = 1;

        Doors = new BaseDoor[4];
        ConectedRooms = new BaseRoom[4];

        AddDoor(0,
            new Vector2(0, Window.Height / 2),
            new Vector2(Window.Width - 100, Window.Height / 2),
            new Room_2_4());

        AddDoor(1,
            new Vector2(Window.Width, Window.Height / 2),
            new Vector2(100, Window.Height / 2),
            new BossRoom());

        AddDoor(2,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new CenterRoom());

        AddDoor(3,
            new Vector2(Window.Width / 2, Window.Height),
            new Vector2(Window.Width / 2, 100),
            new Room_3_5());
    }
}
