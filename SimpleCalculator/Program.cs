using Antlr4.Runtime;
using System;

namespace SimpleCalculator
{
    /// <summary>
    /// Visit our grammar and perform the calculation.
    /// </summary>
    class CalcVisitor : simplecalcBaseVisitor<int>
    {
        /// <summary>
        /// Do add or subtract
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override int VisitAddSub(simplecalcParser.AddSubContext context)
        {
            var left = Visit(context.expr(0));
            var right = Visit(context.expr(1));

            return context.op.Type == simplecalcLexer.ADD ? left + right : left - right;
        }

        /// <summary>
        /// Do multiply or divide
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override int VisitMulDiv(simplecalcParser.MulDivContext context)
        {
            var left = Visit(context.expr(0));
            var right = Visit(context.expr(1));

            return context.op.Type == simplecalcLexer.MUL ? left * right : left / right;
        }

        /// <summary>
        /// Parse an integer
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override int VisitInt(simplecalcParser.IntContext context)
        {
            return int.Parse(context.INT().GetText());
        }
    }

    /// <summary>
    /// Run a quick test
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string test = "10*10 - 20";
            var input = new AntlrInputStream(test);
            var lexer = new simplecalcLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            var parser = new simplecalcParser(tokens);
            var tree = parser.prog();
            Console.WriteLine(tree.ToStringTree(parser));

            // Now visit the darn thing.

            var result = new CalcVisitor().Visit(tree);
            Console.WriteLine("  -> Result: {0}", result);
        }
    }
}
