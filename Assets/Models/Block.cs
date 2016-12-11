using UnityEngine;
using System.Collections;

public class Block  {


    int x;
    public int X { get { return x; } }

    int y;
    public int Y { get { return y; } }
    int z;
    public int Z { get { return z; } }

    string type;
    public string Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }


    Fixed fixedObject;
    public Fixed FixedObject
    {
        get { return fixedObject; }
        set { fixedObject = value; }
    }

    VegetationObject vegetation;

    public VegetationObject Vegetation
    {
        get { return vegetation; }
        set { vegetation = value; }
    }



    public Block(int x,int y,int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }



}

