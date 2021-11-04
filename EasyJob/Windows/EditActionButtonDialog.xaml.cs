using System.Windows;
using System.Windows.Controls;
using EasyJob.Serialization.AnswerDialog;
using EasyJob.TabItems;
using EasyJob.Utils;
using Microsoft.Win32;

namespace EasyJob.Windows
{
    /// <summary>
    /// Interaction logic for EditButtonDialog.xaml
    /// </summary>
    public partial class EditActionButtonDialog : Window
    {
        public ActionButton actionButton = null;

        public EditActionButtonDialog(ActionButton _actionButton)
        {
            InitializeComponent();

            actionButton = _actionButton;
            ButtonText.Text = actionButton.ButtonText;
            ButtonDescription.Text = actionButton.ButtonDescription;
            ButtonScript.Text = actionButton.ButtonScript;
            if (actionButton.ButtonScriptPathType == "relative")
            { ButtonScriptPathType.SelectedIndex = 0; }
            else
            { ButtonScriptPathType.SelectedIndex = 1; }
            if (actionButton.ButtonScriptType == "powershell")
            { ButtonScriptType.SelectedIndex = 0; }
            else
            { ButtonScriptType.SelectedIndex = 1; }
            var answers = actionButton.ButtonArguments;
            foreach (var ans in answers)
            {
                ButtonScriptArguments.Items.Add(new Answer { AnswerQuestion = ans.AnswerQuestion, AnswerResult = ans.AnswerResult });
            }
        }

        private void ADDButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ButtonScriptArguments.Items.Add(new Answer { AnswerQuestion = ButtonScriptArgumentText.Text, AnswerResult = "" });
                ButtonScriptArgumentText.Text = "";
            }
            catch { }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => DialogResult = false;

        private string ConvertScriptPathTypeComboBoxToString(ComboBox cb)
        {
            if (cb.SelectedIndex == 0)
            {
                return "relative";
            }
            else
            {
                return "absolute";
            }
        }

        private string ConvertScriptTypeComboBoxToString(ComboBox cb)
        {
            if (cb.SelectedIndex == 0)
            {
                return "powershell";
            }
            else
            {
                return "bat";
            }
        }

        private void DeleteArgumentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = sender as Button;
                ButtonScriptArguments.Items.Remove((Answer)btn.DataContext);
            }
            catch { }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var hd = new HelpDialog(button.Name);
            hd.ShowDialog();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            actionButton.ButtonText = ButtonText.Text;
            actionButton.ButtonDescription = ButtonDescription.Text;
            actionButton.ButtonScript = ButtonScript.Text;
            actionButton.ButtonScriptPathType = ConvertScriptPathTypeComboBoxToString(ButtonScriptPathType);
            actionButton.ButtonScriptType = ConvertScriptTypeComboBoxToString(ButtonScriptType);

            actionButton.ButtonArguments.Clear();
            foreach (Answer ans in ButtonScriptArguments.Items)
            {
                actionButton.ButtonArguments.Add(new Answer { AnswerQuestion = ans.AnswerQuestion, AnswerResult = ans.AnswerResult });
            }

            DialogResult = true;
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = CommonUtils.ApplicationStartupPath()
            };
            if (ofd.ShowDialog() == true)
            {
                if (ButtonScriptPathType.SelectedIndex == 0)
                {
                    ButtonScript.Text = CommonUtils.ConvertPartToRelative(ofd.FileName);
                }
                else
                {
                    ButtonScript.Text = ofd.FileName;
                }
            }
        }
    }
}
