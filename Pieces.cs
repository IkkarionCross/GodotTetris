using Godot;

public enum PieceType {
	square, I, T, Z, S, L
}

public interface IPieceShape {
	void drawIn(Node2D node);
}

public class SquareNode: Node2D 
{
	private Vector2 squarePosition;
	private Vector2 size;
	private Color color;

	private bool isCollidable;
	public bool IsCollidable
	{
		get  { return isCollidable; }
	}

	public Vector2 fixedPosition
	{
		get 
		{
			float x = GlobalPosition.x + (size.x * 0.5f);
			float y = GlobalPosition.y + (size.x * 0.5f);

			return new Vector2(x, y);
		}
	}

	public SquareNode(Vector2 position, Vector2 size, Color color, bool isCollidable) 
	{
		this.Position = position;
		this.size = new Vector2(size.x - 1, size.y - 1);
		this.color = color;
		this.isCollidable = isCollidable;
	}

	public override void _Draw() 
	{
		this.DrawRect(new Rect2(Position, size), new Color(0,0,1,1));
	}
}

public abstract class PieceShape: IPieceShape {
	protected Vector2 squareSize;
	public  Vector2 SquareSize 
	{
		get {return squareSize;}
	}
	protected Vector2 size;
	public Vector2 Size {
		get {return size;}
	}

	protected Node2D[] _squareParts;
	public Node2D[] Parts {
		get { return _squareParts; }
	}

	public PieceShape(Vector2 squareSize) {
		this.squareSize = squareSize;
	}

	protected abstract void construct();

	public abstract void drawIn(Node2D node);


}

public class SquareShape: PieceShape {
	
	public SquareShape(Vector2 squareSize) : base(squareSize) 
	{
		this.squareSize = squareSize;
		this.size = new Vector2(squareSize.x * 2, squareSize.y * 2);
		this._squareParts = new Node2D[4];
		
		construct();
	}

	protected override void construct()
	{
		SquareNode node1 = new SquareNode(new Vector2(-squareSize.x * 0.5f, -squareSize.y * 0.5f), squareSize, new Color(0,0,1,1), false);
		SquareNode node2 = new SquareNode(new Vector2(0, -squareSize.y * 0.5f), squareSize, new Color(0,0,1,1), false);
		SquareNode node3 = new SquareNode(new Vector2(-squareSize.x * 0.5f, 0), squareSize, new Color(0,0,1,1), true);
		SquareNode node4 = new SquareNode(Vector2.Zero, squareSize, new Color(0,0,1,1), true);

		this._squareParts[0] = node1;
		this._squareParts[1] = node2;
		this._squareParts[2] = node3;
		this._squareParts[3] = node4;
	}

	public override void drawIn(Node2D node) 
	{
		foreach (Node2D part in this._squareParts)
		{
			node.AddChild(part);
		}
	}
}