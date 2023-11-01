// Can Karabulut - Savunma Elektroniği ve Yazılımı (Tezli) - Öğrenci Numarası -> 22310162
// C# ile Yapay Zeka Ödevi - Ödevi Veren : Dr. Mehmet Dikmen
// Public Git Repo -> https://github.com/KarabulutCan/Genis-Oncelikli-Arama-BFS-Odevi-Yuksek-Lisans-YapayZeka-Dersi.git
// Bu kodu https://www.programiz.com/csharp-programming/online-compiler/ üzerinden çalıştırabilirsiniz.Yorum satırında ki Kütüphaneler eklenmelidir.
// Eğer .net 6 veya üstü bir sürüm kullanmıyorsanız gerekli kütüphaneleri eklemelisiniz.
/*
using System;
using System.Collections.Generic;
using System.Linq;
 */
public class Program
{
    public static void Main()
    {
        // Kullanıcıdan sürekli başlangıç numarası girmesi için sonsuz döngü.
        while (true)
        {
            Console.WriteLine("Baslangic sayisini girin: ");
            string start_input = Console.ReadLine();

            // Kullanıcı geçerli bir giriş yapana kadar tekrar input iste.
            while (!IsValidInput(start_input))
            {
                Console.WriteLine("Lütfen 1-4 arasında rakamlardan oluşan 4 basamaklı bir sayı girin:");
                start_input = Console.ReadLine();
            }

            Console.WriteLine("------------------------------");

            // Verilen girişle BFS aramasını gerçekleştir.
            var result = BFS(start_input);
            var path = result.Item1; // Çözüm yolu.
            var expandedNodes = result.Item2; // Arama sırasında genişletilen düğümler.

            // Bir çözüm yolu bulunduysa ->
            if (path != null)
            {
                Console.WriteLine("Dugum Genisletme Sirasi:");
                Console.WriteLine(string.Join(" -> ", expandedNodes) + " -> {0} ", path.LastOrDefault());
                Console.WriteLine($"Hedefe {expandedNodes.Count} adimda ulasildi!");
                Console.WriteLine("Cozum Yolu = " + string.Join(" -> ", path));
                Console.WriteLine("\n Program Tamamlandi \n------------------------------");
            }
            else
            {
                Console.WriteLine("Hedefe ulasilamadi!");
                Console.WriteLine("\n Program Tamamlandi\n------------------------------");
            }
        }
    }

    // Verilen inputun geçerli olup olmadığını kontrol eder.
    public static bool IsValidInput(string input)
    {
        // Girişin boş olup olmadığını veya 4 karakterden farklı bir uzunluğa sahip olup olmadığını kontrol eder.
        if (string.IsNullOrEmpty(input) || input.Length != 4)
            return false;

        // Girişteki tüm karakterlerin rakam olup olmadığını kontrol eder.
        if (!input.All(char.IsDigit))
            return false;

        // Girişte farklı karakterlerin olup olmadığını kontrol eder.
        if (input.Distinct().Count() != 4)
            return false;

        // Girişteki her bir karakterin '1' ile '4' arasında olup olmadığını kontrol eder.
        foreach (var ch in input)
        {
            if (ch < '1' || ch > '4')
                return false;
        }

        return true;
    }

    // Çözüm yolu bulmak için BFS (Genişlik Öncelikli Arama) fonksiyonu.
    // Çözüm yolu ve genişletilen düğümleri içeren bir Tuple döndürür. Eklenen veriyi Yanlışlıkla değiştirme silme yapmamak için Tuple kullandım.
    public static Tuple<List<string>, List<string>> BFS(string start_input, string goal = "1234")
    {
        // Ziyaret edilen düğümleri takip etmek için kullanılan HashSet.
        var visited = new HashSet<string>();
        // BFS için düğümleri yönetmek adına kullanılan Queue.
        var queue = new Queue<Tuple<string, List<string>>>();
        // Arama sırasında genişletilen düğümleri takip etmek için kullanılan Tek Yönlü Bağlantılı Liste.
        var expandedNodes = new List<string>();

        // İlk girişle aramayı başlat.
        queue.Enqueue(new Tuple<string, List<string>>(start_input, new List<string>()));

        // İşlenecek düğüm olduğu sürece aramaya devam et. Bu While döngüsünde ki kontrolü yapmassam düğümler boyunca ilerleyemem.
        while (queue.Count > 0)
        {
            var tuple = queue.Dequeue();
            string node = tuple.Item1;
            List<string> path = new List<string>(tuple.Item2);

            // Eğer şu anki düğüm hedefle eşleşiyorsa, çözümü bulduk demektir.
            if (node == goal)
            {
                path.Add(node);
                return new Tuple<List<string>, List<string>>(path, expandedNodes);
            }

            // Şu anki düğümü ziyaret edildi olarak işaretle.
            visited.Add(node);
            expandedNodes.Add(node);

            // Bitişik karakterleri değiştirerek çocuk düğümleri oluştur.
            var children = new List<string>
            {
                Swap(node, 0, 1),
                Swap(node, 1, 2),
                Swap(node, 2, 3)
            };

            // Eğer ziyaret edilmemişse çocuk düğümleri kuyruğa ekle. Ekrana basmak için gerekli.
            // Bir yol üzerinde aynı sayı birden fazla kez bulmuyor.Denedim.
            foreach (var child in children)
            {
                if (!visited.Contains(child))
                {
                    var newPath = new List<string>(path) { node };
                    queue.Enqueue(new Tuple<string, List<string>>(child, newPath));
                }
            }
        }

        // Eğer buraya geldiysek, bir çözüm bulunamadı demektir. 
        // null check 36. satırda mevcut.
        return new Tuple<List<string>, List<string>>(null, expandedNodes);
    }

    // Bir dizedeki iki karakteri değiştirmek için yardımcı(Util) fonksiyon. 
    public static string Swap(string s, int i, int j)
    {
        char[] array = s.ToCharArray();
        char temp = array[i];
        array[i] = array[j];
        array[j] = temp;
        return new string(array);
    }

    //Kodu tüm varyasyonlarda denedim 100% çalışıyor. 
}
