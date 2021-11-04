using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using EasyJob.Serialization;
using EasyJob.TabItems;
using EasyJob.Utils;

namespace EasyJob.Windows
{
    /// <summary>
    /// Interaction logic for ReorderActionButtonsDialog.xaml
    /// </summary>
    public partial class ReorderActionButtonsDialog : Window
    {
        public bool changesOccured = false;
        public Config config;
        public int currentTabIndex = 0;
        private ObservableCollection<ActionButton> ActionButtons = null;
        private ObservableCollection<TabData> TabItems = null;

        public ReorderActionButtonsDialog(int _currentTabIndex, Config _config)
        {
            InitializeComponent();
            config = _config;
            currentTabIndex = _currentTabIndex;
            LoadConfig();
        }

        public void LoadConfig()
        {
            try
            {
                MainWindowActionButtonsList.ItemsSource = null;

                TabItems = ConfigUtils.ConvertTabsFromConfigToUi(config);

                var list = TabItems[currentTabIndex].TabActionButtons;
                var collection = new ObservableCollection<ActionButton>(list);

                ActionButtons = collection;

                MainWindowActionButtonsList.ItemsSource = ActionButtons;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool SaveConfig()
        {
            if (File.Exists(ConfigUtils.ConfigJsonPath))
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
                SaveConfig();
            }

            return false;
        }

        private void ActionButtonsRedorderDown_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindowActionButtonsList.SelectedIndex == -1)
            {
                MessageBox.Show("Please select item to reorder");
                return;
            }

            var selectedIndex = MainWindowActionButtonsList.SelectedIndex;

            if (selectedIndex + 1 < ActionButtons.Count)
            {
                var itemToMoveDown = ActionButtons[selectedIndex];
                ActionButtons.RemoveAt(selectedIndex);
                ActionButtons.Insert(selectedIndex + 1, itemToMoveDown);
                MainWindowActionButtonsList.SelectedIndex = selectedIndex + 1;

                var myList = new List<ActionButton>(ActionButtons);
                TabItems[currentTabIndex].TabActionButtons = myList;
            }

            changesOccured = true;

            SaveConfig();
        }

        private void ActionButtonsReorderUp_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindowActionButtonsList.SelectedIndex == -1)
            {
                MessageBox.Show("Please select item to reorder");
                return;
            }

            var selectedIndex = MainWindowActionButtonsList.SelectedIndex;

            if (selectedIndex > 0)
            {
                var itemToMoveUp = ActionButtons[selectedIndex];
                ActionButtons.RemoveAt(selectedIndex);
                ActionButtons.Insert(selectedIndex - 1, itemToMoveUp);
                MainWindowActionButtonsList.SelectedIndex = selectedIndex - 1;
                var myList = new List<ActionButton>(ActionButtons);
                TabItems[currentTabIndex].TabActionButtons = myList;
            }

            changesOccured = true;

            SaveConfig();
        }
    }
}
