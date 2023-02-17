using System;
using System.Collections.Generic;
using Godot;

public class RandomPieceGenerator
{

	//Defines a list of 3 bags that contains the 7 pieces in a random order
	private PieceType[,] bags;
	private const int numberOfBags = 3;

	private RandomNumberGenerator randomGenerator;

	private int TypeCount 
	{
		get 
		{
			return ((PieceType[])typeof(PieceType).GetEnumValues()).Length - 1;
		}
	}

	private int currentBag;
	private int pieceInBag;

	private PieceType[] pieces;

	private RandomPieceGenerator()
	{
		randomGenerator = new RandomNumberGenerator();
		
		pieces = (PieceType[])typeof(PieceType).GetEnumValues();
		bags = new PieceType[3,pieces.Length];
		currentBag = 0;
		pieceInBag = 0;

		for(int i = 0; i < bags.GetLength(0); i++)
		{
			ResetBag(i);
		}
	}

	private static RandomPieceGenerator instance;
	public static RandomPieceGenerator Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new RandomPieceGenerator();
			}
			return instance;
		}
	}

	public PieceType Random()
	{
		PieceType type = bags[currentBag, pieceInBag];
		pieceInBag++;

		if (pieceInBag != TypeCount)
		{
			return type;
		}
		
		pieceInBag = 0;
		if (currentBag == numberOfBags - 1) 
		{
			ResetBag(currentBag);
			currentBag = 0;
		}
		else 
		{
			currentBag++;
		}

		return type;
	}

	private void ResetBag(int bag)
	{
		List<PieceType> piecesPool = new List<PieceType>(this.pieces);
		for(int j = 0; j < bags.GetLength(1); j++)
		{
			randomGenerator.Seed = (ulong)DateTime.Now.Ticks;
			int randomInt = (int)Mathf.Floor(randomGenerator.Randf() * (piecesPool.Count));

			bags[bag,j] = piecesPool[randomInt];

			piecesPool.RemoveAt(randomInt);
		}
	}

}