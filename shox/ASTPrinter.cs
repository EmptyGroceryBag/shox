using System.Text;

namespace shox; 

public class ASTPrinter : Expr.IVisitor<string> {
  public string VisitBinaryExpr(Expr.Binary expr) {
    return parenthesize(expr.op.Lexeme, expr.left, expr.right);
  }

  public string VisitGroupingExpr(Expr.Grouping expr) {
    return parenthesize("group", expr.expression);
  }

  public string VisitLiteralExpr(Expr.Literal expr) {
    // @@@ Maybe I should replace `object` with a type parameter
    if (expr.value == null)
      return "nil";
    return expr.value.ToString();
  }

  public string VisitUnaryExpr(Expr.Unary expr) {
    return parenthesize(expr.op.Lexeme, expr.right);
  }
  
  public string parenthesize(string name, params Expr[] exprs) {
    StringBuilder exprString = new();

    exprString.Append($"({name}");
    foreach (Expr expr in exprs)
      exprString.Append($" {expr.Accept<string>(this)}");

    exprString.Append(')');

    return exprString.ToString();
  }
}