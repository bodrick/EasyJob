using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;

namespace WpfRichText
{
    public static class RichTextBoxAssistant
    {
        public static readonly DependencyProperty BoundDocument =
           DependencyProperty.RegisterAttached("BoundDocument", typeof(string), typeof(RichTextBoxAssistant),
           new FrameworkPropertyMetadata(null,
               FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
               OnBoundDocumentChanged)
               );

        public static string GetBoundDocument(DependencyObject dependencyObject)
        {
            if (dependencyObject != null)
            {
                var html = dependencyObject.GetValue(BoundDocument) as string;
                var xaml = string.Empty;

                if (!string.IsNullOrEmpty(html))
                {
                    xaml = HtmlToXamlConverter.ConvertHtmlToXaml(html, false);
                }

                return xaml;
            }
            return string.Empty;
        }

        public static void SetBoundDocument(DependencyObject dependencyObject, string value)
        {
            if (dependencyObject != null)
            {
                var html = HtmlFromXamlConverter.ConvertXamlToHtml(value, false);
                dependencyObject.SetValue(BoundDocument, html);
            }
        }

        private static void AttachEventHandler(RichTextBox box)
        {
            var binding = BindingOperations.GetBinding(box, BoundDocument);

            if (binding != null)
            {
                if (binding.UpdateSourceTrigger is UpdateSourceTrigger.Default or UpdateSourceTrigger.LostFocus)
                {
                    box.LostFocus += HandleLostFocus;
                }
                else
                {
                    box.TextChanged += HandleTextChanged;
                }
            }
        }

        private static void HandleLostFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as RichTextBox;

            var tr = new TextRange(box.Document.ContentStart, box.Document.ContentEnd);

            using var ms = new MemoryStream();
            tr.Save(ms, DataFormats.Xaml);
            var xamlText = Encoding.UTF8.GetString(ms.ToArray());
            SetBoundDocument(box, xamlText);
        }

        private static void HandleTextChanged(object sender, RoutedEventArgs e)
        {
            // TODO: TextChanged is currently not working!
            var box = sender as RichTextBox;

            var tr = new TextRange(box.Document.ContentStart,
                               box.Document.ContentEnd);

            using var ms = new MemoryStream();
            tr.Save(ms, DataFormats.Xaml);
            var xamlText = Encoding.UTF8.GetString(ms.ToArray());
            SetBoundDocument(box, xamlText);
        }

        private static void OnBoundDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not RichTextBox box)
            {
                return;
            }

            RemoveEventHandler(box);

            var newXAML = GetBoundDocument(d);

            box.Document.Blocks.Clear();

            if (!string.IsNullOrEmpty(newXAML))
            {
                using var xamlMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(newXAML));
                var parser = new ParserContext();
                parser.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
                parser.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
                //FlowDocument doc = new FlowDocument();
                var section = XamlReader.Load(xamlMemoryStream, parser) as Section;

                box.Document.Blocks.Add(section);
            }

            AttachEventHandler(box);
        }

        private static void RemoveEventHandler(RichTextBox box)
        {
            var binding = BindingOperations.GetBinding(box, BoundDocument);

            if (binding != null)
            {
                if (binding.UpdateSourceTrigger is UpdateSourceTrigger.Default or UpdateSourceTrigger.LostFocus)
                {
                    box.LostFocus -= HandleLostFocus;
                }
                else
                {
                    box.TextChanged -= HandleTextChanged;
                }
            }
        }
    }
}
