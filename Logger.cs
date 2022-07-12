using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedDataGrouper
{
    /// <summary>
    /// Used to log events. Puts strings into a list, which will automatically update
    /// the given RichTextBox.
    /// </summary>
    internal class Logger
    {
        private List<string> _lines = new List<string>();

        private readonly RichTextBox _textbox;

        public Logger(RichTextBox textbox)
        {
            _textbox = textbox;
        }

        /// <summary>
        /// Writes the string line to the end of the lines in the RichTextBox.
        /// </summary>
        /// <param name="line"></param>
        public void Log(string line)
        {
            LogRaw(line);
            Refresh();
        }

        /// <summary>
        /// Writes the string lines to the end of the lines in the RickTextBox.
        /// </summary>
        /// <param name="lines"></param>
        public void Log(string[] lines)
        {
            foreach (string line in lines)
            {
                LogRaw(line);
            }
            Refresh();
        }

        /// <summary>
        /// Writes the line to the end of the lines. 
        /// Does not update the RichTextBox with the new line.
        /// </summary>
        /// <param name="line"></param>
        public void LogRaw(string line)
        {
            _lines.Add(line);
        }

        /// <summary>
        /// Deletes the last line from the logger, and updates
        /// the RichTextBox.
        /// </summary>
        public void DeleteLastLine()
        {
            if(_lines.Any())
            {
                _lines.RemoveAt(_lines.Count - 1);
                Refresh();
            }
        }

        /// <summary>
        /// Refreshes the RichTextBox with the current lines
        /// within the Logger.
        /// </summary>
        public void Refresh()
        {
            _textbox.Lines = _lines.ToArray();

            //scroll to bottom
            _textbox.SelectionStart = _textbox.Text.Length;
            _textbox.ScrollToCaret();
        }

        /// <summary>
        /// Clears the lines from the Logger, and updates the
        /// RichTextBox.
        /// </summary>
        public void Clear()
        {
            _lines.Clear();
            Refresh();
        }
    }
}
