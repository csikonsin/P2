using UnityEngine;
using System.Collections;
using System;

public class World  {

    int width;
    int height;
    int length;

    Block[,,] blocks;

    public int Width { get { return width; } }
    public int Length { get { return length; } }

    public int Height { get { return height; } }

    public World(int width =100,int length =100,int height=100)
    {
        this.width = width;
        this.height = height;
        this.length = length;
        blocks = new Block[this.width,this.length, this.height];
        GenerateWorld();
    }

    internal Block GetBlockAt(int x, int y, int z)
    {
        return blocks[x, y, z];
    }

    internal bool PlaceBlockAt(Block block,int x,int y, int z)
    {
        if (GetBlockAt(x, y, z) != null)
        {
            return false;
        }
        blocks[x, y, z] = block;
        return true;
    }


    void GenerateWorld()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                int y = 0;
                Block b= new Block(x, y, z);

                b.Type = "solid_grass";

                blocks[x, y, z] = b;
            }
        }
    }


}
