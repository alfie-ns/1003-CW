﻿
using System;   // Don't use anything else than System and only use C-core functionality; read the specs!







/// Declare what sort of data we store in the tree.
/// 
/// We use simple integers for convenience, but this could be anything sortable in general.
/// 

/// <summary>
/// Declare what sort of data we store in the tree.
/// 
/// We use simple integers for convenience, but this could be anything sortable in general.
/// </summary>
class DataEntry
{
    public int data;
}


/// <summary>
/// A single node in the tree;
/// </summary>

class Node
{


    public DataEntry data;
    public Node right;
    public Node left;
}


/// <summary>
/// The top-level tree structure
/// </summary>
class Tree
{
    public Node root;
}

class Program
{

    /// THIS LINE: If you want to add methods add them between THIS LINE and THAT LINE


    /// Your methods go here  
    
    // In this CW u do lots of recursion, which means where the function has a part where it calls itself, to 'traverse' through a whole subtree or a tree 
    
    // [ ] Method for handling duplicate values in the tree?
    // [ ] Helper function for finding size
    // [ ] Helper function for finding parent


    
    /// .... (and nowhere else)
     


    

    /// THAT LINE: If you want to add methods add them between THIS LINE and THAT LINE



    
    /// Recursively traverse a tree depth-first printing data in in-fix order.
    /// 
    /// Note that we expect the root Node as argument, not a Tree structure.
    /// Otherwise we would need an auxiliary function as we do recursion over Nodes.
    /// 
    /// In fact, the method below can print any non-empty sub-tree.
    /// 
    
    /// <param name="subtree">The *root node* of the tree to traverse and print</param>
    static void PrintTree(Node tree)
    {
        if (tree.left != null) // if the left subtree is NOT empty
            PrintTree(tree.left); // recursively print 

        Console.Write(tree.data.data + "  "); // print the current node's data

        if (tree.right != null) // if the right subtree is NOT empty
            PrintTree(tree.right); // recursively print
    }


    
    /// Compare whether the data in one Node is smaller than data in another Node. 
    /// 
    /// The data held in Nodes could be different from integers, but it must be sortable.
    /// This function/method defines when the data in Node item1 is smaller than in item2.
    /// As we assume Integers for convenience, the comparison is just the usual "smaller than".
    
    /// <param name="item1">First Node</param>
    /// <param name="item2">Second Node</param>
    /// <returns>True if the data in item1 is smaller than the data in item2, and false otherwise.</returns>
    static bool IsSmaller(Node item1, Node item2)
    {
        // Checks if the data in item1 is smaller than the data in item2
        return item1.data.data < item2.data.data;
    }

    
    
    /// Predicate that checks if two Nodes hold the same value.
    /// 
    /// As we assume Integers for convenience, the comparison is just the usual "equality" on integers.
    /// Equality could be more complicated for other sorts of data.
    
    /// <param name="item1">First Node</param>
    /// <param name="item2">Second Node</param>
    /// <returns>True if two Nodes have the same value, false otherwise.</returns>
    
    static bool IsEqual(Node item1, Node item2)
    {
        return item1.data.data == item2.data.data; // if item1 data == item2 data return true

    }

    // THOMAS' FUNCTION
    
    /// Insert a Node into a Tree 
    
    /// Note that the root node has to be provided, not the Tree reference, because we do recursion over the Nodes.
    /// The function makes use of IsSmaller and would work for other sorts of data than Integers.
    
    /// <param name="tree">The *root node* of the tree</param>
    /// <param name="item">The item to insert</param>
    static void InsertItem(ref Node tree, Node item)
    {
        if (tree == null) // if tree Node is empty,
        {
            tree = item; // insert item into tree
            return;
        }

        if (IsSmaller(item, tree)) // if item data is smaller than tree's data
        {
            InsertItem(ref tree.left, item); // recursively insert into the left subtree
        }
        else if (IsSmaller(tree, item)) // if item data is larger than tree's data
        {
            InsertItem(ref tree.right, item); // recursively insert into the right subtree
        }

        // otherwise the item data is already in the tree and we discard it 
    }


    
    /// Insert a Node into a Tree
    /// 
    /// This is an auxiliary function that expects a Tree structure, in contrast to the previous method. 
    /// It always inserts on the toplevel and is not recursive. It's just a wrapper.
    
    /// <param name="tree">The Tree (not a Node as in InsertItem())</param>
    /// <param name="item">The Node to insert</param>
    static void InsertTree(Tree tree, Node item) { 
    
        if (tree.root == null) // Base case: If the tree is empty
        {
            tree.root = item; // make the item the root of the tree
            return; // return 
        }

        Node current = tree.root; // the current node is the root of the tree
        while (true) // while there is a current node
        {
            if (IsSmaller(item, current)) // if item < tree.root
            {
                if (current.left == null) // if left child is empty
                {
                    current.left = item; // left child becomes the item
                    return; // return
                }
                current = current.left; // current node becomes the left child
            }
            else if (IsSmaller(current, item)) // if item > tree.root
            {
                if (current.right == null) // if right child is empty
                {
                    current.right = item; // right child becomes the item
                    return; // return
                }
                current = current.right; // current node becomes the right child
            }
            else // if item == tree.root do nothing
            {
                return;
            }
        }
    }


    //  SearchTree

    /// Find a value in a tree. 
    /// 
    /// This requires the IsEqual() predicate defined above for general data.
    
    /// <param name="tree">The root node of the Tree.</param>
    /// <param name="value">The Data to find</param>
    /// <returns>True if the value is found and false otherwise.</returns>
   static bool SearchTree(Node tree, DataEntry value) {

        if (tree == null)
        {
            return false;
        }

        // If the current node's value matches the search value, return true.
        if (tree.data.data == value.data) { 
            return true;
        }

        // If the search value is smaller than the current node's value, search the left subtree.
        else if (value.data < tree.data.data)
        {
            return SearchTree(tree.left, value);
        }

        // If the search value is greater than the current node's value, search the right subtree.
        else
        {
            return SearchTree(tree.right, value);
        }
    }


    
    /// Find a Node in a tree
    /// 
    /// This compares Node references not data values.
    
    /// <param name="tree">The root node of the tree.</param>
    /// <param name="item">The Node (reference) to be found.</param>
    /// <returns>True if the Node is found, false otherwise.</returns>
    static bool SearchTreeItem(Node tree, Node item)
    {

        if (tree == null) { // if the tree is empty, return false
            return false;
        }
        if (ReferenceEquals(tree, item)) { // if the tree reference is the same to the item reference, return true
            return true;
        }

        // Recursively search in the left subtree.
        bool foundInLeftSubtree = SearchTreeItem(tree.left, item);
        if (foundInLeftSubtree) return true; // If found in the left subtree, return true.

        // Recursively search in the right subtree.
        bool foundInRightSubtree = SearchTreeItem(tree.right, item);
        if (foundInRightSubtree) return true; // If found in the right subtree, return true.

        return false; // If not found in the left or right subtree, return false.


    }
    
    /// Delete a Node from a tree
    
    /// <param name="tree">The root of the tree</param>
    /// <param name="item">The Node to remove</param>
    static void DeleteItem(Tree tree, Node item)
    {

        Node current = tree.root; 
        Node parent = null; 
        bool isLeftChild = false; 

        
        while (current != null && !IsEqual(current, item)) // while current node is NOT null AND the current node is NOT equal to the item
        {
            parent = current; // the parent node becomes the current node WHY
            if (IsSmaller(item, current)) // if the item is smaller than the current node
            {
                current = current.left; // the current node becomes the left child of the current node
                isLeftChild = true;
            }
            else
            {
                current = current.right;
                isLeftChild = false;
            }
        }

        if (current == null) return; // Item not found

        
        if (current.left == null && current.right == null)
        {
            if (current == tree.root) tree.root = null;
            else if (isLeftChild) parent.left = null;
            else parent.right = null;
        }
        
        else if (current.right == null)
        {
            if (current == tree.root) tree.root = current.left;
            else if (isLeftChild) parent.left = current.left;
            else parent.right = current.left;
        }
        else if (current.left == null)
        {
            if (current == tree.root) tree.root = current.right;
            else if (isLeftChild) parent.left = current.right;
            else parent.right = current.right;
        }
        
        else
        {
            Node successor = current.right;
            Node successorParent = current;

            
            while (successor.left != null)
            {
                successorParent = successor;
                successor = successor.left;
            }

            
            current.data = successor.data;

            
            if (successorParent == current) successorParent.right = successor.right;
            else successorParent.left = successor.right;
        }
    }

    
    /// Returns how many elements are in a Tree
    
    /// <param name="tree">The Tree.</param>
    /// <returns>The number of items in the tree.</returns>
    static int Size(Node node)
    {
        
        if (node == null) 
        {
            return 0; 
        }
        else
        {
            
            return 1 + Size(node.left) + Size(node.right);
        }
    }


    
    /// Returns the depth of a tree with root "tree"
    /// 
    /// Note that this function should work for any non-empty subtree
    
    /// <param name="tree">The root of the tree</param>
    /// <returns>The depth of the tree.</returns>
    static int Depth(Node tree)
    {

        // Base case: If the tree is empty, return 0
        if (tree == null)
        {
            return 0;
        }

        // Recursively calculate the depth of the left and right subtrees
        int leftDepth = Depth(tree.left);
        int rightDepth = Depth(tree.right);

        // Return the maximum depth between the left and right subtrees, plus 1 for the current node
        return Math.Max(leftDepth, rightDepth) + 1;
    }


    
    /// Find the parent of Node node in Tree tree.
    
    /// <param name="tree">The Tree</param>
    /// <param name="node">The Node</param>
    /// <returns>The parent of node in the tree, or null if node has no parent.</returns>
    static Node Parent(Tree tree, Node node)
    {

        return null;
    }


    
    /// Find the Node with maximum value in a (sub-)tree, given the IsSmaller predicate.
    
    /// <param name="tree">The root node of the tree to search.</param>
    /// <returns>The Node that contains the largest value in the sub-tree provided.</returns>
    static Node FindMax(Node tree)
    {
        return null;
    }


    
    /// Delete the Node with the smallest value from the Tree. 
    
    /// <param name="tree">The Tree to process.</param>
    static void DeleteMin(Tree tree)
    {

    }


    /// SET FUNCTIONS 


    
    /// Merge the items of one tree with another one.
    /// Note that duplicate data entries are prohibited.
    
    /// <param name="tree1">A tree</param>
    /// <param name="tree2">Another tree</param>
    /// <returns>A new tree with all the values from tree1 and tree2.</returns>
    static Tree Union(Tree tree1, Tree tree2)
    {
        return null;
    }


    
    /// Find all values that are in tree1 AND in tree2 and copy them into a new Tree.
    
    /// <param name="tree1">The first Tree</param>
    /// <param name="tree2">The second Tree</param>
    /// <returns>A new Tree with all values in tree1 and tree2.</returns>
    static Tree Intersection(Tree tree1, Tree tree2)
    {
        return null;
    }


    
    /// Compute the set difference of the values of two Trees (interpreted as Sets).
    
    /// <param name="tree1">Tree one</param>
    /// <param name="tree2">Tree two</param>
    /// <returns>The values of the set difference tree1/tree2 in a new Tree.</returns>
    static Tree Difference(Tree tree1, Node tree2)
    {
        return null;
    }


    
    /// Compute the symmetric difference of the values of two Trees (interpreted as Sets).
    
    /// <param name="tree1">Tree one</param>
    /// <param name="tree2">Tree two</param>
    /// <returns>The values of the symmetric difference tree1/tree2 in a new Tree.</returns>
    static Tree SymmetricDifference(Node tree1, Tree tree2)
    {
        return null;
    }



    /*  
     *  TESTING 
     */


    
    /// Testing of the Tree methods that does some reasonable checks.
    /// It does not have to be exhaustive but sufficient to suggest the code is correct.
    
    static void TreeTests()  // some tests
    {
        Tree tree = new Tree();
        Random r = new Random();
        DataEntry data;


        // Build a tree inserting 10 random values as data

        Console.WriteLine("Build a tree inserting 10 random values as data");

        for (int i = 1; i <= 10; i++)
        {
            data = new DataEntry();
            data.data = r.Next(10);

            Node current = new Node();
            current.left = null;
            current.right = null;
            current.data = data;

            InsertItem(ref tree.root, current);
            //InsertTree(tree, current); 
        }

        // print out the (ordered!) tree

        Console.WriteLine("Print out the (ordered!) tree");
        PrintTree(tree.root);
        Console.WriteLine();

        Console.WriteLine("Search for 10 random values");

        data = new DataEntry();
        for (int i = 0; i < 10; i++)
        {
            data.data = r.Next(10);       // vvvv this is ugly ... improve it! vvvvv 
            Console.WriteLine(data.data + " was" + (!SearchTree(tree.root, data) ? " NOT" : "") + " found");
        }



        //  Add more tree testing here .... 

        



    }


    
    /// Testing of the Set methods that does some reasonable checks.
    /// It does not have to be exhaustive but sufficient to suggest the code is correct.
    
    static void SetTests()
    {

        //   Tests for the Set methods 

    }


    
    /// The Main entry point into the code. Don't change anythhing here. 
    
    static void Main()
    {
        TreeTests();

        SetTests();
    }

}

