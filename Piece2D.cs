using Godot;

public class Piece2D: Node2D
{
    private float time = 0.0f;
    const float TIME_TO_MOVE = 1.0f;

	private Vector2 velocity = new Vector2(0, 10);

    private bool isMoving;
    public bool IsMoving {
        get { return isMoving; }
    }

	private PieceShape shape;

	private PieceType _type;
	public PieceType Type {
		get { return _type; }
	}

    public Vector2 SquareSize {
        get {return shape.Size;}
    } 
	
	public Piece2D(PieceType type, PieceShape shape) {
		this._type = type;
		this.shape = shape;
        this.isMoving = true;
	}

    public void stopMoving() {
        this.isMoving = false;
    }

    public override void _Ready()
	{
        Vector2 viewPortSize = GetViewportRect().Size;
		Vector2 translation = new Vector2((viewPortSize.x * 0.5f) - (SquareSize.x * 0.5f), (viewPortSize.y * 0.5f) - (SquareSize.y * 0.5f));
		this.Transform = new Transform2D(Vector2.Right, Vector2.Down, translation);
	}

    public override void _Process(float delta) {
        if (!isMoving) { return; }

        time += delta;
        if (time >= TIME_TO_MOVE) {
            time = 0;
			this.GlobalTransform = GlobalTransform.Translated(velocity);
            Update();
        }
    }

    public override void _Draw() {
		shape.drawIn(this);
	}

    public void adjustPositionForCollisionWithFloor(Vector2 viewPortSize) 
    {
        Position = new Vector2(Position.x, Position.y - (viewPortSize.y - Position.y));
    }

    public bool hasCollidedWithFloor(Vector2 viewPortSize) 
    {
        return Position.y + (SquareSize.y * 0.5f) >= viewPortSize.y;
    }

    public bool hasCollidedWithSquare(SquareNode node) 
    {
        return false;
    }



}