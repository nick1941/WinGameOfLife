using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// A Windows Forms (.NET 5.0 Controls) Implementation
/// of Conway's Game of Life
/// </summary>
namespace WinGameOfLife
{
    /// <summary>
    /// The Game of Life form consists of a menu strip,
    /// a grid of cells that can be clicked to paint them black or white,
    /// and a status strip (TODO: use yet to be determined)
    /// </summary>
    public partial class FormGameOfLife : Form
    {
        #region FieldVariables
        private bool           gridDrawn;   // Initially false, set true when the grid is drawn
        private bool [,]       grid;        // Array of grid elements
        private Game           game;
        private Introduction   introForm;
        private List<bool [,]> grids = new List<bool [,]> ();
        private Pen            blackPen;   // Pen used to draw grid elements
        private Rectangle [,]  cells;      // Array of grid cells (painted black or white)
        private SolidBrush     brush;      // Brush used to paint grid cells

        public int generation;             // Keeps track of current lifetime generation

        //public System.Timers.Timer generationTimer = new System.Timers.Timer (1000);
        #endregion FieldVariables

        #region Properties
        private int Columns { get; set; }   // Get/set grid columns
        private int Rows    { get; set; }   // Get/set grid rows
        #endregion Properties

        #region Constants
        private const int cellWidth = 10;   // Side size of each grid square
        #endregion Constants

        /// <summary>
        /// The form's constructor
        /// </summary>
        public FormGameOfLife ()
        {
            InitializeComponent ();
        }

        #region Methods
        /// <summary>
        /// Draw the Game of Life grid.
        /// </summary>
        /// <param name="e">Gives access to PaintEventArgs information</param>
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

            gridDrawn = true;

        } // method SetupGrid

        /// <summary>
        /// Set up the game grid and instantiate the Game object.
        /// </summary>
        private void SetupGame ()
        {
            grid       = new bool [Columns, Rows];
            game       = new Game (Columns, Rows, cellWidth, grid);

        } // method SetupGame

        // TODO: Implement MenuStrip File, Operation, Options
        #endregion Methods

        #region EventHandlers
        private void MenuItemClick (object sender, EventArgs e)
        {
            switch (sender.ToString ())
            {
                case "&Start":
                    generation = 0;

                    generationTimer.Start ();

                    startToolStripMenuItem.Enabled = false;
                    stopToolStripMenuItem.Enabled  = true;
                    pauseToolStripMenuItem.Enabled = true;
                    break;
                case "Sto&p":
                    generationTimer.Stop ();
                    generationTimer.Dispose ();

                    startToolStripMenuItem.Enabled = true;
                    stopToolStripMenuItem.Enabled  = false;
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
            }
        }

        /// <summary>
        /// Handle the grid panel Paint event.
        /// </summary>
        /// <param name="sender">Sender of this event</param>
        /// <param name="e">Information needed to handle the event</param>
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

        /// <summary>
        /// Before the main form is loaded, display the introduction
        /// dialog box.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs information</param>
        private void FormGameOfLife_Load (object sender, EventArgs e)
        {
            introForm = new Introduction ();

            if (!introForm.noShowIntro)
                introForm.ShowDialog ();

            startToolStripMenuItem.Click  += MenuItemClick;
            pauseToolStripMenuItem.Click  += MenuItemClick;
            resumeToolStripMenuItem.Click += MenuItemClick;
            stopToolStripMenuItem.Click   += MenuItemClick;

        } // event handler FormGameOfLife_Load

        /// <summary>
        /// Handle a mouse click in the panel grid.
        /// If the grid element under the mouse is empty, fill it.
        /// If the grid element under the mouse is filled, empty.
        /// Update the grid array (true if being filled; false, otherwise)
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Information about the event</param>
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
            Graphics cellGraphics = panelGrid.CreateGraphics ();
            Rectangle rect = cells [x, y];

            // Call the Panel's Paint handler to paint the cell.
            PanelGrid_Paint (this,new PaintEventArgs (cellGraphics, rect));
            brush.Dispose   ();
            cellGraphics.Dispose ();
            
        } // event handler PanelGrid_MouseClick

        /// <summary>
        /// Options\ShowIntro ToolStripMenuItem event handler
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Information about the event</param>
        private void ShowIntroToolStripMenuItem_Click (object sender, EventArgs e)
        {
            // Show the introductory screen on the next application startup
            introForm.SaveShowIntroFlag (false);
        }

        /// <summary>
        /// Generation timer tick event handler
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Information about the event</param>
        private void GenerationTimer_Tick (object sender, EventArgs e)
        {
            generation++;

            grid = game.NewGeneration ();

            UpdateGrid ();
            //Application.DoEvents ();
        }
        #endregion EventHandlers

        private void UpdateGrid ()
        {
            var brushBlack = new SolidBrush (Color.Black);
            var brushWhite = new SolidBrush (SystemColors.Control);

            Graphics cellGraphics = panelGrid.CreateGraphics ();

            for (int i = 0; i < grid.GetUpperBound (0); i++)
                for (int j = 0; j < grid.GetUpperBound (1); j++)
                {
                    if (grid [i,j])
                        brush = brushBlack;
                    else
                        brush = brushWhite;

                    Rectangle rect = cells [i, j];

                    PanelGrid_Paint (this, new PaintEventArgs (cellGraphics, rect));
                }
            brushBlack.Dispose ();
            brushWhite.Dispose ();
        }
    } // class FormGameOfLife
} // namespace WinGameOfLife
