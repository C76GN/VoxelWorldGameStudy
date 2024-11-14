using System;
using UnityEngine;

public static class Chunk
{
    public static void LoopThroughTheBlocks(ChunkData chunkData, Action<int, int, int> actionToPerform)
    {
        for(int index = 0; index < chunkData.blocks.Length; index++)
        {
            var position = GetPostitionFromIndex(chunkData, index);
            actionToPerform(position.x, position.y, position.z);
        }
    }

    private static Vector3Int GetPostitionFromIndex(ChunkData chunkData, int index)
    {
        int x = index % chunkData.chunkSize;
        int y=(index/chunkData.chunkSize)%chunkData.chunkHeight;
        int z=index/(chunkData.chunkSize*chunkData.chunkHeight);
        return new Vector3Int(x, y, z);
    }

    public static MeshData GetChunkMeshData(ChunkData chunkData)
    {
        MeshData meshData = new MeshData(true);

        return meshData;
    }
}