using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using EasyJob.Serialization;
using EasyJob.Serialization.AnswerDialog;
using EasyJob.TabItems;
using Newtonsoft.Json;

namespace EasyJob.Utils
{
    public static class ConfigUtils
    {
        public static string ConfigJsonPath = AppContext.BaseDirectory + "config.json";

        public static ObservableCollection<TabData> ConvertTabsFromConfigToUi(Config config)
        {
            var tabs = new ObservableCollection<TabData>();

            foreach (var configTab in config.tabs)
            {
                var actionButtons = new List<ActionButton>();

                foreach (var configButton in configTab.Buttons)
                {
                    var configArguments = new List<Answer>();
                    foreach (var configArgument in configButton.Arguments)
                    {
                        configArguments.Add(new Answer { AnswerQuestion = configArgument.ArgumentQuestion, AnswerResult = configArgument.ArgumentAnswer });
                    }

                    actionButtons.Add(new ActionButton { ID = configButton.Id, ButtonText = configButton.Text, ButtonDescription = configButton.Description, ButtonScript = configButton.Script, ButtonScriptPathType = configButton.ScriptPathType, ButtonScriptType = configButton.ScriptType, ButtonArguments = configArguments });
                }

                tabs.Add(new TabData { ID = configTab.ID, TabHeader = configTab.Header, ConsoleBackground = config.console_background, ConsoleForeground = config.console_foreground, TabActionButtons = actionButtons, TabTextBoxText = "" });
            }

            return tabs;
        }

        /// <summary>
        /// Saves the configs.
        /// </summary>
        /// <param name="tabs">The tabs.</param>
        public static List<ConfigTab> ConvertTabsFromUiToConfig(IEnumerable<TabData> tabs)
        {
            var configTabs = new List<ConfigTab>();

            foreach (var tab in tabs)
            {
                var buttons = new List<ConfigButton>();
                foreach (var button in tab.TabActionButtons)
                {
                    var configArguments = new List<ConfigArgument>();
                    foreach (var answer in button.ButtonArguments)
                    {
                        configArguments.Add(new ConfigArgument(answer.AnswerQuestion, answer.AnswerResult));
                    }

                    buttons.Add(new ConfigButton(button.ID, button.ButtonText, button.ButtonDescription, button.ButtonScript, button.ButtonScriptPathType, button.ButtonScriptType, configArguments));
                }

                configTabs.Add(new ConfigTab(tab.ID, tab.TabHeader, buttons));
            }

            return configTabs;
        }

        public static bool SaveFromConfigToFile(Config config)
        {
            try
            {
                var configJson = JsonConvert.SerializeObject(config);
                File.WriteAllText(ConfigJsonPath, configJson, Encoding.UTF8);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
