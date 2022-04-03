using System.Collections.Generic;
using Xunit;

namespace calculator_sharp.Test
{
    public class ParserTest
    {
        public static IEnumerable<object> GetNumberTestSource()
        {
            yield return new object[] { 0, new List<Token>() { new Token(TokenType.Number, "0") } };
            yield return new object[] { 10, new List<Token>() { new Token(TokenType.Number, "10") } };
            yield return new object[] { 100, new List<Token>() { new Token(TokenType.Number, "100") } };
            yield return new object[] { 1000, new List<Token>() { new Token(TokenType.Number, "1000") } };
            yield return new object[] { 10000, new List<Token>() { new Token(TokenType.Number, "10000") } };
        }

        public static IEnumerable<object> GetFactorTestSource()
        {
            yield return new object[] {
                100,
                new List<Token>() {
                    new Token(TokenType.LeftParen, "("),
                    new Token(TokenType.Number, "100"),
                    new Token(TokenType.RightParen, ")")
                }
            };
            yield return new object[] {
                100,
                new List<Token>() {
                    new Token(TokenType.LeftParen, "("),
                    new Token(TokenType.LeftParen, "("),
                    new Token(TokenType.Number, "100"),
                    new Token(TokenType.RightParen, ")"),
                    new Token(TokenType.RightParen, ")")
                }
            };
            yield return new object[] {
                100,
                new List<Token>() {
                    new Token(TokenType.LeftParen, "("),
                    new Token(TokenType.LeftParen, "("),
                    new Token(TokenType.LeftParen, "("),
                    new Token(TokenType.Number, "100"),
                    new Token(TokenType.RightParen, ")"),
                    new Token(TokenType.RightParen, ")"),
                    new Token(TokenType.RightParen, ")")
                }
            };
        }

        public static IEnumerable<object> GetTermTestSource()
        {
            yield return new object[] {
                1,
                new List<Token>() {
                    new Token(TokenType.Number, "1"),
                    new Token(TokenType.Multi, "*"),
                    new Token(TokenType.Number, "1")
                }
            };
            yield return new object[] {
                6,
                new List<Token>() {
                    new Token(TokenType.Number, "2"),
                    new Token(TokenType.Multi, "*"),
                    new Token(TokenType.Number, "3")
                }
            };
            yield return new object[] {
                288,
                new List<Token>() {
                    new Token(TokenType.Number, "16"),
                    new Token(TokenType.Multi, "*"),
                    new Token(TokenType.Number, "18")
                }
            };
            yield return new object[] {
                2,
                new List<Token>() {
                    new Token(TokenType.Number, "6"),
                    new Token(TokenType.Divide, "/"),
                    new Token(TokenType.Number, "3")
                }
            };
            yield return new object[] {
                64,
                new List<Token>() {
                    new Token(TokenType.Number, "1024"),
                    new Token(TokenType.Divide, "/"),
                    new Token(TokenType.Number, "16")
                }
            };
        }

        public static IEnumerable<object> GetExpressionTestSource()
        {
            yield return new object[] {
                2,
                new List<Token>() {
                    new Token(TokenType.Number, "1"),
                    new Token(TokenType.Plus, "/"),
                    new Token(TokenType.Number, "1")
                }
            };
            yield return new object[] {
                5,
                new List<Token>() {
                    new Token(TokenType.Number, "2"),
                    new Token(TokenType.Plus, "+"),
                    new Token(TokenType.Number, "3")
                }
            };
            yield return new object[] {
                100,
                new List<Token>() {
                    new Token(TokenType.Number, "61"),
                    new Token(TokenType.Plus, "+"),
                    new Token(TokenType.Number, "39")
                }
            };
            yield return new object[] {
                1,
                new List<Token>() {
                    new Token(TokenType.Number, "2"),
                    new Token(TokenType.Minus, "-"),
                    new Token(TokenType.Number, "1")
                }
            };
            yield return new object[] {
                2,
                new List<Token>() {
                    new Token(TokenType.Number, "5"),
                    new Token(TokenType.Minus, "-"),
                    new Token(TokenType.Number, "3")
                }
            };
            yield return new object[] {
                62,
                new List<Token>() {
                    new Token(TokenType.Number, "98"),
                    new Token(TokenType.Minus, "-"),
                    new Token(TokenType.Number, "36")
                }
            };
        }

        public static IEnumerable<object> GetComplexFormulaTestSource()
        {
            yield return new object[] {
                9,
                new List<Token>() {
                    new Token(TokenType.Number, "2"),
                    new Token(TokenType.Plus, "+"),
                    new Token(TokenType.Number, "3"),
                    new Token(TokenType.Plus, "+"),
                    new Token(TokenType.Number, "4")
                }
            };
            yield return new object[] {
                14,
                new List<Token>() {
                    new Token(TokenType.Number, "2"),
                    new Token(TokenType.Plus, "+"),
                    new Token(TokenType.Number, "3"),
                    new Token(TokenType.Multi, "*"),
                    new Token(TokenType.Number, "4")
                }
            };
            yield return new object[] {
                9,
                new List<Token>() {
                    new Token(TokenType.Number, "2"),
                    new Token(TokenType.Plus, "+"),
                    new Token(TokenType.Number, "3"),
                    new Token(TokenType.Multi, "*"),
                    new Token(TokenType.Number, "4"),
                    new Token(TokenType.Minus, "-"),
                    new Token(TokenType.Number, "5")
                }
            };
            yield return new object[] {
                20,
                new List<Token>() {
                    new Token(TokenType.LeftParen, "("),
                    new Token(TokenType.Number, "2"),
                    new Token(TokenType.Plus, "+"),
                    new Token(TokenType.Number, "3"),
                    new Token(TokenType.RightParen, ")"),
                    new Token(TokenType.Multi, "*"),
                    new Token(TokenType.Number, "4")
                }
            };
            yield return new object[] {
                1,
                new List<Token>() {
                    new Token(TokenType.LeftParen, "("),
                    new Token(TokenType.LeftParen, "("),
                    new Token(TokenType.Number, "1"),
                    new Token(TokenType.Plus, "+"),
                    new Token(TokenType.Number, "2"),
                    new Token(TokenType.RightParen, ")"),
                    new Token(TokenType.Multi, "*"),
                    new Token(TokenType.Number, "3"),
                    new Token(TokenType.Minus, "-"),
                    new Token(TokenType.Number, "4"),
                    new Token(TokenType.RightParen, ")"),
                    new Token(TokenType.Divide, "/"),
                    new Token(TokenType.Number, "5")
                }
            };
        }

        [Theory, MemberData(nameof(GetNumberTestSource))]
        public void NumberTest(int expected, IReadOnlyList<Token> tokens)
        {
            Parser parser = new Parser(tokens);
            Assert.Equal(expected, parser.Parse());
        }

        [Theory, MemberData(nameof(GetFactorTestSource))]
        public void FactorTest(int expected, IReadOnlyList<Token> tokens)
        {
            Parser parser = new Parser(tokens);
            Assert.Equal(expected, parser.Parse());
        }

        [Theory, MemberData(nameof(GetTermTestSource))]
        public void TermTest(int expected, IReadOnlyList<Token> tokens)
        {
            Parser parser = new Parser(tokens);
            Assert.Equal(expected, parser.Parse());
        }

        [Theory, MemberData(nameof(GetExpressionTestSource))]
        public void ExpressionTest(int expected, IReadOnlyList<Token> tokens)
        {
            Parser parser = new Parser(tokens);
            Assert.Equal(expected, parser.Parse());
        }

        [Theory, MemberData(nameof(GetComplexFormulaTestSource))]
        public void ComplexFormulaTest(int expected, IReadOnlyList<Token> tokens)
        {
            Parser parser = new Parser(tokens);
            Assert.Equal(expected, parser.Parse());
        }
    }
}
