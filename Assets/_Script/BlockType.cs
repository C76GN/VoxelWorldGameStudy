using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BlockType 枚举表示游戏中不同类型的方块
public enum BlockType
{
    // 无方块，表示空白或不可见的区域
    Nothing,

    // 空气方块，通常用于玩家可进入的区域，不会被渲染
    Air,

    // 草地方块，顶部覆盖有草，底部是泥土
    Grass_Dirt,

    // 泥土方块，通常用于地面或地下层
    Dirt,

    // 石头覆盖的草地方块，顶部是草，底部是石头
    Grass_Stone,

    // 石头方块，通常用于山脉或地下，硬度较大
    Stone,

    // 树干方块，表示树木的主干部分
    TreeTrunk,

    // 透明树叶方块，表示树木的树叶部分，可以透过部分光线
    TreeLeafesTransparent,

    // 实心树叶方块，表示树木的树叶部分，不透光
    TreeLeafsSolid,

    // 水方块，通常用于水体，可以设置为半透明
    Water,

    // 沙子方块，通常用于海滩或沙漠区域
    Sand
}
