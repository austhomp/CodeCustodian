namespace CodeCustodian.ViewModel
{
    using System;
    using System.Collections.ObjectModel;

    using CodeCustodian.Core;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            if (this.IsInDesignMode)
            {
                var fakeList = new ObservableCollection<CodeRepositoryItem>();
                fakeList.Add(new CodeRepositoryItem("Test", "OK", "test"));
                fakeList.Add(new CodeRepositoryItem("Test2", "Out of Date", "test"));
                fakeList.Add(new CodeRepositoryItem("Test3", "Merge Conflict", "test"));
                this.ItemList = fakeList;
            }
            else
            {
                // Code runs "for real"
                this.codeRepositoryStore = new CodeRepositoryStore();
                this.RetrieveItemsList();
            }
            this.InitCommands();
        }

        private void RetrieveItemsList()
        {
            var items = this.codeRepositoryStore.RetrieveConfiguredCodeRepositories();
            this.ItemList.Clear();
            foreach (var item in items)
            {
                this.ItemList.Add(item);
            }
        }

        public RelayCommand CommandAppExit { get; private set; }

        public RelayCommand CommandGetLatest { get; private set; }

        public RelayCommand CommandOpenScreenSettings { get; private set; }

        public RelayCommand CommandOpenScreenAbout { get; private set; }

        public const string ItemListPropertyName = "ItemList";

        private ObservableCollection<CodeRepositoryItem> itemList = new ObservableCollection<CodeRepositoryItem>();

        private ICodeRepositoryStore codeRepositoryStore;

        public ObservableCollection<CodeRepositoryItem> ItemList
        {
            get
            {
                return this.itemList;
            }

            set
            {
                if (this.itemList == value)
                {
                    return;
                }

                var oldValue = this.itemList;
                this.itemList = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ItemListPropertyName);
            }
        }

        private void InitCommands()
        {
            this.CommandAppExit = new RelayCommand(ExitApp, () => true);
            this.CommandGetLatest = new RelayCommand(GetLatestCode, CanGetLatestCode);
            this.CommandOpenScreenSettings = new RelayCommand(OpenScreenSettings, () => true);
            this.CommandOpenScreenAbout = new RelayCommand(OpenScreenAbout, () => true);
        }

        private void GetLatestCode()
        {
            return; // todo
        }

        private bool CanGetLatestCode()
        {
            return true; // todo query conditions
        }

        private static void ExitApp()
        {
            Messenger.Default.Send<string, MainWindow>("close");
        }

        private static void OpenScreenAbout()
        {
            // someday make this a nice window instead of a messagebox
            string aboutMessage = string.Format("{0}", "Code Custodian");
            System.Windows.MessageBox.Show(aboutMessage, "About", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        private static void OpenScreenSettings()
        {
            System.Windows.MessageBox.Show("Sorry this hasn't been finished yet...", "Clang Clang", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
        }
    }
}