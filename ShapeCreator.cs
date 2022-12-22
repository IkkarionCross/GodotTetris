using Godot;
using System;

public class ShapeCreator : Node2D
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

		addNewPiece();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		time += delta;
        if (time >= TIME_TO_CREATE && !currentPiece.IsMoving)
		{
            time = 0;
			addNewPiece();
		}
 	}

	private void addNewPiece() 
	{
		PieceType type = RandomPieceGenerator.Instance.Random();
		currentPiece = create(type); 
        AddChild(currentPiece);
	}

	private Piece2D create(PieceType pieceType)
	{
		switch (pieceType)
		{
			case PieceType.O:
				return createO();
			case PieceType.I:
				return createI();
			case PieceType.J:
				return createJ();
			case PieceType.L:
				return createL();
			case PieceType.Z:
				return createZ();
			case PieceType.T:
				return createT();
			case PieceType.S:
				return createS();
			default:
				return null;
		}
	}

	private Piece2D createZ()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.T;
		fallingPiece.Shape = new ZShape(board2D.squareSize);
		fallingPiece.Position = board2D.pieceStartPosition(fallingPiece.Shape) + (Vector2.Down * board2D.squareSize.x  * 0.5f) + (Vector2.Left * board2D.squareSize.x * 2);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D createT()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.T;
		fallingPiece.Shape = new TShape(board2D.squareSize);
		fallingPiece.Position = board2D.pieceStartPosition(fallingPiece.Shape) + (Vector2.Left * board2D.squareSize.x * 3);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D createI()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.I;
		fallingPiece.Shape = new IShape(board2D.squareSize);
		fallingPiece.Position = board2D.pieceStartPosition(fallingPiece.Shape) + (Vector2.Left * board2D.squareSize.x * 3);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D createL()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.L;
		fallingPiece.Shape = new LShape(board2D.squareSize);
		fallingPiece.Position = board2D.pieceStartPosition(fallingPiece.Shape) + (Vector2.Left * board2D.squareSize.x * 3);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D createJ()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.L;
		fallingPiece.Shape = new JShape(board2D.squareSize);
		fallingPiece.Position = board2D.pieceStartPosition(fallingPiece.Shape) + (Vector2.Left * board2D.squareSize.x * 3);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D createO()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.O;
		fallingPiece.Shape = new OShape(board2D.squareSize);
		fallingPiece.Position = board2D.pieceStartPosition(fallingPiece.Shape) + (Vector2.Right * board2D.squareSize.x);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

	private Piece2D createS()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.S;
		fallingPiece.Shape = new SShape(board2D.squareSize);
		fallingPiece.Position = board2D.pieceStartPosition(fallingPiece.Shape) + (Vector2.Right * board2D.squareSize.x);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}
}
