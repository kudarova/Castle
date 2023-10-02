using UnityEngine;

class T_FloatingIsland // : Template || : ITemplate
{
    // принимает массив вокселей - полностью мир без применения шаблона
    // отдает мир с примененным шаблоном - делать это нужно до генерации структур.

    private int ellipsoidCenterW 
    { 
        get { return WorldData.VoxelWidth / 2; } 
    }
    private float ellipsoidCenterH 
    { 
        get { return ChunkData.VoxelHeight / 2; } 
    }
    private Vector3 ellipsoidCenter;
    private Vector3 ellipsoidSize;

    private float radius;
    private float ring;

    private void GetIslandInfo() // TEMPLATE
    {
        ellipsoidCenter = new Vector3(ellipsoidCenterW, ellipsoidCenterH, ellipsoidCenterW);
        ellipsoidSize = new Vector3(WorldData.VoxelWidth, ChunkData.VoxelHeight, WorldData.VoxelWidth);

        radius = Mathf.Pow(ellipsoidCenterW, 2);
        ring = radius - WorldData.VoxelWidth - ellipsoidCenterW;
    }

    private void GetVoxel()
    {
        /*
        //circle island TEMPLATE
        float circle = Mathf.Pow(pos.x - ellipsoidCenterW, 2) + Mathf.Pow(pos.z - ellipsoidCenterW, 2);
        if (circle > radius)
            return 0;

        float sphere = Mathf.Pow(pos.x - ellipsoidCenter.x, 2) + Mathf.Pow(pos.y - ellipsoidCenter.y + terrainHeight / 2 - 5, 2) + Mathf.Pow(pos.z - ellipsoidCenter.z, 2);
        if (sphere > radius && yPos > terrainHeight)
            return 0;

        // side stone
        if (yPos == terrainHeight && circle >= ring)
            return 3;

        // dirt
        float ellipsoid = Mathf.Pow(pos.x - ellipsoidCenter.x, 2) / Mathf.Pow(ellipsoidSize.x, 2) + Mathf.Pow(pos.y - ellipsoidCenter.y, 2) / Mathf.Pow(ellipsoidSize.y, 2) + Mathf.Pow(pos.z - ellipsoidCenter.z, 2) / Mathf.Pow(ellipsoidSize.z, 2);
        if (ellipsoid <= .24f && yPos < terrainHeight)
            return 2;

        // stalactites
        if (yPos < terrainHeight && yPos >= rockHeight - 4)
            return 3;
        */
    }
}