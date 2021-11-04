using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using EasyJob.Serialization;

namespace EasyJob.Windows
{
    /// <summary>
    /// Interaction logic for TroubleshootingWindow.xaml
    /// </summary>
    public partial class TroubleshootingWindow : Window
    {
        private readonly Config _config;

        public TroubleshootingWindow(Config config)
        {
            InitializeComponent();
            _config = config;
            FillKnownData();
        }

        private void FillKnownData()
        {
            OSDescription.Text = RuntimeInformation.OSDescription;
            FrameworkDescription.Text = RuntimeInformation.FrameworkDescription;
            OsD.Text = RuntimeInformation.OSArchitecture.ToString();

            PowerShellPath.Text = _config.default_powershell_path;

            if (_config.powershell_arguments?.Length == 0)
            {
                PowerShellArguments.Text = "empty";
                PowerShellArguments.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
            }
            else
            {
                PowerShellArguments.Text = _config.powershell_arguments;
            }

            if (File.Exists(_config.default_powershell_path))
            {
                try
                {
                    var versionInfo = FileVersionInfo.GetVersionInfo(_config.default_powershell_path);
                    PowerShellVersion.Text = versionInfo.FileVersion;
                }
                catch
                {
                    PowerShellVersion.Text = "unable to get version";
                    PowerShellVersion.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                }
            }

            if (Directory.Exists(Path.GetPathRoot(Environment.SystemDirectory) + @"Program Files\WindowsPowerShell\Modules"))
            {
                try
                {
                    var dirs = Directory.GetDirectories(
                        Path.GetPathRoot(Environment.SystemDirectory) + @"Program Files\WindowsPowerShell\Modules", "*",
                        SearchOption.TopDirectoryOnly);
                    foreach (var dir in dirs)
                    {
                        PowerShellModules.Text = PowerShellModules.Text + dir + Environment.NewLine;
                    }
                }
                catch
                {
                }
            }

            if (Directory.Exists(Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\System32\WindowsPowerShell\v1.0\Modules"))
            {
                try
                {
                    var dirs = Directory.GetDirectories(
                        Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\System32\WindowsPowerShell\v1.0\Modules", "*",
                        SearchOption.TopDirectoryOnly);
                    foreach (var dir in dirs)
                    {
                        PowerShellModules.Text = PowerShellModules.Text + dir + Environment.NewLine;
                    }
                }
                catch { }
            }

            if (Directory.Exists(Path.GetPathRoot(Environment.SystemDirectory) + @"Program Files (x86)\WindowsPowerShell\Modules"))
            {
                try
                {
                    var dirs = Directory.GetDirectories(
                        Path.GetPathRoot(Environment.SystemDirectory) + @"Program Files (x86)\WindowsPowerShell\Modules", "*",
                        SearchOption.TopDirectoryOnly);
                    foreach (var dir in dirs)
                    {
                        PowerShellModules.Text = PowerShellModules.Text + dir + Environment.NewLine;
                    }
                }
                catch { }
            }
        }

        private void TroubleshootingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                Task.Factory.StartNew(() =>
                {
                    string URL = "https://google.com";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                    request.ContentType = "text/html";
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        MessageBox.Show(reader.ReadToEnd());
                    }
                }).ContinueWith((task) =>
                {
                    // do this on the UI thread once the task has finished..
                }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            */
        }
    }
}
