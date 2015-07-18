
using Antlr4.Runtime;
using System;
namespace CalculatorWithVersion4
{
    class Program
    {
        /// <summary>
        /// Using http://programming-pages.com/2013/12/14/antlr-4-with-c-and-visual-studio-2012/ as a guide.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string test = "a = 10*10";
            var input = new AntlrInputStream(test);
            var lexer = new calculatorLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            var parser = new calculatorParser(tokens);
            var tree = parser.equation();
            Console.WriteLine(tree.ToStringTree(parser));
        }
    }
}
