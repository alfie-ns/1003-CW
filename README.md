# 1003-CW Report

- [X] **Create a VS C# command line project called BinarySearchTree.** The implementation **must use the Program.cs file** available on the DLE in the Assessment section. This file has been modified from the implementation provided for the worksheet on Binary Search Trees.
- [ ] Explain to Thomas that I made the github repo for my own wishes/uses, but this also showes high-level initiative and craftsmanship

## BST Functions 40 Marks

*Complete the implementation of a binary search tree in the Program.cs file provided by filling in the code requested. The methods/functions to implement are:*

- [X] static bool IsEqual(Node item1, Node item2)
- [X] static void InsertTree(Tree tree, Node item)
- [X] static bool SearchTree(Node tree, DataEntry value)
- [X] static bool SearchTreeItem(Node tree, Node item)
- [X] static void DeleteItem(Tree tree, Node item)
- [X] static int Size(Tree tree)
- [X] static int Depth(Node tree)
- [X] static Node Parent(Tree tree, Node node)
- [X] static Node FindMax(Node tree)
- [X] staticvoid DeleteMin(Tree tree)

## Set Data Type Functions 12 Marks

*Implement the following functions for a Binary Search Tree (BST) in the Program.cs file. The methods/functions to implement are:*

- [X] static Tree Union(Tree tree1, Tree tree2)
- [X] static Tree Intersection(Tree tree1, Tree tree2)
- [ ] static Tree Difference(Tree tree1, Node tree2)
- [ ] static Tree SymmetricDifference(Node tree1, Tree tree2)

*This subtask interprets Trees as representing mathematical Sets and asks to implement the basic Set operations. Short descriptions of each function, its arguments and return values are in the file Program.cs. Do not change these method prototypes. Each method is worth a maximum of 3 marks individually.*

## AVL Tree 18 Marks

- [X] https://youtu.be/jDM6_TnYIqE?si=SiG3GblT_3r_UQr7

Extend the implementation from a) by adding a tree balancing mechanism. Specifically, make your trees AVL Trees. Do not use another balancing mechanism. This is an advanced task and only recommended if you are a confident programmer. The same methods as in part a) are requested but with an AVL-type balancing mechanism. This will need extensions to the data structures defined in a) as well as, most likely, some auxiliary functions.

Start with reading about AVL Trees. The Wikipedia page about AVL Trees is a good start.

If you do task c) you do not need to implement task a) as both tasks require the same methods to be implemented. If you do c) you would receive up to 40 marks for the methods requested in a) and up to 20 marks for the balancing implementation. In other words, hand in only one solution, either for a) or for c) with the methods listed in a).

The Set functions/methods in b) should not depend on whether you implement balancing (a) or not (c). The Set functionality is built on top of the list of methods in a). These methods should be assumed to be opaque to the user and only their prototypes known.

## Extra 30 marks

- [ ] An additional 10 marks are allocated for good and efficient use of data structures and algorithms. Try to make your implementation as efficient as possible.
- [ ] An additional 10 marks are allocated for some error checking and/or handling of special cases, as well as some testing of your functions. Use the method stubs in the file Program.cs for this. Do not change the Main() method. (If useful, you are allowed to program individual test functions in the area in the Program between the lines THIS LINE and THAT LINE and call these in TreeTests() or SetTests().)
- [ ] An additional 10 marks are allocated for good programming style, like proper layout, readability, identifier names, commenting, modularisation, and the like.

## Testing

- [ ] Test the BST/AVL Functions
- [ ] Test the Set Data Type Functions

Do not use object-oriented programming other than method-free classes for the data containers. Do not use the C# Collection library. Keep your code simple and clear. The module’s DLE page contains an introduction into the C-core of C#. There are numerous examples in the lectures and labs showing what you can use.
