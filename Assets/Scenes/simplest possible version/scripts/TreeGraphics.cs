using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGraphics : MonoBehaviour
{
    public SpriteRenderer CanopySprite;
    public SpriteRenderer TrunkSprite;
    public SpriteRenderer RootsSprite;
    public float RootScaleIncrementMultiplier = 1;
    public float TrunkScaleIncrementMultiplier = 1;
    public float CanopyScaleIncrementMultiplier = 1;
    public void updateGraphics(Statuses status)
    {
        {
            //increment root wideness and depth
            float rootwideness = 1 + (RootScaleIncrementMultiplier * status.roots.width);
            float rootdepth = 1 + (RootScaleIncrementMultiplier * status.roots.height);

            RootsSprite.transform.localScale = new Vector2(rootwideness, rootdepth);
        }
        {
            //increment trunk wideness and depth
            float trunkwideness = 1 + (TrunkScaleIncrementMultiplier * status.trunk.width);
            float trunkdepth = 1 + (TrunkScaleIncrementMultiplier * status.trunk.height);

            RootsSprite.transform.localScale = new Vector2(trunkwideness, trunkdepth);
        }
        {
            //increment canopy wideness and depth
            float canopyWideness = 1 + (CanopyScaleIncrementMultiplier * status.branchesLeaves.wideness);
            float canopyDepth = 1 + (CanopyScaleIncrementMultiplier * status.branchesLeaves.height);

            CanopySprite.transform.localScale = new Vector2(canopyWideness, canopyDepth);
        }
        {
            //increment leaf color

        }
        {
            //align all sprites
            //bottom of trunk align with top of roots
            sitOnTopOfReposition(TrunkSprite, RootsSprite);
            //bottom of canopy align with top of trunk
            sitOnTopOfReposition(CanopySprite, TrunkSprite);
        }

    }

    void sitOnTopOfReposition(SpriteRenderer topOne, SpriteRenderer bottomone)
    {
        //align sprite topone to sit on top of other sprite bottomone
        float newY = bottomone.gameObject.transform.position.y + bottomone.bounds.extents.y/2f + topOne.bounds.extents.y/2f;
        topOne.transform.position += (Vector3.up * newY);
    }
}
