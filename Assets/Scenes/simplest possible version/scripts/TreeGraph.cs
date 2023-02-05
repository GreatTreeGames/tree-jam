using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.simplest_possible_version.scripts
{
    public class TreeGraph : MonoBehaviour
    {
        [SerializeField] private bool enableInput = false;

        [SerializeField] private float _minDistance;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _minDegreesFromParent;
        [SerializeField] private float _maxDegreesFromParent;
        [SerializeField] private float _parentWeightFactor;
        public TreeGraphicsManager manager;
        
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
                SpawnNodesOnLeaves(_minDistance,
                    _maxDistance,
                    _minDegreesFromParent,
                    _maxDegreesFromParent,
                    _parentWeightFactor);
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                Action<TreeGraphNode, TreeGraphNode, int> kek = (parent, current, rank) =>
                {
                    current.Weight += Mathf.Pow(0.5f, rank + 1);
                };
                ActOnTree(kek);
            }
        }

        private void SpawnNodesOnLeaves(float minDistance, float maxDistance, float minDegreesFromParent, float maxDegreesFromParent, float parentWeightFactor)
        {
            Action<TreeGraphNode, TreeGraphNode, int> kek = (parent, current, rank) =>
            {
                if (current.HasChildren) return;
                int numBranches = Random.Range(1, TreeGraphNode.MaxChildren - current.NumChildren);
                for (int i = 0; i < numBranches; i++)
                {
                    if (manager != null)
                    {
                        var newTreeNode = Instantiate(manager.RootPrefab, transform); 
                        float distance = Random.Range(minDistance, maxDistance);
                        float degreesFromOppositeParent = Random.Range(minDegreesFromParent, maxDegreesFromParent);
                        var finalDisplacement = distance * (Quaternion.AngleAxis(degreesFromOppositeParent, Vector3.forward) * current.ToParent.normalized);
                        current.AddChild(newTreeNode, current.Position - finalDisplacement, current.Weight * parentWeightFactor);
                    }
                    else
                    {
                        var newTreeNode = Instantiate(TreeManager.Instance.RootPrefab, transform);  
                        float distance = Random.Range(minDistance, maxDistance);
                        float degreesFromOppositeParent = Random.Range(minDegreesFromParent, maxDegreesFromParent);
                        var finalDisplacement = distance * (Quaternion.AngleAxis(degreesFromOppositeParent, Vector3.forward) * current.ToParent.normalized);
                        current.AddChild(newTreeNode, current.Position - finalDisplacement, current.Weight * parentWeightFactor);
                    }
                    
                    
                }
            };
            ActOnTree(kek);
        }

        public void VariableSpawnStep(float bias)
        {
            //small ratio means bigger width than height
            //large ratio means bigger height than width
            //more volume the closer the ratio is to 1

            float branchspreadrange = 15;
            float branchspreadgainrate = 1f;
            float branchlengthminimum = 0.2f;
            float branchlengthmaximum = 2f;
            float branchlengthgainrate = 0.1f;
            //choose a number of branches to spawn on each leaf node
            var numBranches = UnityEngine.Random.Range(1, 4);
            //perform each branch spawn, getting farther and wider for each new branch spawned
            for (int i = 0; i <= numBranches; i++)
            {
             //   Root.TreeNodeGameObj.SpawnBranch(UnityEngine.Random.Range(branchlengthminimum + (i *branchlengthgainrate), branchlengthmaximum), UnityEngine.Random.Range(-branchspreadrange* (i+branchspreadgainrate), branchspreadrange * (i+1)));
            }
            // SetChildrenArray();
        }

        
    }
}
