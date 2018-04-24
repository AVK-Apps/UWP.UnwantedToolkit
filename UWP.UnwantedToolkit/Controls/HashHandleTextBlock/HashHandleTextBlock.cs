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

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(HashHandleTextBlock), new PropertyMetadata(string.Empty));

        public HashHandleTextBlock()
        {
            DefaultStyleKey = typeof(HashHandleTextBlock);

            RegisterPropertyChangedCallback(FontSizeProperty, OnPropertyChanged);
        }

        private void OnPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            Render();
        }

        protected override void OnApplyTemplate()
        {
            richTextBlock.Blocks.Add(paragraph);
            _rootBorder = GetTemplateChild("RootElement") as Border;
            _rootBorder.Child = richTextBlock;
            Render();
        }

        private void Render()
        {
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
                    RenderText(start, pos);
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
                pos++;
            }
        }

        internal void RenderText(int start, int end)
        {
            Run run = new Run() { Text = Text.Substring(start, end) };
            paragraph.Inlines.Add(run);
        }

        internal void RenderHandle(string Handle, bool isHandle)
        {
            string uri = isHandle ? "https://twitter.com/" + Handle.Substring(1, Handle.Length - 1) : "https://twitter.com/hashtag/" + Handle.Substring(1, Handle.Length - 1);

            Hyperlink hyperlink = new Hyperlink();
            hyperlink.NavigateUri = new Uri(uri);
            Run run = new Run() { Text = Handle };
            hyperlink.Inlines.Add(run);

            paragraph.Inlines.Add(hyperlink);

            run = new Run() { Text = " " };
            paragraph.Inlines.Add(run);
        }
    }
}
