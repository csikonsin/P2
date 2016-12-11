using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;

public class WorldController : MonoBehaviour {

    public GameObject FixedObjectsContainer;
    public GameObject LooseObjectsContainer;
    public GameObject TerrainContainer;
    public GameObject UIContainer;


    // Use this for initialization
    public static WorldController Instance { get; protected set; }

    World world;


    Dictionary<Block, GameObject> blockGameObjectMap;



    Dictionary<string, Fixed> fixedObjectPrototypes;

    Dictionary<string, GameObject> prefabMap;


    void Start () {

        if (Instance != null)
        {
            Debug.Log("There should only be one worldcontroller.");
            return;
        }
        Instance = this;

        loadFixedObjectPrototypes();


        world = new World(100,100);
        blockGameObjectMap = new Dictionary<Block, GameObject>();

        LoadPrefabs();
        

        for (int x = 0; x < world.Width; x++)
        {
            for (int z = 0; z < world.Length; z++)
            {
                int y = 0;
                Block block_data = world.GetBlockAt(x, y, z);
                GameObject block_go = Instantiate(prefabMap[block_data.Type]);
                block_go.name = "Block_" + x + "_" + y + "_" + z;
                block_go.transform.position = transform.TransformPoint(new Vector3(block_data.X, block_data.Y, block_data.Z));
                block_go.transform.SetParent(TerrainContainer.transform, true);
                

               

                blockGameObjectMap.Add(block_data, block_go);

            }
        }
    }

   
    // Update is called once per frame
    void Update () {
    
    }

 
    private void loadFixedObjectPrototypes()
    {
        fixedObjectPrototypes = new Dictionary<string, Fixed>();

        fixedObjectPrototypes.Add("wall", Fixed.CreatePrototype("wall", 1, 1, 2));
    }


    public void BuildBlock(RaycastHit hit,string type)
    {
        int hitX = Mathf.FloorToInt(hit.transform.position.x);
        int hitY = Mathf.FloorToInt(hit.transform.position.y);
        int hitZ = Mathf.FloorToInt(hit.transform.position.z);

        Block block_data = world.GetBlockAt(hitX,hitY, hitZ);
        GameObject block_go = blockGameObjectMap[block_data];
        if(type=="bulldoze")
        {
            blockGameObjectMap.Remove(block_data);
            Destroy(block_go);
            block_go = null;
            return;
        }

              
        Block block_origin = world.GetBlockAt(hitX, hitY+1, hitZ);
        if(block_origin!=null)
        {
            Debug.Log("Can't build object on top of selected block ");
            return;
        }

        if (fixedObjectPrototypes.ContainsKey(type))
        {
            Fixed fixedObjectPrototype = fixedObjectPrototypes[type];
            int inx = 0;
            Block[] parentBlocks = new Block[fixedObjectPrototype.Width*fixedObjectPrototype.Height*fixedObjectPrototype.Length];
            for (int y = hitY+1; y < hitY+1 +fixedObjectPrototype.Height; y++)
            {
                for (int x = hitX; x < hitX+fixedObjectPrototype.Width; x++)
                {
                    for (int z = hitZ; z < hitZ+fixedObjectPrototype.Length; z++)
                    {
                        if(world.GetBlockAt(x,y,z)==null)
                        {
                            Block fixedParent = new Block(x, y, z);
                            if(world.PlaceBlockAt(fixedParent,x,y,z))
                            {
                                parentBlocks[inx] = fixedParent;
                                inx++;
                            }
                           
                        }
                        
                    }
                }
            }
            //Check if not all blocks are ok
            if(parentBlocks[parentBlocks.Length-1]==null)
            {
                Debug.Log("Not enough space for fixed objet");
                return;
            }
            

            Fixed fixedObjectInstance = Fixed.InstantiatePrototype(fixedObjectPrototypes[type],parentBlocks);

            GameObject block_above_go = Instantiate(prefabMap[type]);
            block_above_go.name = type.ToCharArray()[0].ToString().ToUpper() + type.Substring(1) + "_" + hitX + "_" + hitY+1 + "_" + hitZ;
            block_above_go.transform.position = transform.TransformPoint(new Vector3(hitX, hitY+1, hitZ));
            block_above_go.transform.SetParent(FixedObjectsContainer.transform, true);
            blockGameObjectMap.Add(world.GetBlockAt(hitX,hitY+1,hitZ), block_above_go);


        }






    }


    void LoadPrefabs()
    {
        prefabMap = new Dictionary<string, GameObject>();
        GameObject[] gos = Resources.LoadAll<GameObject>("Prefabs");
        foreach (GameObject go in gos)
        {
            prefabMap.Add(go.name, go);
        }

        if (prefabMap.Count == 0)
        {
            Debug.Log("WorldController::LoadPrefabs - No Prefab loaded");
        }
    }

    /// <summary>
    /// Display a transparent ghost image that snaps to the grid;
    /// </summary>
    /// <param name="type"></param>
    public GameObject SetGhostImage(string type)
    {
        if (type == "bulldoze") return null;
        GameObject ghost =  Instantiate(prefabMap[type]);
        Color c = ghost.GetComponent<Renderer>().material.color;
        Destroy(ghost.GetComponent<BoxCollider>());
        c.a = 0.5f;
        ghost.GetComponent<Renderer>().material.color = c;
        

        ghost.transform.SetParent(UIContainer.transform);
        ghost.name = "BuildGhost_" + type;
        ghost.SetActive(false);
        return ghost;
    }
    public void UpdateGhostImage(RaycastHit hit, GameObject goGhostImage)
    {
        if (goGhostImage == null) return;
        int hitX = Mathf.FloorToInt(hit.transform.position.x);
        int hitY = Mathf.FloorToInt(hit.transform.position.y);
        int hitZ = Mathf.FloorToInt(hit.transform.position.z);
        Block block_hit = world.GetBlockAt(hitX, hitY, hitZ);

        Block block_hit_above = world.GetBlockAt(hitX, hitY+1, hitZ);
        if (block_hit_above == null)
        {
            if(!goGhostImage.activeSelf) goGhostImage.SetActive(true);
            Vector3 ghostImagePosition = new Vector3(hitX,hitY+1,hitZ);
            goGhostImage.transform.position = ghostImagePosition;
        }

    }
}

