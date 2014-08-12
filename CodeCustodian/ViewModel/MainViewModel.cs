namespace CodeCustodian.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using CodeCustodian.Core;
    using CodeCustodian.Helpers;
    using CodeCustodian.TFS;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using UpdateControls.Collections;
    using UpdateControls.Fields;

    public class MainViewModel
    {
        public MainViewModel()
        {
            if (ViewModelHelper.IsInDesignMode)
            {
                var fakeList = new IndependentList<CodeRepositoryItem>();
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
                var tfsCommandFactory = new TfsCommandFactory(new TfsCommandPathLocator());
                var tfsCommandOutputParser = new TfsCommandOutputParser();
                queryServices.Add(new CodeRepositoryQueryService(tfsCommandFactory, new TfsWorkspaceQueryService(tfsCommandFactory, tfsCommandOutputParser)));
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

        private IndependentList<CodeRepositoryItem> itemList = new IndependentList<CodeRepositoryItem>();

        private ICodeRepositoryStore codeRepositoryStore;

        private CodeRepositoryMonitor codeRepositoryMonitor;

        private AppConfiguration appConfiguration;

        private bool isRefreshing = false;

        public IndependentList<CodeRepositoryItem> ItemList
        {
            get
            {
                return this.itemList;
            }

            set
            {
                this.itemList = value;
            }
        }

        private void InitCommands()
        {
            this.CommandAppExit = new RelayCommand(ExitApp, () => true);
            this.CommandGetLatest = new RelayCommand(GetLatestCode, CanGetLatestCode);
            this.CommandQueryStatus = new RelayCommand(QueryCodeStatusAsync, CanQueryCodeStatus);
            this.CommandOpenScreenSettings = new RelayCommand(OpenScreenSettings, () => true);
            this.CommandOpenScreenAbout = new RelayCommand(OpenScreenAbout, () => true);
        }

        private async void QueryCodeStatusAsync()
        {
            await Task.Run(() => this.QueryCodeStatus());
        }

        private void QueryCodeStatus()
        {
            if (isRefreshing) return;
            isRefreshing = true;

            try
            {
                this.codeRepositoryMonitor.Refresh(this.ItemList);
            }
            finally
            {
                isRefreshing = false;
                RefreshUI();
            }
        }

        private bool CanQueryCodeStatus()
        {
            return !isRefreshing;
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

        private static void RefreshUI()
        {
            Messenger.Default.Send<string, MainWindow>("refreshbindings");
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