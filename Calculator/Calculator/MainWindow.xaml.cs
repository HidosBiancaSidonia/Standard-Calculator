using System;
using System.Linq;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Diagnostics;
using System.IO;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Double result = 0;
        String operation = "";
        bool enter_value = false;
        Double memory;
        System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();

        public MainWindow()
        {
            InitializeComponent();

            this.WindowState = WindowState.Normal;
            notifyIcon.Visible = false;
            notifyIcon.Icon = null;

            btnMC.IsEnabled = false;
            btnMR.IsEnabled = false;

            focus();
        }


        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void focus()
        {
            textBox1.Focus();
            Keyboard.Focus(textBox1);
        }
        private void numbers_Only(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            focus();
            if ((textBox1.Text == "0") || (enter_value))
                textBox1.Text = "";
            enter_value = false;

            if (b.Content.ToString() == ".")
            {
                if (!textBox1.Text.Contains("."))
                {
                    textBox1.Text = textBox1.Text + b.Content;
                }
            }
            else
            {
                textBox1.Text = textBox1.Text + b.Content;
            }
            digit_Grouping();
            focus();
        }

        private void operator_Click(object sender, EventArgs e)
        {
            focus();
            Button b = (Button)sender;
           
            if (result != 0)
            {
                btnEgal_Click(sender, e);
                enter_value = true;
                operation = b.Content.ToString();
                result = Double.Parse(textBox1.Text);
            }
            else
            {
                operation = b.Content.ToString();
                try {
                    result = Double.Parse(textBox1.Text);
                    textBox1.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
               
            }
            
            digit_Grouping();
        }

        private void btnEgal_Click(object sender, EventArgs e)
        {
            focus();
            switch (operation)
            {
                case "+":
                    textBox1.Text = (result + Double.Parse(textBox1.Text)).ToString();
                    break;
                case "-":
                    textBox1.Text = (result - Double.Parse(textBox1.Text)).ToString();
                    break;
                case "*":
                    textBox1.Text = (result * Double.Parse(textBox1.Text)).ToString();
                    break;
                case "/":
                    if (Double.Parse(textBox1.Text) == 0)
                        MessageBox.Show("Cannot divide by 0!", "Error");
                    else
                        textBox1.Text = (result / Double.Parse(textBox1.Text)).ToString();
                    break;

                default:
                    break;
            }
            result = result * Double.Parse(textBox1.Text);
            operation = "";
            digit_Grouping();
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            focus();
            textBox1.Text = "0";
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            focus();
            textBox1.Text = "0";
            result = 0;
        }


        private void btnDEL_Click(object sender, EventArgs e)
        {
            focus();
            if (textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
            }
            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
        }

        private void btnPlusMinus_Click(object sender, RoutedEventArgs e)
        {
            focus();
            if (!textBox1.Text.Contains('-'))
            {
                textBox1.Text = '-' + textBox1.Text;
            }
            else
            {
                textBox1.Text = textBox1.Text.Trim('-');
            }
        }

        private void btnSQRT_Click(object sender, EventArgs e)
        {
            focus();
            double sq = Double.Parse(textBox1.Text);
            sq = Math.Sqrt(sq);
            textBox1.Text = System.Convert.ToString(sq);
            digit_Grouping();
        }

        private void btnXpatrat_Click(object sender, RoutedEventArgs e)
        {
            focus();
            Double a;
            a = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(textBox1.Text);
            textBox1.Text = System.Convert.ToString(a);
            digit_Grouping();
        }

        private void btnFractie_Click(object sender, RoutedEventArgs e)
        {
            focus();
            Double a;
            a = Convert.ToDouble(1.0 / Convert.ToDouble(textBox1.Text));
            textBox1.Text = System.Convert.ToString(a);
            digit_Grouping();
        }

        private void btnProcent_Click(object sender, RoutedEventArgs e)
        {
            if (Double.Parse(textBox1.Text) == 0 || string.IsNullOrEmpty(textBox1.Text))
                MessageBox.Show("Cannot divide by 0!", "Error");
            focus();
            Double a;
            a = Convert.ToDouble(textBox1.Text) / Convert.ToDouble(100);
            textBox1.Text = System.Convert.ToString(a);
            digit_Grouping();
        }

        private void btnMS_Click(object sender, RoutedEventArgs e)
        {
            focus();
            memory = Double.Parse(textBox1.Text);
            btnMC.IsEnabled = true;
            btnMR.IsEnabled = true;
        }

        private void btnMR_Click(object sender, RoutedEventArgs e)
        {
            focus();
            textBox1.Text = memory.ToString();
        }

        private void btnMC_Click(object sender, RoutedEventArgs e)
        {
            focus();
            textBox1.Text = "0";
            memory = 0;
            btnMR.IsEnabled = false;
            btnMC.IsEnabled = false;
        }

        private void btnMplus_Click(object sender, RoutedEventArgs e)
        {
            focus();
            memory += Double.Parse(textBox1.Text);
        }

        private void btncMminus_Click(object sender, RoutedEventArgs e)
        {
            focus();
            memory -= Double.Parse(textBox1.Text);
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Paste();
            string verificare = textBox1.Text;
            if (verificare.All(char.IsDigit) == false)
            {
                textBox1.Text = "0";
                MessageBox.Show("Only digits are accepted!", "Error");

            }
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Cut();
            textBox1.Text = "0";
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Copy();
        }

        private void textBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    btnEgal_Click(sender, e);
                    break;

                case Key.Escape:
                    btnC_Click(sender, e);
                    break;

                case Key.D0:
                    numbers_Only(btn0, e);
                    break;
                case Key.D1:
                    numbers_Only(btn1, e);
                    break;
                case Key.D2:
                    numbers_Only(btn2, e);
                    break;
                case Key.D3:
                    numbers_Only(btn3, e);
                    break;
                case Key.D4:
                    numbers_Only(btn4, e);
                    break;
                case Key.D5:
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                        btnProcent_Click(sender, e);
                    else
                        numbers_Only(btn5, e);
                    break;
                case Key.D6:
                    numbers_Only(btn6, e);
                    break;
                case Key.D7:
                    numbers_Only(btn7, e);
                    break;
                case Key.D8:

                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                        operator_Click(btnInmultire, e);
                    else
                        numbers_Only(btn8, e);
                    break;
                case Key.D9:
                    numbers_Only(btn9, e);
                    break;
                case Key.OemMinus:
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                        operator_Click(btnMinus, e);
                    else
                        operator_Click(btnMinus, e);
                    break;
                case Key.OemPlus:
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                        operator_Click(btnPlus, e);
                    else
                        btnEgal_Click(sender, e);
                    break;
                case Key.Divide:
                    operator_Click(btnDivide, e);
                    break;
                case Key.OemPeriod:
                    numbers_Only(btnPunct, e);
                    break;
                case Key.OemQuestion:
                    operator_Click(btnDivide, e);
                    break;
                case Key.Back:
                    btnDEL_Click(sender, e);
                    break;
                default:
                    break;

            }
            digit_Grouping();
        }



        private void procesoare_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"The number of processors in the system is : {Environment.ProcessorCount.ToString()} .", "Processors");
        }

        private void programator_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The programmer who created the application is :  \n\t  HIDOS BIANCA-SIDONIA .", "Programmer");
        }

        private void versiuneSO_Click(object sender, RoutedEventArgs e)
        {
            OperatingSystem os_info = System.Environment.OSVersion;

            MessageBox.Show($"The operating system version is : {os_info.Version.ToString()}" + $"\n\nEdition: {OS_Name()}", "Operating System Version");
        }
        public static string OS_Name()
        {
            return (string)(from x in new ManagementObjectSearcher(
                "SELECT Caption FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                            select x.GetPropertyValue("Caption")).FirstOrDefault();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized && notifyIcon.Icon == null)
            {
                this.WindowState = WindowState.Minimized;
                notifyIcon.Visible = true;
                notifyIcon.Icon = new System.Drawing.Icon("calculator.ico");
                notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_DoubleClick);
                notifyIcon.ShowBalloonTip(200, "Calculator", "The calculator is active in SystemTray", System.Windows.Forms.ToolTipIcon.Info);

                System.Windows.Forms.ContextMenu notifyIconContextMenu = new System.Windows.Forms.ContextMenu();

                notifyIconContextMenu.MenuItems.Add("Close", new EventHandler(Close));

                notifyIcon.ContextMenu = notifyIconContextMenu;

            }
            if (this.WindowState == WindowState.Normal)
            {
                notifyIcon.Visible = false;
                notifyIcon.Icon = null;
            }
        }
        private void Close(object sender, EventArgs e)
        {
            this.Close();
        }
        private void notifyIcon_DoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
                this.WindowState = WindowState.Normal;

            this.Activate();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            focus();
        }

        private void Window_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.digitGrouping = digitGrouping.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        private void digitGrouping_Loaded(object sender, RoutedEventArgs e)
        {
            digitGrouping.IsChecked = Properties.Settings.Default.digitGrouping;
        }

        void digit_Grouping()
        {
            if (digitGrouping.IsChecked == true)
            {
                if (!String.IsNullOrEmpty(textBox1.Text) && !textBox1.Text.Contains("."))
                {
                    string s=System.Globalization.CultureInfo.CurrentCulture.Name;
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(s);
                    Double text = Double.Parse(textBox1.Text);
                    textBox1.Text = text.ToString("##,#", culture);
                    digitGrouping.IsChecked = true;
                }
            }
            else
            {
                var charsToRemove = new string[] { "," };
                foreach (var c in charsToRemove)
                {
                    textBox1.Text = textBox1.Text.Replace(c, string.Empty);
                }
            }
        }

        private void digitGrouping_Unchecked(object sender, RoutedEventArgs e)
        {
            var charsToRemove = new string[] { "," };
            foreach (var c in charsToRemove)
            {
                textBox1.Text = textBox1.Text.Replace(c, string.Empty);
            }
        }

        private void digitGrouping_Checked(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !textBox1.Text.Contains("."))
            {
                string s = System.Globalization.CultureInfo.CurrentCulture.Name;
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(s);
                Double text = Double.Parse(textBox1.Text);
                textBox1.Text = text.ToString("##,#", culture);
                digitGrouping.IsChecked = true;
            }
        }

        private void textBox1_TextInput(object sender, TextCompositionEventArgs e)
        {

            if (digitGrouping.IsChecked == true)
            {
                if (!String.IsNullOrEmpty(textBox1.Text) && !textBox1.Text.Contains("."))
                {
                    string s = System.Globalization.CultureInfo.CurrentCulture.Name;
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(s);
                    Double text = Double.Parse(textBox1.Text);
                    textBox1.Text = text.ToString("##,#", culture);
                    digitGrouping.IsChecked = true;
                }
            }
            else
            {
                var charsToRemove = new string[] { "," };
                foreach (var c in charsToRemove)
                {
                    textBox1.Text = textBox1.Text.Replace(c, string.Empty);
                }
            }
        }
    }
}
