using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

public class IIb
{
    /// <summary>
    /// Основной метод для обработки FB2 файла.
    /// </summary>
    /// <param name="inputFilePath">Путь к входному .fb2 файлу.</param>
    /// <param name="outputFilePath">Путь к выходному файлу с изменениями.</param>
    public static void ProcessFb2File(string inputFilePath, string outputFilePath)
    {
        try
        {
            // Загружаем XML документ из FB2 файла.
            XDocument doc = XDocument.Load(inputFilePath);
            // Используем StringBuilder для более эффективного изменения строк.
            StringBuilder stringBuilder = new StringBuilder();

            // Находим все текстовые узлы в XML документе.
            var textNodes = doc.DescendantNodes().OfType<XText>();
            // Итерируемся по всем найденным текстовым узлам.
            foreach (XText node in textNodes)
            {
                // Разделяем текст на отдельные слова, исключая теги и другие спецсимволы.
                string[] words = Regex.Split(node.Value, @"\s+|\<.*?\>|[^a-zA-Zа-яА-ЯёЁ']").Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

                // Итерируемся по каждому слову.
                foreach (string word in words)
                {
                    // Проверяем, является ли слово английским.
                    if (Regex.IsMatch(word, @"^[a-zA-Z]+$"))
                    {
                        // Если слово английское, вызываем метод ProcessWord для его обработки.
                        string processedWord = ProcessWord(word);
                        // Добавляем обработанное слово в StringBuilder
                        stringBuilder.Append(processedWord + " ");
                    }
                    else
                    {
                        // Если слово не английское, добавляем его в StringBuilder без изменений.
                        stringBuilder.Append(word + " ");
                    }
                }

                // Обновляем значение текущего текстового узла обработанным текстом.
                node.Value = stringBuilder.ToString().Trim();
                // Очищаем StringBuilder для следующей итерации.
                stringBuilder.Clear();
            }

            // Сохраняем изменения в выходной файл.
            doc.Save(outputFilePath);
        }
        // Обрабатываем исключение, если файл не найден.
        catch (FileNotFoundException)
        {
            Console.WriteLine("Ошибка: Файл не найден.");
        }
        // Обрабатываем исключение, если произошла ошибка при работе с XML.
        catch (XmlException ex)
        {
            Console.WriteLine($"Ошибка XML: {ex.Message}");
        }
        // Обрабатываем все остальные возможные исключения.
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
        }
    }


    /// <summary>
    /// Метод для обработки отдельного слова.
    /// </summary>
    /// <param name="word">Слово для обработки.</param>
    /// <returns>Обработанное слово (инвертированное, если длина равна 5).</returns>
    public static string ProcessWord(string word)
    {
        {
            if (Regex.IsMatch(word, @"^[a-zA-Z]+$") && word.Length == 5)
            {
                return ReverseString(word);
            }
            return word;
        }
    }

    /// <summary>
    /// Метод для инвертирования строки.
    /// </summary>
    /// <param name="str">Строка для инвертирования.</param>
    /// <returns>Инвертированная строка.</returns>
    private static string ReverseString(string str)
    {
        // Преобразуем строку в массив символов.
        char[] charArray = str.ToCharArray();
        // Инвертируем массив символов.
        Array.Reverse(charArray);
        // Создаем и возвращаем новую строку из инвертированного массива.
        return new string(charArray);
    }

    /// <summary>
    /// Основной метод программы.
    /// </summary>
    /// <param name="args">Аргументы командной строки (не используются).</param>
    public static void Main(string[] args)
    {
        // Задаем путь к входному .fb2 файлу.
        string inputFilePath = @"C:\Users\mitra\OneDrive\Рабочий стол\Технология программирования\Лабораторные работы\ИИ\Толкиен М. - Хоббит - 1937.fb2";
        // Задаем путь к выходному файлу.
        string outputFilePath = @"C:\Users\mitra\OneDrive\Рабочий стол\Технология программирования\Лабораторные работы\ИИ\out2.fb2";
        // Вызываем основной метод обработки файла.
        ProcessFb2File(inputFilePath, outputFilePath);
        // Сообщаем об завершении программы.
        Console.WriteLine("Обработка файла завершена.");
    }
}