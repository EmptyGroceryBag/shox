using System.Runtime.InteropServices;
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
    if (
      RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
      RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
    ) {
      Assert.Equal($"Info: {testString}\n", stdout.ToString());
    }
    else {
      Assert.Equal($"Info: {testString}\r\n", stdout.ToString());
    }

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
    if (
      RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
      RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
    ) {
      Assert.Equal($"Warning: {testString}\n", stdout.ToString());
    }
    else {
      Assert.Equal($"Warning: {testString}\r\n", stdout.ToString());
    }

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
    if (
      RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
      RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
    ) {
      Assert.Equal($"Error: {testString}\n", stdout.ToString());
    }
    else {
      Assert.Equal($"Error: {testString}\r\n", stdout.ToString());
    }

    // Cleanup
    Console.SetError(console);
  }
} // namespace test