namespace SimpleScript.Core.Lexical;

public enum TokenType
{
    // Keywords
    Let,
    Function,
    Return,
    Print,    // New: for output
    Input,    // New: for input
    
    // Literals
    Identifier,
    Number,
    String,
    
    // Operators
    Plus,
    Minus,
    Multiply,
    Divide,
    Assign,
    
    // Delimiters
    LeftParen,
    RightParen,
    LeftBrace,
    RightBrace,
    Semicolon,
    Quote,     // New: for string literals
    
    // Special
    EOF
} 