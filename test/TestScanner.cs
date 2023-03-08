using Xunit;
using shox;

namespace test;
public class TestScanner
{
  [Fact]
  public void TestToken()
  {
    Scanner scanner = new Scanner("{}");
    List<Token> tokens = scanner.ScanTokens();

    const int LENGTH = 3;
    Assert.Equal(LENGTH, tokens.Count); // Should be three tokens: {, }, EOF

    Assert.Equal(TokenType.LEFT_BRACE, tokens[0].type);
    Assert.Equal("{", tokens[0].lexeme);

    Assert.Equal(TokenType.RIGHT_BRACE, tokens[1].type);
    Assert.Equal("}", tokens[1].lexeme);

    Assert.Equal(TokenType.EOF, tokens[LENGTH - 1].type);
    Assert.Equal(string.Empty, tokens[LENGTH - 1].lexeme);
  }

  [Fact]
  public void TestComment()
  {
    Scanner scanner = new Scanner("//This is a comment, and the only token in the list should be an EOF");
    List<Token> tokens = scanner.ScanTokens();
    Assert.Single(tokens);

    Assert.Equal(TokenType.EOF, tokens[0].type);
    Assert.Equal(string.Empty, tokens[0].lexeme);
  }

  [Fact]
  public void TestCommentWithAppendedToken()
  {
    Scanner scanner = new Scanner("//This is a comment with a RIGHT_BRACKET after it\n}");
    List<Token> tokens = scanner.ScanTokens();
    const int LENGTH = 2;
    Assert.Equal(LENGTH, tokens.Count);

    Assert.Equal(TokenType.RIGHT_BRACE, tokens[0].type);
    Assert.Equal("}", tokens[0].lexeme);

    Assert.Equal(TokenType.EOF, tokens[LENGTH - 1].type);
    Assert.Equal(string.Empty, tokens[LENGTH - 1].lexeme);
  }

  [Fact]
  public void TestCommentWithPrependedToken() {
    Scanner scanner = new Scanner("}//This is a comment with a RIGHT_BRACKET before it");
    List<Token> tokens = scanner.ScanTokens();
    
    const int LENGTH = 2;
    Assert.Equal(LENGTH, tokens.Count);
    
    Assert.Equal(TokenType.RIGHT_BRACE,tokens[0].type);
    Assert.Equal("}", tokens[0].lexeme);
    
    Assert.Equal(TokenType.EOF, tokens[LENGTH - 1].type);
    Assert.Equal(string.Empty, tokens[LENGTH - 1].lexeme);
  }
  
  [Fact]
  public void TestStringLiteral() {
    Scanner scanner = new Scanner("\"string literal\"");
    List<Token> tokens = scanner.ScanTokens();

    const int LENGTH = 2;
    Assert.Equal(LENGTH, tokens.Count);
    
    Assert.Equal(TokenType.STRING,tokens[0].type);
    Assert.Equal("string literal", tokens[0].lexeme);
    
    Assert.Equal(TokenType.EOF, tokens[LENGTH - 1].type);
    Assert.Equal(string.Empty, tokens[LENGTH - 1].lexeme);
  }
  
  [Fact]
  public void TestUnclosedStringLiteral() {
    Scanner scanner = new Scanner("\"string literal");
    List<Token> tokens = scanner.ScanTokens();
    
    Assert.Single(tokens);

    Assert.Equal(TokenType.EOF,tokens[0].type);
    Assert.Equal(string.Empty, tokens[0].lexeme);
  }
  
  [Fact]
  public void TestIntegerLiteral() {
    Scanner scanner = new Scanner("1234");
    List<Token> tokens = scanner.ScanTokens();

    const int LENGTH = 2;
    Assert.Equal(LENGTH, tokens.Count);
    
    Assert.Equal(TokenType.NUMBER,tokens[0].type);
    Assert.Equal("1234", tokens[0].lexeme);
    Assert.Equal(1234, tokens[0].literal);
    
    Assert.Equal(TokenType.EOF, tokens[LENGTH - 1].type);
    Assert.Equal(string.Empty, tokens[LENGTH - 1].lexeme);
  }
  
  [Fact]
  public void TestFloatingPointLiteral() {
    Scanner scanner = new Scanner("12.34");
    List<Token> tokens = scanner.ScanTokens();

    const int LENGTH = 2;
    Assert.Equal(LENGTH, tokens.Count);
    
    Assert.Equal(TokenType.NUMBER,tokens[0].type);
    Assert.Equal("12.34", tokens[0].lexeme);
    Assert.Equal(12.34, tokens[0].literal);
    
    Assert.Equal(TokenType.EOF, tokens[LENGTH - 1].type);
    Assert.Equal(string.Empty, tokens[LENGTH - 1].lexeme);
  }
  
  [Fact]
  public void TestIdentifier() {
    Scanner scanner = new Scanner("ident");
    List<Token> tokens = scanner.ScanTokens();
    
    const int LENGTH = 2;
    Assert.Equal(LENGTH, tokens.Count);
    
    Assert.Equal(TokenType.IDENTIFIER,tokens[0].type);
    Assert.Equal("ident", tokens[0].lexeme);

    Assert.Equal(TokenType.EOF, tokens[LENGTH - 1].type);
    Assert.Equal(string.Empty, tokens[LENGTH - 1].lexeme);
  }
  
  [Fact]
  public void TestKeyword() {
    Scanner scanner = new Scanner("return");
    List<Token> tokens = scanner.ScanTokens();
    
    const int LENGTH = 2;
    Assert.Equal(LENGTH, tokens.Count);
    
    Assert.Equal(TokenType.RETURN,tokens[0].type);
    Assert.Equal("return", tokens[0].lexeme);

    Assert.Equal(TokenType.EOF,tokens[1].type);
    Assert.Equal(string.Empty, tokens[1].lexeme);
  }
  
  [Fact]
  public void Regression_TestKeywordFollowedBySingleCharToken() {
    Scanner scanner = new Scanner("print(5)");
    List<Token> tokens = scanner.ScanTokens();

    const int LENGTH = 5;
    Assert.Equal(LENGTH, tokens.Count);

    List<Token> expectedTokens = new() {
      new Token(TokenType.PRINT, "print", null, 1),
      new Token(TokenType.LEFT_PAREN, "(", null, 1),
      new Token(TokenType.NUMBER, "5", 5, 1),
      new Token(TokenType.RIGHT_PAREN, ")", null, 1),
      new Token(TokenType.EOF, string.Empty, null, 1),
    };

    for (int i = 0; i < LENGTH; i++) {
      Assert.StrictEqual(expectedTokens[i], tokens[i]);
    }
  }
} // namespace test