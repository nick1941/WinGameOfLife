
namespace WinGameOfLife;

/// <summary>
/// Encapsulates the data and operational aspects of the game.
/// </summary>
class Game
{
    private bool [,] myGrid;
    private int      myRows, myColumns;

    /// <summary>
    /// Creates a new instance of the Game class.
    /// </summary>
    /// <param name="columns">int</param>
    /// <para>Number of columns</para>
    /// <param name="rows">int</param>
    /// <para>Number of rows</para>
    /// <param name="grid">bool [,]</param>
    /// <para>Boolean 2-dimensional array</para>
    public Game (int columns, int rows, bool [,] grid)
    {
        myRows      = rows;
        myColumns   = columns;
        myGrid      = grid;

    }

    // Check the status of the cells surrounding the specified cell location.
    // Parameters:
    //  column int: Current cell column
    //  row    int: Current cell row
    // <returns>The count of surrounding cells that are 'alive'</returns>
    private int CheckStatus (int column, int row)
    {
        int count = 0;

        // if upper-left is alive...
        if ((column - 1 >= 0 && row - 1 > 0) &&
            myGrid [column - 1, row - 1] == true)
            count++;

        // if upper is alive...
        if ((column - 1 >= 0) && myGrid [column - 1, row] == true)
            count++;

        // if upper-right is alive...
        if ((column - 1 >= 0 && row + 1 < myRows) &&
            myGrid [column - 1, row + 1] == true)
            count++;

        // if left is alive...
        if ((row - 1 >= 0) && myGrid [column, row - 1] == true)
            count++;

        // if right is alive...
        if ((row + 1 < myRows) && myGrid [column, row + 1] == true)
            count++;

        // if lower-left is alive...
        if ((column + 1 < myColumns && row - 1 >= 0) &&
            myGrid [column + 1, row - 1] == true)
            count++;

        // if lower is alive...
        if ((column + 1 < myColumns) && myGrid [column + 1, row] == true)
            count++;

        // if lower-right is alive...
        if ((column + 1 < myColumns &&
            row + 1 < myRows) &&
            myGrid [column + 1, row + 1] == true)
            count++;

        return count;

    } // method CheckStatus

    /// <summary>
    /// Set up the grid for the next generation of Life.
    /// </summary>
    public bool [,] NewGeneration ()
    {
        // Set up a new working grid
        bool [,] newGrid = new bool [myColumns, myRows];

        // Scan the current grid.  For each cell in the current grid,
        // get a count of the live cells surrounding it
        for (int column = 0; column < myGrid.GetLength (0); column++)
        {
            for (int row = 0; row < myGrid.GetLength (1); row++)
            {
                int count = CheckStatus (column, row);

                // Based on the live cells surrounding the current cell
                // use the Rules of Life to determine whether the current
                // cell lives or dies
                // If the cell is alive...
                if (myGrid [column, row])
                {
                    // ... and surrounded by 2 or 3 live cells,
                    // it remains alive
                    if (count == 2 || count == 3)
                        newGrid [column, row] = true;

                    // ... and surrounded by less than 2
                    // or greater than three live cells, it dies
                    if (count < 2 || count > 3)
                        newGrid [column, row] = false;
                }
                else // If the cell is dead...
                {
                    // ... and surrounded by exactly three live cells,
                    // it comes to life
                    if (count == 3)
                        newGrid [column, row] = true;
                }
            }
        }
        // Update my grid
        myGrid = newGrid;

        // And return the new grid
        return newGrid;

    } // method NewGeneration

} // class Game
