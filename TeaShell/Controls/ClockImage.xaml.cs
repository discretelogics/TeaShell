using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TeaTime.TeaShell
{
    /// <summary>
    /// Interaction logic for ClockImage.xaml
    /// </summary>
    sealed partial class ClockImage : UserControl
    {
        public ClockImage()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(ClockImage), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(3, 3, 3))));

        public Brush Stroke
        {
            get { return (Brush) GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof (Brush), typeof (ClockImage), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xf0, 0xf0, 0xf0))));

        public Brush Fill
        {
            get { return (Brush) GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
    }
}
