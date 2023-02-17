using Godot;
using System;

public class TetrisGod : Node2D
{
	private float time = 0.0f;
	const float TIME_TO_CREATE = 3.0f;
 
	[Export] private NodePath boardNode;
	private Board2D board2D;

	private Vector2 viewPortSize;

	private Piece2D currentPiece;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (boardNode == null)
		{
			GD.Print("Did you forget to reference the board?");
			return;
		}
		board2D = GetNode<Board2D>(boardNode);
		viewPortSize = GetViewportRect().Size;

		AddNewPiece();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		time += delta;
		if (time >= TIME_TO_CREATE && !currentPiece.IsMoving)
		{
			board2D.CheckTetris();
			time = 0;
			AddNewPiece();
		}
 	}

	private void AddNewPiece() 
	{
		PieceType type = RandomPieceGenerator.Instance.Random();
		currentPiece = Create(type); 
		AddChild(currentPiece);
	}

	private Piece2D Create(PieceType pieceType)
	{
		switch (pieceType)
		{
			case PieceType.O:
				return CreateO();
			case PieceType.I:
				return CreateI();
			case PieceType.J:
				return CreateJ();
			case PieceType.L:
				return CreateL();
			case PieceType.Z:
				return CreateZ();
			case PieceType.T:
				return CreateT();
			case PieceType.S:
				return CreateS();
			default:
				return null;
		}
	}

	private Piece2D CreateZ()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.T;
		fallingPiece.Shape = new ZShape(board2D.SquareSize);
		fallingPiece.Position = board2D.PieceStartPosition(fallingPiece.Shape) + (Vector2.Down * board2D.SquareSize.x  * 0.5f) + (Vector2.Left * board2D.SquareSize.x * 2);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D CreateT()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.T;
		fallingPiece.Shape = new TShape(board2D.SquareSize);
		fallingPiece.Position = board2D.PieceStartPosition(fallingPiece.Shape) + (Vector2.Left * board2D.SquareSize.x * 3);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D CreateI()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.I;
		fallingPiece.Shape = new IShape(board2D.SquareSize);
		fallingPiece.Position = board2D.PieceStartPosition(fallingPiece.Shape) + (Vector2.Left * board2D.SquareSize.x * 3);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D CreateL()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.L;
		fallingPiece.Shape = new LShape(board2D.SquareSize);
		fallingPiece.Position = board2D.PieceStartPosition(fallingPiece.Shape) + (Vector2.Left * board2D.SquareSize.x * 3);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D CreateJ()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.L;
		fallingPiece.Shape = new JShape(board2D.SquareSize);
		fallingPiece.Position = board2D.PieceStartPosition(fallingPiece.Shape) + (Vector2.Left * board2D.SquareSize.x * 2);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D CreateO()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.O;
		fallingPiece.Shape = new OShape(board2D.SquareSize);
		fallingPiece.Position = board2D.PieceStartPosition(fallingPiece.Shape) + (Vector2.Left * board2D.SquareSize.x * 2);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D CreateS()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.S;
		fallingPiece.Shape = new SShape(board2D.SquareSize);
		fallingPiece.Position = board2D.PieceStartPosition(fallingPiece.Shape) + (Vector2.Left * board2D.SquareSize.x * 2);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}
}
