using System;

class AVLNode
{
    public int Key;
    public AVLNode Left;
    public AVLNode Right;
    public int Height;

    public AVLNode(int key)
    {
        Key = key;
        Left = null;
        Right = null;
        Height = 1;
    }
}

class AVLTree
{
    private AVLNode Root;

    public AVLTree()
    {
        Root = null;
    }

    public void Insert(int key)
    {
        Root = InsertHelper(Root, key);
    }

    private AVLNode InsertHelper(AVLNode root, int key)
    {
        if (root == null)
        {
            return new AVLNode(key);
        }

        if (key < root.Key)
        {
            root.Left = InsertHelper(root.Left, key);
        }
        else if (key > root.Key)
        {
            root.Right = InsertHelper(root.Right, key);
        }

        root.Height = 1 + Math.Max(GetHeight(root.Left), GetHeight(root.Right));

        int balanceFactor = GetBalance(root);

        if (balanceFactor > 1)
        {
            if (key < root.Left.Key)
            {
                return RightRotate(root);
            }
            else
            {
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }
        }

        if (balanceFactor < -1)
        {
            if (key > root.Right.Key)
            {
                return LeftRotate(root);
            }
            else
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
            }
        }

        return root;
    }

    public AVLNode Delete(int key)
    {
        Root = DeleteHelper(Root, key);
        return Root;
    }

    private AVLNode DeleteHelper(AVLNode root, int key)
    {
        if (root == null)
        {
            return root;
        }

        if (key < root.Key)
        {
            root.Left = DeleteHelper(root.Left, key);
        }
        else if (key > root.Key)
        {
            root.Right = DeleteHelper(root.Right, key);
        }
        else
        {
            AVLNode temp;
            if (root.Left == null)
            {
                temp = root.Right;
                root = null;
                return temp;
            }
            else if (root.Right == null)
            {
                temp = root.Left;
                root = null;
                return temp;
            }

            temp = GetMinValueNode(root.Right);
            root.Key = temp.Key;
            root.Right = DeleteHelper(root.Right, temp.Key);
        }

        if (root == null)
        {
            return root;
        }

        root.Height = 1 + Math.Max(GetHeight(root.Left), GetHeight(root.Right));

        int balanceFactor = GetBalance(root);

        if (balanceFactor > 1)
        {
            if (GetBalance(root.Left) >= 0)
            {
                return RightRotate(root);
            }
            else
            {
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }
        }

        if (balanceFactor < -1)
        {
            if (GetBalance(root.Right) <= 0)
            {
                return LeftRotate(root);
            }
            else
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
            }
        }

        return root;
    }

    public int GetHeight(AVLNode root)
    {
        if (root == null)
        {
            return 0;
        }
        return root.Height;
    }

    public int GetBalance(AVLNode root)
    {
        if (root == null)
        {
            return 0;
        }
        return GetHeight(root.Left) - GetHeight(root.Right);
    }

    public AVLNode LeftRotate(AVLNode z)
    {
        AVLNode y = z.Right;
        AVLNode T2 = y.Left;

        y.Left = z;
        z.Right = T2;

        z.Height = 1 + Math.Max(GetHeight(z.Left), GetHeight(z.Right));
        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

        return y;
    }

    public AVLNode RightRotate(AVLNode z)
    {
        AVLNode y = z.Left;
        AVLNode T3 = y.Right;

        y.Right = z;
        z.Left = T3;

        z.Height = 1 + Math.Max(GetHeight(z.Left), GetHeight(z.Right));
        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

        return y;
    }

    public AVLNode GetMinValueNode(AVLNode root)
    {
        if (root == null || root.Left == null)
        {
            return root;
        }
        return GetMinValueNode(root.Left);
    }

    public void Visualize()
    {
        VisualizeHelper(Root, "", true);
    }

    private void VisualizeHelper(AVLNode node, string prefix, bool isLeft)
    {
        if (node == null)
        {
            return;
        }

        string nodeStr = node.Key.ToString();
        string line = prefix + (isLeft ? "├── " : "└── ");
        Console.WriteLine(line + nodeStr);

        string childPrefix = prefix + (isLeft ? "│   " : "    ");
        VisualizeHelper(node.Left, childPrefix, true);
        VisualizeHelper(node.Right, childPrefix, false);
    }

    public void InOrderTraversal()
    {
        InOrderTraversalHelper(Root);
        Console.WriteLine();
    }

    private void InOrderTraversalHelper(AVLNode node)
    {
        if (node != null)
        {
            InOrderTraversalHelper(node.Left);
            Console.Write(node.Key + " ");
            InOrderTraversalHelper(node.Right);
        }
    }
}


