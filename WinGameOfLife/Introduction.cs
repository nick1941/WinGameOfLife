using System;
// andrewjivoin
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WinGameOfLife
{
    public partial class Introduction : Form
    {
        public bool noShowIntro;
        public string pathIntro = @"..\..\..\Introduction.xml";

        /// <summary>
        /// Constructor performs standard call to InitializeComponent
        /// and gets the current value of the flag that determines
        /// whether or not to show the introduction screen.
        /// </summary>
        public Introduction()
        {
            InitializeComponent();

            noShowIntro = GetShowIntroFlag ();

        } // Constructor

        /// <summary>
        /// Get the flag that indicates whether or not to show
        /// the Introduction screen.
        /// </summary>
        /// <returns>true to not show intro; otherwise, false</returns>
        private bool GetShowIntroFlag ()
        {
            using (var reader = XmlReader.Create (pathIntro))
            {
                reader.MoveToContent ();

                while (reader.Read ())
                {
                    if (reader.NodeType == XmlNodeType.Element &&
                        reader.Name == "noShowIntro")
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
                                            ($"Unknown noShowIntro value {reader.Value.ToLower ()}"));
                            } // switch
                        } // if text node
                    } // if 'noShowIntro' element node
                } // while
            } // using
            return false;

        } // method GetShowIntroFlag

        /// <summary>
        /// Save the current value of the 'noShowIntro' flag
        /// in the XML file.
        /// </summary>
        /// <param name="flag"></param>
        public void SaveShowIntroFlag (bool flag)
        {
            var doc = new XmlDocument ();

            doc.Load (pathIntro);

            XmlNode root = doc.DocumentElement;

            root.SelectSingleNode ("noShowIntro").InnerText = flag.ToString ();
            doc.Save (pathIntro);

        } // method SaveShowIntroFlag

        #region EventHandlers
        /// <summary>
        /// 'Begin' button click event handler
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void ButtonBegin_Click (object sender, EventArgs e)
        {
            Close ();

        } // event handler ButtonBegin_Click

        /// <summary>
        /// Event handler for checkbox to show/not show Intro
        /// on next startup.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void CheckBoxNoIntro_CheckedChanged (object sender, EventArgs e)
        {
            noShowIntro = checkBoxNoIntro.Checked ? true : false;

            SaveShowIntroFlag (noShowIntro);

        } // event handler CheckBoxNoIntro_CheckedChanged
        #endregion EventHandlers

    } // class Introduction
} // namespace WinGameOfLife
