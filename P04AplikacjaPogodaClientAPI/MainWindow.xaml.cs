using P04AplikacjaPogodaClientAPI.Models;
using P04AplikacjaPogodaClientAPI.Tools;
using System;
using System.Collections.Generic;
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

namespace P04AplikacjaPogodaClientAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            AccuWeatherTool accuWeatherTool = new AccuWeatherTool();
            City[] responce = await accuWeatherTool.GetLocation(txtCity.Text);

            lbData.Items.Clear();
            foreach(var city in responce)
            {
                lbData.Items.Add(city.LocalizedName);
            }
            
        }
    }
}
