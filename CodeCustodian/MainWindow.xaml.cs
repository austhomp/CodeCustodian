namespace CodeCustodian
{
    using System.Windows;

    using GalaSoft.MvvmLight.Messaging;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, true, HandleStringMessage);

        }

        private void HandleStringMessage(string message)
        {
            if ("close" == message)
            {
                this.Close();
            }
        }
    }
}
