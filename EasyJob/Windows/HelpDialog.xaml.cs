using System;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using EasyJob.Utils;

namespace EasyJob.Windows
{
    /// <summary>
    /// Interaction logic for HelpDialog.xaml
    /// </summary>
    public partial class HelpDialog : Window
    {
        public HelpDialog(string helpitem)
        {
            InitializeComponent();
            LoadXml(helpitem);
        }

        private void LoadXml(string helpitem)
        {
            var doc = new XmlDocument();
            doc.LoadXml(CommonUtils.ReadAssemblyFile(@"EasyJob.Documentation.HelpDocumentation.xml"));

            var nodeList = doc.SelectNodes("/items/item[name='" + helpitem + "']");

            HelpHeading.Text = nodeList[0]["heading"].InnerText;
            HelpUsed.Text = nodeList[0]["used"].InnerText;
            HelpDescription.Text = nodeList[0]["description"].InnerText;
            try
            {
                HelpVideo.HorizontalAlignment = HorizontalAlignment.Stretch;
                HelpVideo.VerticalAlignment = VerticalAlignment.Stretch;
                HelpVideo.Stretch = Stretch.Fill;
                HelpVideo.Volume = 0;
                HelpVideo.Source = new Uri(@"Documentation\Videos\" + nodeList[0]["video"].InnerText, UriKind.Relative);
                HelpVideo.Position = TimeSpan.FromSeconds(0);
                HelpVideo.Play();
            }
            catch { }
        }

        private void PlayVideoButton_Click(object sender, RoutedEventArgs e) => HelpVideo.Play();

        private void ReloadVideoButton_Click(object sender, RoutedEventArgs e)
        {
            HelpVideo.Position = TimeSpan.FromSeconds(0);
            HelpVideo.Play();
        }

        private void StopVideoButton_Click(object sender, RoutedEventArgs e) => HelpVideo.Stop();
    }
}
