namespace calculator_sharp
{
    public class Parser
    {
        /*
         * EBNF
         * <expression> ::= <term> [ ('+'|'-') <term> ]*
         * <term>       ::= <factor> [ ('*'|'/') <factor> ]*
         * <factor>     ::= <number> | '(' <expr> ')'
         * <number>     :== ('0' ... '9')+
         */

        public Parser(IReadOnlyList<Token> tokens)
        {
            Tokens = tokens;
        }

        public IReadOnlyList<Token> Tokens { get; }

        public int Parse()
        {
            (int value, _) = Expression(0);
            return value;
        }

        public (int value, int cursor) Expression(int cursor)
        {
            (int value, cursor) = Term(cursor);

            while (cursor < Tokens.Count
                && (Tokens[cursor].Type == TokenType.Plus
                || Tokens[cursor].Type == TokenType.Minus))
            {
                Token operatorToken = Tokens[cursor];
                cursor++;
                (int rightTerm, cursor) = Term(cursor);

                if (operatorToken.Type == TokenType.Plus)
                    value += rightTerm;
                else
                    value -= rightTerm;
            }
            return (value, cursor);
        }

        public (int value, int cursor) Term(int cursor)
        {
            (int value, cursor) = Factor(cursor);

            while (cursor < Tokens.Count
                && (Tokens[cursor].Type == TokenType.Multi
                || Tokens[cursor].Type == TokenType.Divide))
            {
                Token operatorToken = Tokens[cursor];
                cursor++;
                (int rightTerm, cursor) = Factor(cursor);

                if (operatorToken.Type == TokenType.Multi)
                    value *= rightTerm;
                else
                    value /= rightTerm;
            }
            return (value, cursor);
        }

        public (int value, int cursor) Factor(int cursor)
        {
            if (Tokens[cursor].Type != TokenType.LeftParen)
                return Number(cursor);

            cursor++; // Skip TokenType.LeftParen
            (int value, cursor) = Expression(cursor);
            cursor++; // Skip TokenType.RightParen
            return (value, cursor);
        }

        public (int value, int cursor) Number(int cursor)
        {
            int value = int.Parse(Tokens[cursor].Data);
            cursor++;
            return (value, cursor);
        }
    }
}
