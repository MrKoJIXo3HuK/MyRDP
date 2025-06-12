using Microsoft.UI.Xaml.Controls;
using MyRDP.Models;

namespace MyRDP.Views
{
    public sealed partial class EditCredentialDialog : ContentDialog
    {
        public Credential Credential { get; }

        public EditCredentialDialog(Credential credential)
        {
            InitializeComponent();
            Credential = credential;
        }
    }
}