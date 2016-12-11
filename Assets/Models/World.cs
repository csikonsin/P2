using UnityEngine;
using System.Collections;
using System;
using System.Linq;

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

        GenerateVegetation();
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

    internal bool DeleteBlockAt(Block block,int x,int y ,int z)
    {
        if (GetBlockAt(x, y, z) == null)
        {
            return false;
        }
        blocks[x, y, z] = null;
        return true;
    }

    void GenerateWorld()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                for(int y=0;y<height;y++)
                {
                    Block b = new Block(x, y, z);
                    blocks[x, y, z] = b;
                }
               
            }
        }
    }

    void GenerateVegetation()
    {
        for(int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {

                for(int y = 0; y < height; y++)
                {
                    Block b = GetBlockAt(x, y, z);
                    Block[] parentBlocks;
                    if (y == 0)
                    {
                        //Spawn solids

                        parentBlocks = new Block[] { b };
                        Fixed f = Fixed.InstantiatePrototype(WorldController.Instance.FixedObjectPrototypes["solid_grass"], parentBlocks);
                        b.FixedObject = f;
                    }
                    else
                    {
                        Block block_below = GetBlockAt(x, y - 1, z);
                        if (block_below == null || block_below.FixedObject == null) continue;


                        int rnd = UnityEngine.Random.Range(0, 100);

                        int rndVeg = UnityEngine.Random.Range(0, WorldController.Instance.VegetationPrototypes.Count);
                        
                        VegetationObject prototype = WorldController.Instance.VegetationPrototypes.ElementAt(rndVeg).Value;
                        if (prototype == null) continue;

                        parentBlocks = new Block[prototype.Width * prototype.Height * prototype.Length];
                        parentBlocks[0] = b;


                        if (rnd > (100-prototype.SpawnChance))
                        {
                            VegetationObject  v = VegetationObject.InstantiatePrototype(prototype, parentBlocks);
                            b.Vegetation = v;
                        }
                            


                    }

                }

               
            }
        }
    }



}
