using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WindowsFormsAppSummerPracticeMVC.Models
{
    public class MainModel
    {
        private readonly string pathForHTMLFile = $"{Directory.GetCurrentDirectory()}\\HTML.txt";
        private readonly string pathForOutputFile = $"{Directory.GetCurrentDirectory()}\\Output.txt";
        private readonly string[] specialTags = new string[] { "style", "script" };
        private readonly Encoding enc1251 = Encoding.GetEncoding(1251);
        private readonly Encoding enc8859 = Encoding.GetEncoding("iso-8859-1");
        private readonly List<char> parsers = new List<char>() { };
        public MainModel()
        {
            // Заполняем список разделителей
            FillingParsers();
        }
        public async Task GetRequestAsync<T>(string requestUrl)
        {
            // Проверяем введенный URL
            CheckUrl(requestUrl);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(requestUrl))
                {
                    // Если statusCose == 200
                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            // Асинхронно сериализуем содержимое HTTP в строку и декодируем html теги 
                            string text = await content.ReadAsStringAsync();
                            string myDecodedString = HttpUtility.HtmlDecode(text);

                            // Разбиваем строку на отдельные слова
                            StripTagsCharArray(myDecodedString);
                            // Сохраняем HTML-страницу
                            SaveText(myDecodedString);
                        }
                    }
                    else
                    {
                        throw new Exception($"The {response.StatusCode} Bad Request Error!");
                    }
                }
            }
        }
        private void CheckUrl(string requestUrl)
        {
            if (requestUrl == "")
            {
                throw new Exception("Fill in the page url!");
            }
            if (!Uri.IsWellFormedUriString(requestUrl, UriKind.Absolute))
            {
                throw new Exception("Your url is incorrect!");
            }
        }
        private void FillingParsers()
        {
            for (int i = 32; i < 256; i++)
            {
                // Символы ASCII
                if ((i >= 32 && i <= 47) || (i >= 58 && i <= 64) || (i >= 91 && i <= 96) || (i >= 123 && i <= 126))
                {
                    parsers.Add((char)i);
                }
                // Символы ASCII extended
                else if ((i >= 130 && i < 140) || (i >= 145 && i < 150) || (i >= 166 && i < 173) || (i >= 183 && i < 188) && (i != 131 && i != 136 && i != 138 && i != 168 && i != 170 && i != 170 && i != 184 && i != 186) || (i == 139 || i == 155))
                {
                    // Кодируем символ из Unicode в CP1251
                    byte[] bytes = enc8859.GetBytes(new char[] { (char)i });
                    char[] newChar = enc1251.GetChars(bytes);

                    parsers.Add(newChar[0]);
                }
            }
            parsers.Add('\n');
            parsers.Add('\r');
            parsers.Add('\t');
        }
        private void SaveText(string source)
        {
            if (File.Exists(pathForHTMLFile))
            {
                File.Delete(pathForHTMLFile);
            }

            using (StreamWriter sw = new StreamWriter(pathForHTMLFile))
            {
                sw.WriteLine(source);
            }
        }
        private void Output(Dictionary<string, int> dict)
        {
            if (File.Exists(pathForOutputFile))
            {
                File.Delete(pathForOutputFile);
            }

            using (StreamWriter sw = new StreamWriter(pathForOutputFile))
            {
                foreach (var item in dict.OrderByDescending(v => v.Value))
                {
                    Console.WriteLine($"{item.Key}  —  {item.Value}");
                    sw.WriteLine($"{item.Key}  —  {item.Value}");
                }
            }
        }
        private void StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;
            string tagName = "";
            bool insideSpecialtag = false;
            bool charBeforeInsideTag = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    array[arrayIndex] = ' ';
                    arrayIndex++;
                    inside = true;
                    charBeforeInsideTag = false;
                    int ind = i;

                    if (source[ind] != '/')
                    {
                        while (source[ind] != '>')
                        {
                            tagName += source[ind];
                            ind++;
                        }

                        // Проверяем в каком теге находимся
                        foreach (var tag in specialTags)
                        {
                            if (tagName.IndexOf($"<{tag}") != -1)
                            {
                                insideSpecialtag = true;
                                continue;
                            }
                        }
                    }
                    tagName = "";
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    charBeforeInsideTag = true;
                    continue;
                }
                if (let == '/' && source[i - 1] == '<' && insideSpecialtag)
                {
                    int ind = i - 1;

                    while (source[ind] != '>')
                    {
                        tagName += source[ind];
                        ind++;
                    }

                    foreach (var tag in specialTags)
                    {
                        if (tagName.IndexOf($"</{tag}") != -1)
                        {
                            insideSpecialtag = false;
                            continue;
                        }
                    }
                    continue;
                }
                // Исключаем добавление текста из массива специальных тегов
                if (insideSpecialtag)
                {
                    continue;
                }
                if (!inside)
                {
                    if (charBeforeInsideTag)
                    {
                        array[arrayIndex] = ' ';
                        arrayIndex++;
                        charBeforeInsideTag = false;
                    }

                    byte[] bytes = Encoding.Default.GetBytes(new char[] { let });
                    char[] newChar = Encoding.Default.GetChars(bytes);
                    array[arrayIndex] = newChar[0];
                    arrayIndex++;
                }
            }
            // Формируем словарь для статистики
            DictionaryFormation(new string(array, 0, arrayIndex));
        }
        private void DictionaryFormation(string source)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            // Разбиваем строку, используя регулярное выражение и разделители
            string[] str = System.Text.RegularExpressions.Regex.Replace(source, @"[^0-9a-zA-Z]+\\[^0-9а-яА-Я]+", " ").Split(parsers.ToArray());

            foreach (var item in str)
            {
                if (!dict.ContainsKey(item))
                {
                    dict.Add(item, 1);
                    continue;
                }
                dict[item]++;
            }
            // Выводим статистику в консоль и сохраняем в файл
            Output(dict);
        }
    }
}