
using BynaryTreeSearch;

class Program
{
    static void Main(string[] args)
    {

        var tree = new BinarySearchTree<int>();
        tree.Insert(10);
        tree.Insert(13);
        tree.Insert(11);
        tree.Insert(12);
        tree.Insert(15);
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(1);
        tree.Insert(7);

        //tree.PrintTree();
        tree.Print();
        tree.PrintTree();
        //tree.PrintTree();
        Console.WriteLine("");
        Console.WriteLine("Original Tree:");
        PrintTree(tree);
        int f = tree.MaxPathSum();

        int maxPathSum = tree.maxSum;

        Console.WriteLine($"Max path sum = {maxPathSum}");
        Console.WriteLine($"Max path sum = {f}");

        //                10
        //               /  
        //              2    
        //             / \
        //            1   5
        //                 \
        //                  7


    }

    static void PrintTree(AbstractBinarySearchTree<int> tree)
    {
        PrintTreeRec(tree.GetRoot());
    }

    static void PrintTreeRec(Node<int> root)
    {
        if (root == null)
        {
            return;
        }

        Console.Write(root.Value + " ");
        PrintTreeRec(root.LeftChild);
        PrintTreeRec(root.RightChild);
    }



}
