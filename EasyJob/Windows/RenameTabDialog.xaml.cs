using System.Windows;

namespace EasyJob.Windows
{
    /// <summary>
    /// Interaction logic for RenameTabDialog.xaml
    /// </summary>
    public partial class RenameTabDialog : Window
    {
        public string NewTabName = "";

        public RenameTabDialog(string SelectedTabHeader)
        {
            InitializeComponent();
            RenameTabTextBox.Text = SelectedTabHeader;
        }

        private void CancelRenameButton_Click(object sender, RoutedEventArgs e) => DialogResult = false;

        private void RenameTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (RenameTabTextBox.Text.Length > 0)
            {
                NewTabName = RenameTabTextBox.Text;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please specify new name for the tab");
            }
        }
    }
}
