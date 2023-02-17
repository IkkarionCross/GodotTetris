using Godot;

public class ZShape: PieceShape 
{
	protected new Color color
	{
		get { return Colors.DarkOrange; }
	}
	public ZShape(Vector2 squareSize) : base(squareSize) 
	{
		this.squareSize = squareSize;
		this.type = PieceType.T;
		
		Construct();
	}

	protected override void Construct()
	{
		SquareNode node1 = new SquareNode(new Vector2(-squareSize.x, -squareSize.y - squareSize.y * 0.5f), squareSize, color, true);
		SquareNode node2 = new SquareNode(new Vector2(-squareSize.x, -squareSize.y * 0.5f               ), squareSize, color, true);
		SquareNode node4 = new SquareNode(new Vector2(0            , -squareSize.y * 0.5f               ), squareSize, color, true);
		SquareNode node3 = new SquareNode(new Vector2(0            , squareSize.y  * 0.5f               ), squareSize, color, true);

		this.squareParts.Add(node1);
		this.squareParts.Add(node2);
		this.squareParts.Add(node3);
		this.squareParts.Add(node4);

		pivot = node2;
	}
}