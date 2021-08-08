// copyright discretelogics © 2011
using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace TeaTime.TeaShell
{
    class Program
    {
        [DllImport("propsys.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int PSRegisterPropertySchema(string pszPath);

        [STAThread]
        public static void Main(string[] args)
        {
            RegisterPropertySchema();
            
            LicenseManager.RemoveExpiredLicense(args.Contains("remove"));
            LicenseManager.EnsureEvaluation();
            LicenseManager.ShowLicenseDialog();
            if (!LicenseManager.IsValid()) return;

            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            folder = Path.Combine(folder, "DiscreteLogics", "TeaShell", "Dow");
            if (Directory.Exists(folder))
            {
                Process.Start("explorer.exe", folder);
            }
        }

        static void RegisterPropertySchema()
        {
            var programDir = Path.GetDirectoryName(typeof(Program).Assembly.CodeBase.Replace("file:///", ""));
            var teapropdesc = Path.Combine(programDir, "tea.propdesc");
            if (!File.Exists(teapropdesc))
            {
                Console.WriteLine("tea.propdesc is not available.");
                return;
            }
            int n = PSRegisterPropertySchema(teapropdesc);
        }
    }
}