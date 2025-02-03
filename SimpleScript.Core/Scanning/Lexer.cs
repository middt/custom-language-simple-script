using System;
using System.Collections.Generic;
using SimpleScript.Core.Lexical;

namespace SimpleScript.Core.Scanning;

public class Lexer
{
    private readonly string _source;
    private readonly List<Token> _tokens = new();
    private int _start = 0;
    private int _current = 0;
    private int _line = 1;

    private readonly Dictionary<string, TokenType> _keywords = new()
    {
        { "let", TokenType.Let },
        { "function", TokenType.Function },
        { "return", TokenType.Return },
        { "print", TokenType.Print },
        { "input", TokenType.Input }
    };

    public Lexer(string source)
    {
        _source = source;
    }

    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            _start = _current;
            ScanToken();
        }

        _tokens.Add(new Token(TokenType.EOF, "", string.Empty, _line));
        return _tokens;
    }

    private void ScanToken()
    {
        char c = Advance();
        switch (c)
        {
            case '(': AddToken(TokenType.LeftParen); break;
            case ')': AddToken(TokenType.RightParen); break;
            case '{': AddToken(TokenType.LeftBrace); break;
            case '}': AddToken(TokenType.RightBrace); break;
            case ';': AddToken(TokenType.Semicolon); break;
            case '+': AddToken(TokenType.Plus); break;
            case '-': AddToken(TokenType.Minus); break;
            case '*': AddToken(TokenType.Multiply); break;
            case '/': AddToken(TokenType.Divide); break;
            case '=': AddToken(TokenType.Assign); break;
            case '"': String(); break;
            case ' ':
            case '\r':
            case '\t':
                break;
            case '\n':
                _line++;
                break;
            default:
                if (IsDigit(c))
                {
                    Number();
                }
                else if (IsAlpha(c))
                {
                    Identifier();
                }
                else
                {
                    throw new Exception($"Unexpected character at line {_line}");
                }
                break;
        }
    }

    private void Identifier()
    {
        while (IsAlphaNumeric(Peek())) Advance();

        string text = _source.Substring(_start, _current - _start);
        TokenType type = _keywords.ContainsKey(text) ? _keywords[text] : TokenType.Identifier;
        AddToken(type);
    }

    private void Number()
    {
        while (IsDigit(Peek())) Advance();

        string number = _source.Substring(_start, _current - _start);
        AddToken(TokenType.Number, double.Parse(number));
    }

    private bool IsDigit(char c) => c >= '0' && c <= '9';
    private bool IsAlpha(char c) => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
    private bool IsAlphaNumeric(char c) => IsAlpha(c) || IsDigit(c);

    private char Peek() => IsAtEnd() ? '\0' : _source[_current];
    private bool IsAtEnd() => _current >= _source.Length;
    private char Advance() => _source[_current++];

    private void AddToken(TokenType type, object? literal = null)
    {
        string text = _source.Substring(_start, _current - _start);
        _tokens.Add(new Token(type, text, literal ?? string.Empty, _line));
    }

    private void String()
    {
        while (Peek() != '"' && !IsAtEnd())
        {
            if (Peek() == '\n') _line++;
            Advance();
        }

        if (IsAtEnd())
        {
            throw new Exception($"Unterminated string at line {_line}");
        }

        Advance();

        string value = _source.Substring(_start + 1, _current - _start - 2);
        AddToken(TokenType.String, value);
    }
} 