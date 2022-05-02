using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N3U1P9_BST
{
    enum Operator
    {
        Add = ((byte)'+'),
        Sub = ((byte)'-'),
        Mul = ((byte)'*'),
        Div = ((byte)'/'),
        Pow = ((byte)'^')
    }
    abstract class Node
    {
        readonly public char Data;
        readonly public Node Left;
        readonly public Node Right;

        public Node(char d, Node l, Node r)
        {
            Data = d;
            Left = l;
            Right = r;
        }

        public Node(char d) : this(d, null, null) { }
    }

    class OperandNode : Node
    {
        public OperandNode(char d) : base(d) { }
    }

    class OperatorNode : Node
    {
        public readonly Operator Op;

        public OperatorNode(char d, Node l, Node r) : base(d, l, r)
        {
           Op = (Operator) Enum.Parse(typeof(Operator), ((byte) d).ToString());
        }
    }
    class BinaryExpressionTree
    {
        readonly Node Root;

        public static BinaryExpressionTree Build(string expression)
        {
            return Build(expression.ToCharArray());
        }

        public static BinaryExpressionTree Build(char[] expression)
        {
            char[] Operators = { '+', '-', '*', '/', '^' };
            Stack<Node> NodeStack = new Stack<Node>();

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                int n;
                bool isNum = int.TryParse(c.ToString(), out n);
                bool isOp = Operators.Contains(c);

                if (isNum)
                {
                    NodeStack.Push(new OperandNode(c));
                }
                else if (isOp)
                {
                    Node Left = NodeStack.Pop();
                    Node Right = NodeStack.Pop();
                    NodeStack.Push(new OperatorNode(c, Left, Right));
                }
                else
                {
                    throw new InvalidExpressionException(new string(expression), i + 1);
                }
            }

            return new BinaryExpressionTree(NodeStack.Pop());
        }

        public override string ToString()
        {
            if (Root == null) return "";
            else return ToString(Root);
        }

        private string ToString(Node node)
        {
            List<char> WalkList = new List<char>();
            PostOrderWalk(WalkList, node);
            return new string(WalkList.ToArray());
        }

        private void PostOrderWalk(List<char> WalkList, Node p)
        {
            if (p != null)
            {
                PostOrderWalk(WalkList, p.Right);
                PostOrderWalk(WalkList, p.Left);
                WalkList.Add(p.Data);
            }
        }

        public string Convert()
        {
            if (Root == null) return "";
            else return Convert(Root);
        }

        string Convert(Node node)
        {
            string WalkString = "";
            if (node is OperatorNode)
            {
                return WalkString + $"({Convert(node.Right)}{node.Data}{Convert(node.Left)})";
            }
            WalkString += node.Data;
            return WalkString;
            
        }

        public double Evaluate()
        {
            if (Root == null) return 0;
            else return Evaluate(Root);
        }

        double Evaluate(Node node)
        {
            if (node == null) return 0;
            if (node is OperandNode) return Double.Parse(node.Data.ToString());
            else
            {
                double Right = Evaluate(node.Right);
                double Left = Evaluate(node.Left);

                switch ((node as OperatorNode).Op)
                {
                    case Operator.Add:
                        return Right + Left;
                    case Operator.Sub:
                        return Right - Left;
                    case Operator.Mul:
                        return Right * Left;
                    case Operator.Div:
                        return Right / Left;
                    case Operator.Pow:
                        return Math.Pow(Right, Left);
                    default:
                        return double.NaN;
                }
            }
        }

        private BinaryExpressionTree(Node root)
        {
            Root = root;
        }

    }

    class InvalidExpressionException : Exception
    {
        public InvalidExpressionException(string Exp, int Index) : base($"Invalid character found at position: {Index}, in the followin gexpression: '{Exp}'!") { }
        public override string ToString()
        {
            return $"InvalidExpressionException: {Message}";
        }
    }
}
