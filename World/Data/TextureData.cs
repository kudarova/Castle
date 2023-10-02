
public static class TextureData // Managing voxel textures
{
    public static readonly int PixelsInTexture = 32; // must be power of two
    public static readonly int AtlassWidth = 2; // must be power of two

    public static float NormalizedTextureSize
    {
        get { return 1f / AtlassWidth; }
    }
    public static float TextureOffset
    {
        get { return 1f / PixelsInTexture / AtlassWidth; }
    }
}