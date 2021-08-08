using System;
using System.Windows;
using System.Windows.Controls;

namespace TeaTime.TeaShell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                //var w = new SampleFileWriter();
                //var filename = w.CreateSampleFileEventOHLCVRawWithItemsAndContentDescription();
                //this.preview.Snapshot = SnapshotManager.Instance.GetSnapshot(filename);
                this.preview.Snapshot = SnapshotManager.Instance.GetSnapshot(@"C:\Users\hase\AppData\Local\DiscreteLogics\TeaShell\Dow\AA.tea");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);                
            }
        }

        private void RadioClick(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            var w = new SampleFileWriter();
            w.RewriteExistingFiles = true;
            switch (rb.Content.ToString())
            {
                case "design":
                    this.preview.Snapshot = DesignData.Sample;
                    break;

                case "zero":
                    var filename = w.CreateSampleFileEventOHLCVRaw();
                    this.preview.Snapshot = SnapshotManager.Instance.GetSnapshot(filename);
                    break;

                case "normal":
                    filename = w.CreateSampleFileEventOHLCVRawWithItemsAndContentDescription();
                    this.preview.Snapshot = SnapshotManager.Instance.GetSnapshot(filename);
                    break;

                case "namevalues":
                    filename = w.CreateSampleFileEventOHLCVRawWithItemsAndNameValues();
                    this.preview.Snapshot = SnapshotManager.Instance.GetSnapshot(filename);
                    break;

                case "wrong":
                    this.preview.Snapshot = SnapshotManager.Instance.GetSnapshot("c:/temp/x.tea");
                    break;

                case "AA":
                    this.preview.Snapshot = SnapshotManager.Instance.GetSnapshot("H:/dev/reps/github/TeaFiles.Net/Bin/f10m.tea");
                    break;

                case "large":
                    filename = w.CreateSampleFileEventOHLCVRawWithItemsAndContentDescription();
                    this.preview.Snapshot = SnapshotManager.Instance.GetSnapshot(filename);
                    break;
            }
        }
    }
}