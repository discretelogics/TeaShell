using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using LogicNP.CryptoLicensing;

namespace TeaTime.TeaShell
{
    class LicenseManager
    {
        //public static void Remove()
        //{
        //    var license = GetLicense();
        //    license.CryptoLicense.Remove();
        //}

        public static CryptoLicenseWrapper GetLicense()
        {
            var cw = new CryptoLicenseWrapper(new CryptoLicense(
                                                LicenseStorageMode.ToRegistry,
                                                "AMAAMACcIOFJCtBMUd/KoyaF/kiQIsHw3G1gtP56ykzLLLU2XXiB6BdRBRdSytMnQS5bgTcDAAEAAQ=="));
            return cw;
        }

        public static void ShowLicenseDialog()
        {
            using (var lw = GetLicense())
            {
                var license = lw.CryptoLicense;
                license.Load();
                if (license.Status == LicenseStatus.Valid && !license.IsEvaluationLicense()) return; // final license available
                
                var dialog = new EvaluationInfoDialog(license);
                dialog.ProductName = "TeaTime.TeaShell";
                dialog.PurchaseURL = "http://discretelogics.com/shop";
                if(license.ShowEvaluationInfoDialog(dialog))
                {                                    
                    if (license.Status == LicenseStatus.Valid)
                    {
                        license.Save();
                    }
                }
            }
        }

        public static void EnsureEvaluation()
        {
            using (var lw = GetLicense())
            {
                var license = lw.CryptoLicense;
                if (!license.Load())
                {
                    // When app runs for first time, the load will fail, so specify an evaluation code....
                    // This license code was generated from the Generator UI with a "Limit Usage Days To" setting of 30 days.
                    license.LicenseCode = "lgIAAIfm5S07qs0BHgAVAGxpY2Vuc2VlPXNwaWtleSBidW5ueS0MjRzccsCRhk5PBvJIqZ5a31ubYrbZjDUUxnTUWYNARqlwJQ83u8sp23g3l7OobA==";

                    // Save it so that it will get loaded the next time app runs
                    license.Save();
                }
            }
        }

        public static void RemoveExpiredLicense(bool force)
        {
            using (var lw = GetLicense())
            {
                var license = lw.CryptoLicense;
                if (license.Load())
                {
                    if (license.Status == LicenseStatus.Expired || force)
                    {
                        license.Remove();
                    }
                }
            }
        }

        public static bool IsValid()
        {
            using (var lw = GetLicense())
            {
                var license = lw.CryptoLicense;
                if (license.Load())
                {
                    return license.Status == LicenseStatus.Valid;
                }
            }
            return false;
        }
    }
}
