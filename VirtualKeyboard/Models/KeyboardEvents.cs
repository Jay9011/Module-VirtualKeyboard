using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualKeyboard.Models
{
    public class KeyPressEventArgs : EventArgs
    {
        public string Text { get; set; }
        public bool IsComposing { get; set; }
        public bool IsCommitted { get; set; }

        public KeyPressEventArgs(string text, bool isComposing, bool isCommitted)
        {
            Text = text;
            IsComposing = isComposing;
            IsCommitted = isCommitted;
        }
    }

    public class CompositionEventArgs : EventArgs
    {
        public string Text { get; set; }

        public CompositionEventArgs(string text)
        {
            Text = text;
        }
    }
}
