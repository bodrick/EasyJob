using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using EasyJob.Serialization;
using EasyJob.Serialization.AnswerDialog;
using EasyJob.Utils;
using Microsoft.Win32;

namespace EasyJob.Windows
{
    /// <summary>
    /// Interaction logic for AddActionButtonDialog.xaml
    /// </summary>
    public partial class AddActionButtonDialog : Window
    {
        public ConfigButton configButton;

        public AddActionButtonDialog()
        {
            InitializeComponent();
            configButton = null;
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

        private void CANCELButton_Click(object sender, RoutedEventArgs e) => DialogResult = false;

        private string ConvertScriptPathTypeComboBoxToString(ComboBox cb) => cb.SelectedIndex == 0 ? "relative" : "absolute";

        private string ConvertScriptTypeComboBoxToString(ComboBox cb) => cb.SelectedIndex == 0 ? "powershell" : "bat";

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

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            var configArguments = new List<ConfigArgument>();
            foreach (Answer answer in ButtonScriptArguments.Items)
            {
                configArguments.Add(new ConfigArgument(answer.AnswerQuestion, answer.AnswerResult));
            }

            var buttonScriptPathTypeValue = ConvertScriptPathTypeComboBoxToString(ButtonScriptPathType);
            var buttonScriptTypeValue = ConvertScriptTypeComboBoxToString(ButtonScriptType);
            var newConfigButton = new ConfigButton(Guid.NewGuid(), ButtonText.Text, ButtonDescription.Text, ButtonScript.Text, buttonScriptPathTypeValue, buttonScriptTypeValue, configArguments);
            configButton = newConfigButton;

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
