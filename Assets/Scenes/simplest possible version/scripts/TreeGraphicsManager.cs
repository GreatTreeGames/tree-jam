using UnityEngine;

namespace Scenes.simplest_possible_version.scripts
{
    public class TreeGraphicsManager : MonoBehaviour
    {
        public static TreeGraphicsManager Instance { get; private set; }

        [SerializeField] private float startingRootWeight;
        [SerializeField] private float startingTrunkHeight;
        [SerializeField] private float startingCanopyWeight;
        
        [field: SerializeField] public TreeNode RootPrefab { get; private set; }
        [field: SerializeField] public TreeNode BranchPrefab { get; private set; }
        [field: SerializeField] public Leaf LeafPrefab { get; private set; }
        
        [field: SerializeField] public TreeGraph PlayerRoots { get; private set; }
        [field: SerializeField] public TreeGraph PlayerCanopy { get; private set; }

        private void Awake()
        {
            var p1RootRootObj = Instantiate(RootPrefab, PlayerRoots.transform);
            PlayerRoots.Root = new TreeGraphNode(p1RootRootObj, PlayerRoots.transform.position, default, startingRootWeight, true);

            var p1CanopyRootObj = Instantiate(BranchPrefab, PlayerCanopy.transform);
            PlayerCanopy.Root = new TreeGraphNode(p1CanopyRootObj,
                PlayerCanopy.transform.position + new Vector3(0, startingTrunkHeight, 0),
                default, startingCanopyWeight,
                true);

        }
    }
}