using Godot;

public class Score: Label
{
    private int current;
    public int Current
    {
        get { return current; }
    }

    public Score() 
    { 
        current = 0;
    }

    public void OnLinesRemoved(int linesRemoved)
    {
        if (linesRemoved < 0)
        {
            return;
        }
        current += linesRemoved * 100;
        
        this.Text = current.ToString();
    }
}