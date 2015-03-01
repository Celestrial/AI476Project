using System.Collections;

public abstract class PlayerManager  {

	public class Move{
		protected int x;
		protected int y;
	}

	char color;
	//bool AI; 
	Move move;

	public PlayerManager(char color)
	{
		this.color = color;
	}

	public abstract  Move getMove ();

}
