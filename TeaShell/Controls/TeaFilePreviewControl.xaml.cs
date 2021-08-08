using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Linq;
using TeaTime;

namespace TeaTime.TeaShell
{
    sealed partial class TeaFilePreviewControl : UserControl
    {
        public TeaFilePreviewControl()
        {
            InitializeComponent();
        }

        public LicensedSnapshot Snapshot
        {
            get
            {
                return (LicensedSnapshot)this.DataContext;
            }
            set
            {
				Trace.WriteLine("Snapshot set. Valid=" + value.V);
				Trace.WriteLine("Snapshot set. Shot=" + value.Snapshot);

                this.DataContext = value;
            }
        }

        public string Version
        {
            get { return "Version: {0}".Formatted(this.GetType().Assembly.GetName().Version.ToString()); }
        }

		//void ShowLicense(object sender, EventArgs ea)
		//{
		//    if (LicenseManager.StartLicenseDialogProcessIfNoFinalLicense())
		//    {
		//        // reload after license possibly changed
		//        if (this.Snapshot != null)
		//        {
		//            this.DataContext = SnapshotManager.Instance.GetSnapshot(this.Snapshot.FileName);
		//        }
		//    }
		//}
    }
}
