using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;
using TeaTime;

namespace TeaShell.Tests
{
    [TestClass]
    public class SnapshotTest
    {
        [TestInitialize]
        public void Init()
        {
            // Ensure english error messages on non english systems.
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }

        [TestMethod]
        public void TeaFileSnapshotTest()
        {
            var filename = TestUtils.CreateTestTeaFile();
            var s = TeaFileSnapshot.Get(filename, 0);
            
            s.Should().Not.Be.Null();
            s.Count.Should().Be(7);
            s.Description.Should().Not.Be.Null();
            s.Description.ItemDescription.Should().Not.Be.Null();
            s.First.Values.Should().Have.Count.EqualTo(2);
            s.Last.Values.Should().Have.Count.EqualTo(2);
            
            s.First.Values[0].Should().Be(new Time(2000, 1, 1));
            s.First.Values[1].Should().Be(0 * 1100);

            s.Last.Values[0].Should().Be(new Time(2000, 1, 7));
            s.Last.Values[1].Should().Be(6 * 1100);
        }
    }
}