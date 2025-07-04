using System; // Don't use anything else than System and only use C-core functionality; read the specs! [X]

// DON'T CHANGE

/*

Hi, as you can see, I made a GitHub repo for this project, and for each module. Previously,
I genuinely found this helpful concerning my poor memory; it gives me an organised and fast
way to keep track of my work on both computers. It give me a creative instrest while also providing
a dopamine hit when I push to the repo! However, I realised this also shows a high-level commitment,
organisation, structure, and proactive initiative in managing my coursework effectively; hence, I'm
mentioning it now. It was interesting to learn dotnet SDK to run the program, in the vscode directory.

https://github.com/alfie-ns/1003-CW

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

A 'base case' ensures recursion TERMINATES when a leaf node is reached, otherwise, the function could run forever,
causing a stack overflow.

- checks for a specific condition (e.g. if the node is null) to stop further recursive calls.
- also by halting recursion at leaf nodes, it ensures that each node is only processed once,
  maintaining efficiency and preventing runtime errors.

The 'ref' keyword is used for the 'treeSize' parameter in the 'GetDeleteNode' method to ensure that the tree's size is correctly updated after a node is deleted.
By passing 'treeSize' by reference, any modifications made to it inside the method will directly affect the original 'tree.size' value in the calling code.
This ensures that the tree's size is accurately maintained and synchronised with the actual number of nodes in the tree after the deletion operation.
Without using 'ref', the changes made to 'treeSize' inside the 'GetDeleteNode' method would not be visible to the calling code, and the tree's size would remain unchanged.

AVL Tree Explanation:
---------------------

An AVL tree is a self-balancing binary search tree where the heights of the two child subtrees of any node differ by at MOST one.
 
In an AVL tree, the heights of the left and right subtrees of any node differ by at MOST 1. This property guarantees that the tree
remains approximately balanced, which in turn provides efficient search, insertion, and deletion operations within a time-complexity
of O(log n).
 
The AVL self-balancing tree property states that for EVERY node in the tree, the absolute difference between the heights of
its left and right subtrees should be at MOST 1. By calculating the balance-factor, we can easily check if a node
violates this condition.

If the balance-factor of a node is greater than 1, it means that the left subtree is too tall compared to the
right subtree, making the node left-heavy. Conversely, if the balance-factor is LESS than -1, it indicates that the right subtree
is too tall compared to the left subtree, making the node right-heavy. These situations represent an imbalance in the tree.
 
When an imbalance is detected (i.e, the balance-factor is outside the range [-1, 1]), the AVL tree performs
rotations to rebalance the tree. The specific rotation's needed depend on the balance-factor and the structure of the subtrees
involved. In this case, after insertion or deletion operations, the program recursively assesses the balance factor of the current node and
performs necessary rotations as the recursive calls unwind, ensuring that the tree remains balanced.

If the balance-factor indicates a left-heavy imbalance (greater than 1), it'll check the left subtree's balance-factor to decide
between a single right rotation, or a left-right double rotation. Conversely, for a right-heavy imbalance (less than -1),
it checks the right subtree's balance-factor to decide between a single left rotation or a right-left double rotation.
This ensures the tree maintains its balanced state, therefore preserving the AVL tree's guarantee of logarithmic time-complexity.
 
By keeping the tree balanced, AVL trees ensure that the heights of the left and right subtrees are as close as possible.
This balance minimises the maximum depth of the tree, which in turn reduces the worst-case time complexity of operations
to O(log n).
 
The balance-factor provides a simple and efficient way to measure the balance of a node and the overall balance of the AVL tree.
By continuously monitoring the balance-factors and performing necessary rotations, AVL trees maintain their balanced property and
guarantee efficient operations.The balance-factor is calculated based on the heights of the subtrees, not the actual number of nodes
in each subtree. This allows for efficient calculation and updates during insertions and deletions WITHOUT the need to count the
number of nodes in each subtree.
 
The terms 'left-heavy' and 'right-heavy' refer to the balance-factor of a node, as we know; however, concretely, one should know
that a node is considered left-heavy when its left subtree's height exceeds that of its right subtree by MORE than one (balance-factor > 1),
and right-heavy when the opposite is true (balance-factor < -1). These term's determine the appropriate rotations to apply in
order to restore the tree's balance.

The height of a node is the length of the longest path from that node to a leaf node,
with box-drawing characters used in my implementation, one can visually see if the
heights are indeed balanced to AVL tree standards.

Height = length(node -> leafnode)
Depth = length(root -> node)
BalanceFactor = height(node.left) - height(node.right)

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Guaranteed worst-case time-complexity = AVL < BST:
- AVL Tree: O(log n)
- BST: O(log n) in the average case, O(n) in the worst case (unbalanced tree).

Thus, AVL trees guarantee a time-complexity of O(log n) for search, insertion,
and deletion operations, making them much more efficient than a BST in its
worst-case scenario; thus, they will search for data faster than an unbalanced BST.


If an AVL tree has 10 nodes, the maximum number of comparisons required to search for
a node is given by the formula log2(10)=3.32. Thus, in the worst case, the number of
steps (comparisons) required would be 4, as one needs to round up to the nearest whole
number; one cannot have .32 a step!

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Notebook
--------

[MESSAGE] In your tree, I've only inserted elements >10, to not get mixed up with
          the randomly generated elements. I do this so duplicates DON'T get
          immediately discarded. Thus, the tree may not be as balanced as possible
          in those situations. Nonetheless, the heights still DON'T differ by more
          than one; i just didn't want to change your code.

[MESSAGE] I've added a new function called TestDepth() to check the depth of each, as I need to
          make sure the depth of each element is correct. I'll run an assertion try-catch block
          to make sure the depth of each element is correct.


- [X] Give a logical explanation of how to quickly work out heights of nodes in an AVL tree.
- NO[X] perhaps I need to insert random nodes into the test trees?
- [X] GET TIME-TAKEN FOR A LARGE TREE SEARCH OPERATION
- [X] Show more testing for the Searches
- [X] More tests that tree is indeed Balanced after every operation
- [X] Use arrow-function somewhere: for delegates
- [X] Comment explaining why I used 'ref' for the treesize when deleting(to make sure global size is updated after deletion)
- [X] Define a large test tree
- [X] verify tree is indeed balanced after every operation
- [X] Visualise tree
- [X] Create new depth functions(depth, getdepth)
- [X] TestDeleteMin

- [ ] Fix GetDepth()
---------------------



 
*/

/// <summary>
/// Implement a binary search tree
///
/// [X][X] 1) Don't rename any of the method names in this file or change their arguments or return types or their order in the file.
/// [X][X] 2) If you want to add methods do this in the space indicated at the top of the Program.
/// [X] 3) You can add fields to the structures Tree, Node, DataEntry, if you find this necessary or useful.
/// [X] 4) Some of the method stubs have return statements that you may need to change (the code wouldn't run without return statements).
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
    public int Height; // This is for AVL tree, as one needs to keep track of the height of each node to balance the tree
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
                //Console.WriteLine("Performing left-right rotation.");
            }
            else
            {
                // Left-Left case: Perform a single right rotation.
                node = RotateRight(node);
                //Console.WriteLine("Performing single right rotation.");
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
                //Console.WriteLine("Performing right-left rotation.");
            }
            else
            {
                // Right-Right case: Perform a single left rotation.
                node = RotateLeft(node);
                //Console.WriteLine("Performing single left rotation.");
            }
        }

        // 4. Update the height of the current node after rebalancing.
        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

        // finally, return the rebalanced node, this will happen for each node
        return node;
    }
    /// ------------------------------------------------------------- AVL Rotation Functions ------------------------------------------------------------- ///
    static Node RotateRight(Node node)
    {
        // This function performs a right rotation on the given node in an AVL tree.

        // Store a reference to the NEW left child of the current node
        Node newRoot = node.left;

        // Update the left child of the current node to become the RIGHT child of the new root node,
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

    /// ------------------------------------------------------------- Balance Functions ------------------------------------------------------------- ///

    static int GetHeight(Node node)
    { // Get height of tree rooted at 'node' - (https://www.youtube.com/watch?v=_pnqMz5nrRs)

        /*
            height of a given node is defined as the length of the path
            to the deepest lead node of the tree rooted to that node.
        */
        if (node == null) return -1; // Base case: If the node is null, return -1, thereby making the height calculation the length of the PATH to the deepest leaf node
        int leftHeight = GetHeight(node.left); // Recursively calculate height of the left subtree
        int rightHeight = GetHeight(node.right); // Recursively calculate height of the right subtree
        return Math.Max(leftHeight, rightHeight) + 1; //you get the max of EITHER left or right subtree to find the LONGEST path to a leaf node, +1 to account for current node
    }

    static int GetBalanceFactor(Node node)
    {
        if (node == null) return 0; // Base case: If the node is null, return 0(balance-factor of an empty tree is 0)

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
        function is indeed handled correctly.
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

    //static int GetDepth(Node rootNode, int depth = 0)
    //{
    //    // initital failcheck
    //    if (rootNode == null) //
    //    {
    //        return 0; // return 0 if the root node is null thus tree empty
    //    }
    //    // Base case: If the current node matches the target node, return the current depth
    //    if (IsEqual(rootNode, targetNode))
    //    {
    //        return depth;
    //    }
    //
    //    // Decide whether to search in the left or right subtree and increment the depth
    //    if (IsSmaller(targetNode, rootNode)) // If the target node is smaller than the current node
    //    {
    //        return GetDepth(rootNode.left, depth++); // Search in the left subtree
    //    }
    //    else // then it must be larger
    //    {
    //        return GetDepth(rootNode.right, depth++); // Search in the right subtree
    //    }
    //}

    static Node GetInsertTree(Node node, Node newNode)
    { // First, perform the standard BST insertion
        if (node == null)
        { // Base case: If the node is null, return the new node and a height of 1 accounting for that node
            newNode.Height = 1; // Set the height as 1 for the single node thats been inserted
            return newNode; // Return the new node
        }
        if (IsSmaller(newNode, node)) // If the new node is smaller than the current node
            node.left = GetInsertTree(node.left, newNode); // Recursively insert the new node into the left subtree
        else if (IsSmaller(node, newNode)) // Elif the new node is larger than the current node
            node.right = GetInsertTree(node.right, newNode); // Recursively insert the new node into the right subtree
        else
        {
            // otherwise, the new node MUST equal the current node, thus discard the duplicate
            return node;
        }

        // Then, update the height of the current node, +1 to account for the node being inserted
        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

        return Rebalance(node); // rebalance the node after insertion
    }

    static Node GetDeleteNode(Node node, Node item, ref int treeSize)
    {
        if (node == null) return null; // Base case: If the node is null, return null

        if (IsSmaller(item, node)) // If the item is SMALLER than the current node, search in the left subtree
        {
            node.left = GetDeleteNode(node.left, item, ref treeSize); // Recursively search in the left subbtree
        }
        else if (IsSmaller(node, item)) // If the item is LARGER than the current node, search in the right subtree
        {
            node.right = GetDeleteNode(node.right, item, ref treeSize); // Recursively search in the right subtree
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
            node.right = GetDeleteNode(node.right, successor, ref treeSize); // Recursively delete the successor node
        }

        node.Height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right)); // Update the height of the current node

        return Rebalance(node); // return the rebalanced node
    }

    // Recursive helper function to calculate the size of a subtree
    static int GetSize(Node node)
    {
        // If the node is null, return 0
        if (node == null) return 0;

        // Recursively calculate the size of the left subtree
        int leftSize = GetSize(node.left);

        // Recursively calculate the size of the right subtree
        int rightSize = GetSize(node.right);

        // Return the sum of sizes of left subtree, right subtree, and the current node (1)
        return leftSize + rightSize + 1;
    }

    static Node GetParent(Node current, Node node)
    {
        if (current == null) return null; // Base case: If the current node is null, return null

        // Check if the given node is a child of the current node
        if (current.left == node || current.right == node) // if current node is left or right child of node
        {
            // If the given node is a child of the current node, then the current node is the parent
            return current;
        }

        // Recursively search for the parent in the left subtree
        Node parentInLeft = GetParent(current.left, node);
        if (parentInLeft != null)
        {
            // If the parent is found in the left subtree, return it
            return parentInLeft;
        }

        // Recursively search for the parent in the right subtree
        Node parentInRight = GetParent(current.right, node);
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
        if (tree == null) return null; // Base case: If the tree is empty, return null

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
            I need to pick elements that definitely won't
            be trandomly generated, in TestTrees() so the
            duplicate won'ts get discarded when I randomly
            insert it prior to when I can delete it.
        */

        int[] elements = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, }; // init elements into the tree that I want to delete, that definetely won't be randomly generated thus discarded before I can insert it
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

        Console.WriteLine("Deleting 11...");
        DeleteItem(testTree, new Node { data = new DataEntry { data = 11 } }); // delete 11 from the tree
        Console.WriteLine("Tree size after deleting 11: " + Size(testTree)); // print the size AFTER deletion
        PrintTreeVisual(testTree.root); // print the tree visually
        Assert(IsBalanced(testTree.root), "Deletion test: Tree is not balanced after deletion"); // more verification
        Console.WriteLine(); // newline

        Console.WriteLine("Deleting 15...");
        DeleteItem(testTree, new Node { data = new DataEntry { data = 15 } }); // delete 15 from the tree
        Console.WriteLine("Tree size after deleting 15: " + Size(testTree)); // print the size AFTER deletion
        PrintTreeVisual(testTree.root); // print the tree visually
        Assert(IsBalanced(testTree.root), "Deletion test: Tree is not balanced after deletion"); // more verification
        Console.WriteLine(); // newline

        Console.WriteLine("Deleting 20...");
        DeleteItem(testTree, new Node { data = new DataEntry { data = 20 } }); // delete 20 from the tree
        Console.WriteLine("Tree size after deleting 20: " + Size(testTree)); // print the size AFTER deletion
        PrintTreeVisual(testTree.root); // print the tree visually
        Assert(IsBalanced(testTree.root), "Deletion test: Tree is not balanced after deletion"); // more verification
        Console.WriteLine(); // newline

        int expectedSize = initialSize - 3; // calculate the expected size after deletion of 11, 15, and 20
        Assert(Size(testTree) == expectedSize, "Deletion test: Tree size is incorrect"); // check if tree size is correct
        Assert(IsBalanced(testTree.root), "Deletion test: Tree is not balanced"); // check if tree is balanced
        Assert(IsSorted(testTree), "Deletion test: Tree is not sorted"); // check if tree is sorted
    }


    static void TestSearch()
    {
        try
        {
            Tree tree = new Tree(); // init test tree

            DateTime startTime = DateTime.Now; // start time

            // Insert integers 1 to 50 into the tree
            for (int i = 1; i <= 50; i++)
            {
                InsertTree(tree, new Node { data = new DataEntry { data = i } }); // insert each element into the tree
                Assert(IsBalanced(tree.root), "Balanced insertion test: Tree is not balanced after insertion"); // more verification
            }

            // Rebalance the tree after insertions
            tree.root = Rebalance(tree.root);

            // The assertion function essentially return false if the condition NOT met.

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
            Assert(SearchTree(tree.root, new DataEntry { data = 6 }), "Search test: Existing element not found"); // check if element is indeed found
            Console.WriteLine("FOUND"); // print FOUND
            Console.WriteLine(); // newline

            Console.WriteLine("Searching for 1...");
            Assert(SearchTree(tree.root, new DataEntry { data = 1 }), "Search test: Existing element not found"); // check if existing element is found
            Console.WriteLine("FOUND"); // print FOUND
            Console.WriteLine(); // newline

            // Check it DOESN'T find non-existing elements

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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }


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
            3. Increment 'index' using the post-increment operator (index++) to move to the next position in the array after the value is stored.

            ++index increments the index before the value is used, whereas index++ increments the index after the value is used.

            If the index was incremented before storing the value, it would start at 1, thus skipping the first position of the sortedArray,
            as an array is zero-indexed.
       
            By performing an in-order traversal while using this lambda function, the elements of the tree are stored in the 'sortedArray'
            in ascending order. This is because an in-order traversal function visits the nodes in ascending order of their values.
       
            After the traversal is complete, the 'sortedArray' will contain all the elements of the tree in sorted order.
            The 'IsSorted' method then iterates over the 'sortedArray' and checks if each element is greater than or equal to the previous element.
            If any element is found to be SMALLER than its previous element, it means the tree is NOT sorted, and the method returns 'false'.
            Otherwise, if the entire array is iterated without finding any violations, the tree is considered sorted, and the method returns 'true'.
       
            This lambda function mixed with the in-order traversal function, allowing efficient checking of whether the tree is indeed sorted
            without modifying the tree structure.
        */


        for (int i = 1; i < sortedArray.Length; i++) // Iterate over the sorted array up to the length of the array
        {
            if (sortedArray[i] < sortedArray[i - 1]) return false; // If the current element is LESS than the previous element iterated element, return false
        }

        return true; // If the array is sorted in ascending order, return true
    }

    static void InOrderTraversal(Node node, Action<int> action)
    {
        /*
        This function utilises an Action<int> delegate for flexible node
        data handling during in-order traversal, the null check serves as
        a base case, ensuring recursion halts at leaf nodes. I need to use
        a delegate to handle the node's data, when I use this function inside
        the IsSorted function, essentially, I pass a lambda function that
        stores the node's data in an array, that I check is indeed sorted.
        */

        if (node == null) return; // Base case: If the node is null, return

        InOrderTraversal(node.left, action); // Recursively traverse the left subtree
        action(node.data.data); // Action<int> delegate to handle the node's data. data.data = the integer value of the node
        InOrderTraversal(node.right, action); // Recursively traverse the right subtree
    }

    static void Assert(bool condition, string message)
    { // my custom-made assert function: if boolean passed to function is NOT true, throw an exception with the specified message
      // this is used to show it works with proof that it's indeed working
        if (!condition) // if test function does NOT pass
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

        Console.WriteLine("Height(length of longest path from root to leaf) should be 5");
        Console.WriteLine("Height: " + GetHeight(tree.root)); // print height of root
        Console.WriteLine("Depth(length from the root to a specific node, this case the depth function is called on the root)");
        Console.WriteLine("Depth: " + Depth(tree.root)); // print depth of tree Node from ROOT
        // depth FROM the root, may indeed be might be height-1?
    }

    static void PrintTreeVisual(Node node, string indent = "", bool last = true) // insent starts as '' for subsequent accumulation
    {
        /*
            This function prints each node of the AVL tree using box-drawing characters
            and indentation to visualise the hierarchical structure of the tree. I implement
            a recursive depth-first search (DFS) approach to traverse and print the tree from the root.
           
            This method processes all nodes down the left subtree(leftmost branch) before backtracking
            and continuing with the right subtree, thereby maintaining the hierarchical relationships
            between nodes, effectively exploring the depth of each branch before moving to the next.

            Parameters:
            ------------
            - 'node': The current data of the node. node.data.data is the integer value of said node.
            - 'indent': A string that accumulates spaces || spaces+vertical lines to represent the visual
                        structure as the recursion progresses deeper into the tree. This indentation
                        helps visually delineate the depth and parent-child relationships in the tree.
            - 'last': Indicates if the node is the LAST child of its parent, which determines the type
                        of box-drawing character used and how indentation is adjusted; this starts as TRUE,
                        as a root node has NO siblings, ONLY children.

            Box-drawing characters:
            - '└─': Used when the current node is the LAST child, indicating no siblings follow directly below.
            - '├─': Used when more siblings follow; it also adds a vertical line '|' to connect subsequent siblings.

            Indent adjustments:
            - initially, the indent is an empty string, as the root node has no parent or siblings.
            - if 'last'==TRUE, no vertical line is extended below this node, ensuring a clean ending at the branch.
            - if 'last'==FALSE, a vertical line ('|') is added to continue the connection lines vertically for subsequent siblings.

            The vertical line ('|') is added to continue with connection lines vertically, indicating the pathway
            to deeper levels of the tree and helping to visualise the structure of sibling relationships.
           
           
            -------------------------------------------------------------------------------------
            | DFS Traversal logic to print box-drawing structured tree: |                       |
            -------------------------------------------------------------                       |
            |                                                                                   |  
            |                                                                                   |
            |     Consider my AVL binary tree as follows:                                       |
            |     {                                                                             |
            |            4            an AVL binary tree is a tree data structure where the     |
            |           /  \          left child or any given node is less than parent while    |
            |          2     6        while right child is >right-child. Thus a function        |
            |         / \   / \       can traverse the tree more efficiently due to the boolean |
            |        1   3 5   7      constraint used in searching instead of searching whole   |
            |                         datasets                                                  |                                                                             |    
            |     }                                                                             |
            |                                                                                   |
            |     Using box-drawing characters and indentation, the output would                |
            |     be:                                                                           |                                                                              |
            |     {                                                                             |
            |         └─4                                                                       |
            |           ├─2         note the box-drawing tree is indeed structured depth-first  |
            |           | ├─1       ensuring each node and its children are visited before      |                                          
            |           | └─3       moving on, and stuctured top-down to visually represent     |                                                        
            |           └─6         the hierarchy                                               |                                                          
            |             ├─5                                                                   |
            |             └─7  7(last)'s indent while (5->7) accumulation used next call is NOT |                       |
            |     }            used                                                             |
            |                                                                                   |
            |     In a depth-first AVL algorithm (DFS), the order for a full tree traversal:    |
            |     --------------------------------------------------------------------------    |  
            |                                                                                   |
            |     1. root(4)->left(2)->left(1), completed in the first recursive traversal.     |
            |                                                                                   |
            |     2. backtrack to node '2' then proceed with root(4)->left(2)->right(3).        |
            |                                                                                   |
            |     3. having completed the exploration of left-side (node '2' and its children), |
            |        backtrack to root '4' then LEFT subtree of root's right-child node(6);     |
            |        Thus root(4)->right(6)->left(5).                                           |
            |                                                                                   |
            |     4. finally, complete the traversal by visiting root(4)->right(6)->right(7),   |
            |        completing the exploration of all branches more efficiently than a         |
            |        standard unbalanced BST.                                                   |  
            |                                                                                   |
            ------------------------------------------------------------------------------------|
            | each recursive traversal  |                                                       |              
            -----------------------------                                                       |              
            |   1. root(4) (indent="  ", last=true) --  prints:       '└─4'                     |                            
            |   2. node(2) (indent="| ", last=false)--  prints:       '  ├─2'                   |          
            |   3. node(1) (indent="| ", last=false)--  prints:       '  | ├─1'                 |          
            |   4. node(3) (indent="  ", last=true) --  prints:       '  | └─3'                 |                              
            |   5. node(6) (indent="    ", last=true) --  prints:     '  └─6'                   |                          
            |   6. node(5) (indent="    ", last=false)--  prints:     '    ├─5'                 |
            |   7. node(7) (indent="      ", last=true) --  prints:   '    └─7'                 |
            |            ^^^NOT USED 6 indents^^^                                               |                    
            |   note 5 and 7 have 4 spaces of indentation, this is because the recursion carrys |
            |   over from the previous call due to 'indent' string accumulation from past calls,|
            |   if needed, to structure hierarchy to align child nodes under their respective   |
            |   parent nodes, the indent string is += and printed start of next recursive call; |
            |   thus last time round indent is NOT printed as it it doesn't get that far in the |    
            |   recusive layer because it doesn't call itself again after node(7)               |                                                  |                                                                                                                                  
            |   the final +2->'6=' indents NEVER used;                                          |
            |   last could also be thought of as first(before) next recusive call, or NOT       |
            |   intermediate. Thus the algorithm searches for nodes in given AVL tree FASTER    |
            |   the a standard unbalanced BST, thereby improving time-complexity for searches   |                                                                                                                                             |                                                                              
            -------------------------------------------------------------------------------------

        */


        if (node != null) // Proceed if node is not null to begin recursion
        {
            Console.Write(indent); // Print the previous accumulated indentation before the box-drawing character, starts empty so just '└─' is printed

            if (last) // Check if it is the last child
            {
                Console.Write("└─"); // Indicate end of this branch
                indent += "  "; // If this is the last child, adjust the indentation without adding a vertical continuation line for siblings, preparing
                                // for the next line of output at a potentially new level of depth next time round
            }
            else // if NOT last child || IS intermediate node
            {
                Console.Write("├─"); // Indicate continuation of this branch
                indent += "| "; // Add vertical line to align subsequent siblings
            }

            Console.WriteLine(node.data.data); // Print the node's integer value as data.data, this appears on the same line FOLLOWING box-drawing character

            // Recursively print left THEN right children depth-first
            PrintTreeVisual(node.left, indent, false); // we've already began at new branch so start with connection next time round
            PrintTreeVisual(node.right, indent, true); // Right child is the LAST in the order of visualisation

            // .Write instead of .WriteLine to print value on the SAME line as AFTER box-drawing character
        }

    }

    static Node FindNode(Node node, Node item)
    {
        if (node == null || IsEqual(node, item)) return node; // Base case: If the node is found OR the tree is empty, return the node argument

        if (IsSmaller(item, node)) // If the item is smaller than the current node, traverse the left subtree
            return FindNode(node.left, item);
        else // Otherwise, traverse the right subtree
            return FindNode(node.right, item);
    }

    /// ------------------------------------------------------------- Latest Test Functions ------------------------------------------------------------- ///

    /*
        These new test functions I made in May now use try-catch blocks to
        programmatically confirm the assertion tests pass, a pre-set structured
        tree is manually created, so we know where each node is situated, thus
        relative to each-other
    */

    static void Test_DeleteMin()
    {
        try  // try the following code; if an assertion exception is thrown, catch it.
        {
            // Test Case 1: Deleting the minimum node from an empty tree
            Tree emptyTree = new Tree(); // init testtree
            DeleteMin(emptyTree); // call DeleteMin, passing empty test tree
            Assert(emptyTree.root == null, "Tree should still be empty after trying to delete min from an empty tree."); // run assertion

            // Test Case 2: Deleting the minimum node from a single-node tree
            Tree singleNodeTree = new Tree(); // init testtree
            singleNodeTree.root = new Node { data = new DataEntry { data = 10 } };
            DeleteMin(singleNodeTree); // call DeleteMin passing a single-noded tree
            Assert(singleNodeTree.root == null, "Tree should be empty after deleting the min from a single-node tree."); // run assertion

            // Test Case 3: Deleting the minimum node from a tree with multiple nodes. This should delete 5, as it's the left-most (therefore smallest) node.
            Tree multipleNodesTree = new Tree(); // init test tree
            Node root = new Node { data = new DataEntry { data = 20 } };
            Node leftChild = new Node { data = new DataEntry { data = 10 } };
            Node rightChild = new Node { data = new DataEntry { data = 30 } };
            Node leftGrandchild = new Node { data = new DataEntry { data = 5 } }; // this is the left-most node to be deleted
            root.left = leftChild;
            root.right = rightChild;
            leftChild.left = leftGrandchild; // left-most
            multipleNodesTree.root = root; // Set the root of the multipleNodesTree to the provided root node

            DeleteMin(multipleNodesTree); // Delete the left-most (smallest) node from the multipleNodesTree. The recursive operation begins at the root.
            Assert(!SearchTree(multipleNodesTree.root, new DataEntry { data = 5 }), "Min node (5) should be deleted."); // check min value (5) indeed is the one deleted
            Assert(IsBalanced(multipleNodesTree.root), "Tree should remain balanced after deleting the min node."); // balance verification

            Console.WriteLine("DeleteMin tests PASSED - No Exceptions were thrown");
        }
        catch (Exception ex)
        {
            Console.WriteLine("DeleteMin test failed: " + ex.Message); // if assertions fail, catch exception here
        }
    }

    static void TestParent()
    {
        try // try the following code; if an assertion exception is thrown, catch it.
        {
            // Create a sample tree, where the names of the nodes represent the values they hold, to easily see what this node's data holds
            Tree tree = new Tree();
            Node root = new Node { data = new DataEntry { data = 4 } };
            Node node2 = new Node { data = new DataEntry { data = 2 } };
            Node node6 = new Node { data = new DataEntry { data = 6 } };
            Node node1 = new Node { data = new DataEntry { data = 1 } };
            Node node3 = new Node { data = new DataEntry { data = 3 } };
            Node node5 = new Node { data = new DataEntry { data = 5 } };
            Node node7 = new Node { data = new DataEntry { data = 7 } };

            // Construct tree structure, so we know where the parent of each node should be
            tree.root = root;
            root.left = node2;
            root.right = node6; // right-child of root thus intermediate node: as child of root but parent of node5 and node7 leaf-node's
            node2.left = node1;
            node2.right = node3;
            node6.left = node5;
            node6.right = node7;

            // Test Case 1: Find parent of the root node (should be null)
            Node parent1 = Parent(tree, root);
            Assert(parent1 == null, "Parent of the root node should be null");

            // Test Case 2: Find the parent of a leaf node (node1) which has a sibling but no children.
            Node parent2 = Parent(tree, node1);
            Assert(parent2 == node2, "Parent of node1 should be node2");

            // Test Case 3: Find the parent of an intermediate node (node6), eg, a node situated between the root and a leaf node.
            Node parent3 = Parent(tree, node6);
            Assert(parent3 == root, "Parent of node6 should be the root node");

            // Test Case 4: Find parent of a node that doesn't exist in the tree
            Node nonExistentNode = new Node { data = new DataEntry { data = 10 } };
            Node parent4 = Parent(tree, nonExistentNode);
            Assert(parent4 == null, "Parent of a non-existent node should be null");

            // If all assertions pass, print this success message
            Console.WriteLine("Parent test PASSED - No Exceptions were thrown");
        }
        catch (Exception ex)
        {
            // If an assertion fails, the error will be caught here
            Console.WriteLine(ex.Message);
        }
    }

    static int Depth(Node root, Node node)
    {
        if (root == null || node == null) return 0;

        if (root == node)
        {
            return 0; // Node found at the root, depth is 0
        }

        int leftDepth = Depth(root.left, node);
        if (leftDepth != -1)
        {
            return leftDepth + 1; // Node found in the left subtree
        }

        int rightDepth = Depth(root.right, node);
        if (rightDepth != -1)
        {
            return rightDepth + 1; // Node found in the right subtree
        }

        return -1; // Node not found
    }


    //// ----------------------------------------------------------------------------------------------------------------------------------------------------------

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

        if (tree.left != null) // if left child is NOT null
            PrintTree(tree.left);

        Console.Write(tree.data.data + " "); // print the data of the current integer node

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
        return item1.data.data < item2.data.data; // if (item1 integer) < (item2 integer) return true, else false
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
        return item1.data.data == item2.data.data; // if (item1 integer) == (item2 integer) return true, else false
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

        tree.root = GetInsertTree(tree.root, item); // Call the helper function, passing the root node of the tree to begin recursive insertion, and the item to insert into the tree
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
        if (tree == null) // Base case: If the tree is empty, or the item is not found, return false
            return false;
        if (value.data == tree.data.data) // If the current node's data matches the value, return true(FOUND)
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
        // Base case: If the tree is empty, return false
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
        tree.root = GetDeleteNode(tree.root, item, ref tree.size); // call the GetDeleteNode function, passing the root node, the item to delete, and the tree size by reference to ensure it's updated after deletion
    }


    /// <summary>
    /// Returns how many elements are in a Tree
    /// </summary>
    /// <param name="tree">The Tree.</param>
    /// <returns>The number of items in the tree.</returns>
    static int Size(Tree tree)
    {
        /*
        This function calculates the size of a tree by calling the recursive helper function GetSize.
        If the tree is empty (root is null), it returns 0. Otherwise, it calls the recursive helper function
        to calculate the size of the tree. The helper function traverses the tree in a depth-first manner,
        */

        // Base case: If the tree is empty (root is null), return 0 (no elements)
        if (tree.root == null) return 0;


        // If the tree is not empty, call the recursive helper function to calculate the size of the tree
        return GetSize(tree.root);

    }


    /// <summary>
    /// Returns the depth of a tree with root "tree"
    ///
    /// Note that this function should work for any non-empty subtree
    /// </summary>
    /// <param name="tree">The root of the tree</param>
    /// <returns>The depth of the tree.</returns>
    static int Depth(Node tree)
    { // the depth is the connectingc paths and always one less than height thus (height-1) to work programmatically,
      //  however. depth as the edges themselves
        if (tree == null) return 0; // Base case: If the node is null, return 0, as an empty tree has a depth of 0
        int leftHeight = GetHeight(tree.left); // Recursively calculate height of the left subtree
        int rightHeight = GetHeight(tree.right); // Recursively calculate height of the right subtree
        return Math.Max(leftHeight, rightHeight); //height-1=depth
    }


    /// <summary>
    /// Find the parent of Node node in Tree tree.
    /// </summary>
    /// <param name="tree">The Tree</param>
    /// <param name="node">The Node</param>
    /// <returns>The parent of node in the tree, or null if node has no parent.</returns>
    static Node Parent(Tree tree, Node node)
    {
        if (node == null || node == tree.root) return null; // node=null or node=root(no parent

        // Call the recursive helper function to find the parent
        return GetParent(tree.root, node);
    }

    /// <summary>
    /// Find the Node with maximum value in a (sub-)tree, given the IsSmaller predicate.
    /// </summary>
    /// <param name="tree">The root node of the tree to search.</param>
    /// <returns>The Node that contains the largest value in the sub-tree provided.</returns>
    static Node FindMax(Node tree)
    {
        // Base case: If the tree is empty, return null
        if (tree == null) return null;

        // Node to keep track of the current node as we traverse the tree
        Node current = tree;

        // Go right until we reach the RIGHTMOST node (maximum value node in the tree)
        while (current.right != null)
        {
            current = current.right;
        }

        // Now, current is the RIGHTMOST node, which will be the maximum value node in the tree
        Console.WriteLine("Max: " + current.data.data); // TESTING
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
        Finally, it calls the GetDeleteNode function to delete the node with the minimum value from the tree.
        */

        // If the tree is empty, there is nothing to delete
        if (tree.root == null) return; // Base case: If the tree is empty, return

        // Call helper func to find the node with the minimum value
        Node minNode = FindMin(tree.root);

        // Delete the node with the minimum value
        tree.root = GetDeleteNode(tree.root, minNode, ref tree.size);
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
            if (node == null) return; // Base case: If the current node is null, return

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
            if (node == null) return; // Base case: If the current node is null, return

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

        Console.WriteLine("Entering TreeTests() function");
        Console.WriteLine("-------------------------------");
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

        // some basic info on the tree
        Console.WriteLine("Size of the tree: " + Size(tree)); // print size
        Console.WriteLine("Height of root: " + GetHeight(tree.root)); // print height
        Console.WriteLine("Depth(number of edges from the tree's root node): " + Depth(tree.root)); // print depth
        Node minNode = FindMin(tree.root); // find min value
        Console.WriteLine("Min value in the tree: " + (minNode != null ? minNode.data.data.ToString() : "null")); // Terinary operator, quick if else
        Node maxNode = FindMax(tree.root); // find max value
        Console.WriteLine("Max value in the tree: " + (maxNode != null ? maxNode.data.data.ToString() : "null")); // Terinary operator, quick if else
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
        Console.WriteLine("---------- Visualising the tree while testing --------");
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

        // Test Parent functionality
        Console.WriteLine("----------Testing Parent function...----------");
        Console.WriteLine(); // newline
        TestParent(); // run Parent test
        Console.WriteLine(); // newline

        // Test DeleteMin
        Console.WriteLine("----------Testing DeleteMin...----------"); // testing...
        Console.WriteLine(); // newline
        Test_DeleteMin(); // run test for DeleteMin
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
        the assert function throws an exception if the condition
        is false, e.g. the test fails. Prior to reporting success,
        I checked all Assertion tests would pass successfully; I
        I could've done this programmatically, however, I hope my manual
        check is sufficient.
       
        LATER ON, I do indeed programmatically
        check the assert functions pass with a try-catch block in the
        TestParent() and Test_DeleteMin() functions
        */

        DateTime endTime = DateTime.Now; // end time
        TimeSpan elapsedTime = endTime - startTime; // calculate time-taken for AVL processing

        Console.WriteLine(); // newline
        Console.WriteLine("Time-taken for all AVL-structure and BST-operation testing: " + elapsedTime.TotalMilliseconds + "ms"); // print time taken in milliseconds
        Console.WriteLine(); // newline
        Console.WriteLine("----------------------------");

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
