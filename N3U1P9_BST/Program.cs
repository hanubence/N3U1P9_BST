using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N3U1P9_BST
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test 'number' is only node..");
            Teszteles(BinaryExpressionTree.Build("7"));
            Console.WriteLine();

            Console.WriteLine("Test 'simple' operators..");
            Teszteles(BinaryExpressionTree.Build("28+"));
            Teszteles(BinaryExpressionTree.Build("28-"));
            Teszteles(BinaryExpressionTree.Build("28*"));
            Teszteles(BinaryExpressionTree.Build("28/"));
            Teszteles(BinaryExpressionTree.Build("28^"));
            Console.WriteLine();

            Console.WriteLine("Test 'example' expressions..");
            Teszteles(BinaryExpressionTree.Build("234*+"));
            Teszteles(BinaryExpressionTree.Build("23*4+"));
            Teszteles(BinaryExpressionTree.Build("23*45*+"));
            Teszteles(BinaryExpressionTree.Build("23+45-/"));
            Teszteles(BinaryExpressionTree.Build("23+4*5+67^8+/"));
            Console.WriteLine();

            try
            {
                Teszteles(BinaryExpressionTree.Build("12-3-A-45"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            

            Console.ReadKey();
        }

        static void Teszteles(BinaryExpressionTree fa)
        {
            Console.Write($"{fa.ToString()}\t= {fa.Convert()}\t= {fa.Evaluate()}\n");
        }
    }
}
