class Structure
{
    // make structure from start point
    // try make it with scrtiptable objects

    public readonly int VoxelWidth = 16; // for ex
    public float ChunkWidth
    {
        get { return (float)VoxelWidth / ChunkData.VoxelWidth; }
    }
    public readonly int requiredFullness = 30; // in percents

    public Structure() // like a BuildData() method
    {
        // random width
        // required fullness
    }
}