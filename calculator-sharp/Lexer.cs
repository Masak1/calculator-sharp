using System.Text.RegularExpressions;

namespace calculator_sharp
{
    public class Lexer
    {
        private static readonly Regex _patternNumber = new Regex("[0-9]");

        public Lexer(string formula)
        {
            Formula = formula;
        }

        public string Formula { get; }

        public List<Token> Tokenize()
        {
            int cursor = 0;
            List<Token> tokens = new List<Token>();

            while (cursor < Formula.Length)
            {
                Token nextToken = GetNextToken(cursor);
                tokens.Add(nextToken);
                cursor += nextToken.Data.Length;
            }

            return tokens;
        }

        public Token GetNextToken(int cursor)
        {
            char nextChar = Formula[cursor];
            Token? token = GetSymbolToken(nextChar);

            if (token != null)
                return token;

            Token? numberToken = ParseNumber(cursor);
            Token? whiteSpaceToken = ParseWhiteSpace(cursor);

            if (numberToken != null)
                return numberToken;

            if (whiteSpaceToken != null)
                return whiteSpaceToken;

            return new Token(TokenType.Error, nextChar);
        }

        public Token? GetSymbolToken(char symbol) => symbol switch
        {
            '+' => new Token(TokenType.Plus, symbol),
            '-' => new Token(TokenType.Minus, symbol),
            '*' => new Token(TokenType.Multi, symbol),
            '/' => new Token(TokenType.Divide, symbol),
            '(' => new Token(TokenType.LeftParen, symbol),
            ')' => new Token(TokenType.RightParen, symbol),
            _ => null
        };

        public Token? ParseNumber(int cursor)
        {
            int peekTimes = 0;
            List<char> numbers = new List<char>();

            while (cursor + peekTimes < Formula.Length)
            {
                char nextChar = Formula[cursor + peekTimes];

                if (!_patternNumber.IsMatch(nextChar.ToString()))
                    break;

                numbers.Add(nextChar);
                peekTimes += 1;
            }

            if (peekTimes == 0 || numbers.Count == 0)
            {
                return null;
            }

            return new Token(TokenType.Number, numbers.ToArray());
        }

        public Token? ParseWhiteSpace(int cursor)
        {
            char nextChar = Formula[cursor];
            string nextCharStr = nextChar.ToString();

            if (!string.IsNullOrEmpty(nextCharStr) && string.IsNullOrWhiteSpace(nextCharStr))
            {
                return new Token(TokenType.WhiteSpace, nextChar);
            }

            return null;
        }
    }
}