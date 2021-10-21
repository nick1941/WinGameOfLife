# WinGameOfLife
This is an interactive demonstration of how Conway's Rules of Life affect the pattern of **cells** of living entities to cause certain cells to **die**, other **living cells** to remain **alive**', and currently **dead** cells to come to life.

The first time the application is run, it displays an introductory explanation dialog box.  This is a brief introduction to the game that shows how to select/deselect cells, and how to start the game.  It also displays the Rules of Life that guide the demonstration.

Before pressing **Begin**, the user can prevent this introductory text from being displayed in future invocations of the application by *checking* the CheckBox at the bottom of the Introductory dialog box.

The user displays the initial grid of cells by clicking the **Begin** button.

After clicking on **Begin**, the main form is displayed with a grid of empty cells.  The user makes use of the mouse to select an initial pattern of cells to start out **alive** to see what happens to them for each generation.  To start the generation time, the user selects *Operation > Start*.  The application will then follow Conway's Rules of Life to kill off cells and generate live cells based on the presence or absence of each cell's neighbor.

**NOTE**: Selecting only one or two cells will cause all cells to die as soon as the generation timer is started, and no new cells will be generated.  You must select at least 3 consecutive cells to see any continuing activity.  For example, selecting three horizontal cells, such as

1)                                                   ▄▄▄

will result in a second generation that looks like

                                                     ▐
2)                                                   ▐
                                                     ▐

The next generation will look like (1) above.  The following generation will look like (2) above), and so on.  This is known as a repeater pattern.  Other initial patterns may die out, eventually reach a repeating pattern, or go on producing new patterns without ever repeating.  Experimenting with different patterns is part of the fun of the game.

To stop producing new generations, select *Operation > Stop*.  The user may also temporarily stop a game by selecting *Operation > Pause* and then continue again by selecting *Operation > Resume*.

If you forget the rules, you may select the *Options > ShowIntroOnStartup* menu item to again display the introductory dialog box the next time the application is started.

There is still much to be done with this application.  Some planned features are:
1. A way to clear the grid to start with a new pattern.
2. A way to save a pattern to a file and reload that pattern back into the grid.
3. Provide special preset game patterns of interest.
4. Add detailed instructions available from the *Help* menu.

 													 
