using System.Windows.Forms;
using EasyJobPSTools.Windows;

namespace EasyJobPSTools
{
    public class Program
    {
        public static string ShowEJInputBoxWindow(string Header, string Text, bool AllowEmptyResult)
        {
            var sejib = new ShowEJInputBox(Header, Text, AllowEmptyResult);
            return sejib.ShowDialog() == true ? sejib.windowResult : "";
        }

        public static string ShowEJSelectFileWindow()
        {
            var ofd = new OpenFileDialog();
            return ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : "";
        }

        public static string ShowEJSelectFileWindow(string fileType)
        {
            var ofd = new OpenFileDialog
            {
                Filter = fileType
            };
            return ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : "";
        }

        public static string ShowEJSelectFolderWindow()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                var result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }

                return "";
            }
        }
    }
}
