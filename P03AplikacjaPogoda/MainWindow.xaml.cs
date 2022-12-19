using P03AplikacjaPogoda.Tools;
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

namespace P03AplikacjaPogoda
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

        //scenariusz 1 wywołanie async z czekaniem
        private async void btnWczytajPogode_Click(object sender, RoutedEventArgs e)
        {
            PogodaManager mp = new PogodaManager();

            string miasto = txtMiasto.Text;
            string[] miasta = miasto.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            var t = await Task.Run<int>(() =>
            {
                int temp = mp.PodajTemp(miasta[0]);
                return temp;
            });

            // wykona się dopiero wtedy gdy cała metoda anonimowa zdefiniowana powyżej wykona się w całości - await
            lblKomunikaty.Content += $"Procesuję miasto: {miasta[0]} \n";
            lbWynik.Items.Add($"Temperatura w mieście {miasta[0]} wynosi {t}");

            //foreach(var x in miasta)
            //{
            //    lblKomunikaty.Content += $"Procesuję miasto: {x} \n";
            //    int temp = mp.PodajTemp(x);
            //    lbWynik.Items.Add($"Temperatura w mieście {x} wynosi {temp}");
            //}
                      
        }

        //scenariusz 2 wywołanie bez czekania
        private async void btnWczytajPogode2_Click(object sender, RoutedEventArgs e)
        {
            PogodaManager mp = new PogodaManager();

            string miasto = txtMiasto.Text;
            string[] miasta = miasto.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            var t = Task.Run<int>(() =>
            {
                int temp = mp.PodajTemp(miasta[0]);
                return temp;
            });

            lblKomunikaty.Content += $"Procesuję miasto: {miasta[0]} \n";

            t.GetAwaiter().OnCompleted(() =>
            {
                lbWynik.Items.Add($"Temperatura w mieście {miasta[0]} wynosi {t.Result}");
            });

            await Task.CompletedTask;
        }

        //Wywołanie asynchroniczne wiele miasto jedno po drugim
        private async void btnWczytajPogode3_Click(object sender, RoutedEventArgs e)
        {
            PogodaManager mp = new PogodaManager();

            string miasto = txtMiasto.Text;
            string[] miasta = miasto.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach(var m in miasta)
            {
                var t = await Task.Run<int>(() =>
                {
                    int temp = mp.PodajTemp(m);
                    return temp;
                });

                lblKomunikaty.Content += $"Procesuję miasto: {m} \n";
                lbWynik.Items.Add($"Temperatura w mieście {m} wynosi {t}");
            }
        }

        //Scenariusz wywołanie async (wszystkie miasta wywołują się niezależnie i równolegle) ale czekam aż
        //wszystkie zadania się wykonają i dopiero po wszystkich zadaniach podaje wynik
        private async void btnWczytajPogode4_Click(object sender, RoutedEventArgs e)
        {
            //PogodaManager mp = new PogodaManager();

            string miasto = txtMiasto.Text;
            string[] miasta = miasto.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<Task> zadania = new List<Task>();

            foreach (var m in miasta)
            {
                //var t = Task.Run<object>(() =>
                //{
                //    int temp = mp.PodajTemp(m);
                //    return new { NazwaMiasta = m, Temp = temp};
                //});
                var t = Task.Run(() => PodajTemp2(m));
                
                zadania.Add(t);
            }

            await Task.WhenAll(zadania);

            foreach(Task<dynamic> t in zadania)
            {
                lblKomunikaty.Content += $"Procesuję miasto: {t.Result.NazwaMiasta} \n";
                lbWynik.Items.Add($"Temperatura w mieście {t.Result.NazwaMiasta} wynosi {t.Result.Temp}");
            }

        }

        private async Task<(string NazwaMiasta, int Temp)> PodajTemp2 (string miasto)
        {
            PogodaManager mp = new PogodaManager();
            var temp = mp.PodajTemp(miasto);

            return await Task.FromResult((miasto, temp));
        }

        private void btnWczytajPogode5_Click(object sender, RoutedEventArgs e)
        {
            //PogodaManager mp = new PogodaManager();

            string miasto = txtMiasto.Text;
            string[] miasta = miasto.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            pbPostep.Maximum = miasta.Length;
            pbPostep.Value = 0;

            lblKomunikaty.Content = "";
            lbWynik.Items.Clear();

            foreach(var m in miasta)
            {
                //var t = Task.Run<int>(() =>
                //{
                //    int temp = mp.PodajTemp(m);
                //    return temp;
                //});

                var t = Task.Run(() => PodajTemperature(m));

                lblKomunikaty.Content += $"Procesuję miasto: {m} \n";

                t.GetAwaiter().OnCompleted(() =>
                {
                   lbWynik.Items.Add($"Temperatura w mieście {m} wynosi {t.Result}");
                    pbPostep.Value = +1;
                });
            }
        }

        private async Task<int> PodajTemperature(string miasto)
        {
            PogodaManager pogodaManager = new PogodaManager();
            int wynik = pogodaManager.PodajTemp(miasto);

            return await Task.FromResult(wynik);
        }
        private async void btnWczytajPogode6_Click(object sender, RoutedEventArgs e)
        {
            PogodaManager mp = new PogodaManager();

            string miasto = txtMiasto.Text;
            string[] miasta = miasto.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            pbPostep.Maximum = miasta.Length;
            pbPostep.Value = 0;

            lblKomunikaty.Content = "";
            lbWynik.Items.Clear();

            foreach (var m in miasta)
            {
                lblKomunikaty.Content += $"Procesuję miasto: {m} \n";
                var t = await Task.Run<int>(() =>
                {
                    int temp = mp.PodajTemp(m);
                    return temp;
                });

                lbWynik.Items.Add($"Temperatura w mieście {m} wynosi {t}");
                pbPostep.Value += 1;
            }

        }
    }
}
