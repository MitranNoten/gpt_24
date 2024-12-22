using System.Xml.Linq;

namespace TestProject3ZD
{
    /// <summary>
    /// �����, ���������� ����� ��� �������� ���������������� Fb2SentenceProcessor.
    /// </summary>
    public class Fb2SentenceProcessorTests
    {
        /// <summary>
        /// ����, �����������, ��� ����� ProcessTextNode ��������� ������� �����������, ����� ������� ������ 7 ����.
        /// </summary>
        [Fact]
        public void ProcessTextNode_RemoveSentencesLongerThan7_Replaced()
        {
            // Arrange
            string input = "��� ����������� �� ���� ����. ��� ����������� �� ������ ���� ������� ������ ���� �������. ��� ��������.";
            string expected = "��� ����������� �� ���� ����. ��� ��������.";

            // Act
            string result = Fb2SentenceProcessor.ProcessTextNode(input);

            // Assert
            Assert.Equal(expected, result);
        }
        /// <summary>
        /// ����, �����������, ��� ����� ProcessTextNode �� �������� �����, ���� � �� ��� �����������, ����� ������� ������ 7 ����.
        /// </summary>
        [Fact]
        public void ProcessTextNode_NoSentencesLongerThan7_Unchanged()
        {
            // Arrange
            string input = "��� ����������� �� ���� ����. ��� ��������. � ���.";
            string expected = "��� ����������� �� ���� ����. ��� ��������. � ���. ";

            // Act
            string result = Fb2SentenceProcessor.ProcessTextNode(input);

            // Assert
            Assert.Equal(expected, result);
        }
        /// <summary>
        /// ����, �����������, ��� ����� ProcessTextNode ��������� ������������ ������ �����, ��������� ������ ������.
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
        /// ����, �����������, ��� ����� ProcessFb2File ��������� ������������ xml � �������� ����������� � �����.
        /// </summary>
        [Fact]
        public void ProcessFb2File_ValidFile_ReplacedWords()
        {
            // Arrange
            string inputFilePath = "test_input.fb2";
            string outputFilePath = "test_output.fb2";
            string inputContent = "<section><p>��� ����������� �� ���� ����. ��� ����������� �� ������ ���� ������� ������ ���� �������. ��� ��������.</p></section>";
            string expectedOutputContent = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<section><p>��� ����������� �� ���� ����. ��� ��������. </p></section>";

            File.WriteAllText(inputFilePath, inputContent);
            File.WriteAllText("expected_output.fb2", expectedOutputContent);

            // Act
            Fb2SentenceProcessor.ProcessFb2File(inputFilePath, outputFilePath);

            // Assert
            XDocument actualXml = XDocument.Load(outputFilePath);
            XDocument expectedXml = XDocument.Load("expected_output.fb2");

            Assert.True(XNode.DeepEquals(expectedXml, actualXml));
            // CleanUp - ������� �� ����� ��������� �����
            File.Delete(inputFilePath);
            File.Delete(outputFilePath);
            File.Delete("expected_output.fb2");
        }

    }
}