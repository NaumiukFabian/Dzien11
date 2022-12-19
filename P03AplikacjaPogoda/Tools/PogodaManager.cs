using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P03AplikacjaPogoda.Tools
{
   
    public class PogodaManager
    {
        private const string szablon = "<div class=\"BNeawe iBp4i AP7Wnd\">(-{0,1}\\d{1,3}).C<\\/div>";
        private const string adresBase = "https://www.google.com/search?q=pogoda";
        public int PodajTemp(string miasto)
        {
            string adres = $"{adresBase} {miasto}";

            WebClient wc = new WebClient();
            string dane = wc.DownloadString(adres);

            //File.WriteAllText("strona.html", dane);

            
            Regex rx = new Regex(szablon);
            Match match = rx.Match(dane);

            string wynik = match.Groups[1].Value;

            return Convert.ToInt32(wynik);
        }
    }
}
