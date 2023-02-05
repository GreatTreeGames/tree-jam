using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.simplest_possible_version.scripts
{
    public class TreeGraphNode
    {
        private List<TreeGraphNode> _children = new List<TreeGraphNode>();
        
        public TreeGraphNode(TreeNode treeNodeGameObj, Vector3 position, Vector3 toParent, float weight, bool isRoot = false)
        {
            TreeNodeGameObj = treeNodeGameObj;
            Position = position;
            ToParent = toParent;
            Weight = weight;
            IsRoot = isRoot;
        }

        public TreeNode TreeNodeGameObj { get; set; }
        public Vector3 Position { get => TreeNodeGameObj.transform.position; set => TreeNodeGameObj.transform.position = value; }
        public Vector3 ToParent { get; set; }
        public float Weight { get => TreeNodeGameObj.Weight; set => TreeNodeGameObj.Weight = value; }
        public bool IsRoot { get; }

        /// <summary>Rank 0 is the root node. All children of the root node are rank 1 and so on.</summary>
        public void ActOnSubgraph(Action<TreeGraphNode, TreeGraphNode, int> action, TreeGraphNode parent, int rank)
        {
            foreach (TreeGraphNode child in _children)
            {
                child.ActOnSubgraph(action, this, rank + 1);
            }
            action(parent, this, rank);
        }

        public void AddChild(TreeNode treeNodeGameObj, Vector3 position, float weight)
        {
            Vector3 newToParent = Position - position;
            var newNode = new TreeGraphNode(treeNodeGameObj, position, newToParent, weight);
            _children.Add(newNode);
            CopyChildrenToObj(_children, TreeNodeGameObj.Children);
        }

        private static void CopyChildrenToObj(IReadOnlyList<TreeGraphNode> source, IList<TreeNode> target)
        {
            for (int i = 0; i < target.Count; i++)
            {
                if (i < source.Count)
                {
                    target[i] = source[i].TreeNodeGameObj;
                }
                else
                {
                    target[i] = null;
                }
            }
        }
    }
}
