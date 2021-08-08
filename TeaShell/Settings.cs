// copyright discretelogics © 2011
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TeaTime.TeaShell
{
    class SettingsManager
    {
        Settings settings;
        string filename;

        string Filename
        {
            get
            {
                if (this.filename == null)
                {
                    this.filename = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    this.filename = Path.Combine(this.filename, "DiscreteLogics", "TeaShell", "TeaShell.config");
                }
                return this.filename;
            }
        }

        public Settings Get()
        {
            if (this.settings == null)
            {
                this.settings = this.Load();
            }
            return this.settings;
        }

        public Settings Load()
        {
            var s = new Settings();
            try
            {
                if (!File.Exists(this.Filename)) return new Settings();
                XDocument doc;
                try
                {
                    doc = XDocument.Load(this.Filename);
                }
                catch (Exception ex)
                {
                    this.LogError("Configuration File is invalid: " + ex.Message);
                    return null;
                }
                var elements = doc.Descendants();
                var e = elements.FirstOrDefault(ee => ee.Name == "DateTimeFormat");
                if (e != null)
                {
                    try
                    {
                        DateTime.Now.ToString(e.Value);
                        s.DateTimeFormat = e.Value;
                    }
                    catch (Exception ex)
                    {
                        this.LogWarn("Error reading configuration value: Name:{0} Value:{1} Error:{2}. Using defailt value:{3} ", "DateTimeFormat", ex.Message, s.DateTimeFormat);
                    }
                }
                s.Tracing = elements.Any(ee => ee.Name == "Tracing");
            }
            catch (Exception ex)
            {
                this.LogError("Error reading configuration file: " + ex.Message);
            }
            return s;
        }

        void LogError(string message)
        {
            EventLog.WriteEntry("Application", message, EventLogEntryType.Error, 1, 0);
        }

        void LogWarn(string message, params object[] formatValues)
        {
            EventLog.WriteEntry("Application", string.Format(message, formatValues), EventLogEntryType.Warning, 1, 0);
        }

        #region singleton

        public static SettingsManager Instance
        {
            get { return Singleton.instance; }
        }

        // ReSharper disable ClassNeverInstantiated.Local
        class Singleton
            // ReSharper restore ClassNeverInstantiated.Local
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            // ReSharper disable EmptyConstructor

            internal static readonly SettingsManager instance = new SettingsManager();

            static Singleton()
                // ReSharper restore EmptyConstructor
            {
            }
        }

        #endregion
    }

    class Settings
    {
        public Settings()
        {
            this.DateTimeFormat = "yyyy/MM/dd hh:mm:ss";
        }

        public static Settings Default
        {
            get { return SettingsManager.Instance.Get(); }
        }

        public string DateTimeFormat { get; set; }
        public bool Tracing { get; set; }
    }
}