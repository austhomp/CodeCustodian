namespace CodeCustodian.ViewModel
{
    using System;
    using System.Collections.Generic;
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
                fakeList.Add(new CodeRepositoryItem("Test", "test", null, "OK"));
                fakeList.Add(new CodeRepositoryItem("Test2", "test", null, "Out of Date"));
                fakeList.Add(new CodeRepositoryItem("Test3", "test", null, "Merge Conflict"));
                this.ItemList = fakeList;
            }
            else
            {
                // todo inject these instead of newing them up
                this.appConfiguration = new AppConfiguration();
                this.codeRepositoryStore = new CodeRepositoryStore(this.appConfiguration);
                var updateServices = new List<ICodeRepositoryUpdateService>();
                var queryServices = new List<ICodeRepositoryQueryService>();
                queryServices.Add(new TFS.CodeRepositoryQueryService());
                this.codeRepositoryMonitor = new CodeRepositoryMonitor(queryServices, updateServices);
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
        
        public RelayCommand CommandQueryStatus { get; private set; }

        public RelayCommand CommandOpenScreenSettings { get; private set; }

        public RelayCommand CommandOpenScreenAbout { get; private set; }

        public const string ItemListPropertyName = "ItemList";

        private ObservableCollection<CodeRepositoryItem> itemList = new ObservableCollection<CodeRepositoryItem>();

        private ICodeRepositoryStore codeRepositoryStore;

        private CodeRepositoryMonitor codeRepositoryMonitor;

        private AppConfiguration appConfiguration;

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

                this.itemList = value;
                RaisePropertyChanged(ItemListPropertyName);
            }
        }

        private void InitCommands()
        {
            this.CommandAppExit = new RelayCommand(ExitApp, () => true);
            this.CommandGetLatest = new RelayCommand(GetLatestCode, CanGetLatestCode);
            this.CommandQueryStatus = new RelayCommand(QueryCodeStatus, CanQueryCodeStatus);
            this.CommandOpenScreenSettings = new RelayCommand(OpenScreenSettings, () => true);
            this.CommandOpenScreenAbout = new RelayCommand(OpenScreenAbout, () => true);
        }

        private void QueryCodeStatus()
        {
            this.codeRepositoryMonitor.Refresh(this.ItemList);

            // todo replace this with a better way of having the data refresh (INotifyPropertyChanged in a non-UI class? Probably need a viewmodel)
            var copy = this.ItemList;
            this.ItemList = null;
            this.ItemList = copy;
        }

        private bool CanQueryCodeStatus()
        {
            return true;
        }

        private void GetLatestCode()
        {
            // todo
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