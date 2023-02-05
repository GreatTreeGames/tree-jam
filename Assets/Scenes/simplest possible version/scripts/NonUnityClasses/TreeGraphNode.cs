using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scenes.simplest_possible_version.scripts
{
    public class TreeGraphNode
    {
        public const int MaxChildren = 4;
        
        private List<TreeGraphNode> _children = new List<TreeGraphNode>();
        
        public TreeGraphNode(TreeNode treeNodeGameObj, Vector3 position, Vector3 toParent, float weight, bool isRoot = false)
        {
            TreeNodeGameObj = treeNodeGameObj;
            TreeNodeGameObj.transform.position = position;
            ToParent = toParent;
            Weight = weight;
            IsRoot = isRoot;
        }

        public TreeNode TreeNodeGameObj { get; set; }
        public SpriteRenderer Leaves { get; set; }
        public int NumChildren => _children.Count;
        public bool HasChildren => _children.Count > 0;
        public bool HasLeaves => Leaves != null;
        public int DistanceToClosestLeaf => GetDistanceToClosestLeaf(0);
        /// <summary> World position of this node's game object </summary>
        public Vector3 Position { get => TreeNodeGameObj.transform.position; }
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

        public void AddLeaves(SpriteRenderer leaves)
        {
            if (HasLeaves)
            {
                Object.Destroy(Leaves);
            }
            Leaves = leaves;
            Leaves.transform.position = Position;
            // Leaves.transform.localScale = Leaves.transform.localScale.Multiply(new Vector3(Weight, Weight, 1));
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
        
        private int GetDistanceToClosestLeaf(int currentDepth)
        {
            if (NumChildren == 0) return currentDepth;
            
            return _children.Select(c => c.GetDistanceToClosestLeaf(currentDepth + 1)).Min();
        }
    }
}
