using System;
using System.IO;
using System.Text.Json;

public class CovidConfig
{
    public string SatuanSuhu { get; set; } = "celcius";
    public int BatasHariDemam { get; set; } = 14;
    public string PesanDitolak { get; set; } = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
    public string PesanDiterima { get; set; } = "Anda dipersilahkan untuk masuk ke dalam gedung ini";

    public static CovidConfig LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File konfigurasi tidak ditemukan. Menggunakan nilai default.");
            return new CovidConfig();
        }

        try
        {
            string json = File.ReadAllText(filePath);
            JsonDocument doc = JsonDocument.Parse(json);
            CovidConfig config = new CovidConfig();

            if (doc.RootElement.TryGetProperty("satuan_suhu", out JsonElement suhu))
                config.SatuanSuhu = suhu.GetString();

            if (doc.RootElement.TryGetProperty("batas_hari_demam", out JsonElement batas))
                config.BatasHariDemam = batas.GetInt32();

            if (doc.RootElement.TryGetProperty("pesan_ditolak", out JsonElement ditolak))
                config.PesanDitolak = ditolak.GetString();

            if (doc.RootElement.TryGetProperty("pesan_diterima", out JsonElement diterima))
                config.PesanDiterima = diterima.GetString();

            return config;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Gagal membaca file config: {ex.Message}");
            return new CovidConfig();
        }
    }

    public void UbahSatuan()
    {
        if (SatuanSuhu.ToLower() == "celcius")
            SatuanSuhu = "fahrenheit";
        else
            SatuanSuhu = "celcius";
    }
}
