namespace shox {
public class Shox {
  public static int Main(string[] args) {
    
    /*
     // @@@ Debug
    Expr expression = new Expr.Binary(
      new Expr.Unary(new Token(TokenType.MINUS, "-", null, 1), new Expr.Literal(123)),
      new Token(TokenType.STAR, "*", null, 1),
      new Expr.Grouping(new Expr.Literal(45.67))
    );

    Console.WriteLine(expression.Accept(new ASTPrinter()));
    
    return 0;
    */
    
    if (args.Length > 1) {
      Console.WriteLine("Usage: shox [script]");
      return 1;
    }
    // @@@ Improve command line arg parsing
    else if (args.Length == 1) {
      RunFile(args[0]);
    }
    else {
      RunPrompt();
    }

    return 0;
  }

  public static void RunLine(string line) {
    // @@@ TODO: Update `new` declarations to shortened syntax
    Scanner scanner = new(line);
    List<Token> tokens = scanner.ScanTokens();

    foreach (Token t in tokens) {
      Console.WriteLine(t.ToString());
    }
  }
  
  public static void RunFile(string fileName) {
    // @@@ TODO
  }

  public static void RunPrompt() {
    for (;;) {
      Console.Write("> ");
      string? line = Console.ReadLine();
      if (line == null)
        break;
      Console.WriteLine($"{line}\n");
      RunLine(line);
    }
  }

  public enum SeverityLevel {
    INFO, WARNING, ERROR
  }

  public static void LogMessage(SeverityLevel sevLevel, string message) {
    switch(sevLevel) {
      case SeverityLevel.INFO: Console.WriteLine($"Info: {message}"); break;
      case SeverityLevel.WARNING: Console.WriteLine($"Warning: {message}"); break;
      case SeverityLevel.ERROR: Console.Error.WriteLine($"Error: {message}"); break;
    }
  }
}
} // namspace shox
