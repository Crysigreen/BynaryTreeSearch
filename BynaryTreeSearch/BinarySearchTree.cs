using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BynaryTreeSearch
{
    public class BinarySearchTree<E> : AbstractBinarySearchTree<E> where E : IComparable<E>
    {
        private Node<E> root;
        public int maxSum { get; set; } = int.MinValue;

        public void Insert(E element)
        {
            root = InsertRec(root, element);
        }

        private Node<E> InsertRec(Node<E> root, E element)
        {
            if (root == null)
            {
                return new Node<E>(element);
            }

            if (element.CompareTo(root.Value) < 0)
            {
                root.LeftChild = InsertRec(root.LeftChild, element);
            }
            else if (element.CompareTo(root.Value) > 0)
            {
                root.RightChild = InsertRec(root.RightChild, element);
            }

            return root;
        }

        public bool Contains(E element)
        {
            return ContainsRec(root, element);
        }

        private bool ContainsRec(Node<E> root, E element)
        {
            if (root == null)
            {
                return false;
            }

            if (element.CompareTo(root.Value) == 0)
            {
                return true;
            }

            if (element.CompareTo(root.Value) < 0)
            {
                return ContainsRec(root.LeftChild, element);
            }
            else
            {
                return ContainsRec(root.RightChild, element);
            }
        }

        public AbstractBinarySearchTree<E> Search(E element)
        {
            Node<E> result = SearchRec(root, element);
            return result != null ? new BinarySearchTree<E> { root = result } : new BinarySearchTree<E>();
        }

        private Node<E> SearchRec(Node<E> root, E element)
        {
            if (root == null || element.CompareTo(root.Value) == 0)
            {
                return root;
            }

            if (element.CompareTo(root.Value) < 0)
            {
                return SearchRec(root.LeftChild, element);
            }
            else
            {
                return SearchRec(root.RightChild, element);
            }
        }

        public Node<E> GetRoot()
        {
            return root;
        }

        public Node<E> GetLeft()
        {
            return root?.LeftChild;
        }

        public Node<E> GetRight()
        {
            return root?.RightChild;
        }

        public E GetValue()
        {
            return root != null ? root.Value : default(E);
        }

        public int MaxPathSum()
        {
            return MaxPathSumRec(root);
        }

        private int MaxPathSumRec(Node<E> node)
        {
            if (node == null)
            {
                return 0;
            }

            // Рекурсивно находим максимальную сумму для левого и правого поддерева
            int leftGain = Math.Max(MaxPathSumRec(node.LeftChild), 0);
            int rightGain = Math.Max(MaxPathSumRec(node.RightChild), 0);

            // Считаем gain для текущей вершины node
            int priceNewPath = Convert.ToInt32(node.Value) + leftGain + rightGain;

            // Обновляем глобальный maxSum (если необходимо)
            // maxSum - это ваша глобальная переменная для хранения максимальной суммы
            maxSum = Math.Max(maxSum, priceNewPath);

            // Возвращаем максимальное поддерево, которое может быть частью другого пути
            return Convert.ToInt32(node.Value) + Math.Max(leftGain, rightGain);
        }
    




    public void PrintTree()
        {
            PrintTree(root, 0);
        }

        private void PrintTree(Node<E> node, int indent)
        {
            if (node == null)
                return;

            PrintTree(node.RightChild, indent + 4);

            Console.Write(new string(' ', indent));
            Console.WriteLine(node.Value);

            PrintTree(node.LeftChild, indent + 4);
        }

    }


    public static class BinaryTreePrinter
    {
        class NodeInfo<E> where E : IComparable<E>
        {
            public Node<E> Node;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo<E> Parent, Left, Right;
        }

        public static void Print<E>(this BinarySearchTree<E> root, int topMargin = 2, int leftMargin = 2) where E : IComparable<E>
        {
            if (root == null || root.GetRoot() == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo<E>>();
            var next = root.GetRoot();
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo<E> { Node = next, Text = $" {next.Value} " };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + 1;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.LeftChild)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos);
                    }
                }
                next = next.LeftChild ?? next.RightChild;
                for (; next == null; item = item.Parent)
                {
                    Print(item, rootTop + 2 * level);
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos;
                        next = item.Parent.Node.RightChild;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos;
                        else
                            item.Parent.StartPos += (item.StartPos - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print<E>(NodeInfo<E> item, int top) where E : IComparable<E>
        {
            SwapColors();
            Print(item.Text, top, item.StartPos);
            SwapColors();
            if (item.Left != null)
                PrintLink(top + 1, "┌", "┘", item.Left.StartPos + item.Left.Size / 2, item.StartPos);
            if (item.Right != null)
                PrintLink(top + 1, "└", "┐", item.EndPos - 1, item.Right.StartPos + item.Right.Size / 2);
        }

        private static void PrintLink(int top, string start, string end, int startPos, int endPos)
        {
            Print(start, top, startPos);
            Print("─", top, startPos + 1, endPos);
            Print(end, top, endPos);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }

        private static void SwapColors()
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
        }
    }




}
