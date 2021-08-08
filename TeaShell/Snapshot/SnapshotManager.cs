// copyright discretelogics © 2011
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using LogicNP.CryptoLicensing;
using LicenseManager = TeaTime.TeaShell.LicenseManager;

namespace TeaTime.TeaShell
{
    interface ISnapshotManager
    {
        LicensedSnapshot GetSnapshot(string filename);
    }

    class SnapshotManager : ISnapshotManager
    {
        #region singleton

        static ISnapshotManager instance;

        public static ISnapshotManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SnapshotManager();
                }
                return instance;
            }
            set { instance = value; }
        }

        #endregion

        public LicensedSnapshot GetSnapshot(string filename)
        {
            var ls = new LicensedSnapshot();
            ls.FileName = filename;
            var license = LicenseManager.GetLicense().CryptoLicense;
            license.Load();
            var fields = license.ParseUserData("$");
            ls.L = fields["licensee"] as string;
            ls.E = license.IsEvaluationExpired();
            ls.V = license.Status == LicenseStatus.Valid;
            ls.EV = license.IsEvaluationLicense();
            if (ls.V)
            {
                try
                {
                    ls.Snapshot = TeaFileSnapshot.Get(filename);
                }
                catch (Exception)
                {
                    ls.Snapshot = null;
                }
            }
            return ls;
        }
    }
}