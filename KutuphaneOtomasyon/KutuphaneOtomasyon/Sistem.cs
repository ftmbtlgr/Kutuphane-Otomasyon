using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KutuphaneOtomasyon
{
    public class Kutuphane
    {
        private List<Kitap> kitaplar = new List<Kitap>();
        private List<KullaniciVeri> oduncKitaplar = new List<KullaniciVeri>();

        public void KitapEkle(Kitap yeniKitap)
        {
            kitaplar.Add(yeniKitap);
            Console.WriteLine("Yeni kitap eklendi: " + yeniKitap.Title);
        }

        public void TumKitaplariListele()
        {
            Console.WriteLine("Kütüphanedeki Tüm Kitaplar:");
            foreach (var kitap in kitaplar)
            {
                Console.WriteLine($"Başlık: {kitap.Title}, Yazar: {kitap.Author}, Kopya Sayısı: {kitap.CopyCount}");
            }
        }

        public void KitapAra(string anahtarKelime)
        {
            var sonuclar = kitaplar.Where(kitap =>
                kitap.Title.Contains(anahtarKelime, StringComparison.OrdinalIgnoreCase) ||
                kitap.Author.Contains(anahtarKelime, StringComparison.OrdinalIgnoreCase)).ToList();

            if (sonuclar.Any())
            {
                Console.WriteLine("Arama Sonuçları:");
                foreach (var kitap in sonuclar)
                {
                    Console.WriteLine($"Başlık: {kitap.Title}, Yazar: {kitap.Author}, Kopya Sayısı: {kitap.CopyCount}");
                }
            }
            else
            {
                Console.WriteLine("Aranan kriterlere uygun kitap bulunamadı.");
            }
        }

        public void KitapOduncAl(string kitapBasligi, string kullaniciAdi, DateTime oduncTarihi, DateTime iadeTarihi)
        {
            var kitap = kitaplar.FirstOrDefault(k => k.Title.Equals(kitapBasligi, StringComparison.OrdinalIgnoreCase));

            if (kitap != null && kitap.CopyCount > kitap.BorrowedCount)
            {
                kitap.BorrowedCount++;
                var oduncKitap = new KullaniciVeri
                {
                    UserName = kullaniciAdi,
                    BookTitle = kitapBasligi,
                    BorrowDate = oduncTarihi,
                    ReturnDate = iadeTarihi
                };
                oduncKitaplar.Add(oduncKitap);
                Console.WriteLine($"{kullaniciAdi} adlı kullanıcı {kitapBasligi} kitabını ödünç aldı.");
            }
            else
            {
                Console.WriteLine("Kitap ödünç alınamadı. Yetersiz kopya mevcut veya kitap bulunamadı.");
            }
        }

        public void KitapIadeEt(string kitapBasligi, string kullaniciAdi)
        {
            var oduncKitap = oduncKitaplar.FirstOrDefault(k =>
                k.UserName.Equals(kullaniciAdi, StringComparison.OrdinalIgnoreCase) &&
                k.BookTitle.Equals(kitapBasligi, StringComparison.OrdinalIgnoreCase));

            if (oduncKitap != null)
            {
                var kitap = kitaplar.FirstOrDefault(k => k.Title.Equals(kitapBasligi, StringComparison.OrdinalIgnoreCase));
                if (kitap != null)
                {
                    kitap.BorrowedCount--;
                    oduncKitaplar.Remove(oduncKitap);
                    Console.WriteLine($"{kullaniciAdi} adlı kullanıcı {kitapBasligi} kitabını iade etti.");
                }
            }
            else
            {
                Console.WriteLine("Kitap iade edilemedi. Geçerli bir ödünç alma kaydı bulunamadı.");
            }
        }

        public void GecikmisKitaplarListele()
        {
            var gecikmisKitaplar = oduncKitaplar.Where(k => k.ReturnDate < DateTime.Now).ToList();

            if (gecikmisKitaplar.Any())
            {
                Console.WriteLine("Süresi Geçmiş Kitaplar:");
                foreach (var kitap in gecikmisKitaplar)
                {
                    Console.WriteLine($"Kullanıcı: {kitap.UserName}, Kitap: {kitap.BookTitle}, İade Tarihi: {kitap.ReturnDate}");
                }
            }
            else
            {
                Console.WriteLine("Süresi geçmiş kitap bulunmamaktadır.");
            }
        }
    }
}

