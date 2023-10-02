using UnityEngine;

public static class VoxelData
{
    public static readonly Vector3[] voxelVerts = new Vector3[8] 
    {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 1.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 1.0f)
    };

    public static readonly int[,] voxelTris = new int[6, 4] 
    {
        {0, 3, 1, 2}, //back face
        {5, 6, 4, 7}, //front face
        {4, 7, 0, 3}, //left face
        {1, 2, 5, 6}, //right face
        {3, 7, 2, 6}, //top face
        {1, 5, 0, 4} //bottom face
    };

    public static readonly Vector3Int[] faceChecks = new Vector3Int[6] 
    {
        new Vector3Int(0, 0, -1),
        new Vector3Int(0, 0, 1),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0)
    };
}