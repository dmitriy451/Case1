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
    /// Логика взаимодействия для AddDrink.xaml
    /// </summary>
    public partial class AddDrink : Page
    {
        public AddDrink(Drink drink)
        {
            InitializeComponent();
            ActualDrink = drink;
            this.DataContext = ActualDrink;
            
        }
        Drink ActualDrink;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WebClient web = new WebClient();
            if (ActualDrink.Id == 0)
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:64054/api/Drinks?Count={ActualDrink.Count}");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(ActualDrink);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Terminal.frame.GoBack();
            }
            else
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:64054/api/Drinks?Count={ActualDrink.Count}");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(ActualDrink);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Terminal.frame.GoBack();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(ActualDrink.Id != 0)
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:64054/api/Drinks/{ActualDrink.Id}");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "DELETE";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Terminal.frame.GoBack();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Terminal.frame.GoBack();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ImageFileDialog = new Microsoft.Win32.OpenFileDialog();
            ImageFileDialog.FileName = "Фото";
            ImageFileDialog.DefaultExt = ".png";
            ImageFileDialog.Filter = "Image files (.png)|*.png";
            Nullable<bool> result = ImageFileDialog.ShowDialog();
            if (result == true)
            {
                BlockFileName.Text = ImageFileDialog.FileName;
                ActualDrink.Image = File.ReadAllBytes(ImageFileDialog.FileName);
                
                this.DataContext = null;
                this.DataContext = ActualDrink;
            }
        }
    }
}