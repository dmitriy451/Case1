using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using Newtonsoft.Json;

namespace Vending
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WebClient web = new WebClient();
            var json = web.DownloadString("http://localhost:64054/api/Drinks/1");
            List<Drink> Drinks = JsonConvert.DeserializeObject<List<Drink>>(json);
            
            LViewDrinks.ItemsSource = Drinks;

            List<Coins> coins = new List<Coins>();
            web = new WebClient();
            json = web.DownloadString("http://localhost:64054/api/VendingMachineCoins1/1");
            coins = JsonConvert.DeserializeObject<List<Coins>>(json);
            Denom_1_act.IsEnabled = Convert.ToBoolean(coins.First(p => p.Denomination == 1).IsActive);
            Denom_2_act.IsEnabled = Convert.ToBoolean(coins.First(p => p.Denomination == 2).IsActive);
            Denom_5_act.IsEnabled = Convert.ToBoolean(coins.First(p => p.Denomination == 5).IsActive);
            Denom_10_act.IsEnabled = Convert.ToBoolean(coins.First(p => p.Denomination == 10).IsActive);

            var timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();

            
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            WebClient web = new WebClient();
            var json = web.DownloadString("http://localhost:64054/api/Drinks/1");
            List<Drink> Drinks = JsonConvert.DeserializeObject<List<Drink>>(json);

            LViewDrinks.ItemsSource = Drinks;

            List<Coins> coins = new List<Coins>();
            web = new WebClient();
            json = web.DownloadString("http://localhost:64054/api/VendingMachineCoins1/1");
            coins = JsonConvert.DeserializeObject<List<Coins>>(json);
            Denom_1_act.IsEnabled = Convert.ToBoolean(coins.First(p => p.Denomination == 1).IsActive);
            Denom_2_act.IsEnabled = Convert.ToBoolean(coins.First(p => p.Denomination == 2).IsActive);
            Denom_5_act.IsEnabled = Convert.ToBoolean(coins.First(p => p.Denomination == 5).IsActive);
            Denom_10_act.IsEnabled = Convert.ToBoolean(coins.First(p => p.Denomination == 10).IsActive);
        }

        float Count = 0;
        List<Drink> Drinks = new List<Drink>();
        string Secret_key = "1210"; //51021210
        string Secret_key_input;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Secret_key_input += (sender as Button).Content;
            if(Secret_key_input.Length >= Secret_key.Length)
            {
                if (Secret_key_input == Secret_key)
                {
                    Summa.Text = "";
                    Secret_key_input = "";
                    MessageBox.Show("Вы вошли в админ панель");
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.ShowDialog();
                }
            }
            Count += Convert.ToInt32((sender as Button).Content);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:64054/api/addMonetka?denomination={(sender as Button).Content}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new Coins());

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            Summa.Text = Count.ToString();
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Secret_key_input = "";
            if (Count > 0) {
            MessageBox.Show("Возьмите сдачу: " + Summa.Text);
            Count = 0;
            Summa.Text = Count.ToString();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:64054/api/VendingMachineCoins1?Change={Summa.Text}");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(new Coins());

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Summa.Text = "";
            }
        }

        private void LViewDrinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if (LViewDrinks.SelectedItem != null)
                if (Count >= (LViewDrinks.SelectedItem as Drink).Cost)
                {
                    Count -= (LViewDrinks.SelectedItem as Drink).Cost;
                    Summa.Text = Count.ToString();
                    MessageBox.Show($"Куплен напиток: {(LViewDrinks.SelectedItem as Drink).Name}");
                    (LViewDrinks.SelectedItem as Drink).Count--;
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:64054/api/Drinks?Count={(LViewDrinks.SelectedItem as Drink).Count}");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "PUT";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(LViewDrinks.SelectedItem as Drink);

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    LViewDrinks.UnselectAll();
                }
                else
                {
                    MessageBox.Show("Недостаточно средств");
                    LViewDrinks.UnselectAll();
                }
        }

    }
}
