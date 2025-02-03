namespace SimpleScript.Core.Lexical;

public class Token
{
    public TokenType Type { get; }
    public string Lexeme { get; }
    public object Value { get; }
    public int Line { get; }

    public Token(TokenType type, string lexeme, object value, int line)
    {
        Type = type;
        Lexeme = lexeme;
        Value = value;
        Line = line;
    }

    public override string ToString()
    {
        return $"{Type} {Lexeme} {Value}";
    }
} 