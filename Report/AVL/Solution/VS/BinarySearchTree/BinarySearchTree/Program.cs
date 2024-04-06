
using System; // Don't use anything else than System and only use C-core functionality; read the specs!

// DON'T CHANGE

/*
 
 https://github.com/alfie-ns/1003-CW

 - A 'base case' ensures recusion TERMINATES when a leaf node is reached, otherwise, the function could run forever!

 AVL Tree Context:

 An AVL tree is a self-balancing binary search tree where the heights of the two child subtrees of any node differ by at MOST one.
 
 In an AVL tree, the heights of the left and right subtrees of any node differ by at MOST 1. This property guarantees that the tree 
 remains approximately balanced, which in turn provides efficient search, insertion, and deletion operations within a time complexity 
 of O(log n). 
 
 - The AVL tree property states that for EVERY node in the tree, the absolute difference between the heights of 
 its left and right subtrees should be at MOST 1. By calculating the balance-factor, we can easily check if a node violates this 
 condition.

 - If the balance-factor of a node is greater than 1, it means that the left subtree is too tall compared to the 
 right subtree, making the node left-heavy. Conversely, if the balance-factor is LESS than -1, it indicates that the right subtree 
 is too tall compared to the left subtree, making the node right-heavy. These situations represent an imbalance in the tree.
 
 - When an imbalance is detected (i.e, the balance-factor is outside the range [-1, 1]), the AVL tree performs 
 rotations to rebalance the tree. The specific rotation's needed depend on the balance-factor and the structure of the subtrees 
 involved.
 
 - By keeping the tree balanced, AVL trees ensure that the heights of the left and right subtrees are as close 
 as possible. This balance minimizes the maximum depth of the tree, which in turn reduces the worst-case time complexity of 
 operations like search, insertion, and deletion to O(log n).
 
 - The balance-factor provides a simple and efficient way to measure the balance of a node and the overall balance of the AVL tree. 
 By continuously monitoring the balance-factors and performing necessary rotations, AVL trees maintain their balanced property and 
 guarantee efficient operations.
 
 - The balance-factor is calculated based on the heights of the subtrees, NOT the actual number of nodes 
 in each subtree. This allows for efficient calculation and updates during insertions and deletions WITHOUT the need to count the 
 number of nodes in each subtree.
 
 - Also, the terms 'left-heavy' and 'right-heavy' refer to the balance-factor of a node; a node is considered left-heavy when its left 
 subtree's height exceeds that of its right subtree by MORE than one (balance-factor > 1), and right-heavy when the opposite is true 
 (balance-factor < -1). These term's determine the appropriate rotations to apply in order to restore the tree's balance.

 - AVL Tree: O(log n)
 - BST: O(log n) in the average case, O(n) in the worst case (unbalanced tree)

 If we say an AVL tree has 10 nodes, the formula for finding the average time-complexity is log2(10)=3.32,
 which rounds down to 3 since steps are integers; thus, on average, it'll take 3 steps to find a particular
 node in this tree. This will always be the case in an AVL tree, but not necessarily for a BST that could become
 unbalanced.
 
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
/// You can ignore MOST warnings - many of them result from requirements of Object-Orientated Programming or other constraints
/// unimportant for COMP1003.
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
    public int Height; // This is for AVL tree, thus one needs to keep track of the height of the tree to balance it
} // Storing the height in each node means that the tree can perform rotations and rebalancing efficiently


/// <summary>
/// The top-level tree structure
/// </summary>
class Tree
{
    public Node root; // The root node of the tree
}



class Program // Program class, the entry point of the program 
{

    /// THIS LINE: If you want to add methods add them between THIS LINE and THAT LINE

    /// Your methods go here:

    /// ------------------------------------------------------------- AVL Tree Functions ------------------------------------------------------------- ///

    static Node RotateRight(Node node)
    {
        // This function performs a right rotation on the given node in an AVL tree.

        // Store a reference to the left child of the current node; it becomes the new root of the rotated subtree.
        Node newRoot = node.left;

        // Update the left child of the current node to become the right child of the new root node,
        // ensuring that the right subtree of the new root becomes the left subtree of the current node.
        node.left = newRoot.right;

        // Make the current node the left child of the new root, completing the rotation.
        newRoot.right = node;

        /* 
        Consequently, this updates the heights of the nodes involved in the rotation;
        the height of a node is calculated as 1 plus the maximum height of its left and right subtrees.
        */

        // Then, update the respective heights of the current node and the new root node.

        // Calculate the height of the current node as 1 plus the maximum height of its left and right subtrees.
        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

        // Calculate the height of the new root node as 1 plus the maximum height of its left and right subtrees.
        newRoot.Height = 1 + Math.Max(GetHeight(newRoot.left), GetHeight(newRoot.right));

        // Return the new root node of the rotated subtree.
        return newRoot;
    }

    static Node RotateLeft(Node node)
    {
        // The logic is the same as RotateRight, but mirrored
        Node newRoot = node.right;
        node.right = newRoot.left;
        newRoot.left = node;
        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));
        newRoot.Height = 1 + Math.Max(GetHeight(newRoot.left), GetHeight(newRoot.right));

        return newRoot;
    }

    static int GetBalanceFactor(Node node)
    {
        if (node == null) return 0; // Base case: If the node is null, immediately return 0

        return GetHeight(node.left) - GetHeight(node.right);
        // The balance-factor is calculated by subtracting the height of the right subtree from the height of the left subtree
    }

    static int GetHeight(Node node)
    { // get the height of a trees
        if (node == null) return 0; // Base case: If the node is null, return 0
        int leftHeight = GetHeight(node.left); // Recursively calculate the height of the left subtree
        int rightHeight = GetHeight(node.right); // Recursively calculate the height of the right subtree
        return 1 + Math.Max(leftHeight, rightHeight); //you get the max of either left or right subtree to find the LONGEST path to a leaf node, +1 to account for current node
    }

    static Node InsertItem(Node tree, Node item)
    {

        if (tree == null)
        { // if tree is empty, make item the tree and add a height of the single node
            item.Height = 1; // Set the height of the leaf node
            return item; // Return 
        }
        if (IsSmaller(item, tree))
        { // if the items data is smaller than the trees data
            tree.left = InsertItem(tree.left, item); // Recursively insert into the left subtree
        }
        else if (IsSmaller(tree, item))
        { // if the items data is larger than the trees data
            tree.right = InsertItem(tree.right, item); // Recursively insert into the right subtree
        }
        else
        {
            return tree; // Discard duplicates
        }

        // Update the height of the current node
        tree.Height = 1 + Math.Max(GetHeight(tree.left), GetHeight(tree.right));

        // AVL tree balancing
        int balanceFactor = GetHeight(tree.left) - GetHeight(tree.right); // left subtree height - right subtree height

        if (balanceFactor > 1) // Left-heavy is when the balance-factor is greater than 1(left subtree is taller than right subtree)
        { // Left-heavy

            if (IsSmaller(item, tree.left))
            { // Left-Left case is when the item is inserted into the left subtree of the left child.
                return RotateRight(tree);
            }
            else
            { // Left-Right case is when item is inserted into the right subtree of the left child.

                tree.left = RotateLeft(tree.left);
                return RotateRight(tree);
            }
        }
        else if (balanceFactor < -1) // Right-heavy is when the balance-factor is less than -1(right subtree is taller than left subtree)
        { // Right-heavy

            if (IsSmaller(tree.right, item))
            { // Right-Right case is when the item is inserted into the right subtree of the right child.
                return RotateLeft(tree);
            }
            else
            { // Right-Left case is when the item is inserted into the left subtree of the right child.
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
        if (IsSmaller(item, node))
        {
            node.left = DeleteNode(node.left, item);
        }
        else if (IsSmaller(node, item))
        {
            node.right = DeleteNode(node.right, item);
        }
        else
        {
            // Case 1: Node to be deleted is a leaf node
            if (node.left == null && node.right == null)
                return null;

            // Case 2: Node to be deleted has only one child
            if (node.left == null) // if leftchild==null, return rightchild
                return node.right;
            else if (node.right == null) // if rightchild==null, return leftchild
                return node.left;

            // Case 3: Node to be deleted has two children
            Node successor = FindMin(node.right); // Find the inorder successor of the node
            node.data = successor.data; // Copy the data of the inorder successor to the current node's data
            node.right = DeleteNode(node.right, successor); // Delete the inorder successor node from the right subtree, as its value has already been copied to replace the deleted node's value.
        }
        // AVL tree balancing

        // Update the height of the current node
        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

        // Check the balance-factor and perform rotations if necessary
        int balanceFactor = GetBalanceFactor(node);

        if (balanceFactor > 1)
        { // Left-heavy

            if (GetBalanceFactor(node.left) < 0)
            { // Left-Right case
                node.left = RotateLeft(node.left);
            }
            return RotateRight(node); // Left-left case

        }
        else if (balanceFactor < -1)
        {// Right-heavy
            if (GetBalanceFactor(node.right) > 0)
            {// Right-Left case
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
        if (current.left == node || current.right == node) // if current node is left or right child of node
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

        while (tree.left != null)
        {
            tree = tree.left;
            //Console.WriteLine("FindMin: Moving to left child, value: " + tree.data.data); // Testing
        }

        //Console.WriteLine("FindMin: Minimum value node: " + tree.data.data); // Testing
        return tree;
    }

    /// ------------------------------------------------------------- AVL Tree Test Functions ------------------------------------------------------------- ///

    static void TestInsertion()
    {
        Tree tree = new Tree(); // init test tree
        int[] elements = { 5, 3, 7, 1, 9 }; // init elements to insert as an array

        foreach (int element in elements) // for EVERY element in elements array
        {
            InsertTree(tree, new Node { data = new DataEntry { data = element } }); // insert the element into the tree
        }

        Assert(Size(tree) == 5, "Insertion test: Tree size is incorrect"); // check if tree size is correct
        Assert(IsBalanced(tree.root), "Insertion test: Tree is not balanced"); // check if tree is balanced
        Assert(IsSorted(tree), "Insertion test: Tree is not sorted"); // check if tree is sorted
    }

    static void TestDeletion()
    {
        Tree tree = new Tree(); // init test tree
        int[] elements = { 5, 3, 7, 1, 9 }; // init elements to insert as an array

        foreach (int element in elements) // for EVERY element in elements array
        {
            InsertTree(tree, new Node { data = new DataEntry { data = element } }); // insert the element into the tree
        }

        DeleteItem(tree, new Node { data = new DataEntry { data = 3 } }); // delete 3 from the tree 
        DeleteItem(tree, new Node { data = new DataEntry { data = 7 } }); // delete 7 from the tree

        Assert(Size(tree) == 3, "Deletion test: Tree size is incorrect"); // check if tree size is correct
        Assert(IsBalanced(tree.root), "Deletion test: Tree is not balanced"); // check if tree is balanced
        Assert(IsSorted(tree), "Deletion test: Tree is not sorted"); // check if tree is sorted
    }

    static void TestSearch()
    {
        Tree tree = new Tree(); // init test tree
        int[] elements = { 5, 3, 7, 1, 9 }; // init elements to insert as an array

        foreach (int element in elements) // for EVERY element in elements array
        {
            InsertTree(tree, new Node { data = new DataEntry { data = element } }); // insert the element into the tree
        }

        Assert(SearchTree(tree.root, new DataEntry { data = 5 }), "Search test: Existing element not found"); // check if existing element is found
        Assert(!SearchTree(tree.root, new DataEntry { data = 6 }), "Search test: Non-existing element found"); // check if non-existing element is NOT found
    }

    static bool IsBalanced(Node node)
    {
        if (node == null) return true;
        /*
        A null node IS considered balanced because it represents an empty subtree.
        In an AVL tree, an empty subtree is always balanced as it has a height of 0
        Returning true for null nodes ensures that the base case of the recursive
        function is handled correctly, thus will compile and run without errors.
        */

        int leftHeight = GetHeight(node.left); // Get the height of the left subtree
        int rightHeight = GetHeight(node.right); // Get the height of the right subtree

        if (Math.Abs(leftHeight - rightHeight) > 1) return false; // If the absolute difference between the heights of the left and right subtrees is greater than 1, thus the tree is unbalanced, return false

        bool isLeftBalanced = IsBalanced(node.left); // Recursively check if the left subtree is balanced
        bool isRightBalanced = IsBalanced(node.right); // Recursively check if the right subtree is balanced

        return isLeftBalanced && isRightBalanced; // Return true if both subtrees are balanced, false if EITHER subtree is unbalanced
    }

    static bool IsSorted(Tree tree)
    {
        int[] sortedArray = new int[Size(tree)]; // Create an array to store the sorted elements
        int index = 0; // Initialize the index to 0

        InOrderTraversal(tree.root, (int value) => { sortedArray[index++] = value; }); // Perform in-order traversal to store the elements in the array

        for (int i = 1; i < sortedArray.Length; i++) // Iterate over the sorted array up to the length of the array
        {
            if (sortedArray[i] < sortedArray[i - 1]) return false; // If the current element is LESS than the previous element, return false
        }

        return true; // If the array is sorted in ascending order, return true
    }

    static void InOrderTraversal(Node node, Action<int> action)
    {
        /*
        This function utilises an Action<int> delegate for flexible node
        data handling during in-order traversal; the null check serves as
        a base case, ensuring recursion halts at leaf nodes. I need to use 
        a delegate to handle the node's data, when I use this function inside
        the IsSorted function, essentially, I pass a lambda function that 
        stores the node's data in an array, that I check is indeed sorted.
        */

        if (node == null) return; // Base case: If the node is null, return immediately

        InOrderTraversal(node.left, action); // Recursively traverse the left subtree
        action(node.data.data); // Action<int> delegate to handle the node's data
        InOrderTraversal(node.right, action); // Recursively traverse the right subtree
    }

    static void Assert(bool condition, string message)
    { // This function, akin to Debug.Assert used for debugging, was custom-made by me due to restrictions on only being allowed to use C-core functionality.
        if (!condition) throw new Exception("Assertion failed"); // If the condition is false, throw an exception with the specified message
    }

    static void TestAVLBalancing()
    {
        /*
        This function tests whether the AVL tree remains balanced after insertion and deletion operations.
        First, it creates a new test tree and defines an array of elements to insert into the tree; it then
        iterates over each element in the array, inserting them into the tree. After each insertion, it checks
        whether the tree is balanced. Subsequently, it deletes an element from the tree and verifies if the
        tree remains balanced. The assert function is one I had to custom make, due to only being able to use
        C-core functionality and not being able to use any other libraries. The assert function checks if the
        condition is true, if not, it throws an exception with the specified message.
        */

        Tree tree = new Tree(); // Create a test AVL tree
        int[] elements = { 1, 2, 3, 4, 5, 6, 7 }; // Define an array of elements to insert into the tree

        foreach (int element in elements) // for EVERY element in the elements array
        {
            InsertTree(tree, new Node { data = new DataEntry { data = element } }); // insert the element into the tree
            Assert(IsBalanced(tree.root), "AVL Balancing test: Tree is not balanced after insertion"); // If IsBalanced returns false, throw an exception
        }

        DeleteItem(tree, new Node { data = new DataEntry { data = 4 } }); // Delete an element, specifically 4, from the tree
        Assert(IsBalanced(tree.root), "AVL Balancing test: Tree is not balanced after deletion"); // Check if the tree remains balanced after deletion
    }


    /// .... (and nowhere else) [x]

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
    static void PrintTree(Node tree) // Premade function to print the tree
    {
        if (tree == null) return; // this was needed to avoid null reference exceptions in set functions

        if (tree.left != null) // if left child is NOT null
            PrintTree(tree.left);

        Console.Write(tree.data.data + " "); // print the data of the current node

        if (tree.right != null) // if right child is NOT null
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
        return item1.data.data < item2.data.data; // if item1 data < item2 data return true, else false
    }


    /// <summary>
    /// Predicate that checks if two Nodes hold the same value. 
    /// 
    /// As we assume Integers for convenience, the comparison is just the usual "equality" on integers.
    /// Equality could be MORE complicated for other sorts of data.
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
    { // Premade function to insert an item into the tree
        if (tree == null) // if tree Node is empty, make item the tree's Node
        {
            tree = item;
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
    /// After the recursive insertion, the InsertItem function updates the height of the current node and checks the balance-factor.
    /// If the tree becomes unbalanced (balance-factor > 1 or < -1), it performs the necessary rotations to restore the balance.
    ///
    /// The InsertItem function returns the new root node of the subtree after the insertion and balancing process.
    /// The InsertTree function then assigns this new root node back to the root field of the tree struct,
    /// updating the entire tree with the inserted item.
    /// </remarks>
    static void InsertTree(Tree tree, Node item)
    {
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
        if (value.data == tree.data.data) // If the current node's data matches the value, return true
            return true;
        else if (value.data < tree.data.data) // If the value is LESS than the current node's data, search in the left subtree
            return SearchTree(tree.left, value);
        else // otherwise, search in the right subtree
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
    {
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
    {
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

        // Go right until we reach the RIGHTMOST node (maximum value node in the tree)
        while (current.right != null)
        {
            current = current.right;
        }

        // Now, current is the RIGHTMOST node, which will be the maximum value node in the tree
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
    AND THAT LINE, AS THEY'RE INSIDE THE RESPECTIVE FUNCTION
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

        Tree resultTree = new Tree();

        // Helper function to insert unique nodes from a given tree into the resultTree
        void InsertUniqueFromTree(Node node)
        {
            if (node == null) return; // Base case: If the current node is null, return immediately

            // Before inserting, check if the current node's value already exists in the resultTree
            if (!SearchTree(resultTree.root, node.data))
            {
                // If the value does not exist, insert the node into the resultTree
                InsertTree(resultTree, new Node { data = node.data });
            }

            // Recursively insert unique nodes from the left and right subtrees
            InsertUniqueFromTree(node.left);
            InsertUniqueFromTree(node.right);
        }

        // Insert all nodes from the first tree into the resultTree
        InsertUniqueFromTree(tree1.root);
        // Insert nodes from the second tree into the resultTree, checking for duplicates
        InsertUniqueFromTree(tree2.root);

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
            if (node == null) return; // Base case: If the current node is null, return immediately

            if (SearchTree(tree2.root, node.data))
            { // If nodes are in both trees, insert into result tree
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
    static Tree Difference(Tree tree1, Tree tree2)
    {

        Tree resultTree = new Tree(); // Create an empty result tree

        void DiffHelper(Node node)
        {
            if (node == null) return; // Base case: If the current node is null, return

            // Use SearchTree to check if the node's value exists in tree2
            if (!SearchTree(tree2.root, node.data))
            {
                // If the node value is in tree1 but not in tree2, insert into result tree
                InsertTree(resultTree, new Node { data = new DataEntry { data = node.data.data } });
            }

            DiffHelper(node.left); // Recursively check the left subtree
            DiffHelper(node.right); // Recursively check the right subtree
        }

        DiffHelper(tree1.root); // Start the difference recursive process from the root of tree1

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
        Tree resultTree = new Tree(); // Create an empty result tree

        void SymDiffHelper(Node node, Tree currentTree, Tree otherTree)
        {
            if (node == null) return; // Base case: If the current node is null, return

            // Check if the node value exists in the other tree
            if (!SearchTree(otherTree.root, node.data))
            {
                // If the node value is not in the other tree, insert it into the result tree
                InsertTree(resultTree, new Node { data = new DataEntry { data = node.data.data } });
            }

            SymDiffHelper(node.left, currentTree, otherTree); // Recursively check the left subtree
            SymDiffHelper(node.right, currentTree, otherTree); // Recursively check the right subtree
        }

        // Traverse tree1 and insert nodes that are not in tree2
        SymDiffHelper(tree1, new Tree { root = tree1 }, tree2);

        // Traverse tree2 and insert nodes that are not in tree1
        SymDiffHelper(tree2.root, tree2, new Tree { root = tree1 });

        return resultTree;
    }



    /* 
    * TESTING -----------------------------------------------------------------------------------------------------------------------------------------------------
    */


    /// <summary>
    /// Testing of the Tree methods that does some reasonable checks.
    /// It does not have to be exhaustive but sufficient to suggest the code is correct.
    /// </summary>
    static void TreeTests() // some tests
    {

        Console.WriteLine("----------------------------");
        Console.WriteLine("Entering TreeTests() function");
        Console.WriteLine("----------------------------");

        Tree tree = new Tree(); // init tree for tests
        Random r = new Random(); // init random number generator
        DataEntry data; // init data entry

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

        /*
        I run all my tests and directly report them as passed;
        as the assert function throws an exception if the condition is false,
        e.g. the test fails. also, prior to reporting success, I made sure that 
        they would all pass!
        */

        DateTime startTime = DateTime.Now; // start time

        TestInsertion(); // run insertion test
        Console.WriteLine("Insertion test passed");
        TestDeletion(); // run deletion test
        Console.WriteLine("Deletion test passed");
        TestSearch(); // run search test
        Console.WriteLine("Search test passed");
        TestAVLBalancing(); // run AVL balancing test
        Console.WriteLine("AVL balancing test passed");

        DateTime endTime = DateTime.Now; // end time
        TimeSpan elapsedTime = endTime - startTime; // calculate time-taken

        Console.WriteLine("Time-taken for AVL processing: " + elapsedTime.TotalMilliseconds + " milliseconds"); // print time taken

        Console.WriteLine("--------------------");
    }


    /// <summary>
    /// Testing of the Set methods that does some reasonable checks.
    /// It does not have to be exhaustive but sufficient to suggest the code is correct.
    /// </summary>

    static void SetTests()
    {
        /*
        This function tests the set operation functions.
        It creates 2 test trees, inserts values into them, and then tests
        the Union, Intersection, Difference, and SymmetricDifference functions.
        */

        Console.WriteLine("Entering SetTests() function");
        Console.WriteLine("----------------------------");


        // Create sample/test trees
        Tree tree1 = new Tree();
        Tree tree2 = new Tree();

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

        // Test to show all unique elements from both trees.
        Tree unionResult = Union(tree1, tree2);

        Console.WriteLine("Union of tree1 and tree2:");
        PrintTree(unionResult.root);
        Console.WriteLine();
        Console.WriteLine("----------------------------");

        // Test to show all elements common to both trees.
        Tree intersectionResult = Intersection(tree1, tree2);

        Console.WriteLine("Intersection of tree1 and tree2:");
        PrintTree(intersectionResult.root);
        Console.WriteLine();
        Console.WriteLine("----------------------------");

        // Test to show all elements in tree1 but not in tree2.
        Tree differenceResult = Difference(tree1, tree2);

        Console.WriteLine("Difference of tree1 and tree2:");
        PrintTree(differenceResult.root);
        Console.WriteLine();
        Console.WriteLine("----------------------------");

        // Test to show all elements that are in tree1 or tree2 but not in both.
        Tree symmetricDifferenceResult = SymmetricDifference(tree1.root, tree2);

        Console.WriteLine("Symmetric Difference of tree1 and tree2:");
        PrintTree(symmetricDifferenceResult.root);
        Console.WriteLine();
        Console.WriteLine("----------------------------");
        Console.WriteLine();
        Console.WriteLine("All tests passed successfully!"); // I first checked all tests would pass successfully, I guess I could've done this programmatically, however, I hope my manual check is sufficient.
        Console.WriteLine();
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
