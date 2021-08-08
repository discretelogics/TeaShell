using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace TeaTime.TeaShell
{
    sealed class CasualDataControl : ContentControl
    {
        static CasualDataControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CasualDataControl), new FrameworkPropertyMetadata(typeof(CasualDataControl)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.AssignTemplate();
        }

        static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (CasualDataControl)d;
			Trace.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>> Casual {0} got Data:[{1}]".Formatted(c.Name, c.Data));
            c.AssignTemplate();
        }

        static void OnTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (CasualDataControl)d;
            c.AssignTemplate();
        }

        void AssignTemplate()
        {
            if(DesignerProperties.GetIsInDesignMode(this))
            {
                this.Template = this.DataAvailableTemplate;
                return;
            }
            this.Template = (this.Data != null) ? this.DataAvailableTemplate : this.NullTemplate;
        }

        public static readonly DependencyProperty NullTemplateProperty =
            DependencyProperty.Register("NullTemplate", typeof(ControlTemplate), typeof(CasualDataControl), new PropertyMetadata(OnTemplateChanged));

        public ControlTemplate NullTemplate
        {
            get { return (ControlTemplate)GetValue(NullTemplateProperty); }
            set { SetValue(NullTemplateProperty, value); }
        }

        public static readonly DependencyProperty DataAvailableTemplateProperty =
            DependencyProperty.Register("DataAvailableTemplate", typeof(ControlTemplate), typeof(CasualDataControl), new PropertyMetadata(OnTemplateChanged));

        public ControlTemplate DataAvailableTemplate
        {
            get { return (ControlTemplate)GetValue(DataAvailableTemplateProperty); }
            set { SetValue(DataAvailableTemplateProperty, value); }
        }

        public static readonly DependencyProperty NullTextProperty =
            DependencyProperty.Register("NullText", typeof(string), typeof(CasualDataControl));

        public string NullText
        {
            get { return (string)GetValue(NullTextProperty); }
            set { SetValue(NullTextProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof (object), typeof (CasualDataControl),
            new PropertyMetadata(null, OnDataChanged)); // NO trick!

        public object Data
        {
            get { return (object) GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

    }
}
