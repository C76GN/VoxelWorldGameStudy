using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MeshData 类用于存储网格顶点、三角形、UV 贴图坐标以及碰撞体数据
public class MeshData
{
    // 用于存储网格的顶点，每个 Vector3 表示一个顶点的位置（x, y, z）
    public List<Vector3> vertices = new List<Vector3>();

    // 用于存储网格的三角形面片，每三个整数表示一个三角形的三个顶点索引
    public List<int> triangles = new List<int>();

    // 用于存储 UV 贴图坐标（2D），每个 Vector2 表示一个顶点对应的纹理坐标
    public List<Vector2> uv = new List<Vector2>();

    // 用于存储碰撞体的顶点数据，只有参与碰撞检测的顶点会存储在这里
    public List<Vector3> colliderVertices = new List<Vector3>();

    // 用于存储碰撞体的三角形面片，每三个整数表示一个三角形的顶点索引
    public List<int> colliderTriangles = new List<int>();

    // 水网格（waterMesh）是一个附属网格，专门用于存储和渲染水面数据
    public MeshData waterMesh;

    // 用于标识该网格是否为主网格，默认值为 true，表示主网格
    private bool isMainMesh = true;

    // 构造函数，接收一个布尔值 isMainMesh 以确定该实例是否为主网格
    public MeshData(bool isMainMesh)
    {
        // 如果是主网格，则初始化 waterMesh，用于存储水面的网格数据
        if (isMainMesh)
        {
            waterMesh = new MeshData(false);  // 传递 false，以防止递归生成 waterMesh
        }
    }

    // 添加一个顶点到网格数据的方法，参数 vertex 表示顶点坐标，
    // vertexGeneratesCollider 表示该顶点是否用于碰撞检测
    public void AddVertex(Vector3 vertex, bool vertexGeneratesCollider)
    {
        vertices.Add(vertex); // 将顶点添加到主网格的顶点列表

        // 如果 vertexGeneratesCollider 为 true，则将顶点同时添加到碰撞体的顶点列表
        if (vertexGeneratesCollider)
        {
            colliderVertices.Add(vertex);
        }
    }

    // 添加一个四边形的两个三角形到网格数据的方法，quadGeneratesCollider 表示该四边形是否用于碰撞体
    public void AddQuadTriangles(bool quadGeneratesCollider)
    {
        // 第一个三角形：使用最近添加的四个顶点中的前三个
        triangles.Add(vertices.Count - 4); // 第一个顶点索引
        triangles.Add(vertices.Count - 3); // 第二个顶点索引
        triangles.Add(vertices.Count - 2); // 第三个顶点索引

        // 第二个三角形：使用最近添加的四个顶点中的第一个、第三个和第四个
        triangles.Add(vertices.Count - 4); // 第一个顶点索引
        triangles.Add(vertices.Count - 2); // 第三个顶点索引
        triangles.Add(vertices.Count - 1); // 第四个顶点索引

        // 如果 quadGeneratesCollider 为 true，则将四边形也添加到碰撞体的三角形列表中
        if (quadGeneratesCollider)
        {
            // 第一个三角形：碰撞体的前三个顶点
            colliderTriangles.Add(colliderVertices.Count - 4); // 第一个顶点索引
            colliderTriangles.Add(colliderVertices.Count - 3); // 第二个顶点索引
            colliderTriangles.Add(colliderVertices.Count - 2); // 第三个顶点索引

            // 第二个三角形：碰撞体的第一个、第三个和第四个顶点
            colliderTriangles.Add(colliderVertices.Count - 4); // 第一个顶点索引
            colliderTriangles.Add(colliderVertices.Count - 2); // 第三个顶点索引
            colliderTriangles.Add(colliderVertices.Count - 1); // 第四个顶点索引
        }
    }
}
