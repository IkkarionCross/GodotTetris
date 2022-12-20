using Godot;
using System;
using System.Collections.Generic;

public enum PieceType 
{
	square, I, T, Z, L, J
}

public interface IPieceShape 
{
	void drawIn(Node2D node);
	Node2D removeNode(int id);
}

public abstract class PieceShape: IPieceShape {
	protected Color color;

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

	protected List<Node2D> _squareParts;
	public List<Node2D> Parts {
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
		this._squareParts = new List<Node2D>();
	}

	protected abstract void construct();

	public virtual void rotateRight() 
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

	public virtual void rotateLeft() 
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

	public void drawIn(Node2D node) 
	{
		foreach (SquareNode part in this._squareParts)
		{
			if (part.GetParent() == node) {
				continue;
			}
			node.AddChild(part);
			part.Id = part.GetInstanceId();
		}
	}

	public Node2D removeNode(int id)
	{
		Node2D removedNode = this._squareParts[id];
		this._squareParts.RemoveAt(id);

		return removedNode;
	}
}
