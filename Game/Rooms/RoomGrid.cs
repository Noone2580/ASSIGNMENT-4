using System;
using System.Numerics;
using MohawkGame2D;

public class RoomGrid
{
    public int GridSize = 20;
    public Vector4[] Rooms = new Vector4[0];
    public Vector2 CurrentRoomPosition = Vector2.Zero;
    protected BaseRoom[] RoomTypes = new BaseRoom[0];

    /// <summary>
    ///		For adding rooms. it takes a Grid Postion, a Room Type, and a Room Code.
	///		If the Room Code is grender then 0 then the door is locked.
	///		Room Types are what kind of room is it. And goes as follows.
	///		<list type="number">-|- AllWay
	///      <item> |" TopLeftCorner</item>
	///		 <item>"|  TopRightCorner</item>
	///		 <item>|_  BottomLeftCorner</item>
	///		 <item>_| BottomRightCorner</item>
	///		 <item>|- ThreeWayRight</item>
	///		 <item>-| ThreeWayLeft</item>
	///		 <item>_|_ ThreeWayTop</item>
	///		 <item>"|" ThreeWayBottom</item>
	///		 <item>| HallwayVertical</item>
	///		 <item>-- HallwayHorizontal</item>
	///		</list>
    /// </summary>
	/// <remarks><em><b>Returns</b></em> the index of the new Room</remarks>
    /// <param name="GridPostion"></param>
    /// <param name="RoomType"></param>
    /// <param name="RoomCode"></param>
    /// <returns></returns>
    public int AddRoom(Vector2 GridPostion, int RoomType, int RoomCode)
    {
        for (int i = 0; i < Rooms.Length; i++)
        {
            if (Rooms[i] == Vector4.Zero)
            {
                Rooms[i] = new Vector4(GridPostion, RoomType, RoomCode);
                return i;
            }
        }
        return -1;
    }


    public BaseRoom GetRoomClassAtGrid(Vector2 GridPosition)
    {
        for (int i = 0; i < Rooms.Length; i++)
        {
            if (GridPosition == new Vector2(Rooms[i].X, Rooms[i].Y))
            {
                return RoomTypes[((int)Rooms[i].Z)];
            }
        }

        return null;
    }

    public Vector4 GetRoomAtGrid(Vector2 GridPosition)
    {
        for (int i = 0; i < Rooms.Length; i++)
        {
            if (GridPosition == new Vector2(Rooms[i].X, Rooms[i].Y))
            {
                return Rooms[i];
            }
        }

        return Vector4.Zero;
    }

    public int GetIndexAtGrid(Vector2 GridPosition)
    {
        for (int i = 0; i < Rooms.Length; i++)
        {
            if (GridPosition == new Vector2(Rooms[i].X, Rooms[i].Y))
            {
                return i;
            }
        }

        return -1;
    }

    public Vector4 GetRoomAtIndex(int index)
    {
        index = int.Clamp(index, 0, Rooms.Length - 1);
        return Rooms[index];
    }

    public Vector4 SetRoomAtIndex(int index, Vector4 newRoom)
    {
        index = int.Clamp(index, 0, Rooms.Length - 1);
        Rooms[index] = newRoom;
        return Rooms[index];
    }

    public void Setup()
    {
        Rooms = new Vector4[GridSize];
        RoomTypes =
            [
            new AllWay(),
            new TopLeftCorner(),
            new TopRightCorner(),
            new BottomLeftCorner(),
            new BottomRightCorner(),
            new ThreeWayRight(),
            new ThreeWayLeft(),
            new ThreeWayTop(),
            new ThreeWayBottom(),
            new HallwayVertical(),
            new HallwayHorizontal()
            ];



        AddRoom(new Vector2(1, 0), 0, 0);
        AddRoom(new Vector2(1, 1), 0, 1);
        AddRoom(new Vector2(1, 2), 0, 0);

        AddRoom(new Vector2(2, 0), 0, 0);
        AddRoom(new Vector2(2, 1), 0, 1);
        AddRoom(new Vector2(2, 2), 0, 0);

        AddRoom(new Vector2(3, 0), 0, 0);
        AddRoom(new Vector2(3, 1), 0, 0);
        AddRoom(new Vector2(3, 2), 0, 0);


    }
}
