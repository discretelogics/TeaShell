// copyright discretelogics © 2011
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Deployment.WindowsInstaller;

namespace Setup.Actions
{
    public class CustomActions
    {
        [DllImport("propsys.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int PSRegisterPropertySchema(string pszPath);

        [DllImport("propsys.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int PSUnregisterPropertySchema(string pszPath);

        /// <summary>
        /// Installing a property handler with a wix without custom action is not possible, as registering a propdesc 
        /// file can only be done via wind32 calls PS(Un)RegisterPropertySchema. These functions add a new entry for
        /// the propdesc in the registry, behind other propdesc registrations. This cannot be accomplished by simple
        /// addition of registry keys.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        [CustomAction]
        public static ActionResult RegisterPS(Session session)
        {
            Log(session, "RegisterPropertySystem");
            try
            {
                var programs = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                var teapropdesc = Path.Combine(programs, "DiscreteLogics", "TeaShell", "tea.propdesc");
                if (!File.Exists(teapropdesc))
                {
                    programs = Environment.GetEnvironmentVariable("ProgramW6432");
                    teapropdesc = Path.Combine(programs, "DiscreteLogics", "TeaShell", "tea.propdesc");
                }
                if (!File.Exists(teapropdesc))
                {                    
                    Log(session, "tea.propdesc is not available.");
                }

                int n = PSRegisterPropertySchema(teapropdesc);
                if (n != 0) throw new Exception("Property Schema registration failed, n=" + n);
            }
            catch (Exception ex)
            {
                Log(session, "RegisterPropertySystem FAILED. " + ex.Message);
                return ActionResult.Failure;
            }

            Log(session, "RegisterPropertySystem done");
            return ActionResult.Success;
        }

        static void Log(Session session, string message)
        {
            if (session == null) return;
            session.Log(message);
        }

        [CustomAction]
        public static ActionResult UnRegisterPS(Session session)
        {
            Log(session, "UnRegisterPropertySystem");
            try
            {
                int n = PSUnregisterPropertySchema("tea.propdesc");
                if (n != 0) throw new Exception("Property Schema unregistration failed, n=" + n);
            }
            catch (Exception ex)
            {
                Log(session, "UnRegisterPropertySystem FAILED. " + ex.Message);
                return ActionResult.Failure;
            }

            Log(session, "UnRegisterPropertySystem done");
            return ActionResult.Success;
        }

        [CustomAction]
        public static ActionResult OpenDemoFolder(Session session)
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            folder = Path.Combine(folder, "DiscreteLogics", "TeaShell", "Dow");
            if (Directory.Exists(folder))
            {
                Process.Start("explorer.exe", folder);
            }
            return ActionResult.Success;
        }

        [CustomAction]
        public static ActionResult StartTeaShell(Session session)
        {
            var programs = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var teaShellExe = Path.Combine(programs, "DiscreteLogics", "TeaShell", "DiscreteLogics.TeaShell.exe");
            if (!File.Exists(teaShellExe))
            {
                programs = Environment.GetEnvironmentVariable("ProgramW6432");
                teaShellExe = Path.Combine(programs, "DiscreteLogics", "TeaShell", "DiscreteLogics.TeaShell.exe");
            }
            if (!File.Exists(teaShellExe))
            {
                Log(session, "Starting TeaShell failed.");
                return ActionResult.Failure;
            }

            Process.Start(teaShellExe);
            return ActionResult.Success;
        }
    }
}