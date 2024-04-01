
using System;   // Don't use anything else than System and only use C-core functionality; read the specs!

// DON'T CHANGE

/*
   
   https://github.com/alfie-ns/1003-CW

   AVL Tree Context:

   An AVL tree is a self-balancing binary search tree where the heights of the two child subtrees of any node differ by at most one.

   The reason for using the difference in heights as the balance factor in AVL tree's is to ensure that the AVL tree maintains its balance. 
   In an AVL tree, the heights of the left and right subtrees of any node differ by at MOST 1. This property guarantees that the tree 
   remains approximately balanced, which in turn provides efficient search, insertion, and deletion operations within a time complexity 
   of O(log n). 
  
   - Balance condition: The AVL tree property states that for every node in the tree, the absolute difference between the heights of 
     its left and right subtrees should be at most 1. By calculating the balance factor, we can easily check if a node violates this 
     condition.

   - Detecting imbalance: If the balance factor of a node is greater than 1, it means that the left subtree is too tall compared to the 
     right subtree, making the node left-heavy. Conversely, if the balance factor is less than -1, it indicates that the right subtree 
     is too tall compared to the left subtree, making the node right-heavy. These situations represent an imbalance in the tree.
   
   - Triggering rotations: When an imbalance is detected (i.e., the balance factor is outside the range [-1, 1]), the AVL tree performs 
     rotations to rebalance the tree. The specific rotation(s) needed depend on the balance factor and the structure of the subtrees 
     involved.
   
   - Maintaining efficiency: By keeping the tree balanced, AVL trees ensure that the heights of the left and right subtrees are as close 
     as possible. This balance minimizes the maximum depth of the tree, which in turn reduces the worst-case time complexity of 
     operations like search, insertion, and deletion to O(log n).
  
   - The balance factor provides a simple and efficient way to measure the balance of a node and the overall balance of the AVL tree. 
     By continuously monitoring the balance factors and performing necessary rotations, AVL trees maintain their balanced property and 
     guarantee efficient operations.
  
   - The balance factor is calculated based on the heights of the subtrees, NOT the actual number of nodes 
     in each subtree. This allows for efficient calculation and updates during insertions and deletions WITHOUT the need to count the 
     number of nodes in each subtree.
  
   - Also, the terms 'left-heavy' and 'right-heavy' refer to the balance factor of a node; a node is considered left-heavy when its left 
     subtree's height exceeds that of its right subtree by more than one (balance factor > 1), and right-heavy when the opposite is true 
     (balance factor < -1). These term's determine the appropriate rotations to apply in order to restore the tree's balance.
 
   Binary Search Tree Simple Example (BST)  { 

   O(log2(10)) = 3.32 = 3 steps
    
   n = (arr.size() = 10)
   log2(n) = log2(10) = 3.32
   3.32(steps) = 3 steps
    
   tree.depth() = ?
   tree.parent() = ?
   tree.target(n) = 3 steps
   tree.root() = 1 step
   tree.findMin() = 2 steps(go down left side)
   tree.findMax() = 3 steps(go down right side)
   tree.deleteMin() = same tree without 1(min value of tree)
    
   1st step. 13 is bigger than 8 -> Move to the right child (10)
   2nd step. 13 is bigger than 10 -> Move to the right child (14 
   3rd step. 13 is smaller than 14 -> Move to the left child (13) FOUND 
    
    AVL Tree Example {

    To find a value(13) in an AVL tree of 10 nodes using Binary Search,
    the worst-case scenario would require at most 2 steps:
    10 -> 14 -> (13)

           10 (root node)
         /    \
        5      14
       / \    /  \
      3   8 (13)  18
     / \   \     / \
    1   4   9   16  19

      
 
*/

/// <summary>
/// Implement a binary search tree 
/// 
/// Notes
/// [x] 1) Don't rename any of the method names in this file or change their arguments or return types or their order in the file.
/// [x] 2) If you want to add methods do this in the space indicated at the top of the Program.
/// [x] 3) You can add fields to the structures Tree, Node, DataEntry, if you find this necessary or useful.
/// [x] 4) Some of the method stubs have return statements that you may need to change (the code wouldn't run without return statements).
/// 
///    You can ignore most warnings - many of them result from requirements of Object-Orientated Programming or other constraints
///    unimportant for COMP1003.
///    
/// </summary>



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
    public int Height; // This is for AVL tree, because we need to keep track of the height of the tree to balance it
} // Storing the height in each node means that the tree can perform rotations and rebalancing efficiently


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

    /// Your methods go here:
    
    static Node RotateRight(Node node)
    { // this function performs a right rotation on the given node in an AVL tree
        
        // store reference to left child of current node, becomes new root of rotated subtree
        Node newRoot = node.left; 
        // update left to become right child of new root node, ensuring right subtree of new root becomes left subtree of current node
        node.left = newRoot.right;
        // make current node the left child of the new root, completing the rotation
        newRoot.right = node;

        // Consequently, this updates heights of nodes involved in the rotation, height of a node is calculated as 1 plus the maximum height of its left and right subtrees

        // Update the heights

        // calculate the height of the current node as 1 plus the maximum height of its left and right subtrees
        // adding 1 to account for current node
        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

        // calculate the height of the new root node as 1 plus the maximum height of its left and right subtrees
        // adding 1 to account for new root node itself
        newRoot.Height = 1 + Math.Max(GetHeight(newRoot.left), GetHeight(newRoot.right));

        // return the new root node of the rotated subtree
        return newRoot;
    }

    static Node RotateLeft(Node node)
    {
        // The logic is the same as RotateRight, but mirrored
        Node newRoot = node.right;
        node.right = newRoot.left;
        newRoot.left = node;

        // Update the heights
        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));
        newRoot.Height = 1 + Math.Max(GetHeight(newRoot.left), GetHeight(newRoot.right));

        return newRoot;
    }

    static int GetBalanceFactor(Node node)
    {
        if (node == null) return 0; // Base case: If the node is null, immediately return 0
        // Calculate the balance factor of a node as the difference between the height of its left and right subtrees
        return GetHeight(node.left) - GetHeight(node.right);
    }
    
    static int GetHeight(Node node)
    { // This is a helper function to get the height of a node, for balancing AVL trees
        if (node == null) return 0; // Null check first, If the node is null, return 0
              
        return node.Height; // Else, return the height of the node
    }

    static Node InsertItem(Node tree, Node item)
    { // [ ] VERIFY THIS WORKS
        if (tree == null)
        { // if tree is empty, make item the tree and add a height of the single node
            item.Height = 1; // Set the height of the leaf node
            return item; // Return 
        }
        if (IsSmaller(item, tree)) { // if the items data is smaller than the trees data
            tree.left = InsertItem(tree.left, item); // Recursively insert into the left subtree
        }
        else if (IsSmaller(tree, item)) { // if the items data is larger than the trees data
            tree.right = InsertItem(tree.right, item); // Recursively insert into the right subtree
        }
        else {
            return tree; // Discard duplicates
        }

        // Update the height of the current node
        tree.Height = 1 + Math.Max(GetHeight(tree.left), GetHeight(tree.right));

        // AVL tree balancing
        int balanceFactor = GetHeight(tree.left) - GetHeight(tree.right);

        if (balanceFactor > 1) { // Left-heavy
        
            if (IsSmaller(item, tree.left)){ // Left-Left case
                return RotateRight(tree);
            }
            else { // Left-Right case
            
                tree.left = RotateLeft(tree.left);
                return RotateRight(tree);
            }
        }
        else if (balanceFactor < -1) { // Right-heavy
        
            if (IsSmaller(tree.right, item)) { // Right-Right case
                return RotateLeft(tree);
            }
            else { // Right-Left case
                tree.right = RotateRight(tree.right);
                return RotateLeft(tree);
            }
        }

        return tree;
    }

    static Node DeleteNode(Node node, Node item)
    {
        if (node == null) return null; // Base case: If node is null return null
            
        // Recursively search for the node to delete, going left or right based on the comparison of the item's data with the current node's data
        if (IsSmaller(item, node)){
            node.left = DeleteNode(node.left, item);
        }
        else if (IsSmaller(node, item)){
            node.right = DeleteNode(node.right, item);
        }
        else {
            // Case 1: Node to be deleted is a leaf node
            if (node.left == null && node.right == null)
                return null;

            // Case 2: Node to be deleted has only one child
            if (node.left == null) // if leftchild==null, return rightchild
                return node.right;
            else if (node.right == null) // if rightchild==null, return leftchild
                return node.left;

            // Case 3: Node to be deleted has two children
            Node successor = FindMin(node.right);
            node.data = successor.data;
            node.right = DeleteNode(node.right, successor);
        }

        // AVL tree balancing
        // Update the height of the current node
        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

        // Check the balance factor and perform rotations if necessary
        int balanceFactor = GetBalanceFactor(node);

        if (balanceFactor > 1) { // Left-heavy
        
            if (GetBalanceFactor(node.left) < 0) { // Left-Right case
                node.left = RotateLeft(node.left);
            }
            return RotateRight(node); // Left-Left case

        }
        else if (balanceFactor < -1) {// Right-heavy
            if (GetBalanceFactor(node.right) > 0) {// Right-Left case
                node.right = RotateRight(node.right);
            }
            return RotateLeft(node); // Right-Right case
        }

        return node;
    }

    // Recursive helper function to calculate the size of a subtree
    static int SizeHelper(Node node)
    {
        // If the node is null, return 0
        if (node == null) return 0;
            
        // Recursively calculate the size of the left subtree
        int leftSize = SizeHelper(node.left);

        // Recursively calculate the size of the right subtree
        int rightSize = SizeHelper(node.right);

        // Return the sum of sizes of left subtree, right subtree, and the current node (1)
        return leftSize + rightSize + 1;
    }

   static Node FindParentHelper(Node current, Node node)
    {
        if (current == null)
        {
            // Base case: If the current node is null, return null
            return null;
        }

        // Check if the current node is the parent of the given node
        if (current.left == node || current.right == node)
        {
            // If the current node is the parent, return it
            return current;
        }

        // Recursively search for the parent in the left subtree
        Node parentInLeft = FindParentHelper(current.left, node);
        if (parentInLeft != null)
        {
            // If the parent is found in the left subtree, return it
            return parentInLeft;
        }

        // Recursively search for the parent in the right subtree
        Node parentInRight = FindParentHelper(current.right, node);
        if (parentInRight != null)
        {
            // If the parent is found in the right subtree, return it
            return parentInRight;
        }

        // If the parent is not found in either subtree, return null
        return null;
    }

    static Node FindMin(Node tree)
    {
        if (tree == null) return null; // Base case: If the tree is empty, return null immediately
        
        //Console.WriteLine("FindMin: Current node value: " + tree.data.data); // Testing

        while (tree.left != null) {
            tree = tree.left;
            //Console.WriteLine("FindMin: Moving to left child, value: " + tree.data.data); // Testing
        }

        //Console.WriteLine("FindMin: Minimum value node: " + tree.data.data); // Testing
        return tree;
    }

    /// .... (and nowhere else)


    /// THAT LINE: If you want to add methods add them between THIS LINE and THAT LINE



    /// <summary>
    /// Recursively traverse a tree depth-first printing data in in-fix order.
    /// 
    /// Note that we expect the root Node as argument, not a Tree structure.
    /// Otherwise we would need an auxiliary function as we do recursion over Nodes.
    /// 
    /// In fact, the method below can print any non-empty sub-tree.
    /// 
    /// </summary>
    /// <param name="subtree">The *root node* of the tree to traverse and print</param>
    static void PrintTree(Node tree)
    {
        if (tree == null) return; // this was needed to avoid null reference exceptions in set functions

        if (tree.left != null)
            PrintTree(tree.left);

        Console.Write(tree.data.data + "  ");

        if (tree.right != null)
            PrintTree(tree.right);
    }


    /// <summary>
    /// Compare whether the data in one Node is smaller than data in another Node. 
    /// 
    /// The data held in Nodes could be different from integers, but it must be sortable.
    /// This function/method defines when the data in Node item1 is smaller than in item2.
    /// As we assume Integers for convenience, the comparison is just the usual "smaller than".
    /// </summary>
    /// <param name="item1">First Node</param>
    /// <param name="item2">Second Node</param>
    /// <returns>True if the data in item1 is smaller than the data in item2, and false otherwise.</returns>
    static bool IsSmaller(Node item1, Node item2)
    {
        return item1.data.data < item2.data.data;
    }


    /// <summary>
    /// Predicate that checks if two Nodes hold the same value. 
    /// 
    /// As we assume Integers for convenience, the comparison is just the usual "equality" on integers.
    /// Equality could be more complicated for other sorts of data.
    /// </summary>
    /// <param name="item1">First Node</param>
    /// <param name="item2">Second Node</param>
    /// <returns>True if two Nodes have the same value, false otherwise.</returns>
    static bool IsEqual(Node item1, Node item2)
    { 
        return item1.data.data == item2.data.data; // if item1 data == item2 data return true, else false
    }


    /// <summary>
    /// Insert a Node into a Tree
    /// 
    /// Note that the root node has to be provided, not the Tree reference, because we do recursion over the Nodes.
    /// The function makes use of IsSmaller and would work for other sorts of data than Integers.
    /// </summary>
    /// <param name="tree">The *root node* of the tree</param>
    /// <param name="item">The item to insert</param>
    static void InsertItem(ref Node tree, Node item)
    {
        if (tree == null)                           // if tree Node is empty, make item the tree's Node
        {
            tree = item;
            return;
        }

        if (IsSmaller(item, tree))                  // if item data is smaller than tree's data
        {
            InsertItem(ref tree.left, item);        //     recursively insert into the left subtree
        }
        else if (IsSmaller(tree, item))             // if item data is larger than tree's data
        {
            InsertItem(ref tree.right, item);       //     recursively insert into the right subtree
        }

        // otherwise the item data is already in the tree and we discard it 
    }


    /// <summary>
    /// Insert a Node into a Tree
    /// 
    /// This is an auxiliary function that expects a Tree structure, in contrast to the previous method. 
    /// It always inserts on the toplevel and is not recursive. It's just a wrapper.
    /// </summary>
    /// <param name="tree">The Tree (not a Node as in InsertItem())</param>
    /// <param name="item">The Node to insert</param>
    
    /// <remarks>
    /// The InsertTree function inserts an item into an AVL tree by calling the recursive InsertItem function.
    /// It starts the insertion process from the root node of the tree.
    ///
    /// The InsertItem function traverses the tree recursively, comparing the item to be inserted with the current node.
    /// If the item is smaller, it recursively calls InsertItem on the left subtree.
    /// If the item is greater, it recursively calls InsertItem on the right subtree.
    /// If the item is equal to the current node, it discards the duplicate
    ///
    /// After the recursive insertion, the InsertItem function updates the height of the current node and checks the balance factor.
    /// If the tree becomes unbalanced (balance factor > 1 or < -1), it performs the necessary rotations to restore the balance.
    ///
    /// The InsertItem function returns the new root node of the subtree after the insertion and balancing process.
    /// The InsertTree function then assigns this new root node back to the root field of the tree struct,
    /// updating the entire tree with the inserted item.
    /// </remarks>
    static void InsertTree(Tree tree, Node item)
    { // VERIFY THIS WORKS [ ]
        // Recursively call InsertItem() to insert the item into the AVL tree, starting from the root node.
        tree.root = InsertItem(tree.root, item);
    }


    /// <summary>
    /// Find a value in a tree.
    /// 
    /// This requires the IsEqual() predicate defined above for general data.
    /// </summary>
    /// <param name="tree">The root node of the Tree.</param>
    /// <param name="value">The Data to find</param>
    /// <returns>True if the value is found and false otherwise.</returns>
    static bool SearchTree(Node tree, DataEntry value)
    {
        if (tree == null) // Base case: If the tree is empty, return false immediately
            return false;
        if (value.data == tree.data.data)
            return true;
        else if (value.data < tree.data.data)
            return SearchTree(tree.left, value);
        else
            return SearchTree(tree.right, value);
    }



    /// <summary>
    /// Find a Node in a tree
    /// 
    /// This compares Node references not data values.
    /// </summary>
    /// <param name="tree">The root node of the tree.</param>
    /// <param name="item">The Node (reference) to be found.</param>
    /// <returns>True if the Node is found, false otherwise.</returns>
    static bool SearchTreeItem(Node tree, Node item)
    { // VERIFY THIS WORKS [ ]
        // Base case: If the tree is empty, return false immediately
        if (tree == null)
            return false;

        // If the current node matches the item, the item is found
        if (tree == item)
            return true;

        // 1st recursively search in the LEFT subtree
        bool foundInLeft = SearchTreeItem(tree.left, item);
        if (foundInLeft)
            return true;

        // 2nd recursively search in the RIGHT subtree
        bool foundInRight = SearchTreeItem(tree.right, item);
        if (foundInRight)
            return true;

        // If the item is not found in either subtree, it is not in the tree at all
        return false;
    }


    /// <summary>
    /// Delete a Node from a tree
    /// </summary>
    /// <param name="tree">The root of the tree</param>
    /// <param name="item">The Node to remove</param>
    static void DeleteItem(Tree tree, Node item)
    { // VERIFY THIS WORKS [ ]
        // Recursively call DeleteNode() to delete the item from the AVL tree, starting from the root node.
        tree.root = DeleteNode(tree.root, item);
        // The DeleteNode function returns the new root node of the tree after the deletion and balancing process.
    }


    /// <summary>
    /// Returns how many elements are in a Tree
    /// </summary>
    /// <param name="tree">The Tree.</param>
    /// <returns>The number of items in the tree.</returns>
    static int Size(Tree tree)
    { 
        /* 
            This function calculates the size of a tree by calling the recursive helper function SizeHelper.
            If the tree is empty (root is null), it returns 0. Otherwise, it calls the recursive helper function
            to calculate the size of the tree. The helper function traverses the tree in a depth-first manner,
        */

        // Base case: If the tree is empty (root is null), return 0 (no elements) immediately
        if (tree.root == null) return 0;
        
        
        // If the tree is not empty, call the recursive helper function to calculate the size of the tree
        return SizeHelper(tree.root);
        
    }


    /// <summary>
    /// Returns the depth of a tree with root "tree"
    /// 
    /// Note that this function should work for any non-empty subtree
    /// </summary>
    /// <param name="tree">The root of the tree</param>
    /// <returns>The depth of the tree.</returns>
    static int Depth(Node tree)
    { 
        /*
            First null check, then recursively calculate the depth of the left and right subtrees. 
            The depth is the length of the longest path from the root to a leaf node. It first checks
            the leftDepth then rightDepth, then returns the max depth of the left or right subtree, plus 1
            accounting for the current node.

        */

        // Base case: If the tree is empty (root is null), return 0 (no depth) immediately
        if (tree == null)
            return 0;

        // Else, recursively calculate the depth of the left or right subtree's
        int leftDepth = Depth(tree.left);
        int rightDepth = Depth(tree.right);

        // Return the max depth, either left or right subtree, plus 1 for the current node
        int depth = Math.Max(leftDepth, rightDepth) + 1;

        return depth;
    }

    /// <summary>
    /// Find the parent of Node node in Tree tree.
    /// </summary>
    /// <param name="tree">The Tree</param>
    /// <param name="node">The Node</param>
    /// <returns>The parent of node in the tree, or null if node has no parent.</returns>
    static Node Parent(Tree tree, Node node)
    {
        /* 
           This function should 1st check if the node is
           null or root of the tree(thus no parents). Then
           init parent as an empty value, then check if the 
           current node is the parent, if so will return that
           value. If it's NOT, start traversing the tree, 
           going left subtree to right subtree, it does this
           recursively, until the current node is the parent of 
           the node given to the function. [x]

        */

        if (node == null || node == tree.root)
        {
            // If node is null or the root of the tree, has no parent, so return null immediately
            return null;
        }

        // Call the recursive helper function to find the parent
        return FindParentHelper(tree.root, node);
    }

    /// <summary>
    /// Find the Node with maximum value in a (sub-)tree, given the IsSmaller predicate.
    /// </summary>
    /// <param name="tree">The root node of the tree to search.</param>
    /// <returns>The Node that contains the largest value in the sub-tree provided.</returns>
    static Node FindMax(Node tree)
    {
        // Base case: If the tree is empty, return null immediately
        if (tree == null) return null;

        // Node to keep track of the current node as we traverse the tree
        Node current = tree;

        // Go right until we reach the rightmost node (maximum value node in the tree)
        while (current.right != null)
        {
            current = current.right;
        }

        // Now, current is the rightmost node, which will be the maximum value node in the tree
        return current;
    }


    /// <summary>
    /// Delete the Node with the smallest value from the Tree. 
    /// </summary>
    /// <param name="tree">The Tree to process.</param>
    static void DeleteMin(Tree tree)
    {
        /* 
            This function deletes the node with the minimum value in the tree. 
            It first checks if the tree is empty, if so, there is nothing to delete. 
            Then, it calls the FindMin function to find the node with the minimum value. 
            Finally, it calls the DeleteNode function to delete the node with the minimum value from the tree.
        */

        // If the tree is empty, there is nothing to delete
        if (tree.root == null) return; // Base case: If the tree is empty, return immediately

        // Call helper func to find the node with the minimum value
        Node minNode = FindMin(tree.root);

        // Delete the node with the minimum value
        tree.root = DeleteNode(tree.root, minNode);
    }







// --------------------------------------------------------------------------------------------------









    ///!!! <remarks> !!!
    /* 
       MADE THE HELPER FUNCTIONS INSIDE THE RESPECTIVE FUNCTION
       BECAUSE I WAS CONFUSED REGARDING THE SCOPE OF resultTree.
       NEVERTHELESS, THESE STILL ARE NOT FUNCTIONS OUTSIDE THIS LINE
       AND THAT LINE, AS THEY'RE INSIDE THEIR RESPECTIVE FUNCTION
    */
    ///!!! </remarks> !!!
    
    /// SET FUNCTIONS


    /// <summary>
    /// Merge the items of one tree with another one.
    /// Note that duplicate data entries are prohibited.
    /// </summary>
    /// <param name="tree1">A tree</param>
    /// <param name="tree2">Another tree</param>
    /// <returns>A new tree with all the values from tree1 and tree2.</returns>
    static Tree Union(Tree tree1, Tree tree2)
    {

        /* 
            This function merges the values of two trees into a new resultTree. 
            It first creates an empty result tree. Then, it calls the InsertUnique function to insert the unique values from tree1 and tree2 into the result tree. 
            The InsertUnique function recursively traverses the trees and inserts only the unique values into the result tree. 
            Finally, it returns the result tree with all the values from tree1 and tree2.
        */

        Tree resultTree = new Tree(); // Create an empty result tree

        void InsertUnique(Node node, Tree resultTree) // Helper function to insert unique values into the result tree
        {
            if (node == null) return; // Base case: If the current node is null, 

            // Check if the current node's value does not already exist in the result tree
            if (!SearchTree(resultTree.root, node.data))
            {
                // If the value is unique, insert it into the result tree
                InsertTree(resultTree, new Node { data = node.data });
            }

            // Recursively traverse the left and right subtrees
            InsertUnique(node.left, resultTree);
            InsertUnique(node.right, resultTree);
        }

        InsertUnique(tree1.root, resultTree); // traverse tree1 and insert unique values into result tree
        InsertUnique(tree2.root, resultTree); // traverse tree2 and insert unique values into result tree

        return resultTree;
    }


    /// <summary>
    /// Find all values that are in tree1 AND in tree2 and copy them into a new Tree.
    /// </summary>
    /// <param name="tree1">The first Tree</param>
    /// <param name="tree2">The second Tree</param>
    /// <returns>A new Tree with all values in tree1 and tree2.</returns> 
    static Tree Intersection(Tree tree1, Tree tree2)
    {
        /*
            This function finds the intersection of the values in two trees and returns a new tree with the common values. 
            It first creates an empty result tree. Then, it calls the IntersectionHelper function to traverse the trees and find the common values. 
            The IntersectionHelper function recursively compares the values in the trees and inserts the common values into the result tree. 
            Finally, it returns the result tree with the intersection of the values from tree1 and tree2.
        */

        Tree resultTree = new Tree(); // Create an empty result tree

        void IntersectHelper(Node node) 
        {
            if (node == null) return; // Base case: If the current node is null, return

            if (SearchTreeItem(tree2.root, node)) { // If nodes are in both trees, insert into result tree
                InsertTree(resultTree, new Node { data = node.data });
            }

            IntersectHelper(node.left); // Recursively traverse the left subtree
            IntersectHelper(node.right); // Recursively traverse the right subtree
        }

        IntersectHelper(tree1.root); // Start the intersection recursive process from the root of tree1

        return resultTree;
    }


    /// <summary>
    /// Compute the set difference of the values of two Trees (interpreted as Sets).
    /// </summary>
    /// <param name="tree1">Tree one</param>
    /// <param name="tree2">Tree two</param>
    /// <returns>The values of the set difference tree1/tree2 in a new Tree.</returns>
    static Tree Difference(Tree tree1, Node tree2)
    {
        /*
            the difference of two sets A and B is equal
            to the set which consists of elements present
            in A but not in B. Thus the difference
        */

        Tree resultTree = new Tree(); // Create an empty result tree

        void DiffHelper(Node node) 
        {
            if (node == null) return; // Base case: If the current node is null, return

            if (!SearchTreeItem(tree2, node)) // If the node is in tree1 but not in tree2, insert into result tree
                InsertTree(resultTree, new Node { data = new DataEntry { data = node.data.data } });

            DiffHelper(node.left);
            DiffHelper(node.right);
        }

        DiffHelper(tree1.root);

        return resultTree;
    }


    /// <summary>
    /// Compute the symmetric difference of the values of two Trees (interpreted as Sets).
    /// </summary>
    /// <param name="tree1">Tree one</param>
    /// <param name="tree2">Tree two</param>
    /// <returns>The values of the symmetric difference tree1/tree2 in a new Tree.</returns>
    static Tree SymmetricDifference(Node tree1, Tree tree2)
    {
        Tree resultTree = new Tree();

        void SymDiffHelper(Node node, Tree currentTree, Tree otherTree)
        {
            if (node == null) return; // Base case: If the current node is null, return

            if (!SearchTreeItem(otherTree.root, node))
                InsertTree(resultTree, new Node { data = new DataEntry { data = node.data.data } });

            SymDiffHelper(node.left, currentTree, otherTree);
            SymDiffHelper(node.right, currentTree, otherTree);
        }

        SymDiffHelper(tree1, new Tree { root = tree1 }, tree2);
        SymDiffHelper(tree2.root, tree2, new Tree { root = tree1 });

        return resultTree;
    }



    /*  
     *  TESTING 
     */


    /// <summary>
    /// Testing of the Tree methods that does some reasonable checks.
    /// It does not have to be exhaustive but sufficient to suggest the code is correct.
    /// </summary>
    static void TreeTests()  // some tests
    {
        Tree tree = new Tree();
        Random r = new Random();
        DataEntry data;

        // Build a tree inserting 10 random values as data
        Console.WriteLine("Build a tree inserting 10 random values as data");
        Console.WriteLine("------------------------------------------------");

        for (int i = 1; i <= 10; i++)
        {
            data = new DataEntry();
            data.data = r.Next(10);

            Node current = new Node();
            current.left = null;
            current.right = null;
            current.data = data;

            InsertTree(tree, current);
        }

        // print out the (ordered!) tree
        Console.WriteLine("Print out the (ordered!) tree");
        PrintTree(tree.root);
        Console.WriteLine();

        Console.WriteLine("--------------------");

        // print the size of the tree
        Console.WriteLine("Size of the tree: " + Size(tree)); // print size
        Console.WriteLine("Depth of the tree: " + Depth(tree.root)); // print depth
        Node minNode = FindMin(tree.root); // find min value
        Console.WriteLine("Min value in the tree: " + (minNode != null ? minNode.data.data.ToString() : "null")); // Terinary operator, quick if else  
        Node maxNode = FindMax(tree.root); // find max value
        Console.WriteLine("Max value in the tree: " + (maxNode != null ? maxNode.data.data.ToString() : "null"));   
        Console.WriteLine("--------------------");

        // test SearchTree
        Console.WriteLine("Search for 10 random values");

        data = new DataEntry();
        for (int i = 0; i < 10; i++)
        {
            data.data = r.Next(10);
            Console.WriteLine(data.data + " was" + (!SearchTree(tree.root, data) ? " NOT" : "") + " found");
        }

        Console.WriteLine("--------------------");

        // Test the Parent function with nodes from the randomly generated tree
        if (tree.root != null) // If the tree is not empty
        {
            Node randomNode1 = tree.root;
            Node randomNode2 = tree.root.left;
            Node randomNode3 = tree.root.right;

            Console.WriteLine("Parent of randomNode1: " + (Parent(tree, randomNode1)?.data.data.ToString() ?? "null"));
            Console.WriteLine("Parent of randomNode2: " + (Parent(tree, randomNode2)?.data.data.ToString() ?? "null"));
            Console.WriteLine("Parent of randomNode3: " + (Parent(tree, randomNode3)?.data.data.ToString() ?? "null"));
        }

        Console.WriteLine("--------------------");
    }


    /// <summary>
    /// Testing of the Set methods that does some reasonable checks.
    /// It does not have to be exhaustive but sufficient to suggest the code is correct.
    /// </summary>
    
    static void SetTests()
    {
        /*
            This function is for testing the set operation functions
            It creates 3 sample trees, inserts values into them, then tests
            the Union, Intersection, Difference, and SymmetricDifference functions.
        */

        // Create sample trees
        Tree tree1 = new Tree();
        Tree tree2 = new Tree();
        Tree tree3 = new Tree();

        // Insert values into tree1
        InsertTree(tree1, new Node { data = new DataEntry { data = 5 } });
        InsertTree(tree1, new Node { data = new DataEntry { data = 3 } });
        InsertTree(tree1, new Node { data = new DataEntry { data = 7 } });
        InsertTree(tree1, new Node { data = new DataEntry { data = 1 } });

        // Insert values into tree2
        InsertTree(tree2, new Node { data = new DataEntry { data = 6 } });
        InsertTree(tree2, new Node { data = new DataEntry { data = 2 } });
        InsertTree(tree2, new Node { data = new DataEntry { data = 9 } });
        InsertTree(tree2, new Node { data = new DataEntry { data = 5 } });

        // Insert values into tree3
        InsertTree(tree3, new Node { data = new DataEntry { data = 4 } });
        InsertTree(tree3, new Node { data = new DataEntry { data = 7 } });
        InsertTree(tree3, new Node { data = new DataEntry { data = 1 } });
        InsertTree(tree3, new Node { data = new DataEntry { data = 8 } });

        // Test the Union function
        Tree unionResult = Union(tree1, tree2);
        // Expected result: 1, 2, 3, 5, 6, 7, 9
        Console.WriteLine("Union of tree1 and tree2:");
        PrintTree(unionResult.root);
        Console.WriteLine();
        Console.WriteLine("----------------------------");

        // Test the Intersection function
        Tree intersectionResult = Intersection(tree1, tree2);
        // Expected result: 5
        Console.WriteLine("Intersection of tree1 and tree2:");
        PrintTree(intersectionResult.root);
        Console.WriteLine();
        Console.WriteLine("----------------------------");

        // Test the Difference function
        Tree differenceResult = Difference(tree1, tree3.root);
        // Expected result: 3
        Console.WriteLine("Difference of tree1 and tree3:");
        PrintTree(differenceResult.root);
        Console.WriteLine();
        Console.WriteLine("----------------------------");

        // Test the SymmetricDifference function
        Tree symmetricDifferenceResult = SymmetricDifference(tree2.root, tree3);
        // Expected result: 2, 4, 6, 8, 9
        Console.WriteLine("Symmetric Difference of tree2 and tree3:");
        PrintTree(symmetricDifferenceResult.root);
    }


/// ---------------------------------------------------------------------------------------------------------------------------------------------------------------























    /// <summary>
    /// The Main entry point into the code. Don't change anythhing here. 
    /// </summary>
    static void Main()
    {
        TreeTests();

        SetTests();
    }

}

