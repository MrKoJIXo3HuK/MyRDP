using Microsoft.UI.Xaml.Controls;
using MyRDP.Models;
using System.Collections.Generic;

namespace MyRDP.Views
{
    public sealed partial class EditConnectionDialog : ContentDialog
    {
        public Connection Connection { get; }
        public List<ConnectionGroup> Groups { get; }
        public List<Credential> Credentials { get; }

        public EditConnectionDialog(
            Connection connection,
            IEnumerable<ConnectionGroup> groups,
            IEnumerable<Credential> credentials)
        {
            InitializeComponent();
            Connection = connection;
            Groups = new List<ConnectionGroup>(groups);
            Credentials = new List<Credential>(credentials);
        }
    }
}