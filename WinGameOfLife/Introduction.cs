using System;
// andrewjivoin
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace WinGameOfLife;

/// <summary>
/// This class provides a means to allow the user
/// to determine whether or not to display
/// the game introduction screen.
/// </summary>
public partial class Introduction : Form
{
    /// <summary>
    /// If set to true, causes the introductory screen
    /// to not be shown.
    /// </summary>
    public bool   NoShowIntro;

    /// <summary>
    /// Specifies a location for the XML file that stores
    /// a flag indicating whether or not to show the intro screen.
    /// </summary>
    public string PathIntro = @"..\..\..\Introduction.xml";

    /// <summary>
    /// Constructor performs standard call to InitializeComponent
    /// and gets the current value of the flag that determines
    /// whether or not to show the introduction screen.
    /// </summary>
    public Introduction()
    {
        InitializeComponent();

        NoShowIntro = GetShowIntroFlag ();

    } // Constructor

    // Get the flag that indicates whether or not to show
    // the Introduction screen.
    // Returns:
    //   true to not show intro; otherwise, false
    private bool GetShowIntroFlag ()
    {
        using (var reader = XmlReader.Create (PathIntro))
        {
            reader.MoveToContent ();

            while (reader.Read ())
            {
                if (reader.NodeType == XmlNodeType.Element &&
                    reader.Name == "NoShowIntro")
                {
                    _ = reader.Read ();

                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        switch (reader.Value.ToLower ())
                        {
                            case "false":
                                return false;
                            case "true":
                                return true;
                            default:
                                throw new FileFormatException
                                    (string.Format
                                        ($"Unknown NoShowIntro value {reader.Value.ToLower ()}"));
                        } // switch
                    } // if text node
                } // if 'NoShowIntro' element node
            } // while
        } // using
        return false;

    } // method GetShowIntroFlag

    /// <summary>
    /// Save the current value of the 'NoShowIntro' flag
    /// in the XML file.
    /// </summary>
    /// <param name="flag">bool</param>
    /// <para>Set true to not show intro screen; otherwise, false</para>
    public void SaveShowIntroFlag (bool flag)
    {
        var doc = new XmlDocument ();

        doc.Load (PathIntro);

        XmlNode root = doc.DocumentElement;

        root.SelectSingleNode ("NoShowIntro").InnerText = flag.ToString ();
        doc.Save (PathIntro);

    } // method SaveShowIntroFlag

    #region EventHandlers
    // 'Begin' button click event handler
    // Parameters:
    //  sender object:      Source of event
    //  e      EventArgs:   Event arguments
    private void ButtonBegin_Click (object sender, EventArgs e)
    {
        Close ();

    } // event handler ButtonBegin_Click

    // Event handler for checkbox to show/not show Intro
    // on next startup.
    // Parameters:
    //  sender object:      Source of event
    //  e      EventArgs:   Event arguments
    private void CheckBoxNoIntro_CheckedChanged (object sender, EventArgs e)
    {
        NoShowIntro = checkBoxNoIntro.Checked ? true : false;

        SaveShowIntroFlag (NoShowIntro);

    } // event handler CheckBoxNoIntro_CheckedChanged
    #endregion EventHandlers

} // class Introduction
