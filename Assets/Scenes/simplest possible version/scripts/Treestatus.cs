using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treestatus : MonoBehaviour
{
    Statuses status;
    public TreeGraphics visual;

    public void rushRoots()
    {
        status.roots.width+=2;
        visual.updateGraphics(status);
    }

    public void boomRoots()
    {
        status.roots.width++;
        status.roots.height++;
        visual.updateGraphics(status);
    }

    public void turtleRoots()
    {
        status.roots.height+=2;
        visual.updateGraphics(status);
    }

    public void RushTrunk()
    {
        //wow it's fucking nothing
        visual.updateGraphics(status);
    }

    public void boomTrunk()
    {
        status.trunk.height+=2;
        visual.updateGraphics(status);
    }

    public void turtleTrunk()
    {
        status.trunk.width +=2;
        visual.updateGraphics(status);
    }

    public void rushBranchesLeaves()
    {
        status.branchesLeaves.wideness+=2;
        visual.updateGraphics(status);
    }

    public void boomBranchesLeaves()
    {
        status.branchesLeaves.height+= 1;
        status.branchesLeaves.wideness+=1;
        visual.updateGraphics(status);
    }

    public void turtleBranchesLeaves()
    {
        status.branchesLeaves.darkness+=2;
        visual.updateGraphics(status);
    }


}

public struct Statuses
{
    public rootsStatus roots;
    public trunkStatus trunk;
    public branchesLeavesStatus branchesLeaves;
}

public struct rootsStatus
{
    public int width;
    public int height;
}

public struct trunkStatus
{
    public int height;
    public int width;

}

public struct branchesLeavesStatus
{
    public int wideness;
    public int darkness;
    public int height;
}
