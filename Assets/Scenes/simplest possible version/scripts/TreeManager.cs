using UnityEngine;

namespace Scenes.simplest_possible_version.scripts
{
    public class TreeManager : MonoBehaviour
    {
        public static TreeManager Instance { get; private set; }

        [SerializeField] private float startingRootWeight;
        [SerializeField] private float startingTrunkHeight;
        [SerializeField] private float startingCanopyWeight;
        
        [field: SerializeField] public TreeNode RootPrefab { get; private set; }
        [field: SerializeField] public TreeNode BranchPrefab { get; private set; }
        [field: SerializeField] public Leaf LeafPrefab { get; private set; }
        
        [field: SerializeField] public TreeGraph Player1Roots { get; private set; }
        [field: SerializeField] public TreeGraph Player1Canopy { get; private set; }
        
        [field: SerializeField] public TreeGraph Player2Roots { get; private set; }
        [field: SerializeField] public TreeGraph Player2Canopy { get; private set; }
        
        private void Awake() 
        { 
            // If there is an instance, and it's not me, delete myself.
    
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            {
                Instance = this;
                
                var p1RootRootObj = Instantiate(RootPrefab, Player1Roots.transform);
                Player1Roots.Root = new TreeGraphNode(p1RootRootObj, Player1Roots.transform.position, Vector3.up, startingRootWeight, true);
                
                var p1CanopyRootObj = Instantiate(BranchPrefab, Player1Canopy.transform);
                Player1Canopy.Root = new TreeGraphNode(p1CanopyRootObj,
                    Player1Canopy.transform.position + new Vector3(0, startingTrunkHeight, 0),
                    Vector3.down,
                    startingCanopyWeight,
                    true);
                
                var p2RootRootObj = Instantiate(RootPrefab, Player2Roots.transform);
                Player2Roots.Root = new TreeGraphNode(p2RootRootObj, Player2Roots.transform.position, Vector3.up, startingRootWeight, true);
                
                var p2CanopyRootObj = Instantiate(BranchPrefab, Player2Canopy.transform);
                Player2Canopy.Root = new TreeGraphNode(p2CanopyRootObj,
                    Player2Canopy.transform.position + new Vector3(0, startingTrunkHeight, 0),
                    Vector3.down,
                    startingCanopyWeight,
                    true);
            } 
        }
    }
}
