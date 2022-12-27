using Godot;

public class SquareNode: Node2D 
{
	private Vector2 squarePosition;
	private Vector2 size;
	private Color color;

	private ulong id;
	public ulong Id 
	{
		get {return id;}
		set
		{
			this.id = value;
		}
	}

	private bool isCollidable;
	public bool IsCollidable
	{
		get  { return isCollidable; }
	}

	public SquareNode(Vector2 position, Vector2 size, Color color, bool isCollidable, int id = -1) 
	{
		this.Position = position;
		this.size = new Vector2(size.x - 1, size.y - 1);
		this.color = color;
		this.isCollidable = isCollidable;
	}

	public override void _Draw() 
	{
		this.DrawRect(new Rect2(Vector2.Zero, size), color);
	}
}