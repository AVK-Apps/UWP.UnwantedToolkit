using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace UWP.UnwantedToolkit.Controls
{
    /// <summary>
    /// TextBlock control to show Twitter Handles and HashTags as Links.
    /// </summary>
    public sealed class HashHandleTextBlock : Control
    {
        Border _rootBorder;
        RichTextBlock richTextBlock = new RichTextBlock();
        Paragraph paragraph = new Paragraph();

        /// <summary>
        /// Text to show
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Prefix for Twitter Handle. Make sure it ends with "\"
        /// </summary>
        public string HandlePrefix
        {
            get { return (string)GetValue(HandlePrefixProperty); }
            set { SetValue(HandlePrefixProperty, value); }
        }

        /// <summary>
        /// Prefix for Twitter Hash Tags. Make sure it ends with "\"
        /// </summary>
        public string HashPrefix
        {
            get { return (string)GetValue(HashPrefixProperty); }
            set { SetValue(HashPrefixProperty, value); }
        }

        /// <summary>
        /// Foreground for Links
        /// </summary>
        public Brush LinkForeground
        {
            get { return (Brush)GetValue(LinkForegroundProperty); }
            set { SetValue(LinkForegroundProperty, value); }
        }

        private RichTextBlock RichTextBlock { get => richTextBlock; set => richTextBlock = value; }
        private Paragraph Paragraph { get => paragraph; set => paragraph = value; }
        private Border RootBorder { get => _rootBorder; set => _rootBorder = value; }

        /// <summary>
        /// Gets the dependency property for <see cref="Text"/>.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(HashHandleTextBlock),
            new PropertyMetadata(string.Empty, OnPropertyChangedStatic));

        /// <summary>
        /// Gets the dependency property for <see cref="HandlePrefix"/>.
        /// </summary>
        public static readonly DependencyProperty HandlePrefixProperty = DependencyProperty.Register(
            nameof(HandlePrefix), 
            typeof(string), 
            typeof(HashHandleTextBlock), 
            new PropertyMetadata(string.Empty, OnPropertyChangedStatic));

        /// <summary>
        /// Gets the dependency property for <see cref="HashPrefix"/>.
        /// </summary>
        public static readonly DependencyProperty HashPrefixProperty = DependencyProperty.Register(
            nameof(HashPrefix), 
            typeof(string), 
            typeof(HashHandleTextBlock), 
            new PropertyMetadata(string.Empty, OnPropertyChangedStatic));

        /// <summary>
        /// Gets the dependency property for <see cref="LinkForeground"/>.
        /// </summary>
        public static readonly DependencyProperty LinkForegroundProperty = DependencyProperty.Register(
            nameof(LinkForeground),
            typeof(Brush),
            typeof(HashHandleTextBlock),
            new PropertyMetadata(Application.Current.Resources["SystemControlBackgroundAccentBrush"], OnPropertyChangedStatic));

        private static void OnPropertyChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as HashHandleTextBlock;
            instance?.OnPropertyChanged(d, e.Property);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashHandleTextBlock"/> class.
        /// </summary>
        public HashHandleTextBlock()
        {
            DefaultStyleKey = typeof(HashHandleTextBlock);
        }

        private void OnPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            Render();
        }

        /// <inheritdoc />
        protected override void OnApplyTemplate()
        {
            RichTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            RichTextBlock.VerticalAlignment = VerticalAlignment.Top;
            RichTextBlock.Blocks.Add(Paragraph);
            RootBorder = GetTemplateChild("RootElement") as Border;
            RootBorder.Child = RichTextBlock;
            Render();
        }

        private void Render()
        {
            Paragraph.Inlines.Clear();
            List<string> HashText = new List<string>();
            List<string> HandleText = new List<string>();
            int start = 0;
            int end = Text.Length;
            int handlestart = 0;
            int handleEnd = 0;
            int hashstart = 0;
            int hashEnd = 0;

            int pos = start;

            while (pos < end)
            {
                if (Text[pos] == '@')
                {
                    handlestart = pos;
                    handleEnd = Text.IndexOf(" ", handlestart);
                    if (handleEnd == -1) handleEnd = end;
                    string handle = Text.Substring(handlestart, handleEnd - handlestart);
                    pos = handleEnd;
                    RenderHandle(handle, true);
                }
                else if (Text[pos] == '#')
                {
                    hashstart = pos;
                    hashEnd = Text.IndexOf(" ", hashstart);
                    if (hashEnd == -1) hashEnd = end;
                    string hashText = Text.Substring(hashstart, hashEnd - hashstart);
                    RenderHandle(hashText, false);
                    pos = hashEnd;
                }
                else
                {
                    RenderText(Text[pos].ToString());
                }
                pos++;
            }
        }

        internal void RenderText(string RunText)
        {
            Run run = new Run() { Text = RunText };
            Paragraph.Inlines.Add(run);
        }

        internal void RenderHandle(string Handle, bool isHandle)
        {
            string returnString = string.Format(
                "{0}{1}",  
                isHandle ? HandlePrefix : HashPrefix,
                Handle.Substring(1, Handle.Length - 1));

            Hyperlink hyperlink = new Hyperlink() { Foreground = LinkForeground };
            hyperlink.NavigateUri = new Uri(returnString);
            Run run = new Run() { Text = Handle };
            hyperlink.Inlines.Add(run);

            Paragraph.Inlines.Add(hyperlink);

            run = new Run() { Text = " " };
            Paragraph.Inlines.Add(run);
        }
    }
}
