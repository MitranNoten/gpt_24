namespace TestProject2ZD
{
    public class UnitTest1
    {
        public class IIbTests
        {
            [Fact]
            public void ProcessWord_ShortWord_Reversed()
            {
                // Arrange
                string word = "hello";
                // Act
                string result = IIb.ProcessWord(word);
                // Assert
                Assert.Equal("olleh", result);
            }

            [Fact]
            public void ProcessWord_LongWord_Unchanged()
            {
                // Arrange
                string word = "testing";
                // Act
                string result = IIb.ProcessWord(word);
                // Assert
                Assert.Equal("testing", result);
            }

            [Fact]
            public void ProcessWord_NonEnglishWord_Unchanged()
            {
                // Arrange
                string word = "привет";
                // Act
                string result = IIb.ProcessWord(word);
                // Assert
                Assert.Equal("привет", result);
            }

            [Fact]
            public void ProcessWord_MixedCaseWord_Reversed()
            {
                // Arrange
                string word = "hELLo";
                // Act
                string result = IIb.ProcessWord(word);
                // Assert
                Assert.Equal("oLLEh", result);
            }
            [Fact]
            public void ProcessWord_EmptyWord_Unchanged()
            {
                // Arrange
                string word = "";
                // Act
                string result = IIb.ProcessWord(word);
                // Assert
                Assert.Equal("", result);
            }
            [Fact]
            public void ProcessWord_WordWithNumbers_Unchanged()
            {
                // Arrange
                string word = "test1";
                // Act
                string result = IIb.ProcessWord(word);
                // Assert
                Assert.Equal("test1", result);
            }
        }
    }