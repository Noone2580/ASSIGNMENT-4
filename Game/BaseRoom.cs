using System;
using System.Numerics;
using MohawkGame2D;

/// <summary>
///     This Class has the base functions and variables that all rooms inhairint form.
/// </summary>
public class BaseRoom
{
    BaseDoor[] Doors = new BaseDoor[4];


    public virtual void Setup()
    {
        
        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i] = new BaseDoor();

            switch (i) 
            {
                case 0:
                    Doors[i].Position = new Vector2(0, Window.Height / 2);
                    break;
                case 1:
                    Doors[i].Position = new Vector2(Window.Width / 2, 0); 
                    break;
                case 2:
                    Doors[i].Position = new Vector2(Window.Width,Window.Height / 2);
                    break;
                case 3:
                    Doors[i].Position = new Vector2(Window.Width/2, Window.Height);
                    break;
            }
            Doors[i].Setup(); 
        }
    }

    public virtual void Render()
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].Render();
        }
    }
}
