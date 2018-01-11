using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace StormShellcoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            saveSettings(); // initialize settings with default values
        }

        private void saveSettings()
        {
            Settings.setJoinSeq(this.textBoxSettingsJoinSequence.Text);
            Settings.setAddJoinSequenceToBeginning((bool)this.checkBoxSettingsAddSequenceToBeginning.IsChecked);
            Settings.setUseCapitalLetters((bool)this.checkBoxSettingsUseCapitalLetters.IsChecked);
        }

        private string getBinDirectoryPath()
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            return System.IO.Path.GetDirectoryName(ass.Location);
        }

        private System.Diagnostics.Process createProcess()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.WorkingDirectory = getBinDirectoryPath();
            startInfo.FileName = "nasm";
            startInfo.Arguments = "-o temp/output temp/input_code.asm";
            process.StartInfo = startInfo;

            return process;
        }

        private void buttonAssemble_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userInput = this.textBoxInput.Text;

                FileInfo file = new FileInfo("temp/input_code.asm");
                file.Directory.Create();
                File.WriteAllText(file.FullName, userInput);

                System.Diagnostics.Process process = createProcess();
                process.Start();
                process.WaitForExit();

                try
                {
                    Byte[] output = File.ReadAllBytes("temp/output");
                    this.textBoxOutput.Text = DataManipulation.manipulateOutput(BitConverter.ToString(output));
                }
                catch
                {
                    this.textBoxOutput.Text = "Assembler raised an error.\nPlease check your code.";
                }
            }
            catch (Exception exception)
            {
                this.textBoxOutput.Text = exception.ToString();
            }
        }

        private void buttonCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(this.textBoxOutput.Text);
        }

        private void buttonSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            saveSettings();
        }
    }
}
