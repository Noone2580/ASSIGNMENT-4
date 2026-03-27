using MohawkGame2D;
using System;
using System.Numerics;

public class BaseAI : BaseCharacter
{
	

	public BasePlayer GetClosetPlayer() 
	{
        Vector2[] PlayerPos = GetGame.GetAllPlayerPositions();
        float Dis = 99999999999f;
        int Index = 0;

        for (int i = 0; i < PlayerPos.Length; i++)
        {
            if (Vector2.Distance(Position, PlayerPos[i]) <= Dis)
            {
                Dis = Vector2.Distance(Position, PlayerPos[i]);
                Index = i;
            }
        }

        return GetGame.GetAllPlayers()[Index];
    }
}
