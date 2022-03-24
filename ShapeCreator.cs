using Godot;
using System;


public class ShapeCreator : Node2D
{
	private float time = 0.0f;
    const float TIME_TO_CREATE = 1.0f;


	private int created = 0;

	[Export] private NodePath boardNode;
	private Board2D board2D;

	private Vector2 viewPortSize;

	private Piece2D create()
	{
		Piece2D fallingPiece = new Piece2D(PieceType.square, new SquareShape(board2D.squareSize));
		fallingPiece.Position = board2D.pieceStartPosition(fallingPiece.Shape);
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

		GD.Print("Square size: " + board2D.squareSize);

		viewPortSize = GetViewportRect().Size;
		Piece2D piece1 = create();
        AddChild(piece1);
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		time += delta;
        if (time >= TIME_TO_CREATE) 
		{
			if (created == 2)
			{
				return;
			}
            time = 0;
			Piece2D piece = create();
        	AddChild(piece);
			created++;
		}
 	}
	
	public override void _Draw() {
	}
}