using System.Collections.Generic;
using System.Windows;
using EasyJob.Serialization.AnswerDialog;

namespace EasyJob.Windows
{
    /// <summary>
    /// Interaction logic for AnswerDialog.xaml
    /// </summary>
    public partial class AnswerDialog : Window
    {
        public AnswerData answerData = null;

        public AnswerDialog(AnswerData _answerData)
        {
            InitializeComponent();
            answerData = _answerData;
            if (_answerData != null)
            {
                foreach (var answer in _answerData.Answers)
                {
                    answer.AnswerResult = "";
                }
                AnswerDialogItems.ItemsSource = _answerData.Answers;
            }
        }

        private void CANCELButton_Click(object sender, RoutedEventArgs e) => DialogResult = false;

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            var AllowConfirm = true;
            answerData.Answers = (List<Answer>)AnswerDialogItems.ItemsSource;
            foreach (var answer in answerData.Answers)
            {
                if (answer.AnswerResult == "" || answer.AnswerResult == null)
                {
                    AllowConfirm = false;
                }
            }
            if (AllowConfirm == false)
            {
                MessageBox.Show("Please provide value to all textboxes!", "Fill all data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
        }
    }
}
