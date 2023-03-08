namespace shox {
public enum TokenType {
  // Single-character tokens.
  LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE, RIGHT_BRACE,
  COMMA, DOT, MINUS, PLUS, SEMICOLON, SLASH, STAR,

  // One or two character tokens.
  BANG, BANG_EQUAL,
  EQUAL, EQUAL_EQUAL,
  GREATER, GREATER_EQUAL,
  LESS, LESS_EQUAL,

  // Literals.
  IDENTIFIER, STRING, NUMBER,

  // Keywords.
  AND, CLASS, ELSE, FALSE, FUN, FOR, IF, NIL, OR,
  PRINT, RETURN, SUPER, THIS, TRUE, VAR, WHILE,

  EOF
}

public class Token : IEquatable<Token> {
  public TokenType type { get; set; }
  public string lexeme { get; set; }
  public object? literal { get; set; }
  public int line { get; set; }

  public Token(
    TokenType type,
    string lexeme,
    object? literal,
    int line
  ) 
  {
    this.type = type;
    this.lexeme = lexeme;
    this.literal = literal;
    this.line = line;
  }

  public bool Equals(Token? other) {
    if (other == null)
      return false;
    
    if (other.type != type) return false;
    if (other.lexeme != lexeme) return false;
    if (other.literal != null) {
      if (!other.literal.Equals(literal)) 
        return false;
    }

    if (other.line != line)
      return other.line != line;


    return true;
  }

  public override string ToString() {
    string strType    = $"  Type    = {type}";
    string strLexeme  = $"  Lexeme  = {lexeme}";
    string strLiteral = $"  Literal = {literal}";
    string strLine    = $"  Line    = {line}";

    return $"Token:\n{strType}\n{strLexeme}\n{strLiteral}\n{strLine}\n";
  }
}
} // namespace shox
