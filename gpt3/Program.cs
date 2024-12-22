using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
public class Fb2SentenceProcessor
{
    /// <summary>
    ///  Основной метод для обработки FB2 файла.
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
                // Вызываем метод ProcessTextNode для обработки текущего текстового узла.
                string processedText = ProcessTextNode(node.Value);
                // Добавляем обработанный текст в StringBuilder.
                stringBuilder.Append(processedText);
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
    /// Метод для обработки текстового узла, удаляющий предложения длиной 7 слов.
    /// </summary>
    /// <param name="text">Текст для обработки.</param>
    /// <returns>Обработанный текст с удаленными предложениями.</returns>
    public static string ProcessTextNode(string text)
    {
        // Используем StringBuilder для более эффективного изменения строк.
        StringBuilder resultBuilder = new StringBuilder();
        // Разделяем текст на предложения.
        string[] sentences = Regex.Split(text, @"(?<=[.?!])\s+");

        // Итерируемся по каждому предложению.
        for (int i = 0; i < sentences.Length; i++)
        {
            string sentence = sentences[i];
            string cleanedSentence = Regex.Replace(sentence, @"[^а-яА-ЯёЁa-zA-Z\s]", "");
            string[] words = cleanedSentence.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length <= 7)
            {
                if (i != sentences.Length - 1)
                    resultBuilder.Append(sentence + " ");
                else
                    resultBuilder.Append(sentence);
            }
        }
        // Возвращаем обработанный текст.
        return resultBuilder.ToString().Trim();
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
        string outputFilePath = @"C:\Users\mitra\OneDrive\Рабочий стол\Технология программирования\Лабораторные работы\ИИ\output3.fb2";

        // Вызываем основной метод обработки файла.
        ProcessFb2File(inputFilePath, outputFilePath);

        // Сообщаем об завершении программы
        Console.WriteLine("Обработка файла завершена.");
    }
}