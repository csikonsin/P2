using UnityEngine;
using System.Collections;
using System;

public class VegetationObject : Fixed{

    // Use this for initialization
    private int spawnChance;

    public int SpawnChance
    {
        get { return spawnChance; }
        set { spawnChance = value; }
    }

    public static new   VegetationObject  CreatePrototype(string type, int width, int length, int height,int spawnChance)
    {
        VegetationObject v = new VegetationObject();
        v.Prefab = type;
        v.Width = width;
        v.Length = length;
        v.Height = height;
        v.spawnChance = spawnChance;
        return v;
    }

    public static new  VegetationObject InstantiatePrototype(VegetationObject prototype, Block[] parentBlocks)
    {
        VegetationObject v = new VegetationObject();
        v.Prefab = prototype.Prefab;
        v.Width = prototype.Width;
        v.Height = prototype.Height;
        v.Length = prototype.Length;
        v.spawnChance = prototype.spawnChance;
        v.ParentBlocks = parentBlocks;
        return v;
    }



}
