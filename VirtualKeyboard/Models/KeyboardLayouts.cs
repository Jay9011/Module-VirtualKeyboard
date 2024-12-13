using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualKeyboard.Models
{
    public static class KeyboardLayouts
    {
        public static List<KeyboardKey> GetKoreanLayout()
        {
            return new List<KeyboardKey>
            {
                // 1열
                new KeyboardKey { DisplayText = "ㅂ", Value = "ㅂ" },
                new KeyboardKey { DisplayText = "ㅈ", Value = "ㅈ" },
                new KeyboardKey { DisplayText = "ㄷ", Value = "ㄷ" },
                new KeyboardKey { DisplayText = "ㄱ", Value = "ㄱ" },
                new KeyboardKey { DisplayText = "ㅅ", Value = "ㅅ" },

                new KeyboardKey { DisplayText = "ㅛ", Value = "ㅛ" },
                new KeyboardKey { DisplayText = "ㅕ", Value = "ㅕ" },
                new KeyboardKey { DisplayText = "ㅑ", Value = "ㅑ" },
                new KeyboardKey { DisplayText = "ㅐ", Value = "ㅐ" },
                new KeyboardKey { DisplayText = "ㅔ", Value = "ㅔ" },

                // 2열
                new KeyboardKey { DisplayText = "ㅁ", Value = "ㅁ" },
                new KeyboardKey { DisplayText = "ㄴ", Value = "ㄴ" },
                new KeyboardKey { DisplayText = "ㅇ", Value = "ㅇ" },
                new KeyboardKey { DisplayText = "ㄹ", Value = "ㄹ" },
                new KeyboardKey { DisplayText = "ㅎ", Value = "ㅎ" },

                new KeyboardKey { DisplayText = "ㅗ", Value = "ㅗ" },
                new KeyboardKey { DisplayText = "ㅓ", Value = "ㅓ" },
                new KeyboardKey { DisplayText = "ㅏ", Value = "ㅏ" },
                new KeyboardKey { DisplayText = "ㅣ", Value = "ㅣ" },

                // 3열
                new KeyboardKey { DisplayText = "ㅋ", Value = "ㅋ" },
                new KeyboardKey { DisplayText = "ㅌ", Value = "ㅌ" },
                new KeyboardKey { DisplayText = "ㅊ", Value = "ㅊ" },
                new KeyboardKey { DisplayText = "ㅍ", Value = "ㅍ" },
                new KeyboardKey { DisplayText = "ㅠ", Value = "ㅠ" },
                new KeyboardKey { DisplayText = "ㅜ", Value = "ㅜ" },
                new KeyboardKey { DisplayText = "ㅡ", Value = "ㅡ" },

                // 특수키
                new KeyboardKey { DisplayText = "←", Value = "Backspace", IsSpecialKey = true, Width = 80 },
                new KeyboardKey { DisplayText = "Shift", Value = "Shift", IsSpecialKey = true, Width = 60 },
                new KeyboardKey { DisplayText = "Space", Value = "Space", IsSpecialKey = true, Width = 200 },
                new KeyboardKey { DisplayText = "Enter", Value = "Enter", IsSpecialKey = true, Width = 60 },
            };
        }

        public static List<KeyboardKey> GetEnglishLayout()
        {
            return new List<KeyboardKey>
            {
                // 1열
                new KeyboardKey { DisplayText = "Q", Value = "Q" },
                new KeyboardKey { DisplayText = "W", Value = "W" },
                new KeyboardKey { DisplayText = "E", Value = "E" },
                new KeyboardKey { DisplayText = "R", Value = "R" },
                new KeyboardKey { DisplayText = "T", Value = "T" },
                new KeyboardKey { DisplayText = "Y", Value = "Y" },
                new KeyboardKey { DisplayText = "U", Value = "U" },
                new KeyboardKey { DisplayText = "I", Value = "I" },
                new KeyboardKey { DisplayText = "O", Value = "O" },
                new KeyboardKey { DisplayText = "P", Value = "P" },

                // 2열
                new KeyboardKey { DisplayText = "A", Value = "A" },
                new KeyboardKey { DisplayText = "S", Value = "S" },
                new KeyboardKey { DisplayText = "D", Value = "D" },
                new KeyboardKey { DisplayText = "F", Value = "F" },
                new KeyboardKey { DisplayText = "G", Value = "G" },
                new KeyboardKey { DisplayText = "H", Value = "H" },
                new KeyboardKey { DisplayText = "J", Value = "J" },
                new KeyboardKey { DisplayText = "K", Value = "K" },
                new KeyboardKey { DisplayText = "L", Value = "L" },

                // 3열
                new KeyboardKey { DisplayText = "Z", Value = "Z" },
                new KeyboardKey { DisplayText = "X", Value = "X" },
                new KeyboardKey { DisplayText = "C", Value = "C" },
                new KeyboardKey { DisplayText = "V", Value = "V" },
                new KeyboardKey { DisplayText = "B", Value = "B" },
                new KeyboardKey { DisplayText = "N", Value = "N" },
                new KeyboardKey { DisplayText = "M", Value = "M" },

                // 특수키
                new KeyboardKey { DisplayText = "←", Value = "Backspace", IsSpecialKey = true, Width = 80 },
                new KeyboardKey { DisplayText = "Shift", Value = "Shift", IsSpecialKey = true, Width = 60 },
                new KeyboardKey { DisplayText = "Space", Value = "Space", IsSpecialKey = true, Width = 200 },
                new KeyboardKey { DisplayText = "Enter", Value = "Enter", IsSpecialKey = true, Width = 60 },
            };
        }

        public static List<KeyboardKey> GetNumberLayout()
        {
            return new List<KeyboardKey>
            {
                // 1열
                new KeyboardKey { DisplayText = "1", Value = "1" },
                new KeyboardKey { DisplayText = "2", Value = "2" },
                new KeyboardKey { DisplayText = "3", Value = "3" },

                // 2열
                new KeyboardKey { DisplayText = "4", Value = "4" },
                new KeyboardKey { DisplayText = "5", Value = "5" },
                new KeyboardKey { DisplayText = "6", Value = "6" },

                // 3열
                new KeyboardKey { DisplayText = "7", Value = "7" },
                new KeyboardKey { DisplayText = "8", Value = "8" },
                new KeyboardKey { DisplayText = "9", Value = "9" },

                // 4열
                new KeyboardKey { DisplayText = "←", Value = "Backspace", IsSpecialKey = true, Width = 80 },
                new KeyboardKey { DisplayText = "0", Value = "0" },
                new KeyboardKey { DisplayText = "Enter", Value = "\n", IsSpecialKey = true, Width = 60 },
            };
        }
    }
}
