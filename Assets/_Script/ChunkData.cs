using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ChunkData 类用于存储区块（chunk）的数据，包括方块类型、尺寸、位置等信息
public class ChunkData
{
    // 存储该区块中每个位置的方块类型，大小为 chunkSize * chunkHeight * chunkSize
    public BlockType[] blocks;

    // 区块的水平大小，表示区块在 x 和 z 方向上的长度，默认为 16
    public int chunkSize = 16;

    // 区块的高度，表示区块在 y 方向上的高度，默认为 100
    public int chunkHeight = 100;

    // 当前区块所在的世界的引用，用于与世界中的其他区块或数据交互
    public World worldReference;

    // 区块在世界坐标系中的位置，使用 Vector3Int 表示三维整数坐标
    public Vector3Int worldPosition;

    // 标记该区块是否被玩家修改过，初始值为 false
    public bool modifiedByThePlayer = false;

    // 构造函数，用于初始化 ChunkData 类实例
    public ChunkData(int chunkSize, int chunkHeight, World world, Vector3Int worldPosition)
    {
        // 使用传入的 chunkHeight 值来设置区块高度
        this.chunkHeight = chunkHeight;

        // 使用传入的 chunkSize 值来设置区块的水平大小
        this.chunkSize = chunkSize;

        // 设置当前区块所在的世界引用
        this.worldReference = world;

        // 设置当前区块在世界中的位置
        this.worldPosition = worldPosition;

        // 初始化 blocks 数组，大小为 chunkSize * chunkHeight * chunkSize，
        // 用于存储区块内每个方块的位置和类型
        blocks = new BlockType[chunkSize * chunkHeight * chunkSize];
    }
}
