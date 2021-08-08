using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TeaTime.TeaShell
{
    /// <summary>
    /// Interaction logic for Splitter.xaml
    /// </summary>
    sealed partial class Splitter : UserControl
    {
        public Splitter()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            // we add 5 at each end to fill the stroke at the caps
            const double extension = 5;
            this.pathFigure.StartPoint = new Point(-extension, this.ActualHeight / 2);
            this.bezier.Point1 = new Point(this.ActualWidth * 1 / 3, 0);
            this.bezier.Point2 = new Point(this.ActualWidth * 2 / 3, this.ActualHeight);
            this.bezier.Point3 = new Point(this.ActualWidth + extension, this.ActualHeight / 2);

            base.OnRender(drawingContext);
        }
    }
}
