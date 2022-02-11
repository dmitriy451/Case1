using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Vending
{
    /// <summary>
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WebClient web = new WebClient();
            var json = web.DownloadString("http://localhost:64054/api/VendingMachines/1");
            var vending = JsonConvert.DeserializeObject<VendingMachines>(json);
            if (pass.Text == vending.SecretCode)
            {
                MessageBox.Show("Вы вошли в админ панель");
                AdminPanel adminPanel = new AdminPanel();
                adminPanel.ShowDialog();
            }
        }
    }
}
