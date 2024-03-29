using Godot;
using System;
using System.Collections.Generic;

public enum PieceType 
{
	O, I, T, Z, L, J, S
}

public interface IPieceShape 
{
	void DrawIn(Node2D node);
	Node2D RemoveNode(int id);
}

public abstract class PieceShape: IPieceShape {
	protected Color color;

	protected Vector2 squareSize;
	public  Vector2 SquareSize 
	{
		get {return squareSize;}
	}

	protected PieceType type;
	public PieceType Type 
	{
		get { return type; }
	}

	protected List<Node2D> squareParts;
	public List<Node2D> Parts {
		get { return squareParts; }
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

	public PieceShape(Vector2 squareSize) 
	{
		this.squareSize = squareSize;
		this.squareParts = new List<Node2D>();
	}

	protected abstract void Construct();

	public virtual void RotateRight() 
	{
		for (int i = 0; i < Parts.Count; i++)
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

	public virtual void RotateLeft() 
	{
		for (int i = 0; i < Parts.Count; i++)
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

	public void DrawIn(Node2D node) 
	{
		foreach (SquareNode part in this.squareParts)
		{
			if (part.GetParent() == node) {
				continue;
			}
			node.AddChild(part);
			part.Id = part.GetInstanceId();
		}
	}

	public Node2D RemoveNode(int id)
	{
		Node2D removedNode = this.squareParts[id];
		this.squareParts.RemoveAt(id);

		return removedNode;
	}
}
