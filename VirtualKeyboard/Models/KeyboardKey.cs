using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualKeyboard.Models
{
    public enum KeyboardLayoutType
    {
        Korean,
        English,
        Number
    }

    public class KeyboardKey
    {
        public string DisplayText { get; set; }
        public string Value { get; set; }
        public bool IsSpecialKey { get; set; }
        public double Width { get; set; } = 40;
    }
}
