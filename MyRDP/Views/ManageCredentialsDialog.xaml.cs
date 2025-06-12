using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyRDP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MyRDP.Views
{
    public sealed partial class ManageCredentialsDialog : ContentDialog, INotifyPropertyChanged
    {
        public List<Credential> Credentials { get; }

        private Credential _selectedCredential;
        public Credential SelectedCredential
        {
            get => _selectedCredential;
            set
            {
                if (_selectedCredential == value) return;
                _selectedCredential = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsCredentialSelected));
            }
        }

        public bool IsCredentialSelected => SelectedCredential != null;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ManageCredentialsDialog(IEnumerable<Credential> credentials)
        {
            InitializeComponent();
            Credentials = new List<Credential>(credentials.Select(c => new Credential
            {
                Id = c.Id,
                Name = c.Name,
                Username = c.Username,
                Password = c.Password
            }));
        }

        private async void AddCredential_Click(object sender, RoutedEventArgs e)
        {
            var credential = new Credential();
            var dialog = new EditCredentialDialog(credential);
            dialog.XamlRoot = this.XamlRoot;

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                Credentials.Add(credential);
            }
        }

        private async void EditCredential_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCredential == null) return;

            var copy = new Credential
            {
                Id = SelectedCredential.Id,
                Name = SelectedCredential.Name,
                Username = SelectedCredential.Username,
                Password = SelectedCredential.Password
            };

            var dialog = new EditCredentialDialog(copy);
            dialog.XamlRoot = this.XamlRoot;

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                SelectedCredential.Name = copy.Name;
                SelectedCredential.Username = copy.Username;
                SelectedCredential.Password = copy.Password;
            }
        }

        private void DeleteCredential_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCredential != null)
            {
                Credentials.Remove(SelectedCredential);
            }
        }
    }
}