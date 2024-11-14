using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MeshData �����ڴ洢���񶥵㡢�����Ρ�UV ��ͼ�����Լ���ײ������
public class MeshData
{
    // ���ڴ洢����Ķ��㣬ÿ�� Vector3 ��ʾһ�������λ�ã�x, y, z��
    public List<Vector3> vertices = new List<Vector3>();

    // ���ڴ洢�������������Ƭ��ÿ����������ʾһ�������ε�������������
    public List<int> triangles = new List<int>();

    // ���ڴ洢 UV ��ͼ���꣨2D����ÿ�� Vector2 ��ʾһ�������Ӧ����������
    public List<Vector2> uv = new List<Vector2>();

    // ���ڴ洢��ײ��Ķ������ݣ�ֻ�в�����ײ���Ķ����洢������
    public List<Vector3> colliderVertices = new List<Vector3>();

    // ���ڴ洢��ײ�����������Ƭ��ÿ����������ʾһ�������εĶ�������
    public List<int> colliderTriangles = new List<int>();

    // ˮ����waterMesh����һ����������ר�����ڴ洢����Ⱦˮ������
    public MeshData waterMesh;

    // ���ڱ�ʶ�������Ƿ�Ϊ������Ĭ��ֵΪ true����ʾ������
    private bool isMainMesh = true;

    // ���캯��������һ������ֵ isMainMesh ��ȷ����ʵ���Ƿ�Ϊ������
    public MeshData(bool isMainMesh)
    {
        // ��������������ʼ�� waterMesh�����ڴ洢ˮ�����������
        if (isMainMesh)
        {
            waterMesh = new MeshData(false);  // ���� false���Է�ֹ�ݹ����� waterMesh
        }
    }

    // ���һ�����㵽�������ݵķ��������� vertex ��ʾ�������꣬
    // vertexGeneratesCollider ��ʾ�ö����Ƿ�������ײ���
    public void AddVertex(Vector3 vertex, bool vertexGeneratesCollider)
    {
        vertices.Add(vertex); // ��������ӵ�������Ķ����б�

        // ��� vertexGeneratesCollider Ϊ true���򽫶���ͬʱ��ӵ���ײ��Ķ����б�
        if (vertexGeneratesCollider)
        {
            colliderVertices.Add(vertex);
        }
    }

    // ���һ���ı��ε����������ε��������ݵķ�����quadGeneratesCollider ��ʾ���ı����Ƿ�������ײ��
    public void AddQuadTriangles(bool quadGeneratesCollider)
    {
        // ��һ�������Σ�ʹ�������ӵ��ĸ������е�ǰ����
        triangles.Add(vertices.Count - 4); // ��һ����������
        triangles.Add(vertices.Count - 3); // �ڶ�����������
        triangles.Add(vertices.Count - 2); // ��������������

        // �ڶ��������Σ�ʹ�������ӵ��ĸ������еĵ�һ�����������͵��ĸ�
        triangles.Add(vertices.Count - 4); // ��һ����������
        triangles.Add(vertices.Count - 2); // ��������������
        triangles.Add(vertices.Count - 1); // ���ĸ���������

        // ��� quadGeneratesCollider Ϊ true�����ı���Ҳ��ӵ���ײ����������б���
        if (quadGeneratesCollider)
        {
            // ��һ�������Σ���ײ���ǰ��������
            colliderTriangles.Add(colliderVertices.Count - 4); // ��һ����������
            colliderTriangles.Add(colliderVertices.Count - 3); // �ڶ�����������
            colliderTriangles.Add(colliderVertices.Count - 2); // ��������������

            // �ڶ��������Σ���ײ��ĵ�һ�����������͵��ĸ�����
            colliderTriangles.Add(colliderVertices.Count - 4); // ��һ����������
            colliderTriangles.Add(colliderVertices.Count - 2); // ��������������
            colliderTriangles.Add(colliderVertices.Count - 1); // ���ĸ���������
        }
    }
}
