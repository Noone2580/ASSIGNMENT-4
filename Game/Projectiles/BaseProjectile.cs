using System;
using System.Numerics;
using MohawkGame2D;

public class BaseProjectile
{
	Game? GetGame;

	public Vector2 Velocity {  get; protected set; } = Vector2.Zero; 
	public Vector2 Position {  get; protected set; } = Vector2.Zero;
	public float HitRange { get; protected set; } = 10f;
	public float Damage { get; protected set; } = 10f;
	public float HitFroce { get; protected set; } = 100f;
	string RoomName = ""; 

	public void Setup(Game game, Vector2 Spawnposition, Vector2 Speed) 
	{
		GetGame = game;
		Position = Spawnposition;
		Velocity = Speed;
		RoomName = $"{GetGame.CurrentRoom}";
	}



	public void Render() 
	{
		Position += Velocity * Time.DeltaTime;

		Draw.FillColor = Color.White;
		Draw.Circle(Position, HitRange);
	}
}
