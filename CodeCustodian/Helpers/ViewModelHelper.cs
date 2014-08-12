namespace CodeCustodian.Helpers
{
    using System.ComponentModel;
    using System.Windows;

    public static class ViewModelHelper
    {
        private static bool? isInDesignMode;

        public static bool IsInDesignMode
        {
            get
            {
                if (!isInDesignMode.HasValue)
                {
                    isInDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue;
                }

                return isInDesignMode.Value;
            }
        }
    }
}
