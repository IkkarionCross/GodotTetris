using Godot;

public class SquareShape: PieceShape 
{
	protected new Color color
	{
		get { return Colors.Yellow; }
	}

	public SquareShape(Vector2 squareSize) : base(squareSize) 
	{
		this.squareSize = squareSize;
		this.size = new Vector2(squareSize.x * 2, squareSize.y * 2);
		this.type = PieceType.square;
		
		construct();
	}

	protected override void construct()
	{
		SquareNode node1 = new SquareNode(new Vector2(-squareSize.x, -squareSize.y), squareSize, color, true);
		SquareNode node2 = new SquareNode(new Vector2(0            , -squareSize.y), squareSize, color, true);
		SquareNode node3 = new SquareNode(new Vector2(-squareSize.x, 0            ), squareSize, color, true);
		SquareNode node4 = new SquareNode(    Vector2.Zero                         , squareSize, color, true);

		this._squareParts.Add(node1);
		this._squareParts.Add(node2);
		this._squareParts.Add(node3);
		this._squareParts.Add(node4);
	}

	public override void rotateLeft() {}

	public override void rotateRight() {}

}