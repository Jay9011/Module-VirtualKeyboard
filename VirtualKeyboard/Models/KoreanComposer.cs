using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace VirtualKeyboard.Models
{
    public enum KoreanState
    {
        Empty,
        Choseong,
        Jungseong,
        Jongseong,
        Complete
    }

    public class KoreanComposer
    {
        public event EventHandler<CompositionEventArgs> CompositionStart;
        public event EventHandler<CompositionEventArgs> CompositionUpdate;
        public event EventHandler<CompositionEventArgs> CompositionEnd;

        private KoreanState _state = KoreanState.Empty;
        private string _choseong = string.Empty;
        private string _jungseong = string.Empty;
        private string _jongseong = string.Empty;
        private string _tempCons = string.Empty;
        private bool _isCompositionStarted = false;

        private static readonly HashSet<string> Consonants = new HashSet<string>
        {
            "ㄱ", "ㄲ", "ㄴ", "ㄷ", "ㄸ", "ㄹ", "ㅁ", "ㅂ", "ㅃ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅉ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ"
        };

        private static readonly HashSet<string> Vowels = new HashSet<string>
        {
            "ㅏ", "ㅐ", "ㅑ", "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅘ", "ㅙ", "ㅚ", "ㅛ", "ㅜ", "ㅝ", "ㅞ", "ㅟ", "ㅠ", "ㅡ", "ㅢ", "ㅣ"
        };

        private static readonly HashSet<string> ComposedConsonants = new HashSet<string>
        {
            " ", "ㄱ", "ㄲ", "ㄳ", "ㄴ", "ㄵ", "ㄶ", "ㄷ", "ㄹ", "ㄺ", "ㄻ", "ㄼ", "ㄽ", "ㄾ", "ㄿ", "ㅀ", "ㅁ", "ㅂ", "ㅄ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ"

        };

        // 쌍자음 매핑
        private static readonly Dictionary<(string, string), string> DoubleConsonants = new Dictionary<(string, string), string>
        {
            { ("ㄱ", "ㅅ"), "ㄳ" },
            { ("ㄴ", "ㅈ"), "ㄵ" },
            { ("ㄴ", "ㅎ"), "ㄶ" },
            { ("ㄹ", "ㄱ"), "ㄺ" },
            { ("ㄹ", "ㅁ"), "ㄻ" },
            { ("ㄹ", "ㅂ"), "ㄼ" },
            { ("ㄹ", "ㅅ"), "ㄽ" },
            { ("ㄹ", "ㅌ"), "ㄾ" },
            { ("ㄹ", "ㅍ"), "ㄿ" },
            { ("ㄹ", "ㅎ"), "ㅀ" },
            { ("ㅂ", "ㅅ"), "ㅄ" }
        };

        // 복합 모음 매핑
        private static readonly Dictionary<(string, string), string> ComplexVowels = new Dictionary<(string, string), string>
        {
            { ("ㅗ", "ㅏ"), "ㅘ" },
            { ("ㅗ", "ㅐ"), "ㅙ" },
            { ("ㅗ", "ㅣ"), "ㅚ" },
            { ("ㅜ", "ㅓ"), "ㅝ" },
            { ("ㅜ", "ㅔ"), "ㅞ" },
            { ("ㅜ", "ㅣ"), "ㅟ" },
            { ("ㅡ", "ㅣ"), "ㅢ" }
        };

        // 복합 자음 매핑
        private static readonly Dictionary<(string, string), string> ComplexConsonants = new Dictionary<(string, string), string>
        {
            { ("ㄱ", "ㅅ"), "ㄳ" },
            { ("ㄴ", "ㅈ"), "ㄵ" },
            { ("ㄴ", "ㅎ"), "ㄶ" },
            { ("ㄹ", "ㄱ"), "ㄺ" },
            { ("ㄹ", "ㅁ"), "ㄻ" },
            { ("ㄹ", "ㅂ"), "ㄼ" },
            { ("ㄹ", "ㅅ"), "ㄽ" },
            { ("ㄹ", "ㅌ"), "ㄾ" },
            { ("ㄹ", "ㅍ"), "ㄿ" },
            { ("ㄹ", "ㅎ"), "ㅀ" },
            { ("ㅂ", "ㅅ"), "ㅄ" }
        };

        public KoreanComposer()
        {
            Init();
        }

        public void Init()
        {
            _state = KoreanState.Empty;
            _choseong = string.Empty;
            _jungseong = string.Empty;
            _jongseong = string.Empty;
            _tempCons = string.Empty;
        }

        public string ProcessInput(string input)
        {
            if (input == "Backspace")
            {
                return HandleBackspace();
            }

            string result;

            if (Consonants.Contains(input))
            {
                if (!_isCompositionStarted)
                {
                    _isCompositionStarted = true;
                    CompositionStart?.Invoke(this, new CompositionEventArgs(input));
                }
                result = ProcessConsonant(input);
                CompositionUpdate?.Invoke(this, new CompositionEventArgs(result));
                return result;
            }
            else if (Vowels.Contains(input))
            {
                if (!_isCompositionStarted)
                {
                    _isCompositionStarted = true;
                    CompositionStart?.Invoke(this, new CompositionEventArgs(input));
                }
                result = ProcessVowel(input);
                CompositionUpdate?.Invoke(this, new CompositionEventArgs(result));
                return result;
            }

            EndComposition();
            return input;
        }

        private string ProcessConsonant(string input)
        {
            switch (_state)
            {
                case KoreanState.Empty:
                    if (!string.IsNullOrEmpty(_tempCons))
                    {
                        DoubleConsonants.TryGetValue((_tempCons, input), out var doubleCons);
                        if (doubleCons != null)
                        {
                            _choseong = doubleCons;
                            _tempCons = string.Empty;
                        }
                        else
                        {
                            CommitCurrent();
                            _choseong = input;
                            _tempCons = string.Empty;
                        }
                    }
                    else
                    {
                        _tempCons = input;
                    }
                    _state = KoreanState.Choseong;
                    break;
                case KoreanState.Jungseong:
                    _jongseong = input;
                    _state = KoreanState.Jongseong;
                    break;
                case KoreanState.Jongseong:
                    ComplexConsonants.TryGetValue((_jongseong, input), out var comCons);
                    if (comCons != null)
                    {
                        _jongseong = comCons;
                    }
                    else
                    {
                        CommitCurrent();
                        _tempCons = input;
                        _state = KoreanState.Choseong;
                    }
                    break;
            }

            return ComposeChar();
        }

        private string ProcessVowel(string input)
        {
            switch (_state)
            {
                case KoreanState.Empty:
                    _jungseong = input;
                    _state = KoreanState.Jungseong;
                    break;
                case KoreanState.Choseong:
                    if (!string.IsNullOrEmpty(_tempCons))
                    {
                        _choseong = _tempCons;
                        _tempCons = string.Empty;
                    }
                    _jungseong = input;
                    _state = KoreanState.Jungseong;
                    break;
                case KoreanState.Jungseong:
                    ComplexVowels.TryGetValue((_jungseong, input), out var comVowel);
                    if (comVowel != null)
                    {
                        _jungseong = comVowel;
                    }
                    else
                    {
                        CommitCurrent();
                        _jungseong = input;
                        _state = KoreanState.Jungseong;
                    }
                    break;
                case KoreanState.Jongseong:
                    var next_cho = _jongseong;
                    _jongseong = string.Empty;
                    CommitCurrent();
                    _choseong = next_cho;
                    _jungseong = input;
                    _state = KoreanState.Jungseong;
                    break;
            }

            return ComposeChar();
        }

        private void CommitCurrent()
        {
            var lastChar = ComposeChar();

            _state = KoreanState.Empty;
            _choseong = string.Empty;
            _jungseong = string.Empty;
            _jongseong = string.Empty;

            if (_isCompositionStarted)
            {
                EndComposition(lastChar);
            }
        }

        private string ComposeChar()
        {
            if (_state == KoreanState.Empty)
            {
                return string.Empty;
            }

            if (_state == KoreanState.Choseong)
            {
                if (!string.IsNullOrEmpty(_tempCons))
                {
                    return _tempCons;
                }
                return _choseong;
            }

            if (_state == KoreanState.Jungseong && string.IsNullOrEmpty(_choseong))
            {
                return _jungseong;
            }

            if (!string.IsNullOrEmpty(_choseong) && !string.IsNullOrEmpty(_jungseong))
            {
                var choseongIndex = Array.IndexOf(new string[]
                { "ㄱ", "ㄲ", "ㄴ", "ㄷ", "ㄸ", "ㄹ", "ㅁ", "ㅂ", "ㅃ", "ㅅ", "ㅆ",
                    "ㅇ", "ㅈ", "ㅉ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" }, _choseong);

                var jungseongIndex = Array.IndexOf(new string[]
                { "ㅏ", "ㅐ", "ㅑ", "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅘ", "ㅙ",
                    "ㅚ", "ㅛ", "ㅜ", "ㅝ", "ㅞ", "ㅟ", "ㅠ", "ㅡ", "ㅢ", "ㅣ" }, _jungseong);

                var jongseongIndex = string.IsNullOrEmpty(_jongseong) ? 0 :
                    Array.IndexOf(new string[]
                    { "", "ㄱ", "ㄲ", "ㄳ", "ㄴ", "ㄵ", "ㄶ", "ㄷ", "ㄹ", "ㄺ", "ㄻ",
                        "ㄼ", "ㄽ", "ㄾ", "ㄿ", "ㅀ", "ㅁ", "ㅂ", "ㅄ", "ㅅ", "ㅆ", "ㅇ",
                        "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" }, _jongseong);

                if (choseongIndex >= 0 && jungseongIndex >= 0)
                {
                    return char.ConvertFromUtf32(0xAC00 + (choseongIndex * 21 * 28) +
                                                 (jungseongIndex * 28) + jongseongIndex);
                }
            }

            return string.Empty;
        }

        public void EndComposition(string finalChar = "")
        {
            if (_isCompositionStarted)
            {
                _isCompositionStarted = false;
                CompositionEnd?.Invoke(this, new CompositionEventArgs(finalChar));
            }
        }

        private string HandleBackspace()
        {
            if (!string.IsNullOrEmpty(_jongseong))
            {
                _jongseong = string.Empty;
                _state = KoreanState.Jungseong;
            }
            else if (!string.IsNullOrEmpty(_jungseong))
            {
                _jungseong = string.Empty;
                _state = KoreanState.Choseong;
            }
            else if (!string.IsNullOrEmpty(_choseong))
            {
                _choseong = string.Empty;
                _state = KoreanState.Empty;
            }
            else if (!string.IsNullOrEmpty(_tempCons))
            {
                _tempCons = string.Empty;
                _state = KoreanState.Empty;
            }

            return ComposeChar();
        }
    }
}
