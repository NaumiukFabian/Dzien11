// See https://aka.ms/new-console-template for more information


using System.Net;
using System.Text.RegularExpressions;

while (true)
{

    Console.WriteLine("Podaj nazwe miasta");
    string miasto = Console.ReadLine();
    string adres = $"https://www.google.com/search?q=pogoda {miasto}";

    WebClient wc = new WebClient();
    string dane = wc.DownloadString(adres);

    //File.WriteAllText("strona.html", dane);

    string szablon = "<div class=\"BNeawe iBp4i AP7Wnd\">(-{0,1}\\d{1,3}).C<\\/div>";
    Regex rx = new Regex(szablon);
    Match match = rx.Match(dane);

    string wynik = match.Groups[1].Value;




    Console.WriteLine(wynik);
}
