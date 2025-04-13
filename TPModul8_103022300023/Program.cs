using System;

class Program
{
    static void Main()
    {
        var config = CovidConfig.LoadFromFile("covid_config.json");

        Console.Write($"Berapa suhu badan anda saat ini? Dalam nilai {config.SatuanSuhu}: ");
        double suhu = Convert.ToDouble(Console.ReadLine());

        Console.Write("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam? ");
        int hariDemam = Convert.ToInt32(Console.ReadLine());

        bool suhuValid = false;

        if ((config.SatuanSuhu == "celcius" && suhu >= 36.5 && suhu <= 37.5) ||
           (config.SatuanSuhu == "fahrenheit" && suhu >= 97.7 && suhu <= 99.5))
        {
            if (hariDemam < config.BatasHariDemam)
            {
                Console.WriteLine(config.PesanDiterima);
            }
            else
            {
                Console.WriteLine(config.PesanDitolak);
            }
        }
        else
        {
            Console.WriteLine(config.PesanDitolak);
        }

    }
}
