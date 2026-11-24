
using System.ComponentModel;

enum CihazTuru
{
    Lamba,
    Klima,
    GuvenlikSistemi
}

class Cihaz
{
    public string Kimlik{get;set;}
    public string Ad{get;set;}
    public CihazTuru Tur{get;set;}
    public bool AcikMi{get; private set;}

    public Cihaz(string k, string a, CihazTuru t)
    {
        if (string.IsNullOrWhiteSpace(k))
            throw new ArgumentException("kimlik boş olamaz");
        if (string.IsNullOrWhiteSpace(a))
            throw new ArgumentException("ad boş olamaz");
        Kimlik = k;
        Ad = a;
        Tur = t;
        AcikMi = false;

    }
    
    public void Ac()
    {
        AcikMi = true;
    }
    public void Kapat()
    {
        AcikMi = false;
    }

    public void BilgileriGoster()
    {
        Console.Write("Cihaz kimliği:" + Kimlik + ", cihaz adı:" + Ad + ", cihaz turu:" +Tur +", cihaz durumu: ");
        if (AcikMi)
            Console.WriteLine("açık");
        else
            Console.WriteLine("kapalı");
    }
    
}


class Program
{
    static void Main()
    {
        // 7. Cihazlar List<Cihaz> içinde, başlangıçta birkaç cihaz var
        List<Cihaz> cihazlar = new List<Cihaz>();
        cihazlar.Add(new Cihaz("1", "Salon Lambası", CihazTuru.Lamba));
        cihazlar.Add(new Cihaz("2", "Yatak Odası Kliması", CihazTuru.Klima));
        cihazlar.Add(new Cihaz("3", "Ev Alarmı", CihazTuru.GuvenlikSistemi));

        // 8. Menü
        bool devam = true;
        while (devam)
        {
            Console.WriteLine();
            Console.WriteLine("--- AKILLI EV ---");
            Console.WriteLine("1 - Cihazları listele");
            Console.WriteLine("2 - Yeni cihaz ekle");
            Console.WriteLine("3 - Cihaz aç");
            Console.WriteLine("4 - Cihaz kapat");
            Console.WriteLine("0 - Çıkış");
            Console.Write("Seçiminiz: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    Listele(cihazlar);
                    break;
                case "2":
                    CihazEkle(cihazlar);
                    break;
                case "3":
                    CihazAcKapat(cihazlar, true);
                    break;
                case "4":
                    CihazAcKapat(cihazlar, false);
                    break;
                case "0":
                    devam = false;
                    Console.WriteLine("Çıkış yapıldı.");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim!");
                    break;
            }
        }
    }

    // Kimliğe göre cihaz bulur, yoksa null döner
    static Cihaz Bul(List<Cihaz> cihazlar, string kimlik)
    {
        return cihazlar.FirstOrDefault(c => c.Kimlik == kimlik);
    }

    static void Listele(List<Cihaz> cihazlar)
    {
        if (cihazlar.Count == 0)
        {
            Console.WriteLine("Kayıtlı cihaz yok.");
            return;
        }

        foreach (Cihaz c in cihazlar)
            c.BilgileriGoster();
    }

    static void CihazEkle(List<Cihaz> cihazlar)
    {
        Console.Write("Kimlik: ");
        string kimlik = Console.ReadLine();

        // 9. Aynı kimlikte iki cihaz olamaz
        if (Bul(cihazlar, kimlik) != null)
        {
            Console.WriteLine("Hata: Bu kimliğe sahip bir cihaz zaten var!");
            return;
        }

        Console.Write("Ad: ");
        string ad = Console.ReadLine();

        Console.WriteLine("Tür seçin: 0 - Lamba, 1 - Klima, 2 - GuvenlikSistemi");
        Console.Write("Tür: ");
        string turGirdi = Console.ReadLine();

        int turNo;
        if (!int.TryParse(turGirdi, out turNo) || !Enum.IsDefined(typeof(CihazTuru), turNo))
        {
            Console.WriteLine("Hata: Geçersiz tür!");
            return;
        }

        try
        {
            Cihaz yeni = new Cihaz(kimlik, ad, (CihazTuru)turNo);
            cihazlar.Add(yeni);
            Console.WriteLine("Cihaz eklendi.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
        }
    }

    static void CihazAcKapat(List<Cihaz> cihazlar, bool ac)
    {
        Console.Write("Cihaz kimliği: ");
        string kimlik = Console.ReadLine();

        Cihaz cihaz = Bul(cihazlar, kimlik);

        // 10. Olmayan cihaz için hata mesajı
        if (cihaz == null)
        {
            Console.WriteLine("Hata: Bu kimliğe sahip bir cihaz bulunamadı!");
            return;
        }

        if (ac)
        {
            cihaz.Ac();
            Console.WriteLine(cihaz.Ad + " açıldı.");
        }
        else
        {
            cihaz.Kapat();
            Console.WriteLine(cihaz.Ad + " kapatıldı.");
        }
    }
}