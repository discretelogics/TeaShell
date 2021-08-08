using System;
using System.IO;
using System.Linq;
using TeaTime;

#pragma warning disable

namespace TeaTime.TeaShell
{
    struct OHLCV
    {
        public double Open;
        public double High;
        public double Low;
        public double Close;
        public int Volume;
    }

    class SampleFileWriter
    {
        public bool RewriteExistingFiles { get; set; }

        public void CreateAll()
        {
            this.CreateSampleFileEventOHLCVRaw();
            this.CreateSampleFileEventOHLCVRawWithItems();
            this.CreateSampleFileEventOHLCVRawWithItemsAndContentDescription();
            this.CreateSampleFileEventOHLCVRawWithItemsAndNameValues();
            this.CreateTrashFile();
        }

        public string CreateTrashFile()
        {
            var filename = "garbage.tea";
            File.WriteAllText(filename, "garbage");
            return filename;
        }

        public string CreateSampleFileEventOHLCVRaw()
        {
            var filename = "EOHLCV raw.tea";
            if (this.RewriteExistingFiles) File.Delete(filename); else return filename;
            using (TeaFile<Event<OHLCV>>.Create(filename, null, null, false))
            {
            }
            return filename;
        }

        public string CreateSampleFileEventOHLCVRawWithItems()
        {
            var filename = "EOHLCV raw 30 items.tea";
            if (this.RewriteExistingFiles) File.Delete(filename); else return filename;
            using (var tf = TeaFile<Event<OHLCV>>.Create(filename, null, null, false))
            {
                var t = new DateTime(2000, 1, 1);
                var items = Enumerable.Range(10, 30)
                    .Select(i => new Event<OHLCV> {Value = new OHLCV {Open = i, Close = i * 1,}, Time = t.AddDays(i)});
                tf.Write(items);
            }
            return filename;
        }

        public string CreateSampleFileEventOHLCVRawWithItemsAndContentDescription()
        {
            var filename = "EOHLCV raw 30 items cd.tea";
            if (this.RewriteExistingFiles) File.Delete(filename); else return filename;
            using (var tf = TeaFile<Event<OHLCV>>.Create(filename, "some content description", null, true))
            {
                var t = new DateTime(2000, 1, 1);
                var items = Enumerable.Range(10, 1500)
                    .Select(i => new Event<OHLCV> { Value = new OHLCV { Open = i, Close = i * 1, }, Time = t.AddDays(i) });
                tf.Write(items);
            }
            return filename;
        }

        public string CreateSampleFileEventOHLCVRawWithItemsAndNameValues()
        {
            var filename = "EOHLCV raw 30 items nvs.tea";
            if (this.RewriteExistingFiles) File.Delete(filename); else return filename;
            var nvs = new NameValueCollection();
            nvs.Add("name1", 11);
            nvs.Add("name2", "value2");
            using (var tf = TeaFile<Event<OHLCV>>.Create(filename, null, nvs, false))
            {
                var t = new DateTime(2000, 1, 1);
                var items = Enumerable.Range(10, 30)
                    .Select(i => new Event<OHLCV> { Value = new OHLCV { Open = i, Close = i * 1, }, Time = t.AddDays(i) });
                tf.Write(items);
            }
            return filename;
        }
    }
}
