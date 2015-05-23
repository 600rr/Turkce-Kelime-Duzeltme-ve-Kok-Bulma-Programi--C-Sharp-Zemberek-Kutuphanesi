using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net.zemberek.erisim;
using net.zemberek.yapi;
using net.zemberek.tr.yapi;
using net.zemberek.bilgi;
using System.IO;
namespace KelimeDuzeltici
{
    class Program
    {
        static char ayrs;
        static void Main(string[] args)
        {
            //birinci argüman dosya yolu
            //ikinci argüman yeni dosya yolu
            try
            {
                if (args.Length != 0)
                {
                    baslat(args[0], args[1]);
                    ayrs = args[2][0];
                }
                else
                {
                    Console.Write("Okunacak Twit Dosya Yolunu Giriniz : ");
                    string okunacakYol = Console.ReadLine();
                    Console.Write("Yazilacak Twit Dosya Yolunu Giriniz : ");
                    string yazilacakYol = Console.ReadLine();
                    Console.Write("Takılarına Ayristirilsin Mi ?(Y/N) :");
                    ayrs = Convert.ToChar(Console.ReadLine());
                    baslat(okunacakYol, yazilacakYol);
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Beklenmeyen Bir Hata Olustu : " + ex.Message);
                Console.WriteLine("Programı kapatmak için herhangi bir tuşa basınız...");
                Console.ReadLine();
            }
        }
        static void baslat(string okunacak_yol, string yazilacak_yol)
        {
            StreamReader sr = new StreamReader(okunacak_yol, Encoding.Default, true);
            StreamWriter sw = new StreamWriter(yazilacak_yol, true, Encoding.UTF8);
            string twit = sr.ReadLine();
            while (twit != null)
            {
                string[] duzeltilmisKelimeler = kelimeDuzelt(twitiAyristir(twit));
                string duzeltilmisTwit = String.Empty;
                foreach (string kelime in duzeltilmisKelimeler)
                {
                    duzeltilmisTwit += " " + kelime;
                }
                sw.WriteLine(duzeltilmisTwit.Trim());
                twit = sr.ReadLine();
            }
            sr.Close();
            sw.Flush();
            sw.Close();
            Console.WriteLine("Kelime Duzeltme Islemi Basariyla Tamamlandı.");
        }
        static string[] twitiAyristir(string twit)
        {
            twit = twit.Replace(".", " ");
            twit = twit.Replace("!", " ");
            twit = twit.Replace("?", " ");
            twit = twit.Replace(",", " ");
            twit = twit.Replace("'", " ");
            twit = twit.Replace("-", " ");
            twit = twit.Replace("~", " ");
            twit = twit.Replace("[", " ");
            twit = twit.Replace("]", " ");
            twit = twit.Replace(":", " ");
            /*
            twit = twit.Replace(":)", "[gulucuk]");
            twit = twit.Replace(":D", "[kahkaha]");
            twit = twit.Replace("😇", "[iyi]");
            twit = twit.Replace(":"", "[ağlamaklı]");
            twit = twit.Replace(":(", "[uzuntu]");
            twit = twit.Replace(":/", "[sıkkın]");
            twit = twit.Replace(":\\", "[sıkkın]");
            twit = twit.Replace(":|", "[duygusuz]");
            twit = twit.Replace("😀", "[gulucuk]");
            twit = twit.Replace("😘", "[şaşkınlık]");
            twit = twit.Replace("😂", "[komik]");
            twit = twit.Replace("🏃", "[koş]");
            twit = twit.Replace("👎", "[beğenmedim]");
            twit = twit.Replace("📢", "[duyuru]");
            twit = twit.Replace("😍", "[gulucuk]");
            twit = twit.Replace("♥", "[sevgi]");
            */
            twit = twit.Replace(":)", string.Empty);
            twit = twit.Replace(":D", string.Empty);
            twit = twit.Replace("😇", string.Empty);
            twit = twit.Replace(":", string.Empty);
            twit = twit.Replace(":(", string.Empty);
            twit = twit.Replace(":/", string.Empty);
            twit = twit.Replace(":\\", string.Empty);
            twit = twit.Replace(":|", string.Empty);
            twit = twit.Replace("😀", string.Empty);
            twit = twit.Replace("😘", string.Empty);
            twit = twit.Replace("😂", string.Empty);
            twit = twit.Replace("🏃", string.Empty);
            twit = twit.Replace("👎", string.Empty);
            twit = twit.Replace("📢", string.Empty);
            twit = twit.Replace("😍", string.Empty);
            twit = twit.Replace("♥", string.Empty);
            twit = twit.Replace("(", string.Empty);
            twit = twit.Replace(")", string.Empty);
            twit = twit.Replace("  ", " ");
            twit = twit.Replace("   ", " ");
            twit = twit.Replace("    ", " ");
            twit = twit.ToLower();
            twit = twit.Trim();
            string[] kelimeler = twit.Split(' ');
            return kelimeler;
        }
        static string[] kelimeDuzelt(string[] kelimeler)
        {
            bool onerildi = false;
            Zemberek zmbrk = new Zemberek(new TurkiyeTurkcesi());
            string[] duzeltilmis_kelimeler = new string[kelimeler.Length];
            for (int i = 0; i < kelimeler.Length; i++)
            {
                string[] oneriler = zmbrk.oner(kelimeler[i]);
                if (oneriler.Length != 0)
                {
                    //kelime için öneri bulunamamış.
                    string yeniDeger = oneriler[0];
                    foreach (string oneri in oneriler)
                    {
                        if (oneri == kelimeler[i])
                        {
                            yeniDeger = oneri;
                            onerildi = true;
                            break;
                        }
                    }
                    if (onerildi == false)
                    {
                        foreach (string oneri in oneriler)
                        {
                            if (oneri.Contains(kelimeler[i]))
                            {
                                yeniDeger = oneri;
                                onerildi = true;
                                break;
                            }
                        }
                    }
                    if (onerildi == false)
                    {
                        foreach (string oneri in oneriler)
                        {
                            if (oneri.Length > 2 && kelimeler[i].Length > 2)
                                if (oneri.Substring(0, 2) == kelimeler[i].Substring(0, 2))
                                {
                                    yeniDeger = oneri;
                                    onerildi = true;
                                    break;
                                }
                        }
                    }
                    if (onerildi == false)
                    {
                        foreach (string oneri in oneriler)
                        {
                            if (oneri[0] == kelimeler[i][0])
                            {
                                yeniDeger = oneri;
                                onerildi = true;
                                break;
                            }
                        }
                    }
                    if (ayrs == 'Y' || ayrs == 'y')
                        yeniDeger = takilarinaAyir(yeniDeger);
                    duzeltilmis_kelimeler[i] = yeniDeger;
                    onerildi = false;
                    Console.WriteLine("'" + kelimeler[i] + "' Duzeltildi : " + duzeltilmis_kelimeler[i]);
                    //kelimeyi oldugu gibi aldık
                    //bunun yerine kelimeyi görmezden gelebilirdik.
                }
                else
                {

                    duzeltilmis_kelimeler[i] = kelimeler[i];
                    Console.WriteLine("'" + kelimeler[i] + "' Duzeltilemedi !");
                    //ilk önerilen kelimeyi aldık
                    //bundada değişiklikler yapılabilir
                    //foreach dongusune sokularak istenilen kelime alınabilir.
                }
            }
            return duzeltilmis_kelimeler;
        }
        public static string takilarinaAyir(string kelime)
        {
            Zemberek zmb = new Zemberek(new TurkiyeTurkcesi());
            Kelime[] cozumlenmis = zmb.kelimeCozumle(kelime);
            if (cozumlenmis.Length > 0)
            {
                string enBuyuk = cozumlenmis[0].icerikStr();
                foreach (Kelime klm in cozumlenmis)
                {
                    if (enBuyuk.Length < klm.icerikStr().Length)
                        enBuyuk = klm.icerikStr();
                }
                return enBuyuk;
            }
            else
                return kelime;
        }
    }
}
