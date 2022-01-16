using Godot;

public class Board2D: Node2D 
{
    private Vector2 size;
    
    public bool[,] boardPieces;

    public override void _Ready()
	{
        boardPieces = new bool[10,20];
    }


    public override void _Draw() {
	
	}
}