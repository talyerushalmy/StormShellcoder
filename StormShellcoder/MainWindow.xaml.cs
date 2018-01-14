using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace StormShellcoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Brush defaultButtonBackground;

        public MainWindow()
        {
            InitializeComponent();
            cleanTempFolder();
            saveSettings(); // initialize settings with default values
            this.defaultButtonBackground = this.buttonCheckNullByte.Background;
        }

        private void saveSettings()
        {
            // output format related settings
            Settings.setJoinSeq(this.textBoxSettingsJoinSequence.Text);
            Settings.setAddJoinSequenceToBeginning((bool)this.checkBoxSettingsAddSequenceToBeginning.IsChecked);
            Settings.setUseCapitalLetters((bool)this.checkBoxSettingsUseCapitalLetters.IsChecked);

            // output tests related settings
            Settings.setCheckContainsNullByte((bool)this.checkBoxSettingsCheckNullByte.IsChecked);
        }

        private void performTests(Disassembly disasm)
        {
            if (Settings.getCheckContainsNullByte())
            {
                this.buttonCheckNullByte.Background = Brushes.Green;
                foreach (DisassemblyLine dl in disasm.getLines())
                {
                    if ((DataManipulation.splitEveryNChars(dl.getOpcodes(), 2)).Contains("00"))
                    {
                        this.buttonCheckNullByte.Background = Brushes.Red;
                    }
                }
                this.buttonCheckNullByte.IsEnabled = true;
            }
            else
            {
                this.buttonCheckNullByte.Background = this.defaultButtonBackground;
                this.buttonCheckNullByte.IsEnabled = false;
            }
        }

        private string getBinDirectoryPath()
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            return Path.GetDirectoryName(ass.Location);
        }

        private System.Diagnostics.Process createProcess(string filename, string arguments)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.WorkingDirectory = getBinDirectoryPath();
            startInfo.FileName = filename;
            startInfo.Arguments = arguments;

            // for getting stdout to string
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            process.StartInfo = startInfo;

            return process;
        }

        private void cleanTempFolder()
        {
            DirectoryInfo di = new DirectoryInfo("temp");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private void buttonAssemble_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cleanTempFolder();

                string userInput = this.textBoxInput.Text;

                FileInfo file = new FileInfo("temp/input_code.asm");
                file.Directory.Create();
                File.WriteAllText(file.FullName, userInput);

                System.Diagnostics.Process process_assembly = createProcess("nasm", "-o temp/output temp/input_code.asm");
                process_assembly.Start();
                process_assembly.WaitForExit();

                System.Diagnostics.Process process_disassembly = createProcess("ndisasm", "temp/output");
                process_disassembly.Start();
                process_disassembly.WaitForExit();

                try
                {
                    if (process_disassembly.StandardError.ReadToEnd().Contains("No such file or directory"))
                        throw new Exception();

                    string process_disassembly_output = process_disassembly.StandardOutput.ReadToEnd();
                    Disassembly disasm = new Disassembly(process_disassembly_output);

                    this.textBoxOutput.Text = DataManipulation.manipulateOutput(disasm.getAllOpcodes());
                    performTests(disasm);
                }
                catch (Exception exception)
                {
                    this.textBoxOutput.Text = "Assembler raised an error.\nPlease check your code.\n\n" + exception;
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
