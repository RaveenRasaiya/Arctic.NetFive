using Arctic.Tree;
using FluentAssertions;
using Xunit;

namespace Arctic.Tests
{
    public class TreeNodeTests
    {

        [Fact]
        public void Tree_Get_Total()
        {
            TreeNode root = BaseTree();
            var total = TreeNode.GetTotal(root);
            total.Should().Be(15);
        }

        [Fact]
        public void Tree_Get_Total_Fail()
        {
            TreeNode root = BaseTree();
            var total = TreeNode.GetTotal(root);
            total.Should().NotBe(10);
        }

        [Fact]
        public void Tree_Get_Total_More_Level()
        {
            TreeNode root = BaseTree();
            root.RightNode.LeftNode = new TreeNode { Data = 6 };
            root.RightNode.RightNode = new TreeNode { Data = 7 };
            root.LeftNode.LeftNode.LeftNode = new TreeNode { Data = 10 };
            root.LeftNode.LeftNode.RightNode = new TreeNode { Data = 12 };
            var total = TreeNode.GetTotal(root);
            total.Should().Be(50);
        }

        private TreeNode BaseTree()
        {
            TreeNode root = new TreeNode { Data = 1 };
            root.LeftNode = new TreeNode { Data = 2 };
            root.RightNode = new TreeNode { Data = 3 };
            root.LeftNode.LeftNode = new TreeNode { Data = 4 };
            root.LeftNode.RightNode = new TreeNode { Data = 5 };
            return root;
        }
    }
}
