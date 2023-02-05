using System;
using UnityEngine;

namespace Scenes.simplest_possible_version.scripts
{
    public class TreeGraph : MonoBehaviour
    {
        [SerializeField] private bool enableInput = false;
        
        public TreeGraphNode Root { get; set; }

        /// <summary>
        /// First parameter of the action is the node's parent. Second parameter is the node being acted on, rank is the depth of the node.
        /// </summary>
        public void ActOnTree(Action<TreeGraphNode, TreeGraphNode, int> action)
        {
            Root.ActOnSubgraph(action, null, 0);
        }

        private void Update()
        {
            if (!enableInput) return;

            if (Input.GetKeyDown(KeyCode.Equals))
            {
                var newTreeNode = Instantiate(TreeManager.Instance.RootPrefab, transform);
                Root.AddChild(newTreeNode, Vector3.up, 1);
            }
        }
    }
}
