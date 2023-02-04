using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treestatus : MonoBehaviour
{
    public GrowthStatuses startinggrowth;
    public ResourcesStatuses startingresources;
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
    public float rootGatherMultiplier = 1f;
    public float canopyGatherMultiplier = 1f;
    public float trunkStorageMultiplier = 1f;

    public Treestatus opponent;
    public float enemytrunkheightstrength = 1;
    public float enemyoutshadestrength = 1;
    public float totalenemysabotagestrength = 1;
    void Start()
    {
        growth = startinggrowth;
        resources = startingresources;
    }
    void Update()
    {
        
        //perform resource aquisition based on area of leaves and branches
        //store as much resources as possible in the trunk

        {
            //calculate area of all tree elements
            float rootsArea = _growth.roots.width * _growth.roots.height;
            float canopyArea = _growth.canopy.wideness * _growth.canopy.height;
            float trunkArea = _growth.trunk.width * _growth.trunk.height;

            //calculate gather and storage rates from area
            float trunkTotalSunStorage = trunkArea * trunkStorageMultiplier; //replace with a function if the multiplier is ever not constant
            float trunkTotalWaterStorage = trunkTotalSunStorage; //replace with a function if these are ever different
            float rootTotalGatherRate = rootsArea * rootGatherMultiplier;//same
            float canopyTotalGatherRate = canopyArea * canopyGatherMultiplier; //same
            
            //cut gather rate by opponent traits if there is a valid opponent
            cutSunGathering(opponent, canopyTotalGatherRate);
            //gather. clamp by limit
            ResourcesStatuses updatedResources = new ResourcesStatuses();
            updatedResources.sun = Mathf.Clamp(resources.sun + canopyTotalGatherRate, 0, trunkTotalSunStorage);
            updatedResources.water = Mathf.Clamp(resources.water + rootTotalGatherRate, 0, trunkTotalWaterStorage);
            resources = updatedResources;

        }
    }

    void cutSunGathering(Treestatus opponent, float canopyrate)
    {
        if (opponent != null)
        {
            canopyrate = canopyrate/(((opponent.growth.trunk.height*enemytrunkheightstrength) * (opponent.growth.canopy.wideness*enemyoutshadestrength)))*totalenemysabotagestrength;
        }
    }

    
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

    public static bool checkZero(GrowthStatuses g)
    {
        if (rootsStatus.checkZero(g.roots)|| trunkStatus.checkZero(g.trunk) || CanopyStatus.checkZero(g.canopy))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
[System.Serializable]
public struct rootsStatus
{
    public int width;
    public int height;

    public static bool checkZero(rootsStatus r)
    {
        if (r.width <= 0|| r.height <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
[System.Serializable]
public struct trunkStatus
{
    public int height;
    public int width;
    public static bool checkZero(trunkStatus t)
    {
        if (t.width <= 0|| t.height <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
[System.Serializable]
public struct CanopyStatus
{
    public int wideness;
    public int darkness;
    public int height;

    public static bool checkZero(CanopyStatus c)
    {
        if (c.wideness <= 0|| c.height <= 0 || c.darkness <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
[System.Serializable]
public struct ResourcesStatuses
{
    public float water;
    public float sun;

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

    public static bool checkZero(ResourcesStatuses r)
    {
        if (r.sun <= 0|| r.water <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


