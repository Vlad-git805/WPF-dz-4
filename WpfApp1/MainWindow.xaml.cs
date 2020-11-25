using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
        }

        int count_time = 0;
        int count_of_mistaces = 0;

        private void Timer_Tick(object sender, EventArgs e)
        {
            count_time++;
            Label_time.Content = count_time;
        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool check = true;

            if (radioButto1.IsChecked == true)
            {
                if (Text.Text.Length != 0)
                {
                    if (Text.Text[Text.Text.Length - 1] != text_main.Text[Text.Text.Length - 1])
                    {
                        count_of_mistaces++;
                        count_mistake.Content = count_of_mistaces;
                        Text.Foreground = Brushes.Red;
                        check = false;
                    }
                }
            }
            else if (radioButto2.IsChecked == true)
            {
                if (Text.Text.Length != 0)
                {
                    if (Text.Text[Text.Text.Length - 1] - 32 != text_main.Text[Text.Text.Length - 1] && Text.Text[Text.Text.Length - 1] + 32 != text_main.Text[Text.Text.Length - 1] && Text.Text[Text.Text.Length - 1] != text_main.Text[Text.Text.Length - 1])
                    {
                        count_of_mistaces++;
                        count_mistake.Content = count_of_mistaces;
                        Text.Foreground = Brushes.Red;
                        check = false;
                    }
                }
            }

            if (check)
            {
                progres.Value = Text.Text.Length;
                Text.Foreground = Brushes.Green;
                if (Text.Text.Length == text_main.Text.Length - 1)
                {
                    timer.Stop();
                    MessageBox.Show($"Time: {Label_time.Content} sec\nletters: {Text.Text.Length} \nmistakes {count_of_mistaces.ToString()}\naverage speed: { (float)Text.Text.Length / (int)Label_time.Content }");
                    radioButto1.IsEnabled = true;
                    radioButto2.IsEnabled = true;
                    radioButto1.IsChecked = false;
                    radioButto2.IsChecked = false;
                    slider.IsEnabled = true;
                    slider.Value = 0;
                    Text.Text = "";
                    text_main.Text = "";
                    Label_time.Content = 0;
                    count_mistake.Content = 0;
                    count_time = 0;
                    count_of_mistaces = 0;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (radioButto1.IsChecked == true || radioButto2.IsChecked == true)
            {
                radioButto1.IsEnabled = false;
                radioButto2.IsEnabled = false;
                slider.IsEnabled = false;
                timer.Start();
                text_main.Text = "";
                using (StreamReader fout = new StreamReader(@"D:\ШАГ\WPF\dz\dz 4\WpfApp1\WpfApp1\text.txt"))
                {
                    for (int i = 0; i < slider.Value; i++)
                    {
                        text_main.Text += fout.ReadLine();
                    }
                }
                progres.Maximum = text_main.Text.Length - 1;
            }
        }
    }
}
