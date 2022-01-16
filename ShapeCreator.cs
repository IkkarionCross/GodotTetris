using Godot;
using System;


public class ShapeCreator : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	private Vector2 pieceSize = Vector2.One * 5;

    private Piece2D fallingPiece;

	private Vector2 viewPortSize;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		viewPortSize = GetViewportRect().Size;
		fallingPiece = new Piece2D(PieceType.square, new SquareShape(pieceSize));
        AddChild(fallingPiece);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
	if (!fallingPiece.IsMoving) { return; }

	// floor collision
	if (fallingPiece.hasCollidedWithFloor(viewPortSize)) {
    	fallingPiece.adjustPositionForCollisionWithFloor(viewPortSize);
		fallingPiece.stopMoving();
		GD.Print("Stop moving. Collision detected");
	}
 }
	
	
	public override void _Draw() {
	}
}
