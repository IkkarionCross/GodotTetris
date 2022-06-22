using Godot;

public class Piece2D: Node2D
{
    private float time = 0.0f;
    private const float TIME_TO_MOVE = 1.0f;

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

	private PieceType _type;
	public PieceType Type {
		get { return _type; }
        set 
        {
            this._type = value;
        }
	}

    public Vector2 SquareSize {
        get {return shape.SquareSize;}
    } 

    public Vector2 startPosition;

    public Board2D Board;

    public Piece2D() { }

    public void stopMoving() 
    {
        this.isMoving = false;
    }

    public override void _Ready()
	{
        this.isMoving = true;
        shape.drawIn(this);
	}

    public override void _Process(float delta) 
    {
        if (!isMoving) { return; }

        time += delta;
        if (time >= TIME_TO_MOVE) {
            time = 0;

            if (!Board.CanMove(this))
            {
                isMoving = false;
                return;
            }

            Board.resetLocation(this);
            this.GlobalTransform = GlobalTransform.Translated(velocity * Vector2.Down);
            Board.setLocation(this);

            Update();
        }
    }

    public override void _Input(InputEvent inputEvent)
	{
        if (!isMoving) { return; }

		if (inputEvent.IsActionPressed("rotate_right"))
		{
            Board.resetLocation(this);
            shape.rotateRight();
            Board.setLocation(this);
		}
        else if (inputEvent.IsActionPressed("rotate_left"))
		{
            Board.resetLocation(this);
            shape.rotateLeft();
            Board.setLocation(this);
		}
        else if (inputEvent.IsActionPressed("move_right"))
        {
            Board.resetLocation(this);
            this.GlobalTransform = GlobalTransform.Translated(velocity * Vector2.Right);
            Board.setLocation(this);
        }
        else if (inputEvent.IsActionPressed("move_left"))
        {
            Board.resetLocation(this);
            this.GlobalTransform = GlobalTransform.Translated(velocity * Vector2.Left);
            Board.setLocation(this);
        }
	}

    public override void _Draw() 
    {
		shape.drawIn(this);
	}

}