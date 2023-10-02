
public static class WorldData
{
    public static readonly int ChunkWidth = 32; // MUST BE A CONST AFTER GENERATING A NEW WORLD // 64
    public static int ChunksAmount 
    { 
        get { return ChunkWidth * ChunkWidth; }
    }

    public static int VoxelWidth
    {
        get { return ChunkData.VoxelWidth * ChunkWidth; }
    } // MUST BE A CONST SETTING BY GAME MODE AND MAP SETTINGS
    public static int VoxelArea
    {
        get { return VoxelWidth * VoxelWidth; }
    }
    public static int VoxelAmount
    {
        get { return ChunksAmount * ChunkData.VoxelAmount; }
    }
    // size in chunks
    // Center In Voxels
}

public static class ChunkData
{
    public static readonly int VoxelWidth = 8;
    public static readonly int VoxelHeight = 128;
    
    public static int VoxelArea
    {
        get { return VoxelWidth * VoxelWidth; }
    }
    public static int VoxelAmount
    {
        get { return VoxelArea * VoxelHeight; }
    }
    public static int VoxelCenter
    {
        get { return VoxelWidth / 2; }
    }
}