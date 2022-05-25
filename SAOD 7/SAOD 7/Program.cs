using System;

namespace SAOD_7
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> bt = new BinaryTree<int>();
            bt.Add(8);
            bt.Add(5);
            bt.Add(9);
            bt.Add(2);
            bt.Add(3);
            bt.Add(4);

            Console.WriteLine(bt.GetHeight());
        }


        class TreeNode<T>
        {
            public TreeNode<T> Left { set; get; }
            public TreeNode<T> Right { set; get; }
            public T Value { set; get; }

            public TreeNode(T value)
            {
                Value = value;
            }
        }

        // 6 Variant. Tree height

        class BinaryTree<T>
        {
            public int Size { private set; get; }
            TreeNode<T> root;

            public void Add(T value)
            {
                TreeNode<T> newNode = new TreeNode<T>(value);

                ++Size;

                if (root == null)
                {
                    root = newNode;
                    return;
                }

                var current = root;
                while (true)
                {
                    if (((IComparable<T>)value).CompareTo(current.Value) < 0)
                    {
                        if (current.Left == null)
                        {
                            current.Left = newNode;
                            return;
                        }
                        current = current.Left;

                    }
                    else if (((IComparable<T>)value).CompareTo(current.Value) > 0)
                    {
                        if (current.Right == null)
                        {
                            current.Right = newNode;
                            return;
                        }
                        current = current.Right;
                    }
                    else
                    {
                        --Size;
                        break;
                    }
                }
            }

            public int findHeight(TreeNode<T> Node)
            {
                if (Node == null)
                {
                    return -1;
                }
                int lefth = findHeight(Node.Left);
                int righth = findHeight(Node.Right);
                if (lefth > righth)
                {
                    return lefth + 1;
                }
                else
                {
                    return righth + 1;
                }
            }

            public int GetHeight()
            {
                if (root == null)
                    return -1;
                return findHeight(root);
            }

            public void Clear()
            {
                root = null;
            }
        }
    }

}

