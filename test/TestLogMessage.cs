using Xunit;
using shox;

namespace test;
public class TestLogMessage
{
  private string testString = "show me a sign so I know it's you!";
  
  [Fact]
  public void TestLogMessageInfo() {
    // Setup
    StringWriter stdout = new StringWriter();
    TextWriter console = Console.Out;
    Console.SetOut(stdout);
    
    Shox.LogMessage(Shox.SeverityLevel.INFO, testString);
    Assert.Equal($"Info: {testString}\r\n", stdout.ToString());

    // Cleanup
    Console.SetOut(console);
  }

  [Fact]
  public void TestLogMessageWarning() {
    // Setup
    StringWriter stdout = new StringWriter();
    TextWriter console = Console.Out;
    Console.SetOut(stdout);
    
    Shox.LogMessage(Shox.SeverityLevel.WARNING, testString);
    Assert.Equal($"Warning: {testString}\r\n", stdout.ToString());

    // Cleanup
    Console.SetOut(console);
  }

  [Fact]
  public void TestLogMessageError() {
    // Setup
    StringWriter stdout = new StringWriter();
    TextWriter console = Console.Out;
    Console.SetError(stdout);
    
    Shox.LogMessage(Shox.SeverityLevel.ERROR, testString);
    Assert.Equal($"Error: {testString}\r\n", stdout.ToString());

    // Cleanup
    Console.SetError(console);
  }
} // namespace test