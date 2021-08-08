using System;
using System.IO;
using TeaTime;

namespace TeaTime.TeaShell
{
#if DESIGNDATA
#pragma warning disable 0649 // unused fields of Trade
    struct Trade
    {
        public int Volume;
        public double Price;
    }
#endif

    class DesignData
    {
        public static LicensedSnapshot Sample
        {
#if DESIGNDATA
            get
            {
                const string filename = "c:/temp/test.tea";
                File.Delete(filename);
                if (!File.Exists(filename))
                {
                    var nameValues = new NameValueCollection();
                    nameValues.Add("Name1", "Value1");
                    nameValues.Add("Expiry (Quarter)", "4");
                    nameValues.Add("Exchange", "New York Stock Exchange");
                    using (var tf = TeaFile<Event<Trade>>.Create(filename, "contento descriptiono", nameValues))
                    {
                        var t = new Time(2000, 1, 1);
                        for (int i = 1; i < 1000; i++)
                        {
                            Event<Trade> x = new Event<Trade> { Time = t, Value = new Trade { Volume = i * 100, Price = i * 1.11111111 } };
                            tf.Write(x);
                            t = t.AddDays(1);
                        }
                    }
                }
                var s = TeaFileSnapshot.Get(filename, 8);
                var ls = new LicensedSnapshot();
                ls.FileName = "seppl path";
                ls.Snapshot = s;
                return ls;
            }
#else
            get
            {
                return null;
            }
#endif
        }
    }
}
