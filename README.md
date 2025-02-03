# SimpleScript Programming Language

A simple interpreted scripting language implementation in C#.

## Features
- Variable declarations with `let`
- Basic arithmetic operations
- String literals and concatenation
- Function definitions with `function` and `return`
- Input/output operations:
  - `print` statements
  - `input` for user input
- Error handling with line number tracking

## Getting Started

1. Clone repository
```bash
git clone https://github.com/middt/custom-language-simple-script.git
```

2. Build project
```bash
dotnet build
```

3. Run interpreter
```bash
dotnet run --project SimpleScript.Core
```

## Usage Example
```javascript
let name = input "Enter your name: ";
print "Hello, ";
print name;

function greet() {
    let message = "Welcome!";
    print message;
    return message;
}

let result = greet();
```

## Project Structure
```
SimpleScript.Core/
├── Program.cs             - Main entry point and demo program
├── Scanning/
│   └── Lexer.cs          - Lexical analyzer (tokenizer)
├── Parsing/
│   └── Parser.cs         - Syntax parser
├── Interpreting/
│   └── Interpreter.cs    - Execution engine
└── Lexical/
    ├── Token.cs          - Token type definitions
    └── TokenType.cs      - Token classification
```

## Language Syntax
```javascript
// Variable declaration
let x = 42;

// Function definition
function add(a, b) {
    return a + b;
}

// IO operations
let name = input "Enter name: ";
print "Hello " + name;
```

## Development Requirements
- .NET 9.0 SDK
- Visual Studio Code/Rider/Visual Studio
