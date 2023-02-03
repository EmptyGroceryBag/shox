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
  public TokenType Type { get; set; }
  public string Lexeme { get; set; }
  public object? Literal { get; set; }
  public int Line { get; set; }

  public Token(
    TokenType Type,
    string Lexeme,
    object? Literal,
    int Line
  ) 
  {
    this.Type = Type;
    this.Lexeme = Lexeme;
    this.Literal = Literal;
    this.Line = Line;
  }

  public bool Equals(Token? other) {
    if (other == null)
      return false;
    
    if (other.Type != Type) return false;
    if (other.Lexeme != Lexeme) return false;
    if (other.Literal != null) {
      if (!other.Literal.Equals(Literal)) 
        return false;
    }
    if (other.Line != Line) return other.Line != Line;

    return true;
  }

  public override string ToString() {
    string strType    = $"  Type    = {Type}";
    string strLexeme  = $"  Lexeme  = {Lexeme}";
    string strLiteral = $"  Literal = {Literal}";
    string strLine    = $"  Line    = {Line}";

    return $"Token:\n{strType}\n{strLexeme}\n{strLiteral}\n{strLine}\n";
  }
}
} // namespace shox
