using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using NLog;
using TeaTime;
using TeaTime.TeaShell;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("propsys.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int PSRegisterPropertySchema(string pszPath);

        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            var f = SettingsManager.Instance.Get().DateTimeFormat;

            //while(true)
            //{
            //    LicenseEvaluator.Check();
            //}
            

            //return;
            base.OnStartup(e);
        }
    }
}
