using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public GameObject chunkObject;
    private World world;

    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    private int vertexIndex = 0;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();

    private byte[,,] voxelMap = new byte[ChunkData.VoxelWidth, ChunkData.VoxelHeight, ChunkData.VoxelWidth];

    public Chunk(Vector2Int coord, World _world)
    {
        world = _world;
        chunkObject = new GameObject();
        meshFilter = chunkObject.AddComponent<MeshFilter>();
        meshRenderer = chunkObject.AddComponent<MeshRenderer>();

        meshRenderer.material = world.material;
        chunkObject.transform.SetParent(world.transform);
        chunkObject.transform.position = new Vector3(coord.x * ChunkData.VoxelWidth, 0f, coord.y * ChunkData.VoxelWidth);
        chunkObject.name = "Chunk" + coord.x + ", " + coord.y;

        PopulateVoxelMap();
        CreateMeshData();
        CreateMesh();
    }
    private void PopulateVoxelMap()
    {
        for (int y = 0; y < ChunkData.VoxelHeight; y++)
        {
            for (int x = 0; x < ChunkData.VoxelWidth; x++)
            {
                for (int z = 0; z < ChunkData.VoxelWidth; z++)
                {
                    voxelMap[x, y, z] = world.GetVoxel(new Vector3(x, y, z) + position);
                }
            }
        }
    }
    private void CreateMeshData()
    {
        for (int y = 0; y < ChunkData.VoxelHeight; y++)
        {
            for (int x = 0; x < ChunkData.VoxelWidth; x++)
            {
                for (int z = 0; z < ChunkData.VoxelWidth; z++)
                {
                    if (world.blockTypes[voxelMap[x, y, z]].isSolid)
                    {
                        AddVoxelDataToChunk(new Vector3(x, y, z));
                    }
                }
            }
        }
    }
    private bool CheckVoxel(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        int z = Mathf.FloorToInt(pos.z);

        if (!IsVoxelInChunk(x, y, z))
            return world.blockTypes[world.GetVoxel(pos + position)].isSolid;

        return world.blockTypes[voxelMap[x, y, z]].isSolid;
    }
    private void AddVoxelDataToChunk(Vector3 pos)
    {
        for (int i = 0; i < 5; i++)
        {
            if (!CheckVoxel(pos + VoxelData.faceChecks[i]))
            {
                for (int j = 0; j < 4; j++)
                    vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[i, j]]);

                byte blockID = voxelMap[(int)pos.x, (int)pos.y, (int)pos.z];
                AddTexture(world.blockTypes[blockID].GetTextureID(i));

                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 3);

                vertexIndex += 4;
            }
        }
    }
    private void AddTexture(int textureID)
    {
        float y = textureID / TextureData.AtlassWidth;
        float x = textureID - (y * TextureData.AtlassWidth);

        x *= TextureData.NormalizedTextureSize;
        y *= TextureData.NormalizedTextureSize;

        y = 1f - y - TextureData.NormalizedTextureSize;

        uvs.Add(new Vector2(x + TextureData.TextureOffset, y + TextureData.TextureOffset));
        uvs.Add(new Vector2(x + TextureData.TextureOffset, y + TextureData.NormalizedTextureSize - TextureData.TextureOffset));
        uvs.Add(new Vector2(x + TextureData.NormalizedTextureSize - TextureData.TextureOffset, y + TextureData.TextureOffset));
        uvs.Add(new Vector2(x + TextureData.NormalizedTextureSize - TextureData.TextureOffset, y + TextureData.NormalizedTextureSize - TextureData.TextureOffset));
    }
    private void CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

    public byte fullness { get; private set; }
    public bool isActive
    {
        get { return chunkObject.activeSelf; }
        set { chunkObject.SetActive(value); }
    }
    public Vector3 position
    {
        get { return chunkObject.transform.position; }
    }
    private bool IsVoxelInChunk(int x, int y, int z)
    {
        if (x < 0 || x >= ChunkData.VoxelWidth || y < 0 || y > ChunkData.VoxelHeight - 1 || z < 0 || z >= ChunkData.VoxelWidth)
            return false;

        return true;
    }
}