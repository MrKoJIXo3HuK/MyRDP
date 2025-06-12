using MyRDP.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.UI.Xaml.Controls;
using MyRDP.Views;
using System.Linq;
using System;

namespace MyRDP.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<ConnectionGroup> Groups { get; } = new ObservableCollection<ConnectionGroup>();
        public ObservableCollection<Connection> Connections { get; } = new ObservableCollection<Connection>();
        public ObservableCollection<Credential> Credentials { get; } = new ObservableCollection<Credential>();

        public ICommand AddGroupCommand { get; }
        public ICommand EditGroupCommand { get; }
        public ICommand DeleteGroupCommand { get; }
        public ICommand AddConnectionCommand { get; }
        public ICommand EditConnectionCommand { get; }
        public ICommand DeleteConnectionCommand { get; }
        public ICommand ManageCredentialsCommand { get; }

        public MainViewModel()
        {
            AddGroupCommand = new RelayCommand(AddGroup);
            EditGroupCommand = new RelayCommand(EditGroup, () => SelectedGroup != null);
            DeleteGroupCommand = new RelayCommand(DeleteGroup, () => SelectedGroup != null);
            AddConnectionCommand = new RelayCommand(AddConnection);
            EditConnectionCommand = new RelayCommand(EditConnection, () => SelectedConnection != null);
            DeleteConnectionCommand = new RelayCommand(DeleteConnection, () => SelectedConnection != null);
            ManageCredentialsCommand = new RelayCommand(ManageCredentials);
        }

        private ConnectionGroup _selectedGroup;
        public ConnectionGroup SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                if (_selectedGroup == value) return;
                _selectedGroup = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FilteredConnections));
                OnPropertyChanged(nameof(IsGroupSelected));
            }
        }

        public bool IsGroupSelected => SelectedGroup != null;

        private Connection _selectedConnection;
        public Connection SelectedConnection
        {
            get => _selectedConnection;
            set
            {
                if (_selectedConnection == value) return;
                _selectedConnection = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsConnectionSelected));
            }
        }

        public bool IsConnectionSelected => SelectedConnection != null;

        public ObservableCollection<Connection> FilteredConnections =>
            new ObservableCollection<Connection>(Connections.Where(c =>
                SelectedGroup == null || c.GroupId == SelectedGroup.Id));

        private void AddGroup() => ShowGroupDialog(new ConnectionGroup());
        private void EditGroup() => ShowGroupDialog(SelectedGroup);
        private void DeleteGroup()
        {
            Groups.Remove(SelectedGroup);
            var toRemove = Connections.Where(c => c.GroupId == SelectedGroup.Id).ToList();
            foreach (var conn in toRemove) Connections.Remove(conn);
        }

        private void AddConnection() => ShowConnectionDialog(new Connection());
        private void EditConnection() => ShowConnectionDialog(SelectedConnection);
        private void DeleteConnection() => Connections.Remove(SelectedConnection);

        private async void ShowGroupDialog(ConnectionGroup group)
        {
            var dialog = new EditConnectionGroupDialog(group);
            dialog.XamlRoot = App.MainWindow.Content.XamlRoot;

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if (!Groups.Contains(group)) Groups.Add(group);
            }
        }

        private async void ShowConnectionDialog(Connection connection)
        {
            var dialog = new EditConnectionDialog(connection, Groups.ToList(), Credentials.ToList());
            dialog.XamlRoot = App.MainWindow.Content.XamlRoot;

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if (!Connections.Contains(connection)) Connections.Add(connection);
                OnPropertyChanged(nameof(FilteredConnections));
            }
        }

        private async void ManageCredentials()
        {
            var dialog = new ManageCredentialsDialog(Credentials.ToList());
            dialog.XamlRoot = App.MainWindow.Content.XamlRoot;

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                // Обновляем коллекцию после изменений
                Credentials.Clear();
                foreach (var cred in dialog.Credentials)
                {
                    Credentials.Add(cred);
                }
            }
        }
    }
}