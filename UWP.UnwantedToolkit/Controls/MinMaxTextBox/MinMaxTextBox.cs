using System;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace UWP.UnwantedToolkit.Controls
{
    /// <summary>
    /// Textbox Control with Min &amp; Max Length to show remaining in the bottom.
    /// </summary>
    public sealed class MinMaxTextBox : Control
    {
        /// <summary>
        /// Min Length. Default is 5
        /// </summary>
        public int MinLength
        {
            get { return (int)GetValue(MinLengthProperty); }
            set { SetValue(MinLengthProperty, value); }
        }

        /// <summary>
        /// Gets the dependency property for <see cref="MinLength"/>.
        /// </summary>
        public static readonly DependencyProperty MinLengthProperty = DependencyProperty.Register(
            nameof(MinLength),
            typeof(int),
            typeof(MinMaxTextBox),
            new PropertyMetadata(5));

        /// <summary>
        /// Text of TextBox
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets the dependency property for <see cref="Text"/>.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(MinMaxTextBox),
            new PropertyMetadata(string.Empty));

        /// <summary>
        /// Max Length. Default is 600
        /// </summary>
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        /// <summary>
        /// Gets the dependency property for <see cref="MaxLength"/>.
        /// </summary>
        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
            nameof(MaxLength),
            typeof(int),
            typeof(MinMaxTextBox),
            new PropertyMetadata(600));

        /// <summary>
        /// Text of TextBox
        /// </summary>
        public string SelectedText
        {
            get { return (string)GetValue(SelectedTextProperty); }
            set { SetValue(SelectedTextProperty, value); }
        }

        /// <summary>
        /// Gets the dependency property for <see cref="Text"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedTextProperty = DependencyProperty.Register(
            nameof(SelectedText),
            typeof(string),
            typeof(MinMaxTextBox),
            new PropertyMetadata(string.Empty, OnSelectedTextChanged));

        private static void OnSelectedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as MinMaxTextBox;
            RichEditBox TextBox = instance.GetTemplateChild("TextData") as RichEditBox;
            TextBox.Document.Selection.Text = e.NewValue.ToString();
            TextBox.Focus(FocusState.Programmatic);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinMaxTextBox"/> class.
        /// </summary>
        public MinMaxTextBox()
        {
            DefaultStyleKey = typeof(MinMaxTextBox);
        }

        private TextBlock TextInfo;
        private RichEditBox TextBox;

        /// <inheritdoc />
        protected override void OnApplyTemplate()
        {
            TextBox = GetTemplateChild("TextData") as RichEditBox;
            TextInfo = GetTemplateChild("TextInfo") as TextBlock;
            TextBox.PlaceholderText = $"Minimum {MinLength} characters. Maximum {MaxLength}";
            TextBox.TextChanged += TextBox_TextChanged;
            TextBox.SelectionChanged += TextBox_SelectionChanged;
            Render(string.Empty);
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox.Document.GetText(TextGetOptions.None, out string changedText);
            Text = changedText;
            Render(changedText);
            TextBox.Focus(FocusState.Programmatic);
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SelectedText = TextBox.Document.Selection.Text;
        }

        private void Render(string ChangedText)
        {
            TextBox.Document.GetText(TextGetOptions.None, out string changedText);
            if (ChangedText.Length < MinLength)
            {
                TextInfo.Text = $"{MinLength - ChangedText.Length}  more required....";
            }
            else if (ChangedText.Length >= MinLength)
            {
                TextInfo.Text = $"{TextBox.MaxLength - ChangedText.Length} characters remaining";
            }
        }
    }
}