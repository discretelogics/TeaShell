// copyright discretelogics © 2011
using System;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Xml.Linq;
using LogicNP.EZShellExtensions;
using TeaTime.TeaShell;
using TeaTime;

namespace TeaTime.TeaShell
{
    // The GuidAttribute has been applied to the extension class
    // with an automatically generated unique GUID.
    // Every different extension should have such a unique GUID
    [Guid("7740EDAD-5E9C-43CF-9E63-13083E01065F")]
    [ComVisible(true)]
    [PreviewHandler(".tea", true, "TeaFile Preview", "{7E89F764-7BD3-4AA5-A05C-0777F1BD6280}")]
    public class TeaFilePreviewHandler : PreviewHandler
    {
        TeaFilePreviewControl previewControl;
        ElementHost xamlHost;

        public TeaFilePreviewHandler()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.xamlHost = new System.Windows.Forms.Integration.ElementHost();
            this.previewControl = new TeaFilePreviewControl();
            this.SuspendLayout();
            // 
            // xamlHost
            // 
            this.xamlHost.BackColor = System.Drawing.Color.Orange;
            this.xamlHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xamlHost.Location = new System.Drawing.Point(0, 0);
            this.xamlHost.Name = "xamlHost";
            this.xamlHost.Size = new System.Drawing.Size(350, 450);
            this.xamlHost.TabIndex = 0;
            this.xamlHost.Child = this.previewControl;
            // 
            // TeaFilePreviewHandler
            // 
            this.Controls.Add(this.xamlHost);
            this.Name = "TeaFilePreviewHandler";
            this.ResumeLayout(false);
        }

        // Override this method to perform initialization tasks specific to your preview handler extension.
        //protected override bool OnInitialize()
        //{
        //    // Return TRUE if successful, FALSE otherwise
        //    return true;
        //}

        // Called when the preview is to be displayed
        protected override void OnLoadPreview()
        {
            try
            {
                var snapshot = SnapshotManager.Instance.GetSnapshot(this.TargetFile);
                this.previewControl.Snapshot = snapshot;
            }
            catch (Exception ex)
            {
                // should never occcur, since file oeration exceptions are handled inside GetSnapshot().
                if (Settings.Default.Tracing) Trace.WriteLine(ex);
                this.previewControl.Snapshot = null;
            }
        }

        //private const int WS_CLIPCHILDREN = 0x02000000;
        //private const int WS_CLIPSIBLINGS = 0x04000000;

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.Style |= WS_CLIPCHILDREN;
        //        cp.Style |= WS_CLIPSIBLINGS;
        //        cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
        //        return cp;
        //    }
        //}

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }

        [ComRegisterFunction]
        public static void Register(System.Type t)
        {
            Console.WriteLine("register preview");
            PreviewHandler.RegisterExtension(typeof(TeaFilePreviewHandler));
        }

        [ComUnregisterFunction]
        public static void UnRegister(System.Type t)
        {
            Console.WriteLine("unregister preview");
            PreviewHandler.UnRegisterExtension(typeof(TeaFilePreviewHandler));
        }
    }
}