namespace shox {
public class Scanner
{
  public string source { get; set; }
  public List<Token> tokens { get; set; }
  public int start { get; set; } // First char in lexeme
  public int current { get; set; } // Current char in lexeme
  public int line { get; set; }

  public Dictionary<string, TokenType> keywords = new() {
    {"and",    TokenType.AND},
    {"class",  TokenType.CLASS},
    {"else",   TokenType.ELSE},
    {"false",  TokenType.FALSE},
    {"for",    TokenType.FOR},
    {"fun",    TokenType.FUN},
    {"if",     TokenType.IF},
    {"nil",    TokenType.NIL},
    {"or",     TokenType.OR},
    {"print",  TokenType.PRINT},
    {"return", TokenType.RETURN},
    {"super",  TokenType.SUPER},
    {"this",   TokenType.THIS},
    {"true",   TokenType.TRUE},
    {"var",    TokenType.VAR},
    {"while",  TokenType.WHILE}
  };

  public Scanner(string source) {
    this.source = source;
    tokens = new List<Token>(); 
    line = 1;
  }

  public List<Token> ScanTokens() {
    while (!IsAtEnd()) {
      start = current;
      ScanToken();
    }

    tokens.Add(new Token(TokenType.EOF, string.Empty, null, line));
    return tokens;
  }

  public void ScanToken() {
    char c = source[current++];

    switch (c) {
      // Single characters
      case '(':
        AddToken(TokenType.LEFT_PAREN, null); break;
      case ')':
        AddToken(TokenType.RIGHT_PAREN, null); break;
      case '{':
        AddToken(TokenType.LEFT_BRACE, null); break;
      case '}':
        AddToken(TokenType.RIGHT_BRACE, null); break;
      case ',':
        AddToken(TokenType.COMMA, null); break;
      case '.':
        AddToken(TokenType.DOT, null); break;
      case '-':
        AddToken(TokenType.MINUS, null); break;
      case '+':
        AddToken(TokenType.PLUS, null); break;
      case ';':
        AddToken(TokenType.SEMICOLON, null); break;
      case '*':
        AddToken(TokenType.STAR, null); break;

      // Whitespace
      case ' ':
      case '\t':
      case '\r':
        break;

      case '\n':
        line++; break;

      // Possibly multiple characters
      case '!':
        AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG, null); break;
      case '=':
        AddToken(Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL, null); break;
      case '<':
        AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS, null); break;
      case '>':
        AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.LESS, null); break;

      // Multiple characters
      case '/':
        if (Match('/')) {
          while (Peek() != '\n' && !IsAtEnd())
            current++;
        } else {
          AddToken(TokenType.SLASH, null);
        }
        break;

      case '"':
        ScanStringLiteral(); break;
      
      default:
        if (char.IsDigit(c)) {
          ScanNumberLiteral(); break;
        } else if (char.IsLetter(source[current])) {
          ScanIdent(); break;
        }
        Shox.LogMessage(Shox.SeverityLevel.ERROR, $"Line {line}: Unexpected token"); break;
    }
  }

  public char Peek() {
    if (IsAtEnd())
      return '\0';
    return source[current];
  }

  public bool Match(char expected) {
    if (IsAtEnd()) 
      return false;

    if (source[current] != expected) 
      return false;

    current++;
    return true;
  }

  public bool IsAtEnd() {
    return current >= source.Length;
  }

  /*
  public char Advance() {
    return source[current++];
  }
  */

  public void ScanStringLiteral() {
    current++;
    
    while (!IsAtEnd() && source[current] != '"') {
      if (source[current] == '\n')
        line++;
      current++;
    }

    if (IsAtEnd()) {
      Shox.LogMessage(Shox.SeverityLevel.ERROR, "Unclosed string literal");
      return;
    }

    start++;
    AddToken(TokenType.STRING, null);
    current++;
  }

  public void ScanNumberLiteral() {
    bool isFloat = false;
    
    while (!IsAtEnd() && char.IsDigit(source[current]))
      current++;

    if (!IsAtEnd() && source[current] == '.' && char.IsDigit(source[current + 1])) {
      isFloat = true;
      current++; // Eat the decimal point
      
      // @@@ What happens if we run into another decimal point?
      while (!IsAtEnd() && char.IsDigit(source[current]))
        current++;
    }
    
    if(isFloat)
      AddToken(TokenType.NUMBER, Double.Parse(source.Substring(start, current - start)));
    else
      AddToken(TokenType.NUMBER, Int32.Parse(source.Substring(start, current - start)));
  }

  public void ScanIdent() {
    while (!IsAtEnd() && char.IsLetter(source[current]))
      current++;

    TokenType type;
    bool keyResult = keywords.TryGetValue(source.Substring(start, current - start), out type);

    if (keyResult)
      AddToken(type, null);
    else
      AddToken(TokenType.IDENTIFIER, null);
  }
  
  public void AddToken(TokenType type, object? literal) {
    tokens.Add(new Token(type, source.Substring(start, current - start), literal, line));
  }
}
} // namespace shox
