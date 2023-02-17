using System;
using System.Collections.Generic;
using Godot;

public class Board2D: Node2D 
{
    private const int colCount = 10;
    private const int rowCount = 20;

    [Signal]
    private delegate void OnLinesRemoved(int linesRemoved);

    private Block[,] boardBlocks;

    private float marginHorizontal 
    {
        get 
        {
            float marginWidth = (GetViewport().Size.x - size.x + 30) * 0.5f;
            return marginWidth + SquareSize.x;
        }
    }

    private float marginVertical
    {
        get { return (GetViewport().Size.y - size.y) * 0.5f; }
    }

    private Color boardColor 
    {
        get { return Colors.DarkGray; }
    }

    private Vector2 size 
    {
        get { return new Vector2(180, 360); }
    }
    public Vector2 Size 
    {
        get { return size; }
    }

    public Vector2 SquareSize 
    {
        get { return new Vector2((size.x / colCount), (size.y / rowCount)); }
    }

    Board2D()
    {
        this.boardBlocks = new Block[colCount, rowCount];
    }

    public override void _Ready() 
    {
        this.PauseMode = PauseModeEnum.Process;
        for (int i = 0; i < colCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                this.boardBlocks[i, j] = new Block();
            }
        }
    }

    public override void _Draw() 
    {
        for (int c = 0; c < colCount+1; c++)
        {
            float positionX = marginHorizontal + (c * SquareSize.x) ;
            Vector2 start = new Vector2(positionX, marginVertical);
            Vector2 end   = new Vector2(positionX, marginVertical + size.y);

            this.DrawLine(start, end, boardColor, 0.5f);
        }

        for (int r = 0; r < rowCount+1; r++)
        {
            float positionY = marginVertical + (r * SquareSize.y);
            Vector2 start = new Vector2(marginHorizontal, positionY);
            Vector2 end   = new Vector2(size.x + marginHorizontal, positionY);

            this.DrawLine(start, end, boardColor, 0.5f);
        }
	}

    public override void _Input(InputEvent inputEvent)
	{
        if (inputEvent.IsActionPressed("check_tetris"))
        {
            List<List<BoardPoint>> blocksToRemove = GetBlocksToRemove();
            
            if (blocksToRemove.Count == 0) { /* printBoard(); */ return; }

            int linesRemoved = Tetris(blocksToRemove);
            ShiftDown(linesRemoved);
        }
    }

    public void ResetLocation(Piece2D piece) 
    {
        UpdateLocation(piece, false);
    }

    public void SetLocation(Piece2D piece)
    {
        UpdateLocation(piece, true);
    }

    public BoardPoint PointForNode(Node2D node) 
    {
        int marginX = (int)(marginHorizontal);

        int col = (int)Mathf.Floor((node.GlobalPosition.x - marginX) / (SquareSize.x ));
        int row = (int)Mathf.Floor(node.GlobalPosition.y / (SquareSize.y));

        return new BoardPoint(col, row);
    }

    public Vector2 PieceStartPosition(PieceShape shape)
    {
        Vector2 viewPortSize = GetViewport().Size;
        int marginX = (int)marginHorizontal;
        int marginY = (int)marginVertical;
        int x = (int)(marginX + (size.x * 0.5f) + (SquareSize.x * 2));
        int y = (int)(marginVertical + SquareSize.y + 1 - (SquareSize.x * 3));
        return new Vector2(x, y);
    }

    public bool CanMove(Piece2D piece, Vector2 direction)
    {
        List<BoardPoint> beforeMovement = new List<BoardPoint>();

        foreach(SquareNode part in piece.Shape.Parts)
        {
            if (part.GlobalPosition.y < 0 ||
                !part.IsCollidable) 
            {
                continue;
            }

            BoardPoint point = PointForNode(part);
            point.x += 1 * (int)direction.x;
            point.y += 1 * (int)direction.y;

            if (point.y >= rowCount - 1) 
            {
                return false;
            }

            if (point.x >= colCount) 
            {
                return false;
            }

            if (point.x < 0)
            {
                return false;
            }

            if (boardBlocks[point.x, point.y].isFilled == true &&
                !IsCollidingWithItself(point, piece.Shape.Parts))
            {
                return false;
            }
        }

        return true;
    }

    public void CheckTetris()
    {
        List<List<BoardPoint>> blocksToRemove = GetBlocksToRemove();
            
        if (blocksToRemove.Count == 0) { /* printBoard(); */ return; }

        int linesRemoved = Tetris(blocksToRemove);
        ShiftDown(linesRemoved);

        EmitSignal("OnLinesRemoved", linesRemoved);
    }

    private void UpdateLocation(Piece2D piece, bool isInLocation) 
    {
        foreach(SquareNode part in piece.Shape.Parts)
        {
            if (part.GlobalPosition.y < 0) { continue; }

            BoardPoint point = PointForNode(part);
            boardBlocks[point.x, point.y] = new Block();
            boardBlocks[point.x, point.y].nodeId = part.Id;
            boardBlocks[point.x, point.y].piece = piece;
            boardBlocks[point.x, point.y].part = part;
            boardBlocks[point.x, point.y].isFilled = isInLocation;
        }
    }

    private void ShiftDown(int removedLines)
    {
        float downAmmount = removedLines * SquareSize.y;
        for (int j = rowCount - 1; j >= 0; j--)
        {
            for (int i = colCount-1; i >= 0; i--)
            {
                if (!boardBlocks[i, j].isFilled) { continue; }
                SquareNode part = boardBlocks[i, j].part;
                boardBlocks[i, j].isFilled = false;
                part.GlobalTransform = part.GlobalTransform.Translated(downAmmount * Vector2.Down);

                Piece2D piece = boardBlocks[i, j].piece;
                if (piece == null) { continue; }
                
                this.ResetLocation(piece);
                this.SetLocation(piece);
            }
        }
    }

    private int Tetris(List<List<BoardPoint>> blocksToRemove)
    {
        int linesRemoved = blocksToRemove.Count;

        for (int i = 0; i < blocksToRemove.Count; i++)
        {
            for (int j = 0; j < blocksToRemove[i].Count; j++)
            {
                BoardPoint point = blocksToRemove[i][j];
                Block block = boardBlocks[point.x, point.y];
                block.piece.RemoveNodeAt(block.nodeId);
                boardBlocks[point.x, point.y] = new Block();
            }
        }

        return linesRemoved;
    }

    private List<List<BoardPoint>> GetBlocksToRemove()
    {
        List<List<BoardPoint>> blocksToRemove = new List<List<BoardPoint>>();
        
        for (int j = rowCount - 1; j >= 0; j--)
        {
            List<BoardPoint> lineToRemove = new List<BoardPoint>();
            for (int i = 0; i < colCount; i++)
            {
                if (!this.boardBlocks[i, j].isFilled)
                {
                    lineToRemove.Clear();
                    break;
                }
                BoardPoint point = new BoardPoint(i, j);
                lineToRemove.Add(point);
            }
            if (lineToRemove.Count == colCount)
            {
                blocksToRemove.Add(lineToRemove);
            }
        }
        return blocksToRemove;
    }

    private bool IsCollidingWithItself(BoardPoint partPoint, List<Node2D> parts)
    {
        foreach (var excludePart in parts)
        {
            BoardPoint pointPreviousMovement = PointForNode(excludePart);
            if (pointPreviousMovement.x == partPoint.x && 
                pointPreviousMovement.y == partPoint.y) 
            {
                return true;
            }
        }
        return false;
    }

    public void PrintBoard() 
    {
        for(int j = 0; j < rowCount; j++)
        {
            string row = "r: " + j + " ";
            if (j < 10)
            {
                row += " ";
            }
            for(int i = 0; i < colCount; i++)
            {
                if (boardBlocks[i, j].isFilled)
                {
                    row += " " + boardBlocks[i, j].isFilled.ToString() + " ";
                }
                else 
                {
                    row += " " + boardBlocks[i, j].isFilled.ToString();
                }
                
            }
            GD.Print(row);
        }
        
    }
}