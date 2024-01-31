using KutuphaneOtomasyon;
using System;
using System.Collections.Generic;

using System;

namespace KutuphaneOtomasyon
{
    class Program
    {
        static void Main()
        {
            Kutuphane kutuphane = new Kutuphane();

            while (true)
            {               
                Console.WriteLine("\nKütüphane İşlemleri:");
                Console.WriteLine("1. Yeni Kitap Ekle");
                Console.WriteLine("2. Tüm Kitapları Listele");
                Console.WriteLine("3. Kitap Ara");
                Console.WriteLine("4. Kitap Ödünç Al");
                Console.WriteLine("5. Kitap İade Et");
                Console.WriteLine("6. Süresi Geçmiş Kitapları Listele");
                Console.WriteLine("0. Çıkış");

                Console.Write("Yapmak istediğiniz işlemi seçin (0-6): ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        KitapEkle(kutuphane);
                        break;
                    case "2":
                        kutuphane.TumKitaplariListele();
                        break;
                    case "3":
                        KitapAra(kutuphane);
                        break;
                    case "4":
                        KitapOduncAl(kutuphane);
                        break;
                    case "5":
                        KitapIadeEt(kutuphane);
                        break;
                    case "6":
                        kutuphane.GecikmisKitaplarListele();
                        break;
                    case "0":
                        Console.WriteLine("Programdan çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz bir seçim yaptınız. Lütfen tekrar deneyin.");
                        break;
                }
                
            }
        }

        static void KitapEkle(Kutuphane kutuphane)
        {
            Console.Write("Kitap Başlığı: ");
            string baslik = Console.ReadLine();
            Console.Write("Yazar: ");
            string yazar = Console.ReadLine();
            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();
            Console.Write("Kopya Sayısı: ");
            int kopyaSayisi = Convert.ToInt32(Console.ReadLine());

            Kitap yeniKitap = new Kitap
            {
                Title = baslik,
                Author = yazar,
                ISBN = isbn,
                CopyCount = kopyaSayisi,
                BorrowedCount = 0,
                BorrowDate = DateTime.Now
            };
            kutuphane.KitapEkle(yeniKitap);
            Console.WriteLine("Yeni kitap eklendi.");
        }

        static void KitapAra(Kutuphane kutuphane)
        {
            Console.Write("Arama Anahtarı (Başlık veya Yazar): ");
            string anahtarKelime = Console.ReadLine();
            kutuphane.KitapAra(anahtarKelime);
        }

        static void KitapOduncAl(Kutuphane kutuphane)
        {
            Console.Write("Kitap Başlığı: ");
            string kitapBasligi = Console.ReadLine();
            Console.Write("Kullanıcı Adı: ");
            string kullaniciAdi = Console.ReadLine();
            Console.Write("Ödünç Alma Tarihi (yyyy-MM-dd): ");
            DateTime oduncTarihi = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);
            Console.Write("İade Tarihi (yyyy-MM-dd): ");
            DateTime iadeTarihi = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

            kutuphane.KitapOduncAl(kitapBasligi, kullaniciAdi, oduncTarihi, iadeTarihi);
        }

        static void KitapIadeEt(Kutuphane kutuphane)
        {
            Console.Write("Kitap Başlığı: ");
            string kitapBasligi = Console.ReadLine();
            Console.Write("Kullanıcı Adı: ");
            string kullaniciAdi = Console.ReadLine();

            kutuphane.KitapIadeEt(kitapBasligi, kullaniciAdi);
        }
    }
}
