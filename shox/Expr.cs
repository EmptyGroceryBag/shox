namespace shox;

public abstract class Expr {
  public abstract T Accept<T>(IVisitor<T> v);
  
  // AST Classes
  public class Binary : Expr {
    public Expr? left { get; set; }
    public Token op { get; set; }
    public Expr? right { get; set; }

    public Binary(Expr? left, Token op, Expr? right) {
      this.left = left;
      this.op = op;
      this.right = right;
    }

    public override T Accept<T>(IVisitor<T> v) {
      return v.VisitBinaryExpr(this);
    }
  }
  
  public class Grouping : Expr {
    public Expr? expression { get; set; }

    public Grouping(Expr? expression) {
      this.expression = expression;
    }

    public override T Accept<T>(IVisitor<T> v) {
      return v.VisitGroupingExpr(this);
    }
  }
  
  public class Literal : Expr {
    public object? value { get; set; }

    public Literal(object? value) {
      this.value = value;
    }

    public override T Accept<T>(IVisitor<T> v) {
      return v.VisitLiteralExpr(this);
    }
  }
  
  public class Unary : Expr {
    public Token op { get; set; }
    public Expr? right { get; set; }

    public Unary(Token op, Expr? right) {
      this.op = op;
      this.right = right;
    }

    public override T Accept<T>(IVisitor<T> v) {
      return v.VisitUnaryExpr(this);
    }
  }

  // Visitors
  public interface IVisitor<T> {
    public T VisitBinaryExpr(Binary binExpr);
    public T VisitGroupingExpr(Grouping binExpr);
    public T VisitLiteralExpr(Literal binExpr);
    public T VisitUnaryExpr(Unary binExpr);
  }
}