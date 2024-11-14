using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// 确保在附加 ChunkRenderer 时，游戏对象上包含 MeshFilter、MeshRenderer 和 MeshCollider 组件
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ChunkRenderer : MonoBehaviour
{
    // 用于处理网格的组件
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    Mesh mesh;

    // 控制是否在编辑器中显示区块的辅助线框（Gizmo）
    public bool showGizmo = false;

    // 当前区块的数据，包含方块类型、尺寸和位置等信息
    public ChunkData ChunkData { get; private set; }

    // 属性，用于访问和修改区块是否被玩家修改过的状态
    public bool ModifiedByThePlayer
    {
        get
        {
            return ChunkData.modifiedByThePlayer;
        }
        set
        {
            ChunkData.modifiedByThePlayer = value;
        }
    }

    // Awake 是 Unity 生命周期中的方法之一，在对象激活时调用
    private void Awake()
    {
        // 获取并存储游戏对象的 MeshFilter、MeshCollider 组件的引用
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        // 获取 MeshFilter 的网格对象
        mesh = meshFilter.mesh;
    }

    // 初始化区块的数据，将传入的 ChunkData 对象赋值给当前实例的 ChunkData 字段
    public void InitializeChunk(ChunkData data)
    {
        this.ChunkData = data;
    }

    // 渲染网格数据的方法，根据 MeshData 中的数据生成和设置网格
    private void RenderMesh(MeshData meshData)
    {
        mesh.Clear();  // 清除当前网格数据，为新的顶点和三角形数据做准备

        mesh.subMeshCount = 2;  // 设置子网格数量为 2，一个用于地形，一个用于水面
        // 合并主网格和水网格的顶点，并设置到 mesh 中
        mesh.vertices = meshData.vertices.Concat(meshData.waterMesh.vertices).ToArray();

        // 为子网格 0 设置三角形，使用主网格的三角形数据
        mesh.SetTriangles(meshData.triangles.ToArray(), 0);
        // 为子网格 1 设置三角形，将水网格的三角形顶点索引偏移主网格顶点数量
        mesh.SetTriangles(meshData.waterMesh.triangles.Select(val => val + meshData.vertices.Count).ToArray(), 1);

        // 合并主网格和水网格的 UV 坐标，并设置到 mesh 中
        mesh.uv = meshData.uv.Concat(meshData.waterMesh.uv).ToArray();
        mesh.RecalculateNormals();  // 重新计算法线，以确保光照效果正确

        // 碰撞网格设置：清除当前碰撞网格
        meshCollider.sharedMesh = null;
        // 创建新的碰撞网格
        Mesh collisionMesh = new Mesh();
        // 将碰撞体的顶点和三角形数据设置到碰撞网格中
        collisionMesh.vertices = meshData.colliderVertices.ToArray();
        collisionMesh.triangles = meshData.colliderTriangles.ToArray();
        collisionMesh.RecalculateNormals();  // 重新计算碰撞网格的法线

        // 将新的碰撞网格赋值给 meshCollider，以提供碰撞检测
        meshCollider.sharedMesh = collisionMesh;
    }

    // 更新区块，使用当前 ChunkData 中的数据生成新的 MeshData 并渲染网格
    public void UpdateChunk()
    {
        RenderMesh(Chunk.GetChunkMeshData(ChunkData));
    }

    // 更新区块，使用传入的 MeshData 对象直接渲染网格
    public void UpdateChunk(MeshData data)
    {
        RenderMesh(data);
    }

    // Unity 编辑器代码，仅在编辑器中编译，使用 Gizmo 辅助显示区块
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // 如果启用了 showGizmo，则在编辑器中显示区块辅助线框
        if (showGizmo)
        {
            // 在游戏运行时并且 ChunkData 不为空时才显示 Gizmo
            if (Application.isPlaying && ChunkData != null)
            {
                // 设置 Gizmo 的颜色，绿色表示选中，紫色表示未选中
                if (Selection.activeObject == gameObject)
                    Gizmos.color = new Color(0, 1, 0, 0.4f);  // 选中时为半透明绿色
                else
                    Gizmos.color = new Color(1, 0, 1, 0.4f);  // 未选中时为半透明紫色

                // 绘制一个线框立方体，用于表示区块的边界
                Gizmos.DrawCube(
                    transform.position + new Vector3(ChunkData.chunkSize / 2f, ChunkData.chunkHeight / 2f, ChunkData.chunkSize / 2f),
                    new Vector3(ChunkData.chunkSize, ChunkData.chunkHeight, ChunkData.chunkSize)
                );
            }
        }
    }
#endif
}
