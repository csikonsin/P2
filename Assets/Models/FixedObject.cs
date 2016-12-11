
public class Fixed {
    private string prefabName;
    public string Prefab
    {
        get { return prefabName; }
        set { prefabName = value; }
    }

    private string type;
    public string Type
    {
        get { return type; }
        set { type = value; }
    }

    private int width;

    public int Width
    {
        get { return width; }
        set { width = value; }
    }
    private int length;

    public int Length
    {
        get { return length; }
        set { length = value; }
    }
    private int height;

    public int Height
    {
        get { return height; }
        set { height = value; }
    }

    private Block[] parentBlocks;

    public Block[] ParentBlocks
    {
        get { return parentBlocks; }
        set { parentBlocks = value; }
    }




    protected Fixed()
    {

    }

    public static Fixed CreatePrototype(string type, int width, int length, int height)
    {
        Fixed fo = new Fixed();
        fo.Prefab = type;
        fo.Width = width;
        fo.length = length;
        fo.Height = height;
        return fo;
    }

    public static Fixed InstantiatePrototype(Fixed prototype, Block[] parentBlocks)
    {
        Fixed fo = new Fixed();
        fo.Prefab = prototype.Prefab;
        fo.Width = prototype.Width;
        fo.Height = prototype.Height;
        fo.Length = prototype.Length;
        fo.ParentBlocks = parentBlocks;
        return fo;
    }



}
