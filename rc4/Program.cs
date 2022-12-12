using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Text;
Console.OutputEncoding= System.Text.Encoding.UTF8;
baslangic:
byte[] s = Enumerable.Range(0, 256).Select(i => (byte)i).ToArray();
Console.Write("Hangi işlemi yapmak istiyorsunuz?\n1- Şifreleme\n2- Şifre Çözme\nCevabınız: ");
string secim = Console.ReadLine();
Console.Clear();
Console.Write("Anahtarı giriniz: ");
string anahtar = Console.ReadLine();
byte[] ascii_anahtar = new byte[256];
for (int i = 0; i < 256; i++)
{
    ascii_anahtar[i] = (byte)anahtar[i % anahtar.Length];
}
int j = 0;
for (int i = 0; i < 256; i++)
{
    j = (j + s[i] + ascii_anahtar[i]) & 255;
    var temp = s[i];
    s[i] = s[j];
    s[j] = temp;
}
if (secim == "1")
{
    Console.Write("Şifrelenecek metni giriniz: ");
    string metin = Console.ReadLine();
    int a = 0;
    j = 0;
    byte[] k = new byte[metin.Length];
    for (int i = 0; i < metin.Length; i++)
    {
        a = (a + 1) & 255;
        j = (j + s[a]) & 255;
        var temp = s[a];
        s[a] = s[j];
        s[j] = temp;
        k[i] = s[(s[a] + s[j]) & 255];
    }
    a = 0;
    byte[] sifrelenmis_yazi = new byte[metin.Length];
    while (a < metin.Length)
    {
        sifrelenmis_yazi[a] = (byte)(metin[a] ^ k[a]);
        a++;
    }
    Console.Write(Convert.ToBase64String(sifrelenmis_yazi));
}
else if (secim == "2")
{
    int a = 0;
    Console.Write("Çözülecek şifreyi girin: ");
    string kod = Console.ReadLine();
    byte[] sifrelenmis_yazi = Convert.FromBase64String(kod); //     byte[] sifrelenmis_yazi = Convert.FromBase64String(kod);
    j = 0;
    byte[] k = new byte[sifrelenmis_yazi.Length];

    for (int i = 0; i < sifrelenmis_yazi.Length; i++)
    {
        a = (a + 1) & 255;
        j = (j + s[a]) & 255;
        var temp = s[a];
        s[a] = s[j];
        s[j] = temp;
        k[i] = s[(s[a] + s[j]) & 255];
    }
    a = 0;
    byte[] decoded_arr = new byte[sifrelenmis_yazi.Length];
    while (a < sifrelenmis_yazi.Length)
    {
        decoded_arr[a] = (byte)(sifrelenmis_yazi[a] ^ k[a]);
        Console.Write(decoded_arr[a] + " ");
        a++;
    }
    Console.WriteLine();
    Console.Write(Encoding.UTF8.GetString(decoded_arr));
}

Console.Write("\n1- Başa dön\n2- Çıkış Yap\nSeçiminiz: ");
string secim_son = Console.ReadLine();

if (secim_son == "1")
{
    Console.Clear();
    goto baslangic;
}
else Environment.Exit(0);