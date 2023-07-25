namespace API.Utilities.Handlers;

public class GenerateHandler
{
    private static int lastNik = 11111; // NIK terakhir, ganti dengan NIK terakhir yang sesuai

    public static string Nik(string? nik = null)
    {
        // Tambahkan 1 pada NIK terakhir untuk mendapatkan NIK berikutnya
        lastNik++;

        // Format angka menjadi 5 digit dengan leading zeros jika diperlukan
        string nextSerialFormatted = lastNik.ToString("D5");

        // Buat NIK baru berdasarkan NIK terakhir dan serial berikutnya
        string NIK = nextSerialFormatted;

        return NIK;

    }
}
