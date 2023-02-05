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

                PlayerCanopy.manager = this;
                PlayerRoots.manager = this;

        }

        public void updateGraphicsStepwise(GrowthStatuses growthDifference)
        {

            print("update stepwise");
            //right now the only thing that can change is length and width
            //so look at the new addition, and for the next step of growth, determine its ratio of length and width

            float rootnewGrowthRatio = (float)growthDifference.roots.height / (float)growthDifference.roots.width;

            float canopynewGrowthRatio = (float)growthDifference.canopy.height / (float)growthDifference.canopy.wideness;

            float trunknewGrowthRatio = (float)growthDifference.trunk.height / (float)growthDifference.trunk.width;

            //if there is a width difference, grow leaves that bias wide
            //if there is height difference, grow leaves that bias tall
            //if the ratio seems equal, grow denser

            PlayerRoots.VariableSpawnStep(rootnewGrowthRatio);
            PlayerCanopy.VariableSpawnStep(canopynewGrowthRatio);

            //use the ratios of these 2 to bias the growth

            //get the ratio of the width and height of new growth
            //bias straigher branches for height, crookeder branches for width
            //the ratio being 1 will lead to dense growth

        }
    }
}