using System;
using System.IO;
using System.Reflection;
using System.Windows;
using EasyJob.Utils;
using HtmlAgilityPack;

namespace EasyJob.Windows
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutDialog"/> class.
        /// </summary>
        public AboutDialog()
        {
            InitializeComponent();
            LoadDataInfoIntoTheForm();
        }

        /// <summary>
        /// Converts to plain text.
        /// </summary>
        /// <param name="html">The HTML.</param>
        public static string ConvertToPlainText(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var sw = new StringWriter();
            ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString();
        }

        public void LoadDataInfoIntoTheForm()
        {
            AboutTitle.Content = "EasyJob Executor " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AboutInfo.Content = "Author: Akshin Mustafayev. Contrubutions made to the project by the Github community";
            var readme = CommonUtils.ReadAssemblyFile("LICENSE.txt");
            RichTextBox1.Document.Blocks.Clear();
            var plainText = ConvertToPlainText(readme);
            RichTextBox1.AppendText(plainText);
        }

        private static void ConvertContentTo(HtmlNode node, TextWriter outText)
        {
            foreach (var subnode in node.ChildNodes)
            {
                ConvertTo(subnode, outText);
            }
        }

        private static void ConvertTo(HtmlNode node, TextWriter outText)
        {
            switch (node.NodeType)
            {
                case HtmlNodeType.Comment:
                    // don't output comments
                    break;

                case HtmlNodeType.Document:
                    ConvertContentTo(node, outText);
                    break;

                case HtmlNodeType.Text:
                    // script and style must not be output
                    var parentName = node.ParentNode.Name;
                    if (parentName is "script" or "style")
                    {
                        break;
                    }

                    // get text
                    var html = ((HtmlTextNode)node).Text;

                    // is it in fact a special closing node output as text?
                    if (HtmlNode.IsOverlappedClosingElement(html))
                    {
                        break;
                    }

                    // check the text is meaningful and not a bunch of whitespaces
                    if (html.Trim().Length > 0)
                    {
                        outText.Write(HtmlEntity.DeEntitize(html));
                    }
                    break;

                case HtmlNodeType.Element:
                    switch (node.Name)
                    {
                        case "p":
                            // treat paragraphs as crlf
                            outText.Write(Environment.NewLine);
                            break;

                        case "br":
                            outText.Write(Environment.NewLine);
                            break;
                    }

                    if (node.HasChildNodes)
                    {
                        ConvertContentTo(node, outText);
                    }
                    break;
            }
        }

        private void CheckNewReleasesButton_Click(object sender, RoutedEventArgs e) => CommonUtils.OpenLinkInBrowser("https://github.com/akshinmustafayev/EasyJob/releases");

        private void GetInspirationButton_Click(object sender, RoutedEventArgs e) => CommonUtils.OpenLinkInBrowser("https://www.youtube.com/watch?v=l0U7SxXHkPY");

        private void GithubImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => CommonUtils.OpenLinkInBrowser("https://github.com/akshinmustafayev/EasyJob");
    }
}
