using UnityEngine;

class Map // MAP GETS INFO FROM GAME MODE
{
    // ANOTHER MORE OBJECTIVE SCRIPT FOR THIS
    public int seed = 13509905;
    public float offset = .007f; // WE MUST TAKE IT FROM DATA SCRIPT FOR GENERATING WORLD, SUCH AS WorldData SCRIPT OR OTHER

    private byte[,,] voxelMap = new byte[WorldData.VoxelWidth, ChunkData.VoxelHeight, WorldData.VoxelWidth];

    public Map(World world) // OK
    {
        seed = Random.Range(-99999, 99999);

        for (int y = 0; y < ChunkData.VoxelHeight; y++)
        {
            for (int x = 0; x < WorldData.VoxelWidth; x++)
            {
                for (int z = 0; z < WorldData.VoxelWidth; z++)
                {
                    voxelMap[x, y, z] = world.GetVoxel(new Vector3(x, y, z));
                }
            }
        }
    }
}