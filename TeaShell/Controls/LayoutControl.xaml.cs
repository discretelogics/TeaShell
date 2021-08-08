using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TeaTime;
using Point = System.Drawing.Point;

//namespace TeaTime
//{
//    public struct Point
//    {
//        public Point(int row, int column) : this()
//        {
//            this.Row = row;
//            this.Column = column;
//        }

//        public int Row { get; set; }
//        public int Column { get; set; }
//    }
//}

namespace TeaTime.TeaShell
{
    sealed class Part
    {
        public Point Start { get; set; }
        public int Length { get; set; }
        public bool LeftClosed { get; set; }
        public bool RightClosed { get; set; }
    }

    /// <summary>
    /// Interaction logic for LayoutControl.xaml
    /// </summary>
    sealed partial class LayoutControl : UserControl
    {
        public LayoutControl()
        {
            InitializeComponent();

            Bind(null);
        }

        void Bind(ItemDescription id)
        {
            //var size = id.ItemSize;
            int size = 24;

            // create 8 columns
            for (int i = 0; i < 8; i++)
            {
                var cd = new ColumnDefinition();
                cd.Width = new GridLength(20);
                this.grid.ColumnDefinitions.Add(cd);
            }

            int n = size;
            while(n > 0)
            {
                var rd = new RowDefinition();
                this.grid.RowDefinitions.Add(rd);
                n -= 8;
            }

            return;
            //var parts = this.GetParts(3, 16);
            //foreach (var p in parts)
            //{
            //    this.grid
            //}
        }

        const int columnsPerRow = 8;

        public Point GetPoint(int pos)
        {
            var r = pos / columnsPerRow;
            var c = pos - 8 * r;
            return new Point(c, r);
        }

        public List<Part> GetParts(int pos, int length)
        {
            if(length <= 0) throw new ArgumentException("length must have a positive value.");

            List<Part> parts = new List<Part>();

            var part = new Part();
            part.LeftClosed = true;
            part.Start = this.GetPoint(pos);
            var remainingLineSpace = 8 - part.Start.X;
            if(length <= remainingLineSpace)
            {
                part.RightClosed = true;
                part.Length = length;
                length = 0;
            }
            else
            {
                part.RightClosed = false;
                part.Length = remainingLineSpace;
                length -= remainingLineSpace;
            }
            parts.Add(part);
            
            while(length > 0)
            {
                Point nextPartStart = new Point(0, part.Start.Y + 1);
                
                // create next part
                part = new Part();
                part.LeftClosed = false;
                part.Start = nextPartStart;
                remainingLineSpace = 8;
                if (length <= remainingLineSpace)
                {
                    part.RightClosed = true;
                    part.Length = length;
                    length = 0;
                }
                else
                {
                    part.RightClosed = false;
                    part.Length = remainingLineSpace;
                    length -= remainingLineSpace;
                }
                parts.Add(part);
            }
            return parts;
        }
    }
}
