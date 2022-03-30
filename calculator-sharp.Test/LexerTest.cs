using Xunit;

namespace calculator_sharp.Test
{
    public class LexerTest
    {
        [Theory]
        [InlineData('+', TokenType.Plus)]
        [InlineData('-', TokenType.Minus)]
        [InlineData('*', TokenType.Multi)]
        [InlineData('/', TokenType.Divide)]
        [InlineData('(', TokenType.LeftParen)]
        [InlineData(')', TokenType.RightParen)]
        public void SymbolTokenTest(char symbol, TokenType tokenType)
        {
            Lexer lexer = new Lexer("");
            Assert.Equal(new Token(tokenType, symbol), lexer.GetSymbolToken(symbol));
        }

        [Theory]
        [InlineData("1", TokenType.Number)]
        [InlineData("10", TokenType.Number)]
        [InlineData("100", TokenType.Number)]
        public void NumberTokenTest(string number, TokenType tokenType)
        {
            Lexer lexer = new Lexer(number);
            Assert.Equal(new Token(tokenType, number), lexer.ParseNumber(0));
        }

        [Fact]
        public void WhiteSpaceTokenTest()
        {
            string text = " ";
            Lexer lexer = new Lexer(text);
            Assert.Equal(new Token(TokenType.WhiteSpace, text), lexer.ParseWhiteSpace(0));
        }
    }
}