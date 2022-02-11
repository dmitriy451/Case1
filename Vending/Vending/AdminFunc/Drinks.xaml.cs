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
    /// Логика взаимодействия для Drinks.xaml
    /// </summary>
    public partial class Drinks : Page
    {
        public Drinks()
        {
            InitializeComponent();
            WebClient web = new WebClient();
            var json = web.DownloadString("http://localhost:64054/api/Drinks/1");
            List<Drink> Drinks = JsonConvert.DeserializeObject<List<Drink>>(json);
            json = null;
            web = null;
            Drinks.Add(new Drink() { Cost = 0, Image = File.ReadAllBytes("../../Resources/Add.png"), Count = 0, Id = 0, Name = "" });
            LViewDrinks.ItemsSource = Drinks;
        }

        private void LViewDrinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LViewDrinks.SelectedItem != null)
            {
                Terminal.frame.Navigate(new AddDrink(LViewDrinks.SelectedItem as Drink));
                LViewDrinks.UnselectAll();
            }  
        }
    }
}
