using System;
using System.Collections.Generic;

class BinaryTree
{
    public class Node
    {
        public int Data;
        public Node Left;
        public Node Right;

        public Node(int data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    public Node Root;

    public BinaryTree()
    {
        Root = null;
    }

    public void Add(int data)
    {
        Root = InsertRec(Root, data);
    }

    private Node InsertRec(Node root, int data)
    {
        if (root == null)
        {
            root = new Node(data);
            return root;
        }

        if (data < root.Data)
        {
            root.Left = InsertRec(root.Left, data);
        }
        else if (data > root.Data)
        {
            root.Right = InsertRec(root.Right, data);
        }

        return root;
    }

    public bool IsEmpty()
    {
        return Root == null;
    }

    public int GetSize()
    {
        return CountNodes(Root);
    }

    private int CountNodes(Node node)
    {
        if (node == null)
        {
            return 0;
        }
        else
        {
            return 1 + CountNodes(node.Left) + CountNodes(node.Right);
        }
    }

    public bool ContainsNode(int data)
    {
        return SearchRec(Root, data);
    }

    private bool SearchRec(Node root, int data)
    {
        if (root == null)
        {
            return false;
        }

        if (data == root.Data)
        {
            return true;
        }

        if (data < root.Data)
        {
            return SearchRec(root.Left, data);
        }

        return SearchRec(root.Right, data);
    }

    public void Delete(int data)
    {
        Root = DeleteRec(Root, data);
    }

    private Node DeleteRec(Node root, int data)
    {
        if (root == null)
        {
            return root;
        }

        if (data < root.Data)
        {
            root.Left = DeleteRec(root.Left, data);
        }
        else if (data > root.Data)
        {
            root.Right = DeleteRec(root.Right, data);
        }
        else
        {
            if (root.Left == null)
            {
                return root.Right;
            }
            else if (root.Right == null)
            {
                return root.Left;
            }

            root.Data = MinValue(root.Right);

            root.Right = DeleteRec(root.Right, root.Data);
        }

        return root;
    }

    private int MinValue(Node node)
    {
        int minValue = node.Data;
        while (node.Left != null)
        {
            minValue = node.Left.Data;
            node = node.Left;
        }
        return minValue;
    }

    public void TraverseInOrder(Node root)
    {
        if (root != null)
        {
            TraverseInOrder(root.Left);
            Console.Write(root.Data + " ");
            TraverseInOrder(root.Right);
        }
    }

    public void TraversePreOrder(Node root)
    {
        if (root != null)
        {
            Console.Write(root.Data + " ");
            TraversePreOrder(root.Left);
            TraversePreOrder(root.Right);
        }
    }

    public void TraversePostOrder(Node root)
    {
        if (root != null)
        {
            TraversePostOrder(root.Left);
            TraversePostOrder(root.Right);
            Console.Write(root.Data + " ");
        }
    }

    public void TraverseLevelOrder()
    {
        if (Root == null)
        {
            return;
        }

        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            Node tempNode = queue.Dequeue();
            Console.Write(tempNode.Data + " ");

            if (tempNode.Left != null)
            {
                queue.Enqueue(tempNode.Left);
            }

            if (tempNode.Right != null)
            {
                queue.Enqueue(tempNode.Right);
            }
        }
    }

    public void TraverseInOrderWithoutRecursion()
    {
        Stack<Node> stack = new Stack<Node>();
        Node current = Root;

        while (current != null || stack.Count > 0)
        {
            while (current != null)
            {
                stack.Push(current);
                current = current.Left;
            }

            current = stack.Pop();
            Console.Write(current.Data + " ");
            current = current.Right;
        }
    }
    public void Visualize(Node node, string prefix, bool isLeft)
    {
        if (node != null)
        {
            Console.WriteLine(prefix + (isLeft ? "├── " : "└── ") + node.Data);
            Visualize(node.Left, prefix + (isLeft ? "│   " : "    "), true);
            Visualize(node.Right, prefix + (isLeft ? "│   " : "    "), false);
        }
    }
    public void PrintTree(Node node, string indent, bool isLast)
    {
        if (node != null)
        {
            Console.Write(indent);
            if (isLast)
            {
                Console.Write("└─");
                indent += "  ";
            }
            else
            {
                Console.Write("├─");
                indent += "│ ";
            }

            Console.WriteLine(node.Data);

            PrintTree(node.Left, indent, false);
            PrintTree(node.Right, indent, true);
        }
    }

    public void TraversePreOrderWithoutRecursion()
    {
        if (Root == null)
        {
            return;
        }

        Stack<Node> stack = new Stack<Node>();
        stack.Push(Root);

        while (stack.Count > 0)
        {
            Node current = stack.Pop();
            Console.Write(current.Data + " ");

            if (current.Right != null)
            {
                stack.Push(current.Right);
            }

            if (current.Left != null)
            {
                stack.Push(current.Left);
            }
        }
    }

    public void TraversePostOrderWithoutRecursion()
    {
        if (Root == null)
        {
            return;
        }

        Stack<Node> stack = new Stack<Node>();
        stack.Push(Root);
        Stack<int> outputStack = new Stack<int>();

        while (stack.Count > 0)
        {
            Node current = stack.Pop();
            outputStack.Push(current.Data);

            if (current.Left != null)
            {
                stack.Push(current.Left);
            }

            if (current.Right != null)
            {
                stack.Push(current.Right);
            }
        }

        while (outputStack.Count > 0)
        {
            Console.Write(outputStack.Pop() + " ");
        }
    }
}

class BT_run
{
    public static void Main(string[] args)
    {
        BinaryTree tree = new BinaryTree();

        int[] anArrayNodes = {
            17, 6, 5, 20, 19, 18, 11, 14, 12, 13, 2, 4, 10
        };

        foreach (int value in anArrayNodes)
        {
            tree.Add(value);
        }

        // Checking if the tree is empty
        Console.WriteLine("Is the tree empty? " + tree.IsEmpty());

        // Getting the size of the tree
        Console.WriteLine("Size of the tree: " + tree.GetSize());

        // Searching for a value in the tree
        int searchValue = 7;
        Console.WriteLine("Does the tree contain " + searchValue + "? " + tree.ContainsNode(searchValue));

        // Deleting a node from the tree
        int deleteValue = 3;
        tree.Delete(deleteValue);

        // Traversing the tree in different orders
        Console.Write("Inorder traversal: ");
        tree.TraverseInOrder(tree.Root);
        Console.WriteLine();

        Console.Write("Preorder traversal: ");
        tree.TraversePreOrder(tree.Root);
        Console.WriteLine();

        Console.Write("Postorder traversal: ");
        tree.TraversePostOrder(tree.Root);
        Console.WriteLine();

        Console.Write("Level-order traversal: ");
        tree.TraverseLevelOrder();
        Console.WriteLine();

        Console.Write("Inorder traversal without recursion: ");
        tree.TraverseInOrderWithoutRecursion();
        Console.WriteLine();

        Console.Write("Preorder traversal without recursion: ");
        tree.TraversePreOrderWithoutRecursion();
        Console.WriteLine();

        Console.Write("Postorder traversal without recursion: ");
        tree.TraversePostOrderWithoutRecursion();
        Console.WriteLine();

        // Визуализация дерева в консоли
        Console.WriteLine("Tree Visualization:");
        tree.PrintTree(tree.Root, "", true);
        
        
        Console.WriteLine("AVL TREE:");
        AVLTree avlTree = new AVLTree();

        // Insert nodes into the BST
        int[] anArrayNodes1 = {
            17, 6, 5, 20, 19, 18, 11, 14, 12, 13, 2, 4, 10
        };

        foreach (int value in anArrayNodes1)
        {
            avlTree.Insert(value);
        }

        // Visualize the BST
        avlTree.Visualize();

        // In-order traversal of BST
        Console.Write("In-order Traversal: ");
        avlTree.InOrderTraversal();
        
    }
}
