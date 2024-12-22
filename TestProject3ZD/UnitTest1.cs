using System.Xml.Linq;

namespace TestProject3ZD
{
    /// <summary>
    /// Класс, содержащий тесты для проверки функциональности Fb2SentenceProcessor.
    /// </summary>
    public class Fb2SentenceProcessorTests
    {
        /// <summary>
        /// Тест, проверяющий, что метод ProcessTextNode корректно удаляет предложения, длина которых больше 7 слов.
        /// </summary>
        [Fact]
        public void ProcessTextNode_RemoveSentencesLongerThan7_Replaced()
        {
            // Arrange
            string input = "Это предложение из пяти слов. Это предложение из восьми слов которые должны быть удалены. Это короткое.";
            string expected = "Это предложение из пяти слов. Это короткое.";

            // Act
            string result = Fb2SentenceProcessor.ProcessTextNode(input);

            // Assert
            Assert.Equal(expected, result);
        }
        /// <summary>
        /// Тест, проверяющий, что метод ProcessTextNode не изменяет текст, если в нём нет предложений, длина которых больше 7 слов.
        /// </summary>
        [Fact]
        public void ProcessTextNode_NoSentencesLongerThan7_Unchanged()
        {
            // Arrange
            string input = "Это предложение из пяти слов. Это короткое. И это.";
            string expected = "Это предложение из пяти слов. Это короткое. И это. ";

            // Act
            string result = Fb2SentenceProcessor.ProcessTextNode(input);

            // Assert
            Assert.Equal(expected, result);
        }
        /// <summary>
        /// Тест, проверяющий, что метод ProcessTextNode корректно обрабатывает пустой текст, возвращая пустую строку.
        /// </summary>
        [Fact]
        public void ProcessTextNode_EmptyText_Empty()
        {
            // Arrange
            string input = "";
            string expected = "";

            // Act
            string result = Fb2SentenceProcessor.ProcessTextNode(input);

            // Assert
            Assert.Equal(expected, result);
        }
        /// <summary>
        /// Тест, проверяющий, что метод ProcessFb2File правильно обрабатывает xml и заменяет предложения в файле.
        /// </summary>
        [Fact]
        public void ProcessFb2File_ValidFile_ReplacedWords()
        {
            // Arrange
            string inputFilePath = "test_input.fb2";
            string outputFilePath = "test_output.fb2";
            string inputContent = "<section><p>Это предложение из пяти слов. Это предложение из восьми слов которые должны быть удалены. Это короткое.</p></section>";
            string expectedOutputContent = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<section><p>Это предложение из пяти слов. Это короткое. </p></section>";

            File.WriteAllText(inputFilePath, inputContent);
            File.WriteAllText("expected_output.fb2", expectedOutputContent);

            // Act
            Fb2SentenceProcessor.ProcessFb2File(inputFilePath, outputFilePath);

            // Assert
            XDocument actualXml = XDocument.Load(outputFilePath);
            XDocument expectedXml = XDocument.Load("expected_output.fb2");

            Assert.True(XNode.DeepEquals(expectedXml, actualXml));
            // CleanUp - Убираем за собой временные файлы
            File.Delete(inputFilePath);
            File.Delete(outputFilePath);
            File.Delete("expected_output.fb2");
        }

    }
}