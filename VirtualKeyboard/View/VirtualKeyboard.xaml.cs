using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VirtualKeyboard.Models;

namespace VirtualKeyboard.View
{
    /// <summary>
    /// VirtualKeyboard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class VirtualKeyboard : UserControl
    {
        public event EventHandler<KeyPressEventArgs> KeyPressed;

        public static readonly DependencyProperty KeyboardLayoutProperty = 
            DependencyProperty.Register(nameof(LayoutType), typeof(KeyboardLayoutType), typeof(VirtualKeyboard), new PropertyMetadata(KeyboardLayoutType.Korean, OnLayoutTypeChanged));

        public KeyboardLayoutType LayoutType
        {
            get => (KeyboardLayoutType)GetValue(KeyboardLayoutProperty);
            set => SetValue(KeyboardLayoutProperty, value);
        }

        private readonly KoreanComposer _koreanComposer;

        public VirtualKeyboard()
        {
            InitializeComponent();
            
            _koreanComposer = new KoreanComposer();
            _koreanComposer.CompositionStart += OnCompositionStart;
            _koreanComposer.CompositionUpdate += OnCompositionUpdate;
            _koreanComposer.CompositionEnd += OnCompositionEnd;

            LayoutSelector.ItemsSource = Enum.GetValues(typeof(KeyboardLayoutType));
            LayoutSelector.SelectedIndex = (int)LayoutType;

            UpdateKeyboardLayout();
        }

        private void UpdateKeyboardLayout()
        {
            switch (LayoutType)
            {
                case KeyboardLayoutType.Korean:
                    KeyboardKeys.ItemsSource = KeyboardLayouts.GetKoreanLayout();
                    break;
                case KeyboardLayoutType.English:
                    KeyboardKeys.ItemsSource = KeyboardLayouts.GetEnglishLayout();
                    break;
                case KeyboardLayoutType.Number:
                    KeyboardKeys.ItemsSource = KeyboardLayouts.GetNumberLayout();
                    break;
                default:
                    KeyboardKeys.ItemsSource = KeyboardLayouts.GetKoreanLayout();
                    break;
            }
        }

        private static void OnLayoutTypeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is VirtualKeyboard keyboard)
            {
                keyboard.UpdateKeyboardLayout();
            }
        }

        private void LayoutSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LayoutSelector.SelectedItem is KeyboardLayoutType layoutType)
            {
                LayoutType = layoutType;
                if (layoutType != KeyboardLayoutType.Korean)
                {
                    _koreanComposer.Init();
                }
            }
        }

        private void Key_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is KeyboardKey keyboardKey)
            {
                switch (LayoutType)
                {
                    case KeyboardLayoutType.Korean:
                        _koreanComposer.ProcessInput(keyboardKey.Value);
                        break;
                    default:
                        KeyPressed?.Invoke(this, new KeyPressEventArgs(keyboardKey.Value, false, false));
                        break;
                }
            }
        }

        private void OnCompositionStart(object sender, CompositionEventArgs e)
        {
            KeyPressed?.Invoke(this, new KeyPressEventArgs(e.Text, false, false));
        }

        private void OnCompositionUpdate(object sender, CompositionEventArgs e)
        {
            KeyPressed?.Invoke(this, new KeyPressEventArgs(e.Text, true, false));
        }

        private void OnCompositionEnd(object sender, CompositionEventArgs e)
        {
            KeyPressed?.Invoke(this, new KeyPressEventArgs(e.Text, false, true));
        }
    }
}
