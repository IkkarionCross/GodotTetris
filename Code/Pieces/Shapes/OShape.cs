using Godot;

public class OShape: PieceShape 
{
	protected new Color color
	{
		get { return Colors.Yellow; }
	}

	public OShape(Vector2 squareSize) : base(squareSize) 
	{
		this.squareSize = squareSize;
		this.type = PieceType.O;
		
		Construct();
	}

	protected override void Construct()
	{
		SquareNode node1 = new SquareNode(new Vector2(-squareSize.x, -squareSize.y), squareSize, color, true);
		SquareNode node2 = new SquareNode(new Vector2(0            , -squareSize.y), squareSize, color, true);
		SquareNode node3 = new SquareNode(new Vector2(-squareSize.x, 0            ), squareSize, color, true);
		SquareNode node4 = new SquareNode(    Vector2.Zero                         , squareSize, color, true);

		this.squareParts.Add(node1);
		this.squareParts.Add(node2);
		this.squareParts.Add(node3);
		this.squareParts.Add(node4);
	}

	public override void RotateLeft() {}

	public override void RotateRight() {}

}