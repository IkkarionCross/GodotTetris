using Godot;

public class BoardPoint
{
    public int x, y;

    public BoardPoint(int x, int y) 
    {
        this.x = x;
        this.y = y;
    }
}


public class Board2D: Node2D 
{
    private Vector2 size 
    {
        get { return new Vector2(180, 360); }
    }
    public Vector2 Size 
    {
        get { return new Vector2(180, 372); }
    }
    private const int colCount = 10;
    private const int rowCount = 20;

    public Vector2 squareSize 
    {
        get { return new Vector2((size.x / colCount), (size.y / rowCount)); }
    }
    
    private bool[,] boardPieces;

    Board2D()
    {
        this.boardPieces = new bool[colCount, rowCount];
    }

    public void printBoard() 
    {
        for(int j = 0; j < rowCount; j++)
        {
            string row = "";
            for(int i = 0; i < colCount; i++)
            {
                row += " " + boardPieces[i, j].ToString();
            }
            GD.Print(row);
        }
        
    }

    private void updateLocation(Piece2D piece, bool isInLocation) 
    {
        foreach(SquareNode part in piece.Shape.Parts)
        {
             if (part.GlobalPosition.y < 0) 
            {
                continue;
            }

            BoardPoint point = pointForNode(part);
            boardPieces[point.x, point.y] = isInLocation;
        }
    }

    public void resetLocation(Piece2D piece) 
    {
        updateLocation(piece, false);
    }

    public void setLocation(Piece2D piece)
    {
        updateLocation(piece, true);
    }

    public BoardPoint pointForNode(Node2D node) 
    {
        Vector2 viewPortSize = GetViewport().Size;
        int marginX = (int)((viewPortSize.x - size.x) * 0.5f);
        int marginY = (int)((viewPortSize.x - size.x) * 0.5f);

        int col = (int)Mathf.Floor((node.GlobalPosition.x - marginX) / (squareSize.x ));
        int row = (int)Mathf.Floor(node.GlobalPosition.y / (squareSize.y));

        return new BoardPoint(col, row);
    }

    public bool CanMove(Piece2D piece)
    {
        foreach(SquareNode part in piece.Shape.Parts)
        {
            if (part.GlobalPosition.y < 0) 
            {
                continue;
            }

            if (!part.IsCollidable)
            {
                continue;
            }
            
            BoardPoint point = pointForNode(part);
            point.y += 1;

            if (point.y >= rowCount - 1) 
            {
                printBoard();
                return false; 
            }
        }

        return true;
    }

    public Vector2 pieceStartPosition(PieceShape shape)
    {
        Vector2 viewPortSize = GetViewport().Size;
        int marginX = (int)((viewPortSize.x - size.x) * 0.5f);
        int x = (int)(marginX + (size.x * 0.5f) - shape.Size.x + (squareSize.x * 2) + 1);
        int y = (int)(viewPortSize.y - size.y + (squareSize.y * 0.5f) + 1);
        return new Vector2(x, y);
    }

    public override void _Ready()
	{
    }

    public override void _Draw() {
        Vector2 viewPortSize = GetViewport().Size;

        int marginWidth = (int)((viewPortSize.x - size.x) * 0.5f);
        int marginHeight = (int)((viewPortSize.y - size.y) * 0.5f);

        for (int c = 0; c <= colCount; c++)
        {
            float positionX = marginWidth + (c * squareSize.x);
            Vector2 start = new Vector2(positionX, marginHeight);
            Vector2 end   = new Vector2(positionX, 353);

            this.DrawLine(start, end, new Color(1,0,0,1), 0.5f);
        }

        for (int r = 0; r < rowCount; r++)
        {
            float positionY = marginHeight + (r * squareSize.y);
            Vector2 start = new Vector2(marginWidth, positionY);
            Vector2 end   = new Vector2(218, positionY);

            this.DrawLine(start, end, new Color(1,0,0,1), 0.5f);
        }
	}
}