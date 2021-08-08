using System;
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;
using TeaTime.Shell;

namespace TeaShell.Tests
{
    [TestClass]
    public class LayoutControlTest
    {
        [TestMethod]
        public void GetPointTest()
        {
            var lc = new LayoutControl();
            lc.GetPoint(0).Should().Be(new Point(0, 0));
            lc.GetPoint(1).Should().Be(new Point(1, 0));
            lc.GetPoint(2).Should().Be(new Point(2, 0));
            lc.GetPoint(3).Should().Be(new Point(3, 0));
            lc.GetPoint(4).Should().Be(new Point(4, 0));
            lc.GetPoint(5).Should().Be(new Point(5, 0));
            lc.GetPoint(6).Should().Be(new Point(6, 0));
            lc.GetPoint(7).Should().Be(new Point(7, 0));
            lc.GetPoint(8).Should().Be(new Point(0, 1));
            lc.GetPoint(9).Should().Be(new Point(1, 1));
        }

        [TestMethod]
        public void GetPartsTest()
        {
            var lc = new LayoutControl();
            
            var parts = lc.GetParts(0, 3);
            parts.Count.Should().Be(1);
            parts.First().LeftClosed.Should().Be.True();
            parts.First().RightClosed.Should().Be.True();
            parts.First().Length.Should().Be(3);
            parts.First().Start = new Point(0, 0);

            parts = lc.GetParts(0, 8);
            parts.Count.Should().Be(1);
            parts.First().LeftClosed.Should().Be.True();
            parts.First().RightClosed.Should().Be.True();
            parts.First().Length.Should().Be(8);
            parts.First().Start = new Point(0, 0);

            parts = lc.GetParts(0, 9);
            parts.Count.Should().Be(2);
            parts.First().LeftClosed.Should().Be.True();
            parts.First().RightClosed.Should().Be.False();
            parts.First().Length.Should().Be(8);
            parts.First().Start = new Point(0, 0);
            parts.Last().LeftClosed.Should().Be.False();
            parts.Last().RightClosed.Should().Be.True();
            parts.Last().Length.Should().Be(1);
            parts.Last().Start = new Point(0, 1);

            parts = lc.GetParts(4, 9);
            parts.Count.Should().Be(2);
            parts.First().LeftClosed.Should().Be.True();
            parts.First().RightClosed.Should().Be.False();
            parts.First().Length.Should().Be(4);
            parts.First().Start = new Point(4, 0);
            parts.Last().LeftClosed.Should().Be.False();
            parts.Last().RightClosed.Should().Be.True();
            parts.Last().Length.Should().Be(5);
            parts.Last().Start = new Point(0, 1);
        }
    }
}
