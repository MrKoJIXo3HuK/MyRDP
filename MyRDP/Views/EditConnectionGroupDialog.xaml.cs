using Microsoft.UI.Xaml.Controls;
using MyRDP.Models;

namespace MyRDP.Views
{
    public sealed partial class EditConnectionGroupDialog : ContentDialog
    {
        public ConnectionGroup Group { get; }

        public EditConnectionGroupDialog(ConnectionGroup group)
        {
            InitializeComponent();
            Group = group;
        }
    }
}