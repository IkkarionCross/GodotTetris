using Godot;
using System;

public enum PieceType 
{
	square, I, T, Z, L, J
}

public static class RandomPieceType
{
	public static PieceType random()
	{
		PieceType[] types = (PieceType[])typeof(PieceType).GetEnumValues();

		RandomNumberGenerator rand = new RandomNumberGenerator();
		rand.Seed = (ulong)DateTime.Now.Ticks;
		int randomInt = (int)Mathf.Floor(rand.Randf() * (types.Length - 1));

		GD.Print("The random int: " + randomInt);
		
		return types[randomInt];
	}
}

public interface IPieceShape 
{
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

	public SquareNode(Vector2 position, Vector2 size, Color color, bool isCollidable) 
	{
		this.Position = position;
		this.size = new Vector2(size.x - 1, size.y - 1);
		this.color = color;
		this.isCollidable = isCollidable;
	}

	public override void _Draw() 
	{
		this.DrawRect(new Rect2(Vector2.Zero, size), new Color(0,0,1,1));
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

	protected PieceType type;
	public PieceType Type 
	{
		get { return type; }
	}

	protected Node2D[] _squareParts;
	public Node2D[] Parts {
		get { return _squareParts; }
	}

	protected SquareNode pivot;
	protected SquareNode Pivot
	{
		get { return pivot; }
		set 
		{
			pivot = value;
		}
	}

	public PieceShape(Vector2 squareSize) {
		this.squareSize = squareSize;
	}

	protected abstract void construct();

	public virtual void rotateRight() 
	{
		for (int i = 0; i < Parts.Length; i++)
		{
			SquareNode node = (SquareNode)(Parts[i]);
			
			float x = node.GlobalPosition.x - Pivot.GlobalPosition.x;
			float y = node.GlobalPosition.y - Pivot.GlobalPosition.y;

			float x1 = Pivot.GlobalPosition.y - node.GlobalPosition.y;
			float y1 = x;

			float newx = Pivot.GlobalPosition.x + x1;
			float newy = Pivot.GlobalPosition.y + y1;

			node.GlobalPosition = new Vector2(newx, newy); 
		}
	}

	public virtual void rotateLeft() 
	{
		for (int i = 0; i < Parts.Length; i++)
		{
			SquareNode node = (SquareNode)(Parts[i]);

			float x = node.GlobalPosition.x - Pivot.GlobalPosition.x;
			float y = node.GlobalPosition.y - Pivot.GlobalPosition.y;

			float x1 = y;
			float y1 = Pivot.GlobalPosition.x - node.GlobalPosition.x;

			float newx = Pivot.GlobalPosition.x + x1;
			float newy = Pivot.GlobalPosition.y + y1;

			node.GlobalPosition = new Vector2(newx, newy); 
		}
	}

	public abstract void drawIn(Node2D node);
}

public class SquareShape: PieceShape 
{
	public SquareShape(Vector2 squareSize) : base(squareSize) 
	{
		this.squareSize = squareSize;
		this.size = new Vector2(squareSize.x * 2, squareSize.y * 2);
		this._squareParts = new Node2D[4];
		this.type = PieceType.square;
		
		construct();
	}

	protected override void construct()
	{
		SquareNode node1 = new SquareNode(new Vector2(-squareSize.x, -squareSize.y), squareSize, new Color(0,0,1,1), true);
		SquareNode node2 = new SquareNode(new Vector2(0            , -squareSize.y), squareSize, new Color(0,0,1,1), true);
		SquareNode node3 = new SquareNode(new Vector2(-squareSize.x, 0            ), squareSize, new Color(0,0,1,1), true);
		SquareNode node4 = new SquareNode(    Vector2.Zero                         , squareSize , new Color(0,0,1,1), true);

		this._squareParts[0] = node1;
		this._squareParts[1] = node2;
		this._squareParts[2] = node3;
		this._squareParts[3] = node4;
	}

	public override void rotateLeft() {}

	public override void rotateRight() {}

	public override void drawIn(Node2D node) 
	{
		foreach (Node2D part in this._squareParts)
		{
			node.AddChild(part);
		}
	}

}

public class LShape: PieceShape 
{
	public LShape(Vector2 squareSize) : base(squareSize) 
	{
		this.squareSize = squareSize;
		this.size = new Vector2(0, 0);
		this._squareParts = new Node2D[4];
		this.type = PieceType.L;
		
		construct();
	}

	protected override void construct()
	{
		SquareNode node1 = new SquareNode(new Vector2(0			  , -squareSize.y * 2), squareSize, new Color(0,0,1,1), true);
		SquareNode node2 = new SquareNode(new Vector2(0			  , -squareSize.y    ), squareSize, new Color(0,0,1,1), true);
		SquareNode node3 = new SquareNode(new Vector2(0			  , 0				 ), squareSize, new Color(0,0,1,1), true);
		SquareNode node4 = new SquareNode(new Vector2(squareSize.x, 0				 ), squareSize, new Color(0,0,1,1), true);

		this._squareParts[0] = node1;
		this._squareParts[1] = node2;
		this._squareParts[2] = node3;
		this._squareParts[3] = node4;

		pivot = node2;
	}

	public override void drawIn(Node2D node) 
	{
		foreach (Node2D part in this._squareParts)
		{
			node.AddChild(part);
		}
	}
}

public class JShape: PieceShape 
{
	public JShape(Vector2 squareSize) : base(squareSize) 
	{
		this.squareSize = squareSize;
		this.size = new Vector2(0, 0);
		this._squareParts = new Node2D[4];
		this.type = PieceType.J;
		
		construct();
	}

	protected override void construct()
	{
		SquareNode node1 = new SquareNode(new Vector2(0			   , -squareSize.y * 2), squareSize, new Color(0,0,1,1), true);
		SquareNode node2 = new SquareNode(new Vector2(0			   , -squareSize.y    ), squareSize, new Color(0,0,1,1), true);
		SquareNode node3 = new SquareNode(new Vector2(0			   , 0				  ), squareSize, new Color(0,0,1,1), true);
		SquareNode node4 = new SquareNode(new Vector2(-squareSize.x, 0				  ), squareSize, new Color(0,0,1,1), true);

		this._squareParts[0] = node1;
		this._squareParts[1] = node2;
		this._squareParts[2] = node3;
		this._squareParts[3] = node4;

		pivot = node2;
	}

	public override void drawIn(Node2D node) 
	{
		foreach (Node2D part in this._squareParts)
		{
			node.AddChild(part);
		}
	}
}

public class IShape: PieceShape 
{
	public IShape(Vector2 squareSize) : base(squareSize) 
	{
		this.squareSize = squareSize;
		this.size = new Vector2(0, 0);
		this._squareParts = new Node2D[4];
		this.type = PieceType.I;
		
		construct();
	}

	protected override void construct()
	{
		SquareNode node1 = new SquareNode(new Vector2(0, -squareSize.y * 3), squareSize, new Color(0,0,1,1), true);
		SquareNode node2 = new SquareNode(new Vector2(0, -squareSize.y * 2), squareSize, new Color(0,0,1,1), true);
		SquareNode node3 = new SquareNode(new Vector2(0, -squareSize.y    ), squareSize, new Color(0,0,1,1), true);
		SquareNode node4 = new SquareNode(new Vector2(0, 0				  ), squareSize, new Color(0,0,1,1), true);

		this._squareParts[0] = node1;
		this._squareParts[1] = node2;
		this._squareParts[2] = node3;
		this._squareParts[3] = node4;

		pivot = node2;
	}

	public override void drawIn(Node2D node) 
	{
		foreach (Node2D part in this._squareParts)
		{
			node.AddChild(part);
		}
	}
}

public class TShape: PieceShape 
{
	public TShape(Vector2 squareSize) : base(squareSize) 
	{
		this.squareSize = squareSize;
		this.size = new Vector2(0, 0);
		this._squareParts = new Node2D[4];
		this.type = PieceType.T;
		
		construct();
	}

	protected override void construct()
	{
		SquareNode node1 = new SquareNode(new Vector2(0           , -squareSize.y * 2), squareSize, new Color(0,0,1,1), true);
		SquareNode node2 = new SquareNode(new Vector2(0           , -squareSize.y * 1), squareSize, new Color(0,0,1,1), true);
		SquareNode node3 = new SquareNode(new Vector2(0           , 0                ), squareSize, new Color(0,0,1,1), true);
		SquareNode node4 = new SquareNode(new Vector2(squareSize.x, -squareSize.y * 1), squareSize, new Color(0,0,1,1), true);
		
		this._squareParts[0] = node1;
		this._squareParts[1] = node2;
		this._squareParts[2] = node3;
		this._squareParts[3] = node4;

		pivot = node2;
	}

	public override void drawIn(Node2D node) 
	{
		foreach (Node2D part in this._squareParts)
		{
			node.AddChild(part);
		}
	}
}

public class ZShape: PieceShape 
{
	public ZShape(Vector2 squareSize) : base(squareSize) 
	{
		this.squareSize = squareSize;
		this.size = new Vector2(0, 0);
		this._squareParts = new Node2D[4];
		this.type = PieceType.T;
		
		construct();
	}

	protected override void construct()
	{
		SquareNode node1 = new SquareNode(new Vector2(-squareSize.x, -squareSize.y - squareSize.y * 0.5f), squareSize, new Color(0,0,1,1), true);
		SquareNode node2 = new SquareNode(new Vector2(-squareSize.x, -squareSize.y * 0.5f               ), squareSize, new Color(0,0,1,1), true);
		SquareNode node4 = new SquareNode(new Vector2(0            , -squareSize.y * 0.5f               ), squareSize, new Color(0,0,1,1), true);
		SquareNode node3 = new SquareNode(new Vector2(0            , squareSize.y  * 0.5f               ), squareSize, new Color(0,0,1,1), true);

		this._squareParts[0] = node1;
		this._squareParts[1] = node2;
		this._squareParts[2] = node3;
		this._squareParts[3] = node4;

		pivot = node2;
	}

	public override void drawIn(Node2D node) 
	{
		foreach (Node2D part in this._squareParts)
		{
			node.AddChild(part);
		}
	}
}