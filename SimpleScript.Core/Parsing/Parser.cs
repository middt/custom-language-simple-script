using System;
using System.Collections.Generic;
using SimpleScript.Core.Lexical;

namespace SimpleScript.Core.Parsing;

public class Parser
{
    private readonly List<Token> _tokens;
    private int _current = 0;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    public void Parse()
    {
        while (!IsAtEnd())
        {
            try
            {
                ParseDeclaration();
            }
            catch (Exception)
            {
                Synchronize();
            }
        }
    }

    private void ParseDeclaration()
    {
        if (Match(TokenType.Let))
        {
            ParseVariableDeclaration();
        }
        else if (Match(TokenType.Function))
        {
            ParseFunctionDeclaration();
        }
        else
        {
            ParseStatement();
        }
    }

    private void ParseStatement()
    {
        if (Match(TokenType.Return))
        {
            ParseReturnStatement();
        }
        else if (Match(TokenType.Print))
        {
            ParsePrintStatement();
        }
        else if (Match(TokenType.Input))
        {
            ParseInputStatement();
        }
        else
        {
            ParseExpression();
            Consume(TokenType.Semicolon, "Expect ';' after expression.");
        }
    }

    private void ParseReturnStatement()
    {
        if (!Check(TokenType.Semicolon))
        {
            ParseExpression();
        }
        Consume(TokenType.Semicolon, "Expect ';' after return value.");
    }

    private void ParsePrintStatement()
    {
        ParseExpression();
        Consume(TokenType.Semicolon, "Expect ';' after value.");
    }

    private void ParseInputStatement()
    {
        Token variable = Consume(TokenType.Identifier, "Expect variable name after 'input'.");
        Consume(TokenType.Semicolon, "Expect ';' after input statement.");
    }

    private void ParseVariableDeclaration()
    {
        Token name = Consume(TokenType.Identifier, "Expect variable name.");
        Consume(TokenType.Assign, "Expect '=' after variable name.");
        ParseExpression();
        Consume(TokenType.Semicolon, "Expect ';' after variable declaration.");
    }

    private void ParseFunctionDeclaration()
    {
        Token name = Consume(TokenType.Identifier, "Expect function name.");
        Consume(TokenType.LeftParen, "Expect '(' after function name.");
        // Parse parameters
        Consume(TokenType.RightParen, "Expect ')' after parameters.");
        
        // Parse function body
        Consume(TokenType.LeftBrace, "Expect '{' before function body.");
        while (!Check(TokenType.RightBrace) && !IsAtEnd())
        {
            ParseDeclaration();
        }
        Consume(TokenType.RightBrace, "Expect '}' after function body.");
    }

    private void ParseExpression()
    {
        // For now, handle basic expressions
        if (Match(TokenType.Number))
        {
            // Consumed number
            return;
        }
        else if (Match(TokenType.String))
        {
            // Consumed string
            return;
        }
        else if (Match(TokenType.Identifier))
        {
            // Consumed identifier
            return;
        }
        else
        {
            throw new Exception($"Unexpected token {Peek().Type} at line {Peek().Line}");
        }
    }

    private void Synchronize()
    {
        Advance();

        while (!IsAtEnd())
        {
            if (Previous().Type == TokenType.Semicolon) return;

            switch (Peek().Type)
            {
                case TokenType.Function:
                case TokenType.Let:
                case TokenType.Return:
                    return;
            }

            Advance();
        }
    }

    private bool Match(TokenType type)
    {
        if (Check(type))
        {
            Advance();
            return true;
        }
        return false;
    }

    private Token Consume(TokenType type, string message)
    {
        if (Check(type)) return Advance();
        throw new Exception($"{message} at line {Peek().Line}");
    }

    private bool Check(TokenType type)
    {
        if (IsAtEnd()) return false;
        return Peek().Type == type;
    }

    private Token Advance()
    {
        if (!IsAtEnd()) _current++;
        return Previous();
    }

    private bool IsAtEnd() => Peek().Type == TokenType.EOF;
    private Token Peek() => _tokens[_current];
    private Token Previous() => _tokens[_current - 1];
} 