using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treestatus : MonoBehaviour
{
    
    public GrowthStatuses growth
    {
        get
        {
            return _growth;
        }
        set
        {
            _growth = value;
            visual.updateGraphics(_growth);
        }
    }
    GrowthStatuses _growth;
    public ResourcesStatuses resources
     {
        get
        {
            return _resources;
        }
        set
        {
            _resources = value;
            inventoryDisplay.updateGraphics(_resources);
        }
    }
    ResourcesStatuses _resources;
    public TreeGraphics visual;
    public TreeInventoryDisplay inventoryDisplay;
}
[System.Serializable]
public struct GrowthStatuses
{
    public rootsStatus roots;
    public trunkStatus trunk;
    public CanopyStatus canopy;

    public static GrowthStatuses operator + (GrowthStatuses a, GrowthStatuses b)
    {
        GrowthStatuses c = new GrowthStatuses();
        c.roots.width = a.roots.width + b.roots.width;
        c.roots.height = a.roots.height + b.roots.height;
        c.trunk.width = a.trunk.width + b.trunk.width;
        c.trunk.height = a.trunk.height + b.trunk.height;
        c.canopy.wideness = a.canopy.wideness + b.canopy.wideness;
        c.canopy.height = a.canopy.height + b.canopy.height;
        c.canopy.darkness = a.canopy.darkness + b.canopy.darkness;
        return c;
    }

    public static GrowthStatuses operator - (GrowthStatuses a, GrowthStatuses b)
    {
        GrowthStatuses c = new GrowthStatuses();
        c.roots.width = a.roots.width - b.roots.width;
        c.roots.height = a.roots.height - b.roots.height;
        c.trunk.width = a.trunk.width - b.trunk.width;
        c.trunk.height = a.trunk.height - b.trunk.height;
        c.canopy.wideness = a.canopy.wideness - b.canopy.wideness;
        c.canopy.height = a.canopy.height - b.canopy.height;
        c.canopy.darkness = a.canopy.darkness - b.canopy.darkness;
        return c;
    }
}
[System.Serializable]
public struct rootsStatus
{
    public int width;
    public int height;
}
[System.Serializable]
public struct trunkStatus
{
    public int height;
    public int width;

}
[System.Serializable]
public struct CanopyStatus
{
    public int wideness;
    public int darkness;
    public int height;
}
[System.Serializable]
public struct ResourcesStatuses
{
    public int water;
    public int sun;

    public static ResourcesStatuses operator + (ResourcesStatuses a, ResourcesStatuses b)
    {
        ResourcesStatuses c;
        c.water = a.water + b.water;
        c.sun = a.sun + b.sun;
        return c;
    }
    public static ResourcesStatuses operator - (ResourcesStatuses a, ResourcesStatuses b)
    {
        ResourcesStatuses c;
        c.water = a.water - b.water;
        c.sun = a.sun - b.sun;
        return c;
    }
}


