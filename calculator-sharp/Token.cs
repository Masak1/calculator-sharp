namespace calculator_sharp
{
    public enum TokenType
    {
        EOL,
        Number,
        Plus,
        Minus,
        Multi,
        Divide,
        LeftParen,
        RightParen,
        WhiteSpace,
        Error
    }

    public class Token
    {
        public TokenType Type { get; }
        public string Data { get; }

        public Token(TokenType type, string data)
        {
            Type = type;
            Data = data;
        }

        public Token(TokenType type, char data)
        {
            Type = type;
            Data = data.ToString();
        }

        public Token(TokenType type, char[] data)
        {
            Type = type;
            Data = new string(data);
        }

        public override bool Equals(object? obj)
        {
            return obj is Token token
                && Type == token.Type
                && Data == token.Data;
        }
    }
}