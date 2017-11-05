using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SquadManager.UI.Controls.TextBox
{
    public class MyTextBox : System.Windows.Controls.TextBox
    {
        public InputType Input
        {
            get { return (InputType)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input", 
                typeof(InputType), typeof(MyTextBox),
                new PropertyMetadata(InputType.All, InputPropertyChanged));

        private static void InputPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var box = (MyTextBox)d;
            if (box == null) return;

            var inputType = (InputType)d.GetValue(InputProperty);

            switch (inputType)
            {
                case InputType.All:
                    box.TextChanged += (s, e) => { };
                    break;
                case InputType.Numeric:
                    box.TextChanged += (s, e) =>
                    {
                        foreach (var c in box.Text)
                        {
                            if (!Char.IsNumber(c))
                            {
                                box.Text = string.Empty;
                                return;
                            }
                        }

                        var minInput = (int)d.GetValue(MinInputProperty);
                        var boxInput = int.Parse(box.Text);
                        if (boxInput < minInput)
                        {
                            box.Text = string.Empty;
                        }
                    };
                    break;
            }
        }


        public int MinInput
        {
            get { return (int)GetValue(MinInputProperty); }
            set { SetValue(MinInputProperty, value); }
        }

        public static readonly DependencyProperty MinInputProperty =
            DependencyProperty.Register("MinInput", 
                typeof(int), typeof(MyTextBox), 
                new PropertyMetadata(0));
    }

    public enum InputType
    {
        All,
        Numeric,
    }
}
