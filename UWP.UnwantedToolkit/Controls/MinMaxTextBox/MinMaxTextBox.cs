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
        /// Initializes a new instance of the <see cref="MinMaxTextBox"/> class.
        /// </summary>
        public MinMaxTextBox()
        {
            DefaultStyleKey = typeof(MinMaxTextBox);
        }

        private TextBox TextData;
        private TextBlock TextInfo;

        /// <inheritdoc />
        protected override void OnApplyTemplate()
        {
            TextData = GetTemplateChild("TextData") as TextBox;
            TextInfo = GetTemplateChild("TextInfo") as TextBlock;
            TextData.PlaceholderText = $"Minimum {MinLength} characters. Maximum {MaxLength}";
            TextData.TextChanged += TextData_TextChanged;
            Render();
        }

        private void TextData_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = TextData.Text;
            Render();
        }

        private void Render()
        {
            if (TextData.Text.Length < MinLength)
            {
                TextInfo.Text = $"{MinLength - TextData.Text.Length}  more required....";
            }
            else if (TextData.Text.Length >= MinLength)
            {
                TextInfo.Text = $"{TextData.MaxLength - TextData.Text.Length} characters remaining";
            }
        }
    }
}