using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using EasyJob.Serialization;
using EasyJob.TabItems;
using EasyJob.Utils;
using Newtonsoft.Json;

namespace EasyJob.Windows
{
    /// <summary>
    /// Interaction logic for NewTabDialog.xaml
    /// </summary>
    public partial class NewTabDialog : Window
    {
        public Config config;
        public string configJson = "";
        private ObservableCollection<TabData> TabItems = null;

        public NewTabDialog()
        {
            InitializeComponent();
            LoadConfig();
        }

        public void LoadConfig()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "config.json"))
            {
                try
                {
                    configJson = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "config.json");
                    config = JsonConvert.DeserializeObject<Config>(configJson);

                    TabItems = ConfigUtils.ConvertTabsFromConfigToUi(config);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("File " + AppDomain.CurrentDomain.BaseDirectory + "config.json does not exist.");
            }
        }

        public bool SaveConfig()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "config.json";
            if (File.Exists(path))
            {
                try
                {
                    config.tabs.Clear();
                    config.tabs = ConfigUtils.ConvertTabsFromUiToConfig(TabItems);

                    if (ConfigUtils.SaveFromConfigToFile(config) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                //SaveConfig();
            }

            return false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => DialogResult = false;

        private void CreateNewTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CreateNewTabTextBox.Text))
            {
                var tabData = new TabData(CreateNewTabTextBox.Text);
                TabItems.Add(tabData);

                if (SaveConfig())
                {
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Error trying to save added Tab.");
                }
            }
            else
            {
                MessageBox.Show("Tab header name should not be empty.");
            }
        }
    }
}
