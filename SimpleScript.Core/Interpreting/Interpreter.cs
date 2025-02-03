using System;
using System.Collections.Generic;
using SimpleScript.Core.Lexical;

namespace SimpleScript.Core.Interpreting;

public class Interpreter
{
    private readonly Dictionary<string, object> _variables = new();

    public void Execute(List<Token> tokens)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            var token = tokens[i];
            switch (token.Type)
            {
                case TokenType.Let:
                    i++; // Move to variable name
                    if (i < tokens.Count)
                    {
                        var varName = tokens[i].Lexeme;
                        i += 2; // Skip '=' token
                        if (i < tokens.Count)
                        {
                            if (tokens[i].Type == TokenType.Input)
                            {
                                i++; // Skip input token
                                Console.Write($"Enter value for {varName}: ");
                                string? input = Console.ReadLine();
                                _variables[varName] = input ?? string.Empty;
                            }
                            else
                            {
                                _variables[varName] = tokens[i].Value;
                            }
                        }
                    }
                    break;

                case TokenType.Print:
                    i++; // Move to next token
                    if (i < tokens.Count)
                    {
                        var valueToPrint = tokens[i];
                        if (valueToPrint.Type == TokenType.String)
                        {
                            Console.Write(valueToPrint.Value);
                        }
                        else if (valueToPrint.Type == TokenType.Identifier && _variables.ContainsKey(valueToPrint.Lexeme))
                        {
                            Console.Write(_variables[valueToPrint.Lexeme]);
                        }
                        else if (valueToPrint.Type == TokenType.Number)
                        {
                            Console.Write(valueToPrint.Value);
                        }
                    }
                    break;
            }
        }
    }
} 