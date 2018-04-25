using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace UWP.UnwantedToolkit.Controls.HashHandleTextBlock
{
    public sealed class HashHandleTextBlock : Control
    {
        Border _rootBorder;
        RichTextBlock richTextBlock = new RichTextBlock();
        Paragraph paragraph = new Paragraph();

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string HandlePrefix
        {
            get { return (string)GetValue(HandlePrefixProperty); }
            set { SetValue(HandlePrefixProperty, value); }
        }

        public string HashPrefix
        {
            get { return (string)GetValue(HashPrefixProperty); }
            set { SetValue(HashPrefixProperty, value); }
        }

        public RichTextBlock RichTextBlock { get => richTextBlock; set => richTextBlock = value; }
        public Paragraph Paragraph { get => paragraph; set => paragraph = value; }
        public Border RootBorder { get => _rootBorder; set => _rootBorder = value; }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(HashHandleTextBlock),
            new PropertyMetadata(string.Empty, OnPropertyChangedStatic));

        public static readonly DependencyProperty HandlePrefixProperty = DependencyProperty.Register(
            nameof(HandlePrefix), 
            typeof(string), 
            typeof(HashHandleTextBlock), 
            new PropertyMetadata(string.Empty, OnPropertyChangedStatic));

        public static readonly DependencyProperty HashPrefixProperty = DependencyProperty.Register(
            nameof(HashPrefix), 
            typeof(string), 
            typeof(HashHandleTextBlock), 
            new PropertyMetadata(string.Empty, OnPropertyChangedStatic));

        private static void OnPropertyChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as HashHandleTextBlock;
            instance?.OnPropertyChanged(d, e.Property);
        }

        public HashHandleTextBlock()
        {
            DefaultStyleKey = typeof(HashHandleTextBlock);
        }

        private void OnPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            Render();
        }

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

            Hyperlink hyperlink = new Hyperlink();
            hyperlink.NavigateUri = new Uri(returnString);
            Run run = new Run() { Text = Handle };
            hyperlink.Inlines.Add(run);

            Paragraph.Inlines.Add(hyperlink);

            run = new Run() { Text = " " };
            Paragraph.Inlines.Add(run);
        }
    }
}
