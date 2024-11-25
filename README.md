# 1003-CW | Alfie Nurse

# RUN with C# . NET Environment

## Install:

[Download .NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

- Run `dotnet --version` to verify that the .NET SDK has been installed correctly.

Then you can **run** my Adelson-Velsky and Landis dotnet environment using `dotnet run` command.

Or you can **run** the bash scripts in the root directory to quickly run the AVL tree environment.

## To create a dotnet environment/program

1. `dotnet new console -n ProjectName`
2. `cd ProjectName`
3. `dotnet run`

- `Program.cs`: The main C# file as the entry point of the application.
- `.csproj`: The project file with build settings and dependencies. csproj = C# project.
- `bin/`: The output folder for compiled files. bin stands for binary.
- `obj/`: Contains temporary build files. obj stands for object.
- `dotnet run`: The command to compile and run the project.
- `dotnet build`: The command to compile the project.

---

## Report [X] - 81.6

- [X] **Create a VS C# command line project called BinarySearchTree.** The implementation **must use the Program.cs file** available on the DLE in the Assessment section. This file has been modified from the implementation provided for the worksheet on Binary Search Trees.
- [X] Test codebase compiles on VS !!!

### BST/AVL Functions 58 Marks [X]

*'Complete the implementation of a binary search tree in the Program.cs file provided by filling in the code requested. The methods/functions to implement are:'*

- [X] static bool IsEqual(Node item1, Node item2)
- [X] static void InsertTree(Tree tree, Node item)
- [X] static bool SearchTree(Node tree, DataEntry value)
- [X] static bool SearchTreeItem(Node tree, Node item)
- [X] static void DeleteItem(Tree tree, Node item)
- [X] static int Size(Tree tree)
- [X] static int Depth(Node tree)
- [X] static Node Parent(Tree tree, Node node)
- [X] static Node FindMax(Node tree)
- [X] static void DeleteMin(Tree tree)

*'Extend the implementation from a) by adding a tree balancing mechanism. Specifically, make your trees AVL Trees. Do not use another balancing mechanism. This is an advanced task and only recommended if you are a confident programmer. The same methods as in part a) are requested but with an AVL-type balancing mechanism. This will need extensions to the data structures defined in a) as well as, most likely, some auxiliary functions.*

*Start with reading about AVL Trees. The Wikipedia page about AVL Trees is a good start.*

*If you do task c) you do not need to implement task a) as both tasks require the same methods to be implemented. If you do c) you would receive up to 40 marks for the methods requested in a) and up to 20 marks for the balancing implementation. In other words, hand in only one solution, either for a) or for c) with the methods listed in a).*

*The Set functions/methods in b) should not depend on whether you implement balancing (a) or not (c). The Set functionality is built on top of the list of methods in a). These methods should be assumed to be opaque to the user and only their prototypes known.'*

### Set Data Type Functions 12 Marks [X]

- [X] static Tree Union(Tree tree1, Tree tree2)
- [X] static Tree Intersection(Tree tree1, Tree tree2)
- [X] static Tree Difference(Tree tree1, Node tree2)
- [X] static Tree SymmetricDifference(Node tree1, Tree tree2)

*'This subtask interprets Trees as representing mathematical Sets and asks to implement the basic Set operations. Short descriptions of each function, its arguments and return values are in the file Program.cs. Do not change these method prototypes. Each method is worth a maximum of 3 marks individually.'*

### Extra 30 marks [X]

- [X] An additional 10 marks are allocated for good and efficient use of data structures and algorithms. Try to make your implementation as efficient as possible.
- [X] An additional 10 marks are allocated for some error checking and/or handling of special cases, as well as some testing of your functions. Use the method stubs in the file Program.cs for this. Do not change the Main() method. (If useful, you are allowed to program individual test functions in the area in the Program between the lines THIS LINE and THAT LINE and call these in TreeTests() or SetTests().
- [X] An additional 10 marks are allocated for good programming style, like proper layout, readability, identifier names, commenting, modularisation, and the like.

### Testing [X]

- [X] Test the BST/AVL Functions
- [X] Test the Set Data Type Functions
- [X] Insertion testing
- [X] Deletion testing
- [X] Print Visual AVL Tree

'Do not use object-oriented programming other than method-free classes for the data containers. Do no	t use the C# Collection library. Keep your code simple and clear. The module’s DLE page contains an introduction into the C-core of C#. There are numerous examples in the lectures and labs showing what you can use.'

---

81.6	4	33.6	4	4	5	5	4	4	4	4	4	4	9.6	4	4	4	4	14.4	4	28	5	5 4
