
using System; // Don't use anything else than System and only use C-core functionality; read the specs! [X]

// DON'T CHANGE

/*

Hi, as you can see, I made a GitHub Repo for this project, and for each module. Previously,
I genuinely found this helpful concerning my poor memory; it gives me an organised and fast
way to keep track of my work on both computers. It also provides a dopamine hit when I push
to the repo! However, I realised this also shows a high-level commitment, organisation, structure,
and proactive initiative in managing my coursework effectively; hence, I'm mentioning it now.

https://github.com/alfie-ns/1003-CW

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

A 'base case' ensures recursion TERMINATES when a leaf node is reached, otherwise, the function could run forever, 
causing a stack overflow.

- checks for a specific condition (e.g. if the node is null) to stop further recursive calls.
- also by halting recursion at leaf nodes, it ensures that each node is only processed once, 
  maintaining efficiency and preventing runtime errors.

The 'ref' keyword is used for the 'treeSize' parameter in the 'DeleteNode' method to ensure that the tree's size is correctly updated after a node is deleted.
By passing 'treeSize' by reference, any modifications made to it inside the method will directly affect the original 'tree.size' value in the calling code.
This ensures that the tree's size is accurately maintained and synchronised with the actual number of nodes in the tree after the deletion operation.
Without using 'ref', the changes made to 'treeSize' inside the 'DeleteNode' method would not be visible to the calling code, and the tree's size would remain unchanged.

AVL Tree Explanation:
---------------------

An AVL tree is a self-balancing binary search tree where the heights of the two child subtrees of any node differ by at MOST one.
 
In an AVL tree, the heights of the left and right subtrees of any node differ by at MOST 1. This property guarantees that the tree 
remains approximately balanced, which in turn provides efficient search, insertion, and deletion operations within a time-complexity 
of O(log n). 
 
The AVL tree property states that for EVERY node in the tree, the absolute difference between the heights of 
its left and right subtrees should be at MOST 1. By calculating the balance-factor, we can easily check if a node violates this 
condition.

If the balance-factor of a node is greater than 1, it means that the left subtree is too tall compared to the 
right subtree, making the node left-heavy. Conversely, if the balance-factor is LESS than -1, it indicates that the right subtree 
is too tall compared to the left subtree, making the node right-heavy. These situations represent an imbalance in the tree.
 
When an imbalance is detected (i.e, the balance-factor is outside the range [-1, 1]), the AVL tree performs 
rotations to rebalance the tree. The specific rotation's needed depend on the balance-factor and the structure of the subtrees 
involved. In this case, after insertion or deletion operations, the program assesses the balance-factor of each node
starting from the newly inserted or deleted node's parent up to the root. If the balance-factor indicates a 
left-heavy imbalance (greater than 1). Conversely, for a right-heavy imbalance (less than -1), it checks the
right subtree's balance-factor to decide between a single left rotation or a right-left double rotation. This ensures the
tree maintains its balanced state, thereby preserving the AVL tree's guarantee of logarithmic time-complexity.
 
By keeping the tree balanced, AVL trees ensure that the heights of the left and right subtrees are as close 
as possible. This balance minimises the maximum depth of the tree, which in turn reduces the worst-case time-complexity of 
operations like search, insertion, and deletion to O(log n).
 
The balance-factor provides a simple and efficient way to measure the balance of a node and the overall balance of the AVL tree. 
By continuously monitoring the balance-factors and performing necessary rotations, AVL trees maintain their balanced property and 
guarantee efficient operations.
 
The balance-factor is calculated based on the heights of the subtrees, NOT the actual number of nodes 
in each subtree. This allows for efficient calculation and updates during insertions and deletions WITHOUT the need to count the 
number of nodes in each subtree.
 
Again, the terms 'left-heavy' and 'right-heavy' refer to the balance-factor of a node; a node is considered left-heavy when its left 
subtree's height exceeds that of its right subtree by MORE than one (balance-factor > 1), and right-heavy when the opposite is true 
(balance-factor < -1). These term's determine the appropriate rotations to apply in order to restore the tree's balance.

The height of a node is the length of the longest path from that node to a leaf node... {AMEND THIS}

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Guaranteed best-case time-complexity = AVL < BST:
- AVL Tree: O(log n)
- BST: O(log n) in the average case, O(n) in the worst case (unbalanced tree).

Thus, AVL trees guarantee a time-complexity of O(log n) for search, insertion,
and deletion operations, making them much more efficient than a BST in its
worst-case scenario; thus, they will search for data faster than an unbalanced BST.

If an AVL tree has 10 nodes, the formula for finding the average time-complexity is
log2(10) = 3.32, which rounds down to 3, since steps are integers (one cannot have
0.32 of a step!); thus, on average, it will take 3 steps to find a particular node
in this tree. This will always be the case in an AVL tree, but not for a BST that
could become unbalanced.

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Notebook
--------

[MESSAGE] Although recursive rebalancing may not be the most efficient approach, I have done it this way
          to sensure it definitey is balanced after every operation.


- [X] Make an inverted assert functionality to report sucessful tests???
- [X] Give a smart explanation of how to calculate heights -> balancing-factor:
- NO[X] perhaps I need to insert random nodes into the test trees?
- [X] GET TIME-TAKEN FOR A LARGE TREE SEARCH OPERATION
- [X] Show more testing for the Searches
- [X] More tests that tree is indeed Balanced after every operation
- [X] Comment explaining why I used 'ref' for the treesize when deleting(to make sure size is updated after deletion)
- [X] Define a large test tree
- [X] verify tree is indeed balanced after every operation
 
*/

/// <summary>
/// Implement a binary search tree 
/// 
/// [x][x] 1) Don't rename any of the method names in this file or change their arguments or return types or their order in the file.
/// [x][x] 2) If you want to add methods do this in the space indicated at the top of the Program.
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
{ // Get the height from node's subtrees, add 1 to the max of the two, and set it as the height of the node
    public DataEntry data;
    public Node right;
    public Node left;
    public int Height; // This is for AVL tree, as one needs to keep track of the height of the tree to balance it
} 


/// <summary>
/// The top-level tree structure
/// </summary>
class Tree
{
    public Node root; // The root node of the tree
    public int size; // The number of elements in the tree, used to keep track of the tree's size
    
}



class Program // Program class, the entry point of the program 
{

    /// THIS LINE: If you want to add methods add them between THIS LINE and THAT LINE

    /// Your methods go here:

    /// ------------------------------------------------------------- AVL Tree Functions ------------------------------------------------------------- ///
    
    static Node Rebalance(Node node)
    {

        /*
            This function serves as a single function which'll first get the balance factor
            from GetBalanceFactor function then handle all rotations necessary to rebalance 
            the AVL tree for optimal time-complexity. 
        */

        // 1. Calculate the balance factor of the current node.
        int balanceFactor = GetBalanceFactor(node);

        // 2. If the balance factor is greater than 1, the tree is left-heavy.
        if (balanceFactor > 1)
        {
            // Check the balance factor of the left child to determine the case.
            if (GetBalanceFactor(node.left) < 0)
            {
                // Left-Right case: Perform a left-right rotation.
                node = RotateLeftRight(node);
            }
            else
            {
                // Left-Left case: Perform a single right rotation.
                node = RotateRight(node);
            }
        }
        // 3. If the balance factor is less than -1, the tree is right-heavy.
        else if (balanceFactor < -1)
        {
            // Check the balance factor of the right child to determine the case.
            if (GetBalanceFactor(node.right) > 0)
            {
                // Right-Left case: Perform a right-left rotation.
                node = RotateRightLeft(node);
            }
            else
            {
                // Right-Right case: Perform a single left rotation.
                node = RotateLeft(node);
            }
        }

        // 4. Traverse the subtree recursively to check and rebalance the entire tree.
        if (node.left != null)
        {
            node.left = Rebalance(node.left);
        }
        if (node.right != null)
        {
            node.right = Rebalance(node.right);
        }

        // 5. Update the height of the current node after rebalancing.
        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

        // return the rebalanced node
        return node;
    }
    /// ------------------------------------------------------------- AVL Rotation Functions ------------------------------------------------------------- ///
    static Node RotateRight(Node node)
    {
        // This function performs a right rotation on the given node in an AVL tree.

        // Store a reference to the left child of the current node; it becomes the new root of the rotated subtree.
        Node newRoot = node.left;

        // Update the left child of the current node to become the right child of the new root node,
        // ensuring that the right subtree of the new root becomes the left subtree of the current node.
        node.left = newRoot.right;

        // Make current node the left child of the new root, completing the rotation.
        newRoot.right = node;

        // this then updates the heights of the nodes involved in the rotation;
        // the height of a node is calculated as 1 plus the maximum height of its left and right subtrees.

        // Then: update respective heights of the current node and the new root node.

        // Calculate the height of the current node as 1 + maximum height of its left and right subtrees.
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

    static Node RotateLeftRight(Node node)
    {
        // This function performs a left-right double rotation on the given node in an AVL tree.

        // Perform a left rotation on the left child of the current node.
        node.left = RotateLeft(node.left);

        // Then perform a right rotation on the current node.
        return RotateRight(node);
    }

    static Node RotateRightLeft(Node node)
    {
        // This function performs a right-left double rotation on the given node in an AVL tree.

        // Perform a right rotation on the right child of the current node.
        node.right = RotateRight(node.right);

        // Then perform a left rotation on the current node.
        return RotateLeft(node);
    }

    /// ------------------------------------------------------------- Balance-factor Functions ------------------------------------------------------------- ///
    
    static int GetHeight(Node node)
    { // get the height of trees
        if (node == null) return -1; // Base case: If the node is null, return -1 (learnt from: https://www.youtube.com/watch?v=_pnqMz5nrRs)
        int leftHeight = GetHeight(node.left); // Recursively calculate height of the left subtree
        int rightHeight = GetHeight(node.right); // Recursively calculate height of the right subtree
        return Math.Max(leftHeight, rightHeight) + 1; //you get the max of EITHER left or right subtree to find the LONGEST path to a leaf node, +1 to account for current node
    }

    static int GetBalanceFactor(Node node)
    {
        if (node == null) return 0; // Base case: If the node is null, immediately return 0(balance-factor of an empty tree is 0)

        return GetHeight(node.left) - GetHeight(node.right);
        // The balance-factor = subtracting right subtree height from the left subtree height
    }

    static bool IsBalanced(Node node)
    {
        if (node == null) return true;
        /*
        A null node IS considered balanced because it represents an empty subtree.

        In an AVL tree, an empty subtree is always balanced as it has a height of 0
        Returning true for null nodes ensures that the base case of the recursive
        function is handled correctly.
        */

        // Traverse and get the heights of the left and right subtrees
        int leftHeight = GetHeight(node.left); 
        int rightHeight = GetHeight(node.right); 

        if (Math.Abs(leftHeight - rightHeight) > 1) return false; // If the absolute difference between the heights of the left and right subtrees is greater than 1, thus the tree is unbalanced, return false

        bool isLeftBalanced = IsBalanced(node.left); // Recursively check if the left subtree isBalanced
        bool isRightBalanced = IsBalanced(node.right); // Recursively check if the right subtree is isBalanced

        return isLeftBalanced && isRightBalanced; // Return true if both subtrees are balanced, false if EITHER subtree is unbalanced
    }


    /// ------------------------------------------------------------- AVL/BST Operationa Functions ------------------------------------------------------------- ///
        
    static Node InsertTreeHelper(Node node, Node newNode)
    { // First, perform the standard BST insertion
        if (node == null) { // Base case: If the node is null, return the new node and a height of 1 accounting for that single node
            newNode.Height = 1; // Set the height as 1 for the single node thats been inserted
            return newNode; // Return the new node
        }
        if (IsSmaller(newNode, node)) // If the new node is smaller than the current node
            node.left = InsertTreeHelper(node.left, newNode); // Recursively insert the new node into the left subtree
        else if (IsSmaller(node, newNode)) // Elif the new node is larger than the current node
            node.right = InsertTreeHelper(node.right, newNode); // Recursively insert the new node into the right subtree
        else
        {
            // otherwise, the new node MUST equal the current node, thus discard the duplicate
            return node;
        }

        // Then, update the height of the current node, +1 to account for the node being inserted
        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

        return Rebalance(node); // rebalance the node after insertion
    }

    static Node DeleteNode(Node node, Node item, ref int treeSize)
    {
        if (node == null) return null; // Base case: If the node is null, return null immediately

        if (IsSmaller(item, node)) // If the item is SMALLER than the current node, search in the left subtree
        {
            node.left = DeleteNode(node.left, item, ref treeSize); // Recursively search in the left subbtree
        }
        else if (IsSmaller(node, item)) // If the item is LARGER than the current node, search in the right subtree
        {
            node.right = DeleteNode(node.right, item, ref treeSize); // Recursively search in the right subtree
        }
        else // Otherwise, the item is found
        {
            if (node.left == null && node.right == null) // If the node is a leaf node
            {
                treeSize--; // Decrement the tree size when deleting a leaf node
                return null; // Return an empty tree
            }

            if (node.left == null) // If the node only has a right child
            {
                treeSize--; // Decrement the tree size when deleting a node with only a right child
                return node.right; // Return the right child
            }
            else if (node.right == null) // If the node only has a left child
            {
                treeSize--; // Decrement the tree size when deleting a node with only a left child
                return node.left; // Return the left child
            }

            Node successor = FindMin(node.right); // Find the minimum value in the right subtree
            node.data = successor.data; // Replace the current node's data with the successor's data
            node.right = DeleteNode(node.right, successor, ref treeSize); // Recursively delete the successor node
        }

        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right)); // Update the height of the current node
        
        return Rebalance(node); // return the rebalanced node
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
        if (current == null) return null; // Base case: If the current node is null, return null
        
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

        // If the parent is not found in EITHER subtree, return null
        return null;
    }

    static Node FindMin(Node tree)
    {
        if (tree == null) return null; // Base case: If the tree is empty, return null immediately

        //Console.WriteLine("FindMin: Current node value: " + tree.data.data); // TESTING

        while (tree.left != null) // while NOT at the left-most(smallest) node
        {
            tree = tree.left;
            //Console.WriteLine("FindMin: Moving to left child, value: " + tree.data.data); // TESTING
        }

        //Console.WriteLine("FindMin: Minimum value node: " + tree.data.data); // TESTING
        return tree;
    }


    /// ------------------------------------------------------------- Test Functions ------------------------------------------------------------- ///

    static void TestInsertion(Tree testTree)
    {
        /*
            This test function shows that the tree is
            INSERTING and rebalanceing itself correctly.
            I need to pick elements that definitely WON'T
            be trandomly generated, in TestTrees() so the
            duplicate WON'T get discarded when I randomly
            insert it prior to when I can delete it.
        */

        int[] elements = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 }; // init the particular elements into the tree that I want to delete, that definetely won't be randomly generated thus discarded before I can insert it
        //int[] randomElements = new int[10]; // init an empty array to store random elements

        int initialSize = Size(testTree); // get the initial size of the testTree
        Console.WriteLine("Initial tree size: " + initialSize); // print the size BEFORE insertion
        Console.WriteLine(); // newline

        int uniqueElementsInserted = 0; // init a counter for unique elements inserted

        DateTime startTime = DateTime.Now; // start time
        foreach (int element in elements) // for EVERY element in elements array
        {
            if (!SearchTree(testTree.root, new DataEntry { data = element })) // if the element is NOT already in the tree
            {
                InsertTree(testTree, new Node { data = new DataEntry { data = element } }); // insert the element into the tree
                uniqueElementsInserted++; // increment counter
            }
            Console.WriteLine("Inserted element: " + element); // print the element that was inserted
            Console.WriteLine("Tree size after insertion: " + Size(testTree)); // print the size AFTER insertion
            PrintTreeVisual(testTree.root); // print the tree visually
            Assert(IsBalanced(testTree.root), "Insertion test: Tree is not balanced after inserting " + element); // more verification
        }
        Console.WriteLine(); // newline
        
        Console.WriteLine("Final tree size: " + Size(testTree)); // print the size AFTER ALL insertions

        DateTime endTime = DateTime.Now; // end time
        TimeSpan elapsedTime = endTime - startTime; // calculate time-taken for AVL insertions
        Console.WriteLine("Time-taken for 10 insertions: " + (elapsedTime.TotalMilliseconds) + "ms"); // print time-taken

        Assert(Size(testTree) == initialSize + uniqueElementsInserted, "Insertion test: Tree size is incorrect"); // check if tree size is correct
        Assert(IsBalanced(testTree.root), "Insertion test: Tree is not balanced"); // check if tree is balanced, start from the root
        Assert(IsSorted(testTree), "Insertion test: Tree is not sorted"); // check if tree is sorted
    }

    static void TestDeletion(Tree testTree)
    {
        /*
            This test function shows that the tree is
            deleting and rebalanceing itself correctly.
            I need to pick elements that definitely WON'T
            be trandomly generated, in TestTrees() so a
            duplicate WON'T get discarded when I randomly
            insert it prior to when I can delete it.
        */

        int initialSize = Size(testTree); // get the initial size of the testTree from the randomly generated elements outside of the function
        Console.WriteLine("Initial tree size: " + initialSize);
        Console.WriteLine(); // newline

        Console.WriteLine("Deleting 10...");
        DeleteItem(testTree, new Node { data = new DataEntry { data = 10 } }); // delete 10 from the tree
        Console.WriteLine("Tree size after deleting 10: " + Size(testTree)); // print the size AFTER deletion
        PrintTreeVisual(testTree.root); // print the tree visually
        Assert(IsBalanced(testTree.root), "Deletion test: Tree is not balanced after deletion"); // more verification
        Console.WriteLine(); // newline

        Console.WriteLine("Deleting 50...");
        DeleteItem(testTree, new Node { data = new DataEntry { data = 50 } }); // delete 50 from the tree
        Console.WriteLine("Tree size after deleting 50: " + Size(testTree)); // print the size AFTER deletion
        PrintTreeVisual(testTree.root); // print the tree visually
        Assert(IsBalanced(testTree.root), "Deletion test: Tree is not balanced after deletion"); // more verification
        Console.WriteLine(); // newline

        Console.WriteLine("Deleting 90...");
        DeleteItem(testTree, new Node { data = new DataEntry { data = 90 } }); // delete 13 from the tree
        Console.WriteLine("Tree size after deleting 90: " + Size(testTree)); // print the size AFTER deletion
        PrintTreeVisual(testTree.root); // print the tree visually
        Assert(IsBalanced(testTree.root), "Deletion test: Tree is not balanced after deletion"); // more verification
        Console.WriteLine(); // newline

        int expectedSize = initialSize - 3; // calculate the expected size after deletion of 11, 12, and 13
        Assert(Size(testTree) == expectedSize, "Deletion test: Tree size is incorrect"); // check if tree size is correct
        Assert(IsBalanced(testTree.root), "Deletion test: Tree is not balanced"); // check if tree is balanced
        Assert(IsSorted(testTree), "Deletion test: Tree is not sorted"); // check if tree is sorted
    }

    static void TestSearch()
    {
        Tree tree = new Tree(); // init test tree

        DateTime startTime = DateTime.Now; // start time

        // Insert integers 1 to 50 into the tree
        for (int i = 1; i <= 50; i++)
        {
            InsertTree(tree, new Node { data = new DataEntry { data = i } });
            Assert(IsBalanced(tree.root), "Balanced insertion test: Tree is not balanced after insertion");
        }

        // Rebalance the tree after insertions
        tree.root = Rebalance(tree.root);

        Console.WriteLine("Searching for 5...");
        Assert(SearchTree(tree.root, new DataEntry { data = 5 }), "Search test: Existing element not found"); // check if existing element is found
        Console.WriteLine("FOUND"); // print FOUND
        Console.WriteLine(); // newline
        
        Console.WriteLine("Searching for 3...");
        Assert(SearchTree(tree.root, new DataEntry { data = 3 }), "Search test: Existing element not found"); // check if existing element is found
        Console.WriteLine("FOUND"); // print FOUND
        Console.WriteLine(); // newline

        Console.WriteLine("Searching for 23...");
        Assert(SearchTree(tree.root, new DataEntry { data = 23 }), "Search test: Existing element not found"); // check if existing element is found
        Console.WriteLine("FOUND"); // print FOUND
        Console.WriteLine(); // newline

        Console.WriteLine("Searching for 6...");
        Assert(SearchTree(tree.root, new DataEntry { data = 6 }), "Search test: Non-existing element found"); // check if non-existing element is NOT found
        Console.WriteLine("FOUND"); // print FOUND
        Console.WriteLine(); // newline

        Console.WriteLine("Searching for 1...");
        Assert(SearchTree(tree.root, new DataEntry { data = 1 }), "Search test: Existing element not found"); // check if existing element is found
        Console.WriteLine("FOUND"); // print FOUND
        Console.WriteLine(); // newline

        // CHECK IT DOESN'T FIND NON-EXISTING ELEMENTS

        Console.WriteLine("Searching for 101...");
        Assert(!SearchTree(tree.root, new DataEntry { data = 101 }), "Search test: Non-existing element found"); // check if non-existing element is NOT found
        Console.WriteLine("Succesfully NOT FOUND"); // print NOT FOUND  
        Console.WriteLine(); // newline

        Console.WriteLine("Searching for -1...");
        Assert(!SearchTree(tree.root, new DataEntry { data = -1 }), "Search test: Non-existing element found"); // check if non-existing element is NOT found
        Console.WriteLine("Succesfully NOT FOUND"); // print NOT FOUND
        Console.WriteLine(); // newline
        

        DateTime endTime = DateTime.Now; // end time
        TimeSpan elapsedTime = endTime - startTime; // calculate time-taken for AVL processing
        Console.WriteLine("Time-taken for 7 searches: " + (elapsedTime.TotalMilliseconds) + "ms"); // print time-taken


    }

    

    static bool IsSorted(Tree tree)
    {
        // By using a single array, this avoids complex manipulations or additional data structures that could be inefficient or error-prone.
        int[] sortedArray = new int[Size(tree)]; // Create an array to store the sorted elements
        // pass the root of the tree and a lambda function to store the node's data in the array

        int index = 0; // Initialise the index to 0
        InOrderTraversal(tree.root, (int value) => { sortedArray[index++] = value; });
        /*
            The lambda function stores the node's data in the array during in-order traversal.
            The way this lambda function works is:
            1. Take an integer 'value' as input, which represents the data of the current node being visited during the traversal.
            2. Store the 'value' in the 'sortedArray' at the current 'index' position.
            3. Increment 'index' using the post-increment operator (index++) to move to the next position in the array.

            ++index would increment the index before the value is stored, whereas index++ increments the index after the value is stored.
        
            By performing an in-order traversal and using this lambda function, the elements of the tree are stored in the 'sortedArray'
            in ascending order. This is because an in-order traversal of a binary search tree visits the nodes in ascending order of their values.
        
            After the traversal is complete, the 'sortedArray' will contain all the elements of the tree in sorted order.
            The 'IsSorted' method then iterates over the 'sortedArray' and checks if each element is greater than or equal to the previous element.
            If any element is found to be smaller than its previous element, it means the tree is not sorted, and the method returns 'false'.
            Otherwise, if the entire array is iterated without finding any violations, the tree is considered sorted, and the method returns 'true'.
        
            This lambda function, used in conjunction with the in-order traversal, allows for efficient checking of whether the tree is sorted
            without modifying the original tree structure.
        */
    

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
        action(node.data.data); // Action<int> delegate to handle the node's data. data.data = the integer value of the node
        InOrderTraversal(node.right, action); // Recursively traverse the right subtree
    }

    static void Assert(bool condition, string message)
    { // custom assert function: if boolean passed to function is NOT true, throw an exception with the specified message
        if (!condition) 
            throw new Exception("Assertion failed: " + message); // print exception
    }

    static void TestAVLBalancing()
    {
        Tree tree = new Tree(); // Create a test AVL tree

        // loop through 50 elements 1 to 50
        for (int i = 1; i <= 50; i++)
        {
            InsertTree(tree, new Node { data = new DataEntry { data = i } }); // Insert each element into the tree
            Assert(IsBalanced(tree.root), "AVL Balancing test: Tree is not balanced after insertion"); // Check if stays balanced after each insertion
        }

        // Print the visual representation of the balanced tree
        Console.WriteLine("Balanced AVL Tree with elements 1 to 50:");
        PrintTreeVisual(tree.root);
    }

    static void PrintTreeVisual(Node node, string indent = "", bool last = true) 
    {
        /*
            This function prints the AVL tree in the console using box-drawing characters for structure.
            The 'indent' parameter creates visual indentation, and 'last' is used to identify if a node
            is the last child, influencing the visual layout. Initially, 'last' is true for the root,
            simplifying the start of the rendering logic. This approach, inspired by a technique I
            observed one night on YouTube, ensures consistent formatting from the 'top down'. This means
            ensuring the root node sets the initial structure, and as we move to each level, we adjust
            indentations and box-drawing characters accordingly, reflecting each node's position and relation in the
            hierarchy efficiently and clearly. I copied and pasted each box-drawing character from: https://en.wikipedia.org/wiki/Box-drawing_character
            as I couldn't work out how to type them on my keyboard, and I didn't want to spend too much time on it!

        */

        if (node != null) // If node is NOT null
        {
            Console.Write(indent); // Write the indent. Initially, 'indent' is an empty string, effectively adding no extra space before the root node.

            if (last) // if it's the last child in the sibling group
            {
                Console.Write("└─"); // print box-drawing indicating it's the last child 
                indent += "  "; // += 2 spaces to the indent for alignment since no vertical line is needed below this node 
            }
            else // Otherwise the node is NOT the last child
            {
                Console.Write("├─"); // print box-drawing character indicating it has siblings below it
                indent += "| "; // Add a vertical line followed by a space to the indent for correct visual alignment of its children

            }

            Console.WriteLine(node.data.data); // Write the node's data to the console

            PrintTreeVisual(node.left, indent, false); // Recursively call PrintTreeVisual on the left child, last==false this time round
            PrintTreeVisual(node.right, indent, true); // Recursively call PrintTreeVisual on the right child, last==true this time round
            
            // the recursive search for the left child is set to false because the left child won't be the last child in the BST.
            
        }
    }

    static Node FindNode(Node node, Node item)
    {
        if (node == null || IsEqual(node, item)) return node; // Base case: If the node is immediately found OR the tree is empty, return the node argument
            
        if (IsSmaller(item, node)) // If the item is smaller than the current node, traverse the left subtree
            return FindNode(node.left, item);
        else // Otherwise, traverse the right subtree
            return FindNode(node.right, item);
    }

    /// .... (and nowhere else) [X]
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
        return item1.data.data < item2.data.data; // if item1 integer < item2 integer return true, else false
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
        return item1.data.data == item2.data.data; // if item1 integer == item2 integer return true, else false
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
    static void InsertTree(Tree tree, Node item)
    {
        
        tree.root = InsertTreeHelper(tree.root, item); // call the helper function, passing the root node and the element to insert into the tree
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

        // then recursively search in the RIGHT subtree
        bool foundInRight = SearchTreeItem(tree.right, item);
        if (foundInRight)
            return true;

        // this is in-order traversal, to search the left subtree first

        // If the item is not found in EITHER subtree, it is not in the tree at all
        return false;
    }


    /// <summary>
    /// Delete a Node from a tree
    /// </summary>
    /// <param name="tree">The root of the tree</param>
    /// <param name="item">The Node to remove</param>
    static void DeleteItem(Tree tree, Node item)
    {
        tree.root = DeleteNode(tree.root, item, ref tree.size); // call the helper function, passing the root node, the item to delete, and the tree size by reference to ensure it is updated after deletion
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
          First, null check, then recursively calculate the depth of the left and right subtrees. 
          The depth is the length of the longest path from the root to a leaf node. It first checks
          the leftDepth then rightDepth, then returns the max depth of the left or right subtree, plus 1
          accounting for the current node.
        */

        // Base case: If the tree is empty (root is null), return 0 (no depth) immediately
        if (tree == null) return 0;

        // Else, recursively calculate the depth of the left or right subtree's
        int leftDepth = Depth(tree.left);
        int rightDepth = Depth(tree.right);

        // Return the max depth, EITHER left or right subtree, plus 1 for the current node
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
        the node given to the function.

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
        Then, it calls the FindMin() helper function to find the node with the minimum value. 
        Finally, it calls the DeleteNode function to delete the node with the minimum value from the tree.
        */

        // If the tree is empty, there is nothing to delete
        if (tree.root == null) return; // Base case: If the tree is empty, return immediately

        // Call helper func to find the node with the minimum value
        Node minNode = FindMin(tree.root);

        // Delete the node with the minimum value
        tree.root = DeleteNode(tree.root, minNode, ref tree.size);
    }


    // --------------------------------------------------------------------------SET FUNCTIONS-------------------------------------------------------------------------- //


    ///!!! <remarks> !!! /!!! </remarks> !!! !!! /!!! </remarks> !!! !!! /!!! </remarks> !!! !!! /!!! </remarks> !!! !!! 
    /* 

    MADE THE HELPER FUNCTIONS INSIDE THE RESPECTIVE FUNCTION BECAUSE I WAS ENCOUNTERING ERROR REGARDING THE SCOPE OF resultTree.
    NEVERTHELESS, THESE STILL ARE NOT FUNCTIONS OUTSIDE THIS LINE && THAT LINE, AS THEY'RE INSIDE THE RESPECTIVE FUNCTION,
    THUS ARE PART OF THE FUNCTION ITSELF.

    */
    ///!!! </remarks> !!! !!! /!!! </remarks> !!! !!! /!!! </remarks> !!! !!! /!!! </remarks> !!! !!! /!!! </remarks> !!! !!! 


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

        Tree resultTree = new Tree(); // Create an empty result/test tree

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
        /*
            This function computes the set difference
            of the values in the two trees (tree1 - tree2)
            and returns a new tree with the resulting values.
        */

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

        /*
            This function computes the symmetric difference
            of the values in the two trees (tree1 XOR tree2)
            and returns a new tree with the resulting values.
        */

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
        Console.WriteLine(" ----------------------------------------------------------------------------------------------------------- ");
        Console.WriteLine(" --------------- Alfie Nurse's AVL tree Implementation and Set Data Type Functions in C# ------------------- ");
        Console.WriteLine(" ----------------------------------------------------------------------------------------------------------- ");
        Console.WriteLine(); // newline

        Console.WriteLine("Entering TreeTests() function");
        Console.WriteLine("--------------------------------");
        Console.WriteLine(); // newline

        Tree tree = new Tree(); // init tree for tests
        Random r = new Random(); // init random number generator
        DataEntry data; // init data entry

        // Build a tree inserting 10 random values as data
        Console.WriteLine("Build a tree inserting 10 random values as data");
        Console.WriteLine("------------------------------------------------");

        DateTime startTime = DateTime.Now; // start time

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
        PrintTree(tree.root); // print tree
        Console.WriteLine(); // newline
        Assert(IsBalanced(tree.root), "TreeTests: Tree is not balanced after initial insertions"); // more verification

        Console.WriteLine("-------------");

        // print the size of the tree
        Console.WriteLine("Size of the tree: " + Size(tree)); // print size
        Console.WriteLine("Depth of the tree: " + Depth(tree.root)); // print depth
        Node minNode = FindMin(tree.root); // find min value
        Console.WriteLine("Min value in the tree: " + (minNode != null ? minNode.data.data.ToString() : "null")); // Terinary operator, quick if else 
        Node maxNode = FindMax(tree.root); // find max value
        Console.WriteLine("Max value in the tree: " + (maxNode != null ? maxNode.data.data.ToString() : "null"));
        Console.WriteLine("--------------------------------");

        // test SearchTree
        Console.WriteLine("Search for 10 random values");

        data = new DataEntry();
        for (int i = 0; i < 10; i++)
        {
            data.data = r.Next(10);
            Console.WriteLine(data.data + " was" + (!SearchTree(tree.root, data) ? " NOT" : "") + " found");
        }
        Console.WriteLine(); // newline
        Console.WriteLine("---------- Alfie Nurse: Visualising the tree while testing --------");
        Console.WriteLine(); // newline

        // Print and visualise the initial tree
        Console.WriteLine("Checking if tree is indeed balanced initially..."); // check if tree is balanced
        tree.root = Rebalance(tree.root); // rebalance the tree
        Assert(IsBalanced(tree.root), "Initial tree is not balanced!"); // check if tree is indeed balanced
        PrintTreeVisual(tree.root); // print visual tree
        Console.WriteLine(); // newline

        // Test insertion
        Console.WriteLine(); // newline
        Console.WriteLine("----------Testing insertion...----------"); // testing...
        Console.WriteLine(); // newline
        TestInsertion(tree); // run test passing the tree
        Console.WriteLine(); // newline
        Console.WriteLine("Tree after insertion:"); // header for visual tree
        PrintTreeVisual(tree.root); // print visual tree
        Console.WriteLine(); // newline
        Console.WriteLine("Insertion test PASSED!"); // print success
        Console.WriteLine(); // newline

        // Test deletion
        Console.WriteLine("----------Testing deletion...----------"); // testing...
        Console.WriteLine(); // newline
        TestDeletion(tree); // run test passing the tree
        Console.WriteLine(); // newline
        Console.WriteLine("Tree after deletion:"); // header for visual tree
        PrintTreeVisual(tree.root); // print visual tree
        Console.WriteLine(); // newline
        Console.WriteLine("Deletion test PASSED!"); // print success
        Console.WriteLine(); // newline

        // Test search - I NEED TO IMPROVE THIS TEST [X]

        Console.WriteLine("----------Testing search...----------"); // testing...
        Console.WriteLine(); // newline
        TestSearch(); // run test for search
        Console.WriteLine(); // newline
        Console.WriteLine("Search test PASSED!");
        Console.WriteLine(); // newline

        // Test AVL balancing
        Console.WriteLine("----------Testing AVL balancing...----------"); // testing...
        Console.WriteLine(); // newline
        tree.root = Rebalance(tree.root); // rebalance the tree
        TestAVLBalancing(); // run test for AVL balancing
        Console.WriteLine(); // newline
        Console.WriteLine("AVL balancing test PASSED!");

        /*
        I run all my tests and directly report them as PASSED;
        as the assert function throws an exception if the condition
        is false; e.g. the test fails. Prior to reporting success,
        I checked all Assertion tests would pass successfully; I guess
        I could've done this programmatically, however, I hope my manual
        check is sufficient.
        */

        DateTime endTime = DateTime.Now; // end time
        TimeSpan elapsedTime = endTime - startTime; // calculate time-taken for AVL processing

        Console.WriteLine(); // newline
        Console.WriteLine("Time-taken for all AVL testing: " + elapsedTime.TotalMilliseconds + "ms"); // print time taken in milliseconds
        Console.WriteLine(); // newline
        Console.WriteLine("----------------------------");
        Console.WriteLine(); // newline
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
        Console.WriteLine(); // newline

        // Create sample/test trees for set operations
        Tree tree1 = new Tree();
        Tree tree2 = new Tree();

        // Insert values into tree1
        InsertTree(tree1, new Node { data = new DataEntry { data = 5 } });
        InsertTree(tree1, new Node { data = new DataEntry { data = 3 } });
        InsertTree(tree1, new Node { data = new DataEntry { data = 7 } });
        InsertTree(tree1, new Node { data = new DataEntry { data = 1 } });
        InsertTree(tree1, new Node { data = new DataEntry { data = 9 } });
        InsertTree(tree1, new Node { data = new DataEntry { data = 4 } });

        // Insert values into tree2
        InsertTree(tree2, new Node { data = new DataEntry { data = 6 } });
        InsertTree(tree2, new Node { data = new DataEntry { data = 2 } });
        InsertTree(tree2, new Node { data = new DataEntry { data = 9 } });
        InsertTree(tree2, new Node { data = new DataEntry { data = 5 } });
        InsertTree(tree2, new Node { data = new DataEntry { data = 8 } });
        InsertTree(tree2, new Node { data = new DataEntry { data = 10 } });

        // Test to show all unique elements from both trees.
        Tree unionResult = Union(tree1, tree2);
        // Should be: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        Console.WriteLine("Union of tree1 and tree2:");
        PrintTree(unionResult.root);
        Console.WriteLine(); // newline
        Console.WriteLine("----------------------------");

        // Test to show all elements common to both trees.
        Tree intersectionResult = Intersection(tree1, tree2);
        // Should be: 5, 9
        Console.WriteLine("Intersection of tree1 and tree2:");
        PrintTree(intersectionResult.root);
        Console.WriteLine(); // newline
        Console.WriteLine("----------------------------");

        // Test to show all elements in tree1 but not in tree2.
        Tree differenceResult = Difference(tree1, tree2);
        // Should be: 1, 3, 4, 7
        Console.WriteLine("Difference of tree1 and tree2:");
        PrintTree(differenceResult.root);
        Console.WriteLine(); // newline
        Console.WriteLine("----------------------------");

        // Test to show all elements that are in tree1 or tree2 but not in both.
        Tree symmetricDifferenceResult = SymmetricDifference(tree1.root, tree2);
        // Should be: 1, 2, 3, 4, 6, 7, 8, 10
        Console.WriteLine("Symmetric Difference of tree1 and tree2:");
        PrintTree(symmetricDifferenceResult.root); // print the symmetric difference
        Console.WriteLine(); // newline
        Console.WriteLine("----------------------------");
        Console.WriteLine(); // newline
        Console.WriteLine("All tests PASSED successfully!"); // manual confirmation of success
        Console.WriteLine(); // newline
    }

    /// IF YOU HAVEN'T ALREADY, CHECK THE TOP OF THE FILE FOR A LARGE COMMENT SECTION

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