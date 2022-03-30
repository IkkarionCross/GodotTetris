using Godot;

public class Piece2D: Node2D
{
    private float time = 0.0f;
    const float TIME_TO_MOVE = 0.2f;

	public Vector2 velocity 
    {
        get 
        {
            return new Vector2(0, SquareSize.y);
        }
    }

    private bool isMoving;
    public bool IsMoving {
        get { return isMoving; }
    }

	private PieceShape shape;
    public PieceShape Shape
    {
        get {return shape;}
    }

	private PieceType _type;
	public PieceType Type {
		get { return _type; }
	}

    public Vector2 SquareSize {
        get {return shape.SquareSize;}
    } 

    public Vector2 startPosition;

    public Board2D Board;
	
	public Piece2D(PieceType type, PieceShape shape) 
    {
		this._type = type;
		this.shape = shape;
        this.isMoving = true;

        shape.drawIn(this);
	}

    public void stopMoving() {
        this.isMoving = false;
    }

    public override void _Ready()
	{
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
            this.GlobalTransform = GlobalTransform.Translated(velocity);

            Board.setLocation(this);

            GD.Print("Board:");
            Board.printBoard();
            

            Update();
        }
    }

    public override void _Draw() 
    {
		shape.drawIn(this);
	}

    public void adjustPositionForCollisionWithFloor(Vector2 viewPortSize) 
    {
        Position = new Vector2(Position.x, Position.y - (viewPortSize.y - Position.y));
    }

    public bool hasCollidedWithFloor(Vector2 viewPortSize) 
    {
        return Position.y + (shape.Size.y * 0.5f) >= viewPortSize.y;
    }

    public bool hasCollidedWithSquare(SquareNode node) 
    {
        return false;
    }



}