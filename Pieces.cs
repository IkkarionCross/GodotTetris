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

	public SquareNode(Vector2 position, Vector2 size, Color color) 
	{
		this.squarePosition = position;
		this.size = size;
		this.color = color;
	}

	public override void _Draw() 
	{
		this.DrawRect(new Rect2(squarePosition, size), new Color(0,0,1,1));
	}
}

public abstract class PieceShape: IPieceShape {
	private Vector2 size;
	public Vector2 Size {
		get {return size;}
	}

	private Node2D[] _squareParts;
	public Node2D[] Parts {
		get { return _squareParts; }
	}

	public PieceShape(Vector2 size) {
		this.size = size;
	}

	public abstract void drawIn(Node2D node);


}

public class SquareShape: PieceShape {
	
	public SquareShape(Vector2 size) : base(size) {}

	public override void drawIn(Node2D node) {
		SquareNode node1 = new SquareNode(new Vector2(-Size.x, -Size.y), Size, new Color(0,0,1,1));
		SquareNode node2 = new SquareNode(new Vector2(0, -Size.y), Size, new Color(0,0,1,1));
		SquareNode node3 = new SquareNode(new Vector2(-Size.x, 0), Size, new Color(0,0,1,1));
		SquareNode node4 = new SquareNode(Vector2.Zero, Size, new Color(0,0,1,1));

		node.AddChild(node1);
		node.AddChild(node2);
		node.AddChild(node3);
		node.AddChild(node4);

		node1.Position = Vector2.Zero;
		node2.Position = Vector2.Zero;
		node3.Position = Vector2.Zero;
		node4.Position = Vector2.Zero;
	}
}