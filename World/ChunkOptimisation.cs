using System.Collections.Generic;
using UnityEngine;

public class ChunkOptimisation : MonoBehaviour // NOT REAL OPTIMISATION IN FACT, JUST TEST, REWRITE 
{
    public float step = .78f;
    public int xLevel = 12;
    public int maxZLevel = 22;

    private Transform cam;
    private Transform camRig;

    public Transform stepObj;
    private World world;

    private Vector3 lastPos;
    private List<Vector2Int> activeChunks = new List<Vector2Int>();

    private void Start()
    {
        world = GetComponent<World>();
        cam = Camera.main.transform;
        camRig = cam.parent;

        step = Mathf.Clamp(step, .4f, 1);
        step *= ChunkData.VoxelWidth;

        foreach (Chunk chunk in world.chunks)
            chunk.isActive = false;

        SwapChunks();
    }
    private void Update()
    {
        if (cam.position != lastPos || stepObj.rotation != camRig.rotation || Input.GetAxis("Mouse ScrollWheel") != 0)
            SwapChunks();
    }

    private void SwapChunks()
    {
        foreach (var chunk in activeChunks)
            world.chunks[chunk.x, chunk.y].isActive = false;
        activeChunks.Clear();

        stepObj.rotation = camRig.rotation;
        stepObj.position = cam.position;

        int xPos = xLevel;
        int zPos = 1;
        while (zPos != maxZLevel)
        {
            float pos = -((xPos - 1) * step / 2);
            for (int x = 0; x < xPos; x++)
            {
                stepObj.localPosition = new Vector3(pos, 0, stepObj.localPosition.z);
                Vector2 chunkPos = new Vector2(stepObj.position.x, stepObj.position.z);
                if (IsPosInWorld(chunkPos))
                {
                    Vector2Int roundedChunkPos = new Vector2Int(Mathf.FloorToInt(chunkPos.x) / ChunkData.VoxelWidth, Mathf.FloorToInt(chunkPos.y) / ChunkData.VoxelWidth);
                    world.chunks[roundedChunkPos.x, roundedChunkPos.y].isActive = true;
                    activeChunks.Add(roundedChunkPos);
                }

                pos += step;
            }

            stepObj.localPosition += new Vector3(0, 0, step);

            xPos++;
            zPos++;
        }
    }
    private bool IsPosInWorld(Vector2 pos)
    {
        if (pos.x < 0 || pos.x > WorldData.VoxelWidth - 1 || pos.y < 0 || pos.y > WorldData.VoxelWidth - 1)
            return false;

        return true;
    }
}