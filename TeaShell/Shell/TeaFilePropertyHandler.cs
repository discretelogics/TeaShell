// copyright discretelogics © 2011
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using LogicNP.EZShellExtensions;

namespace TeaTime.TeaShell
{
    [Guid("90D13A20-4E01-4B1D-BF94-669C4113A010")]
    [ComVisible(true)]
    [TargetExtension(".tea", true)]
    public class TeaFilePropertyHandler : PropertyHandler
    {
        TeaFileSnapshot snapshot;

        protected override bool OnInitialize()
        {
            try
            {
                // Leaf.EnsureLicenseLoaded();
                var s = SnapshotManager.Instance.GetSnapshot(TargetFile);
                this.snapshot = s.Snapshot;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override Property[] GetProperties()
        {
            return new[]
                {
                    this.Add("ItemType", "Name of a single item in the file"),
                    this.Add("First", "First Item in the file. If the file holds a timeseries, this is the time of the first item, otherwise the first property of the first item."),
                    this.Add("Last", "Last Item in the file. If the file holds a timeseries, this is the time of the last item, otherwise the first property of the last item."),
                    this.Add("Count", "The number of items in the file"),
                    this.Add("ContentDescription", "The file's content description"),
                    this.Add("IsTimeSeries", "Indicates whether the file holds a time series or a matrix."),
                    this.Add("FieldNames", "The names of the item's fields."),
                    this.Add("FieldTypes", "The types of the item's fields."),
                };
        }

        UserDefinedProperty Add(string label, string description)
        {
            var canonicalName = "TeaFile." + label;
            var p = new UserDefinedProperty(canonicalName);
            p.Description = description;
            p.LabelText = label;
            return p;
        }

        protected override object GetPropertyValue(Property property)
        {
            try
            {
                if (this.snapshot == null) return null;
                var description = this.snapshot.Description;
                if (description == null) return null;

                if (property.CanonicalName == "TeaFile.ContentDescription")
                {
                    return description.ContentDescription;
                }

                // item description dependant
                var id = description.ItemDescription;
                if (id == null) return null;
                var value = this.GetValue(this.snapshot, description, id, property);
                if (value is DateTime) return ((DateTime) value).ToString(Settings.Default.DateTimeFormat);
                return value;
            }
            catch (Exception ex)
            {
                if (Settings.Default.Tracing) Trace.WriteLine(ex);
            }
            return null;
        }

        object GetValue(TeaFileSnapshot snapshot, TeaFileDescription description, ItemDescription id, Property property)
        {
            if (property.CanonicalName == "TeaFile.ItemType")
            {
                return id.ItemTypeName;
            }
            else if (property.CanonicalName == "TeaFile.First")
            {
                if (snapshot.Count == 0) return null;
                if (id.HasEventTime)
                {
                    return snapshot.FirstTime;
                }
                else
                {
                    return snapshot.First.Values.FirstOrDefault();
                }
            }
            else if (property.CanonicalName == "TeaFile.Last")
            {
                if (id.HasEventTime)
                {
                    return snapshot.LastTime;
                }
                else
                {
                    return snapshot.Last.Values.FirstOrDefault();
                }
            }
            else if (property.CanonicalName == "TeaFile.Count")
            {
                return snapshot.Count;
            }
            else if (property.CanonicalName == "TeaFile.IsTimeSeries")
            {
                return string.Join(",", id.HasEventTime ? "yes" : "no");
            }
            else if (property.CanonicalName == "TeaFile.FieldNames")
            {
                return string.Join(",", id.Fields.Select(f => f.Name));
            }
            else if (property.CanonicalName == "TeaFile.FieldTypes")
            {
                return string.Join(",", id.Fields.Select(f => f.FieldType));
            }
            return null;
        }

        protected override Property[] GetPropertiesFullDetails()
        {
            return this.GetProperties();
        }

        //protected override Stream GetPropertyDescriptionSchema()
        //{
        //    var s = base.GetPropertyDescriptionSchema();
        //    s.Position = 0;
        //    TextReader r = new StreamReader(s);
        //    File.WriteAllText("c:/temp/propdesc.txt", r.ReadToEnd());
        //    Console.WriteLine("wrote it");
        //    return s;
        //}

        [ComRegisterFunction]
        public static void Register(Type t)
        {
			Console.WriteLine("register property");
            RegisterExtension(typeof (TeaFilePropertyHandler));
        }

        [ComUnregisterFunction]
        public static void UnRegister(Type t)
        {
			Console.WriteLine("unregister property");
            UnRegisterExtension(typeof (TeaFilePropertyHandler));
        }
    }
}