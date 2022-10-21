using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

// A Windows Forms Implementation of Conway's Game of Life
namespace WinGameOfLife;

/// <summary>
/// The Game of Life form consists of a menu strip,
/// a grid of cells that can be clicked to paint them black or white,
/// and a status strip (TODO: use yet to be determined)
/// </summary>
public partial class FormGameOfLife : Form
{
    #region FieldVariables
    private bool           gridDrawn;  // Initially false, set true when the grid is drawn
    private bool [,]       grid;       // Array of grid elements
    private Game           game;       // Game of Life object
    private Introduction   introForm;  // Introductory form
    private Pen            blackPen;   // Pen used to draw grid elements
    private Rectangle [,]  cells;      // Array of grid cells (painted black or white)
    private SolidBrush     brush;      // Brush used to paint grid cells

    /// <summary>
    /// Keeps track of current lifetime generation
    /// </summary>
    public int Generation;

    #endregion FieldVariables

    #region Properties
    private int Columns { get; set; }   // Get/set grid columns
    private int Rows    { get; set; }   // Get/set grid rows
    #endregion Properties

    #region Constants
    private const int cellWidth = 10;   // Side size of each grid square
    #endregion Constants

    /// <summary>
    /// Instantiate a new instance of the FormGameOfLife class.
    /// </summary>
    public FormGameOfLife ()
    {
        InitializeComponent ();
    }

    #region Methods
    // Draw the Game of Life grid.
    // Parameters:
    //  e   PaintEventArgs: Gives access to PaintEventArgs information
    [SupportedOSPlatform ("Windows")]
    private void SetupGrid (PaintEventArgs e)
    {
        // Compute and save the number of columns and rows
        Columns = panelGrid.Width / cellWidth;
        Rows    = panelGrid.Height / cellWidth;

        // Instantiate a black pen to draw the grid
        blackPen = new Pen (Color.Black,1);

        // Create the array of Rectangle's to hold grid squares
        cells = new Rectangle [Columns, Rows];

        // Create and draw the grid squares
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                cells [i,j] = new Rectangle (i * cellWidth, 
                                             j * cellWidth, 
                                             cellWidth, 
                                             cellWidth);

                e.Graphics.DrawRectangle (blackPen, cells [i, j]);
            }
        }
        blackPen.Dispose ();

        // Mark the grid as drawn so as not to call SetupGrid again.
        gridDrawn = true;

    } // method SetupGrid

    // Set up the Boolean game grid and instantiate the Game object.
    private void SetupGame ()
    {
        grid = new bool [Columns, Rows];
        game = new Game (Columns, Rows, grid);

    } // method SetupGame

    // Updates the grid elements (filled or empty).
    [SupportedOSPlatform ("Windows")]
    private void UpdateGrid ()
    {
        // Create the brushes
        var brushBlack = new SolidBrush (Color.Black);
        var brushWhite = new SolidBrush (SystemColors.Control);

        // Get the graphics element
        Graphics cellGraphics = panelGrid.CreateGraphics ();

        // Update each grid element in turn
        for (int i = 0; i < grid.GetUpperBound (0); i++)
            for (int j = 0; j < grid.GetUpperBound (1); j++)
            {
                if (grid [i, j])
                    brush = brushBlack;
                else
                    brush = brushWhite;

                Rectangle rect = cells [i, j];

                PanelGrid_Paint (this, new PaintEventArgs (cellGraphics, rect));
            }
        brushBlack.Dispose ();
        brushWhite.Dispose ();

    } // method UpdateGrid

    // Clear the grid (set all elements to empty).
    [SupportedOSPlatform ("Windows")]
    private void ClearGrid ()
    {
        for (int i = 0; i < grid.GetUpperBound (0); i++)
            for (int j = 0; j < grid.GetUpperBound (1); j++)
                grid [i, j] = false;

        UpdateGrid ();
    }
    #endregion Methods

    #region EventHandlers
    // Load form event handler determines what to initialize
    // when the form is first loaded.
    // Parameters:
    //  sender object:      Source of event
    //  e      EventArgs:   Event arguments
    private void FormGameOfLife_Load (object sender, EventArgs e)
    {
        // Create the introductory form.
        introForm = new Introduction ();

        //  If display of the introductory form has not been disabled,
        //  show the dialog box.
        if (!introForm.NoShowIntro)
            introForm.ShowDialog ();

        // Attach the menu click event handlers for the Operations menu.
        startToolStripMenuItem.Click     += MenuItemClick;
        pauseToolStripMenuItem.Click     += MenuItemClick;
        resumeToolStripMenuItem.Click    += MenuItemClick;
        stopToolStripMenuItem.Click      += MenuItemClick;
        clearGridToolStripMenuItem.Click += MenuItemClick;

    } // event handler FormGameOfLife_Load

    // Event handler for clicking menu items.
    // Parameters:
    //  sender object:      Source of event
    //  e      EventArgs:   Event arguments
    [SupportedOSPlatform ("Windows")]
    private void MenuItemClick (object sender, EventArgs e)
    {
        switch (sender.ToString ())
        {
            case "&Start":
                Generation = 0;

                generationTimer.Start ();

                startToolStripMenuItem.Enabled     = false;
                stopToolStripMenuItem.Enabled      = true;
                pauseToolStripMenuItem.Enabled     = true;
                clearGridToolStripMenuItem.Enabled = false;
                break;
            case "Sto&p":
                generationTimer.Stop ();
                generationTimer.Dispose ();

                startToolStripMenuItem.Enabled     = true;
                stopToolStripMenuItem.Enabled      = false;
                clearGridToolStripMenuItem.Enabled = true;
                break;
            case "&Pause":
                generationTimer.Stop ();

                pauseToolStripMenuItem.Enabled  = false;
                resumeToolStripMenuItem.Enabled = true;
                break;
            case "&Resume":
                generationTimer.Start ();

                resumeToolStripMenuItem.Enabled = false;
                pauseToolStripMenuItem.Enabled  = true;
                break;
            case "&Clear Grid":
                ClearGrid ();
                break;
        } // switch
    } // event handler MenuItemClick

    // Handle the grid panel Paint event.
    // Parameters:
    //  sender object:          Source of event
    //  e      PaintEventArgs:  Event arguments
    [SupportedOSPlatform ("Windows")]
    private void PanelGrid_Paint (object sender, PaintEventArgs e)
    {
        // If the grid has already been drawn, then
        // we only need to handle filling the grid square
        // with black (create a life entity) or white (kill a life entity)
        if (gridDrawn)
        {
            blackPen = new Pen (Color.Black,1);

            e.Graphics.FillRectangle (brush,    e.ClipRectangle);

            // After filling the rectangle (especially with white),
            // we need to redraw the rectangle, some of whose sides
            // may have been overwritten by the brush.
            e.Graphics.DrawRectangle (blackPen, e.ClipRectangle);
            blackPen.Dispose ();
        }
        // If the grid has not yet been drawn (beginning of game)...
        else
        {
            //  Set up the grid, mark it as drawn,
            //  and set up the game to be played.
            SetupGrid (e);
            SetupGame ();
        }
    } // event handler PanelGrid_Paint

    // Handle a mouse click in the panel grid.
    // If the grid element under the mouse is empty, fill it.
    // If the grid element under the mouse is filled, empty it.
    // Update the grid array (true if being filled; false, otherwise)
    // Parameters:
    //  sender object:          Source of event
    //  e      MouseEventArgs:  Event arguments
    [SupportedOSPlatform ("Windows")]
    private void PanelGrid_MouseClick (object sender, MouseEventArgs e)
    {
        // Compute the grid column (x) and row (y)
        int x = e.X/cellWidth;
        int y = e.Y/cellWidth;

        // If the grid element is currently filled,
        // set the brush to clear it.
        if (grid [x, y] == true)
        {
            brush = new SolidBrush (SystemColors.Control);
        }
        // If the grid element is currently empty,
        // set the brush to fill it.
        else
        {
            brush = new SolidBrush (Color.Black);
        }
        // Flip the state of the grid element
        grid [x, y] = !grid [x, y];

        // Create a Graphics object for the Panel and
        // get the rectangle that corresponds to the clicked cell.
        Graphics  cellGraphics = panelGrid.CreateGraphics ();
        Rectangle rect         = cells [x, y];

        // Call the Panel's Paint handler to paint the cell.
        PanelGrid_Paint      (this,new PaintEventArgs (cellGraphics, rect));
        brush.Dispose        ();
        cellGraphics.Dispose ();
        
    } // event handler PanelGrid_MouseClick

    // Options\ShowIntro ToolStripMenuItem event handler
    // Parameters:
    //  sender object:      Source of event
    //  e      EventArgs:   Event arguments
    private void ShowIntroToolStripMenuItem_Click (object sender, EventArgs e)
    {
        // Show the introductory screen on the next application startup
        // 'false' means do not hide the intro screen
        introForm.SaveShowIntroFlag (false);
    }

    // Generation timer tick event handler creates a new generation
    // and updates the grid.
    // Parameters:
    //  sender object:      Source of event
    //  e      EventArgs:   Event arguments
    [SupportedOSPlatform ("Windows")]
    private void GenerationTimer_Tick (object sender, EventArgs e)
    {
        // TODO: Not sure this really does anything yet!
        Generation++;

        grid = game.NewGeneration ();

        UpdateGrid ();
    }
    #endregion EventHandlers

} // class FormGameOfLife
