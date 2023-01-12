using System.Drawing;

namespace WinGameOfLife;

/// <summary>
/// Defines a Life figure to be saved to a file
/// or loaded from a file.
/// </summary>
public class Figure
{
    /// <summary>
    /// Get or set the subgrid to be saved or loaded.
    /// </summary>
    public bool [,] Subgrid { get; set; }
    /// <summary>
    /// Get or set the upper left point of the subgrid.
    /// </summary>
    public Point UpperLeft  { get; set; }
    /// <summary>
    /// Get or set the lower right point of the subgrid.
    /// </summary>
    public Point LowerRight { get; set; }

    /// <summary>
    /// Initializes a new instance of the Figure class.
    /// </summary>
    /// <param name="upperLeft">Point</param>
    /// <para>Defines the upper left corner of the subgrid</para>
    /// <param name="lowerRight">Point</param>
    /// <para>Defines the lower right corner of the subgrid</para>
    /// <param name="grid">bool[,]</param>
    /// <para>An array of boolean values defining the Figure.</para>
    public Figure (Point upperLeft, Point lowerRight, bool [,] grid)
    {
        UpperLeft  = new Point (upperLeft.X, upperLeft.Y);
        LowerRight = new Point (lowerRight.X, lowerRight.Y);

        for (int i = UpperLeft.X; i <= LowerRight.X; i++)
        {
            for (int j = UpperLeft.Y; j <= LowerRight.Y; j++)
                Subgrid [i, j] = grid [i, j];
        }
    }

    /// <summary>
    /// Save this Figure to a file.
    /// </summary>
    /// <param name="path">string</param>
    /// <para>Full path name of the file.</para>
    public void Save (string path)
    {

    }

    /// <summary>
    /// Load a Figure from a file.
    /// </summary>
    /// <param name="path">string</param>
    /// <para>Full path name of the file.</para>
    public void Load (string path)
    {

    }

} // class Figure
