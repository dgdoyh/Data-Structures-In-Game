using System;


public class BinarySearchTree
{
    private Node root;

    public BinarySearchTree()
    {
        root = null;
    }

    public void Insert(int value)
    {
        Node newNode = new Node(value);

        // Insert newNode at the right position and set root if it's null
        root = InsertHelper(root, newNode);
    }

    private Node InsertHelper(Node root, Node nodeToInsert)
    {
        // If the (current) root is null,
        if (root == null)
        {
            // you reached the right place to insert nodeToInsert.
            return nodeToInsert;
        }
        // If root.value is bigger than nodeToInsert.value,
        else if (root.value > nodeToInsert.value)
        {
            // go through root.left's children until it finds the right node to insert nodeToInsert.
            root.left = InsertHelper(root.left, nodeToInsert);
        }
        // If root.value is smaller than nodeToInsert.value,
        else
        {
            // go through root.right's children until it finds the right node to insert nodeToInsert.
            root.right = InsertHelper(root.right, nodeToInsert);
        }

        return root;
    }

    public bool Search(int data)
    {
        return SearchHelper(root, data);
    }

    private bool SearchHelper(Node root, int value)
    {
        // If the current root is null,
        // (this mean either the tree is empty or the data is not found in anywhere)
        if (root == null)
        {
            // return false.
            return false;
        }
        // If the current root.value is the same as the given value,
        else if (root.value == value)
        {
            // found it. Return true.
            return true;
        }
        // If the given value is smaller than the current root.value,
        else if (value < root.value)
        {
            // check the current root.left.
            return SearchHelper(root.left, value);
        }
        // If the given value is bigger than the current root.value,
        else
        {
            // check the current root.right
            return SearchHelper(root.right, value);
        }
    }

    public int GetMinValue()
    {
        if (root == null) { return -1; }

        Node current = root;

        while (current.left != null)
        {
            current = current.left;
        }

        return current.value;
    }

    public void Remove(int value)
    {
        if (Search(value))
        {
            RemoveHelper(root, value);
        }
        else
        {
            Console.WriteLine(value + " couldn't be found.");
        }
    }

    private Node RemoveHelper(Node root, int value)
    {
        // If the current root is null, end the process.
        if (root == null) return root;
        // If the given value is less than the current root.value,
        else if (value < root.value)
        {
            // go through children of root.left until it reaches to the node to remove.
            root.left = RemoveHelper(root.left, value);
        }
        // If the given value is bigger than the current root.value,
        else if (value > root.value)
        {
            // go through children of root.right until it reaches to the node to remove.
            root.right = RemoveHelper(root.right, value);
        }
        // If the given value is equal to the current root.value, (found the node to remove)
        else
        {
            // If the current root is a leaf,
            if (root.left == null && root.right == null)
            {
                // delete the root.
                root = null;
            }
            // If the current root is not a leaf and has a right child,
            else if (root.right != null)
            {
                // find a successor(proper value to replace) of the current root,
                // and assign the successor's value into the current root.value.
                root.value = Successor(root);

                // After replacing the current root - Successor,
                // find the successor and delete.
                root.right = RemoveHelper(root.right, root.value);
            }
            // If the current root is not a leaf and has no right child,
            else
            {
                // find a prodecessor(proper value to replace) of the current root,
                // and assign the prodecessor's value into the current root.value.
                root.value = Prodecessor(root);

                // After replacing the current root - prodecessor,
                // find the prodecessor and delete.
                root.left = RemoveHelper(root.left, root.value);
            }
        }

        return root;
    }

    // This will return the smallest value among children of the given node.right.
    private int Successor(Node node)
    {
        // Starts from the right node of the given node.
        node = node.right;

        // Go through the left children until it reaches a leaf.
        while (node.left != null)
        {
            node = node.left;
        }

        // This value will be used for replace the nodeToRemove.value.
        return node.value;
    }

    // This will return the biggest value among children of the given node.left.
    private int Prodecessor(Node node)
    {
        // Starts from the left node of the given node.
        node = node.left;

        // Go through the right children until it reaches a leaf.
        while (node.right != null)
        {
            node = node.right;
        }

        // This value will be used for replace the nodeToRemove.value.
        return node.value;
    }
}
