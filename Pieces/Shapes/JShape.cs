using Godot;

public class JShape: PieceShape 
{
	protected new Color color
	{
		get { return Colors.WebPurple; }
	}

	public JShape(Vector2 squareSize) : base(squareSize) 
	{
		this.squareSize = squareSize;
		this.size = new Vector2(0, 0);
		this.type = PieceType.J;
		
		construct();
	}

	protected override void construct()
	{
		SquareNode node1 = new SquareNode(new Vector2(0			   , -squareSize.y * 2), squareSize, color, true);
		SquareNode node2 = new SquareNode(new Vector2(0			   , -squareSize.y    ), squareSize, color, true);
		SquareNode node3 = new SquareNode(new Vector2(0			   , 0				  ), squareSize, color, true);
		SquareNode node4 = new SquareNode(new Vector2(-squareSize.x, 0				  ), squareSize, color, true);

		this._squareParts.Add(node1);
		this._squareParts.Add(node2);
		this._squareParts.Add(node3);
		this._squareParts.Add(node4);

		pivot = node2;
	}
}
