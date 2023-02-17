
public class Block
{
    public bool isFilled;
    public ulong nodeId;
    public Piece2D piece;

    public SquareNode part;

    public Block() 
    {
        this.isFilled = false;
    }
}