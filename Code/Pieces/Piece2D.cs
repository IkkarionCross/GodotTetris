using Godot;

public class Piece2D: Node2D
{
    private float time = 0.0f;
    private const float TIME_TO_MOVE = 1.0f;
    private const float FAST_FALL_MULTIPLIER =  0.05f;
    private float timeToMove = 0.0f;

	private Vector2 velocity
    {
        get { return new Vector2(SquareSize.x, SquareSize.y); }
    }

    private bool isMoving;
    public bool IsMoving 
    {
        get { return isMoving; }
    }

	private PieceShape shape;
    public PieceShape Shape
    {
        get {return shape;}
        set 
        {
            this.shape = value;
        }
    }

	private PieceType type;
	public PieceType Type {
		get { return type; }
        set { this.type = value; }
	}

    public Vector2 SquareSize 
    {
        get { return shape.SquareSize; }
    } 

    private Vector2 startPosition;
    public Vector2 StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }

    public Board2D Board;

    public Piece2D() { }

    public override void _Ready()
	{
        this.timeToMove = TIME_TO_MOVE;
        this.isMoving = true;
        shape.DrawIn(this);
	}

    public override void _Process(float delta) 
    {
        if (!isMoving) { return; }

        time += delta;
        if (time >= timeToMove) 
        {
            time = 0;

            if (!Board.CanMove(this, Vector2.Down))
            {
                this.isMoving = false;
                Board.SetLocation(this);
                return;
            }

            Board.ResetLocation(this);
            MoveDown(velocity);
            Board.SetLocation(this);

            Update();
        }
    }

    public override void _Input(InputEvent inputEvent)
	{
        if (!isMoving) { return; }

		if (inputEvent.IsActionPressed("rotate_right"))
		{
            Board.ResetLocation(this);
            shape.RotateRight();
            
            if (!Board.CanMove(this, Vector2.Down) || !Board.CanMove(this, Vector2.Right))
            {
                shape.RotateLeft();
            }

            Board.SetLocation(this);
		}
        else if (inputEvent.IsActionPressed("rotate_left"))
		{
            Board.ResetLocation(this);
            shape.RotateLeft();
            if (!Board.CanMove(this, Vector2.Down) || !Board.CanMove(this, Vector2.Left))
            {
                shape.RotateRight();
            }
            Board.SetLocation(this);
		}
        else if (inputEvent.IsActionPressed("move_right"))
        {
            if (!Board.CanMove(this, Vector2.Right))
            {
                return;
            }
            Board.ResetLocation(this);
            this.GlobalTransform = GlobalTransform.Translated(velocity * Vector2.Right);
            Board.SetLocation(this);
        }
        else if (inputEvent.IsActionPressed("move_left"))
        {
            if (!Board.CanMove(this, Vector2.Left))
            {
                return;
            }
            Board.ResetLocation(this);
            this.GlobalTransform = GlobalTransform.Translated(velocity * Vector2.Left);
            Board.SetLocation(this);
        }
        else if (inputEvent.IsActionPressed("move_down"))
        {
            Board.ResetLocation(this);
            timeToMove = TIME_TO_MOVE * FAST_FALL_MULTIPLIER;
            Board.SetLocation(this);
        }

        if (inputEvent.IsActionReleased("move_down")) 
        {
            timeToMove = TIME_TO_MOVE;
        }
	}

    public override void _Draw() 
    {
		shape.DrawIn(this);
	}

    public void MoveDown(Vector2 velocity)
    {
        this.GlobalTransform = GlobalTransform.Translated(velocity * Vector2.Down);
    }

    public void RemoveNodeAt(ulong nodeId)
    {
        Node2D removedNode = shape.Parts.Find(item => { return item.GetInstanceId() == nodeId; });
        this.RemoveChild(removedNode);
        int RemovedAll = shape.Parts.RemoveAll(item => { return item.GetInstanceId() == nodeId; });
    }

}