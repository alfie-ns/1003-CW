class Node:
    def __init__(self, data):
        self.data = data
        self.left = None
        self.right = None
        self.height = 1 # New node is initially added as leaf node

class AVLTree:
    def get_height(self, root):
        if not root:
            return 0
        return root.height

    def get_balance(self, root):
        if not root:
            return 0
        return self.get_height(root.left) - self.get_height(root.right)

    def rotate_right(self, y):
        x = y.left
        T2 = x.right
        x.right = y
        y.left = T2
        y.height = 1 + max(self.get_height(y.left), self.get_height(y.right))
        x.height = 1 + max(self.get_height(x.left), self.get_height(x.right))
        return x

    def rotate_left(self, x):
        y = x.right
        T2 = y.left
        y.left = x
        x.right = T2
        x.height = 1 + max(self.get_height(x.left), self.get_height(x.right))
        y.height = 1 + max(self.get_height(y.left), self.get_height(y.right))
        return y

    def insert(self, root, data):
        if not root:
            return Node(data)
        elif data < root.data:
            root.left = self.insert(root.left, data)
        else:
            root.right = self.insert(root.right, data)

        root.height = 1 + max(self.get_height(root.left), self.get_height(root.right))

        balance = self.get_balance(root)

        # Left Left Case
        if balance > 1 and data < root.left.data:
            return self.rotate_right(root)

        # Right Right Case
        if balance < -1 and data > root.right.data:
            return self.rotate_left(root)

        # Left Right Case
        if balance > 1 and data > root.left.data:
            root.left = self.rotate_left(root.left)
            return self.rotate_right(root)

        # Right Left Case
        if balance < -1 and data < root.right.data:
            root.right = self.rotate_right(root.right)
            return self.rotate_left(root)

        return root

    def pre_order(self, root):
        if not root:
            return
        print("{0} ".format(root.data), end="")
        self.pre_order(root.left)
        self.pre_order(root.right)

# Let's create an AVL Tree
avl = AVLTree()
root = None

# Insert values
root = avl.insert(root, 10)
root = avl.insert(root, 20)
root = avl.insert(root, 30)
root = avl.insert(root, 40)
root = avl.insert(root, 50)
root = avl.insert(root, 25)

# Preorder Traversal of the constructed AVL tree
print("Preorder traversal of the constructed AVL tree is:")
avl.pre_order(root)
print("\n")

# Height of the AVL Tree
print("Height of the AVL Tree:", root.height)

# Check if findParent finds parent

