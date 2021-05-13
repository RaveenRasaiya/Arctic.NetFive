namespace Arctic.Tree
{
    public class TreeNode
    {
        public int Data { get; set; }
        public TreeNode LeftNode { get; set; }
        public TreeNode RightNode { get; set; }

        public static TreeNode AddNode(int data)
        {
            TreeNode node = new()
            {
                Data = data
            };
            return (node);
        }

        public static int GetTotal(TreeNode rootNode)
        {
            if (rootNode == null)
            {
                return 0;
            }
            return rootNode.Data + GetTotal(rootNode.LeftNode) + GetTotal(rootNode.RightNode);
        }
    }
}
