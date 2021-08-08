// copyright discretelogics © 2011
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using TeaTime;

namespace TeaTime.TeaShell
{
    sealed partial class SplitGrid : UserControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ItemViewCollection), typeof(SplitGrid),
                                        new FrameworkPropertyMetadata(null,
                                        FrameworkPropertyMetadataOptions.AffectsMeasure, OnItemsChanged));

        static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SplitGrid g = (SplitGrid)d;
            g.OnItemsChanged((ItemViewCollection)e.NewValue);
        }

        public ItemViewCollection Items
        {
            get { return (ItemViewCollection)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        #endregion

        public SplitGrid()
        {
            this.InitializeComponent();
        }

        delegate void CreateColumnsDelegate(ItemViewCollection ivc);

        void OnItemsChanged(ItemViewCollection items)
        {
            this.grid.ItemsSource = null;
            Dispatcher.Invoke(DispatcherPriority.Render, new CreateColumnsDelegate(CreateColumnsDispatched), items);
        }

        void CreateColumnsDispatched(ItemViewCollection items)
        {
            this.CreateColumns(items);
            this.grid.ItemsSource = items;
        }

        void CreateColumns(ItemViewCollection items)
        {
            this.grid.Columns.Clear();
            if (items == null) return;

            var c = new DataGridTextColumn();
            c.Binding = new Binding("ItemNumber");

            this.grid.Columns.Add(c);

            foreach (var f in items.Fields)
            {
                c = new DataGridTextColumn();
                c.Header = f.Name;
                c.CellStyle = (Style)this.grid.TryFindResource("ItemCellStyle");
                c.Binding = new Binding("Item.Values[{0}]".Formatted(f.Index));
                if (f.FieldType.IsDecimalType() && this.Items.Decimals.HasValue)
                {
                    string format = "#.";
                    this.Items.Decimals.Value.Times(() => format += "#");
                    c.Binding.StringFormat = format;
                }

                this.grid.Columns.Add(c);
            }
        }

        public Thickness FirstColumnWidth
        {
            get
            {
                return new Thickness(this.grid.Columns.First().ActualWidth, 0, 0, 0);
            }
        }
    }
}