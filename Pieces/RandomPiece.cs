using System;
using Godot;

public static class RandomPieceType
{
	public static PieceType random()
	{
		PieceType[] types = (PieceType[])typeof(PieceType).GetEnumValues();

		RandomNumberGenerator rand = new RandomNumberGenerator();
		rand.Seed = (ulong)DateTime.Now.Ticks;
		int randomInt = (int)Mathf.Floor(rand.Randf() * (types.Length - 1));

		return types[randomInt];
	}
}