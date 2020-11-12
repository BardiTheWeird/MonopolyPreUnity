using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace GameInterface.CustomControls
{
    class ScrollingTextBox : TextBox
    {
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            CaretIndex = Text.Length;
            ScrollToEnd();
        }
    }
}
