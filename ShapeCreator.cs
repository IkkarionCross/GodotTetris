using Godot;
using System;


public class ShapeCreator : Node2D
{
	private float time = 0.0f;
    const float TIME_TO_CREATE = 3.0f;


	private int created = 0;

	[Export] private NodePath boardNode;
	private Board2D board2D;

	private Vector2 viewPortSize;

	private Piece2D createZ()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.T;
		fallingPiece.Shape = new ZShape(board2D.squareSize);
		fallingPiece.Position = board2D.pieceStartPosition(fallingPiece.Shape) + (Vector2.Down * board2D.squareSize.x  * 4.5f) + (Vector2.Left * board2D.squareSize.x * 2);
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

	private Piece2D createSquare()
	{
		Piece2D fallingPiece = new Piece2D();
		fallingPiece.Type = PieceType.square;
		fallingPiece.Shape = new SquareShape(board2D.squareSize);
		fallingPiece.Position = board2D.pieceStartPosition(fallingPiece.Shape) + (Vector2.Right * board2D.squareSize.x);
		fallingPiece.Board = board2D;
		return fallingPiece;
	}

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
		Piece2D piece1 = createZ();
        AddChild(piece1);
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		time += delta;
        if (time >= TIME_TO_CREATE) 
		{
			if (created == 1)
			{
				return;
			}
            time = 0;
			// Piece2D piece = createSquare();
        	// AddChild(piece);
			created++;
		}
 	}

	public override void _Draw() {
	}
}
