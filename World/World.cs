using UnityEngine;

public class World : MonoBehaviour 
{
    public Material material; // must be private!
    public BlockType[] blockTypes; // must be private!
    public Chunk[,] chunks = new Chunk[WorldData.ChunkWidth, WorldData.ChunkWidth];

    // ANOTHER MORE OBJECTIVE SCRIPT FOR THIS
    public int seed;
    public float offset = .007f;
    
    private void Start() => GenerateWorld();
    private void GenerateWorld() // OK
    {
        seed = Random.Range(-99999, 99999);
        for (int x = 0; x < WorldData.ChunkWidth; x++)
        {
            for (int z = 0; z < WorldData.ChunkWidth; z++)
            {
                CreateNewChunk(x, z);
            }
        }
    }
    private void CreateNewChunk(int x, int z) // OK
    {
        chunks[x, z] = new Chunk(new Vector2Int(x, z), this);
    }

    public byte GetVoxel(Vector3 pos) // BUILD WORLD ONLY BY TEMPLATE
    {
        int yPos = Mathf.FloorToInt(pos.y);

        if (!IsVoxelInWorld(pos))
            return 0;

        // noises MB ANOTHER METHOD OR SCRIPT + Why there are so many heights?
        Vector2 position = new Vector2(pos.x, pos.z);
        int terrainH = Mathf.FloorToInt(ChunkData.VoxelHeight * Noise.Get2DPerlin(position, seed, offset)); // grass
        int plateauH = Mathf.FloorToInt(ChunkData.VoxelHeight * Noise.Get2DPerlin(position, seed, .28f)); // plateaus
        int mountainH = Mathf.FloorToInt(ChunkData.VoxelHeight * Noise.Get2DPerlin(position, seed, .4f)); // mountains
        int rockH = Mathf.FloorToInt(ChunkData.VoxelHeight * Noise.Get2DPerlin(position, seed, .9f)); // rocks

        // badrock How to make it usefull?
        // if (yPos == 0)
        //    return 3;

        // stone under the grass
        if (yPos < terrainH)
            return 3;

        // rocks
        // if (yPos >= terrainH && yPos < mountainH - 6)
        //   return 3;

        int firstPlateauLevel = terrainH + 6;

        // plateaus
        if (yPos >= terrainH && yPos < plateauH)
            if (yPos < firstPlateauLevel)
                return 3;

        // grass
        if (yPos == terrainH)
            return 1;

        if (yPos > terrainH && yPos <= plateauH && yPos == firstPlateauLevel)
            return 1;

        // air
        return 0;
    }

    // SOME CHECKS, OK
    private bool IsChunkInWorld(Vector2Int coord)
    {
        if (coord.x < 0 || coord.x > WorldData.ChunkWidth || coord.y < 0 || coord.y > WorldData.ChunkWidth)
            return false;

        return true;
    }
    private bool IsVoxelInWorld(Vector3 pos)
    {
        if (pos.x < 0 || pos.x >= WorldData.VoxelWidth || pos.y < 0 || pos.y > ChunkData.VoxelHeight || pos.z < 0 || pos.z >= WorldData.VoxelWidth)
            return false;

        return true;
    }
}

[System.Serializable]
public class BlockType // SHADERS
{
    public string blockName;
    public bool isSolid;

    [Header("Texture Values")]
    public int backFaceTexture;
    public int frontFaceTexture;
    public int leftFaceTexture;
    public int rightFaceTexture;
    public int topFaceTexture;
    public int bottomFaceTexture;

    public int GetTextureID(int faceIndex)
    {
        switch (faceIndex)
        {
            case 0:
                return backFaceTexture;
            case 1:
                return frontFaceTexture;
            case 2:
                return leftFaceTexture;
            case 3:
                return rightFaceTexture;
            case 4:
                return topFaceTexture;
            case 5:
                return bottomFaceTexture;
            default:
                Debug.LogError("Error in GetTextureID: invalid face index.");
                return 0;
        }
    }
}