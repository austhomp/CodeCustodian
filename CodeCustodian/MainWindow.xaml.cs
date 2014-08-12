namespace CodeCustodian
{
    using System.Windows;
    using System.Windows.Input;

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
            switch (message)
            {
                case "close":
                    this.Close();
                    break;
                case "refreshbindings":
                    Dispatcher.Invoke(CommandManager.InvalidateRequerySuggested);
                    break;
            }
        }
    }
}
