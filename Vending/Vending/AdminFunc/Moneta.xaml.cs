using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace Vending.AdminFunc
{
    /// <summary>
    /// Логика взаимодействия для Moneta.xaml
    /// </summary>
    public partial class Moneta : Page
    {
        public Moneta()
        {
            InitializeComponent();
            //Terminal.frame.
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            coins.First(p => p.Denomination == 1).Count = Convert.ToInt32(Denom_1.Text);
            coins.First(p => p.Denomination == 2).Count = Convert.ToInt32(Denom_2.Text);
            coins.First(p => p.Denomination == 5).Count = Convert.ToInt32(Denom_5.Text);
            coins.First(p => p.Denomination == 10).Count = Convert.ToInt32(Denom_10.Text);

            coins.First(p => p.Denomination == 1).IsActive = Convert.ToInt32( Denom_1_act.IsChecked );
            coins.First(p => p.Denomination == 2).IsActive = Convert.ToInt32(Denom_2_act.IsChecked);
            coins.First(p => p.Denomination == 5).IsActive = Convert.ToInt32(Denom_5_act.IsChecked);
            coins.First(p => p.Denomination == 10).IsActive = Convert.ToInt32(Denom_10_act.IsChecked);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:64054/api/VendingMachineCoins1");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(coins);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        }
        List<Coins> coins = new List<Coins>();
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WebClient web = new WebClient();
            var json = web.DownloadString("http://localhost:64054/api/VendingMachineCoins1/1");
            coins = JsonConvert.DeserializeObject<List<Coins>>(json);
            json = null;
            web = null;
            Denom_1.Text = coins.First(p => p.Denomination == 1).Count.ToString();
            Denom_2.Text = coins.First(p => p.Denomination == 2).Count.ToString();
            Denom_5.Text = coins.First(p => p.Denomination == 5).Count.ToString();
            Denom_10.Text = coins.First(p => p.Denomination == 10).Count.ToString();
            Denom_1_act.IsChecked = Convert.ToBoolean(coins.First(p => p.Denomination == 1).IsActive);
            Denom_2_act.IsChecked = Convert.ToBoolean(coins.First(p => p.Denomination == 2).IsActive);
            Denom_5_act.IsChecked = Convert.ToBoolean(coins.First(p => p.Denomination == 5).IsActive);
            Denom_10_act.IsChecked = Convert.ToBoolean(coins.First(p => p.Denomination == 10).IsActive);
        }
    }
}
