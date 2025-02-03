using System;
using System.Collections.Generic;
using SimpleScript.Core.Lexical;
using SimpleScript.Core.Scanning;
using SimpleScript.Core.Parsing;
using SimpleScript.Core.Interpreting;

namespace SimpleScript.Core;

class Program
{
    static void Main(string[] args)
    {
        string sourceCode = @"
            let name = input name;
            print ""Hello, "";
            print name;
            
            let x = 42;
            print x;
            
            function greet() {
                let message = ""Welcome!"";
                print message;
                return message;
            }
        ";

        try
        {
            Lexer lexer = new Lexer(sourceCode);
            List<Token> tokens = lexer.ScanTokens();
            
            Console.WriteLine("Tokens:");
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }

            Parser parser = new Parser(tokens);
            parser.Parse();
            
            Console.WriteLine("\nParsing completed successfully!");
            
            Console.WriteLine("\nProgram Output:");
            Console.WriteLine("---------------");
            Interpreter interpreter = new Interpreter();
            interpreter.Execute(tokens);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
} 