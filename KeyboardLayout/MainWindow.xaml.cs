using System;
using System.Windows;
using System.Windows.Input;
using VirtualKeyboard.Models;

namespace KeyboardLayout
{
    public partial class MainWindow : Window
    {
        private int _caretIndex;
        private string _composedText;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            KeyboardPopup.IsOpen = true;
            _caretIndex = InputTextBox.CaretIndex;
        }

        private void InputTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!VirtualKeyboard.IsMouseOver)
            {
                KeyboardPopup.IsOpen = false;
            }
        }

        private void VirtualKeyboard_OnKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text))
            {
                return;
            }

            var currentText = InputTextBox.Text;
            var selectionStart = InputTextBox.SelectionStart;
            var selectionLength = InputTextBox.SelectionLength;

            if (selectionLength > 0)
            {
                InputTextBox.Text = InputTextBox.Text.Remove(selectionStart, selectionLength);
            }

            // 입력 시작 : e.IsComposing == false && e.IsCommitted == false
            if (!e.IsComposing && !e.IsCommitted)
            {
                
            }
            // 입력 중 : e.IsComposing == true && e.IsCommitted == false
            else if (e.IsComposing && !e.IsCommitted)
            {
                if (!string.IsNullOrEmpty(_composedText))
                {
                    currentText = currentText.Remove(currentText.Length - 1);
                }
                _composedText = e.Text;
                currentText += e.Text;
            }
            // 입력 종료 : e.IsComposing == false && e.IsCommitted == true
            else if (!e.IsComposing && e.IsCommitted)
            {
                if (!string.IsNullOrEmpty(_composedText))
                {
                    currentText = currentText.Remove(currentText.Length - 1);
                }
                _composedText = string.Empty;
                currentText += e.Text;
            }

            InputTextBox.Text = currentText;
            InputTextBox.SelectionStart = InputTextBox.Text.Length;
            InputTextBox.Focus();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (e.Key == Key.Back && KeyboardPopup.IsOpen)
            {
                e.Handled = true;
            }
        }
    }
}
