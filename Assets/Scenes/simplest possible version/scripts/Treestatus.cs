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
    }

    public void boomRoots()
    {
        status.roots.width++;
        status.roots.height++;
    }

    public void turtleRoots()
    {
        status.roots.height+=2;
    }

    public void RushTrunk()
    {
        //wow it's fucking nothing
    }

    public void boomTrunk()
    {
        status.trunk.height+=2;
    }

    public void turtleTrunk()
    {
        status.trunk.width +=2;
    }

    public void rushBranchesLeaves()
    {
        status.branchesLeaves.wideness+=2;
    }

    public void boomBranchesLeaves()
    {
        status.branchesLeaves.height+= 1;
        status.branchesLeaves.wideness+=1;
    }

    public void turtleBranchesLeaves()
    {
        status.branchesLeaves.darkness+=2;
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
