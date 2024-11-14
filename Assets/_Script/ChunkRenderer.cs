using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// ȷ���ڸ��� ChunkRenderer ʱ����Ϸ�����ϰ��� MeshFilter��MeshRenderer �� MeshCollider ���
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ChunkRenderer : MonoBehaviour
{
    // ���ڴ�����������
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    Mesh mesh;

    // �����Ƿ��ڱ༭������ʾ����ĸ����߿�Gizmo��
    public bool showGizmo = false;

    // ��ǰ��������ݣ������������͡��ߴ��λ�õ���Ϣ
    public ChunkData ChunkData { get; private set; }

    // ���ԣ����ڷ��ʺ��޸������Ƿ�����޸Ĺ���״̬
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

    // Awake �� Unity ���������еķ���֮һ���ڶ��󼤻�ʱ����
    private void Awake()
    {
        // ��ȡ���洢��Ϸ����� MeshFilter��MeshCollider ���������
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        // ��ȡ MeshFilter ���������
        mesh = meshFilter.mesh;
    }

    // ��ʼ����������ݣ�������� ChunkData ����ֵ����ǰʵ���� ChunkData �ֶ�
    public void InitializeChunk(ChunkData data)
    {
        this.ChunkData = data;
    }

    // ��Ⱦ�������ݵķ��������� MeshData �е��������ɺ���������
    private void RenderMesh(MeshData meshData)
    {
        mesh.Clear();  // �����ǰ�������ݣ�Ϊ�µĶ����������������׼��

        mesh.subMeshCount = 2;  // ��������������Ϊ 2��һ�����ڵ��Σ�һ������ˮ��
        // �ϲ��������ˮ����Ķ��㣬�����õ� mesh ��
        mesh.vertices = meshData.vertices.Concat(meshData.waterMesh.vertices).ToArray();

        // Ϊ������ 0 ���������Σ�ʹ�������������������
        mesh.SetTriangles(meshData.triangles.ToArray(), 0);
        // Ϊ������ 1 ���������Σ���ˮ����������ζ�������ƫ�������񶥵�����
        mesh.SetTriangles(meshData.waterMesh.triangles.Select(val => val + meshData.vertices.Count).ToArray(), 1);

        // �ϲ��������ˮ����� UV ���꣬�����õ� mesh ��
        mesh.uv = meshData.uv.Concat(meshData.waterMesh.uv).ToArray();
        mesh.RecalculateNormals();  // ���¼��㷨�ߣ���ȷ������Ч����ȷ

        // ��ײ�������ã������ǰ��ײ����
        meshCollider.sharedMesh = null;
        // �����µ���ײ����
        Mesh collisionMesh = new Mesh();
        // ����ײ��Ķ�����������������õ���ײ������
        collisionMesh.vertices = meshData.colliderVertices.ToArray();
        collisionMesh.triangles = meshData.colliderTriangles.ToArray();
        collisionMesh.RecalculateNormals();  // ���¼�����ײ����ķ���

        // ���µ���ײ����ֵ�� meshCollider�����ṩ��ײ���
        meshCollider.sharedMesh = collisionMesh;
    }

    // �������飬ʹ�õ�ǰ ChunkData �е����������µ� MeshData ����Ⱦ����
    public void UpdateChunk()
    {
        RenderMesh(Chunk.GetChunkMeshData(ChunkData));
    }

    // �������飬ʹ�ô���� MeshData ����ֱ����Ⱦ����
    public void UpdateChunk(MeshData data)
    {
        RenderMesh(data);
    }

    // Unity �༭�����룬���ڱ༭���б��룬ʹ�� Gizmo ������ʾ����
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // ��������� showGizmo�����ڱ༭������ʾ���鸨���߿�
        if (showGizmo)
        {
            // ����Ϸ����ʱ���� ChunkData ��Ϊ��ʱ����ʾ Gizmo
            if (Application.isPlaying && ChunkData != null)
            {
                // ���� Gizmo ����ɫ����ɫ��ʾѡ�У���ɫ��ʾδѡ��
                if (Selection.activeObject == gameObject)
                    Gizmos.color = new Color(0, 1, 0, 0.4f);  // ѡ��ʱΪ��͸����ɫ
                else
                    Gizmos.color = new Color(1, 0, 1, 0.4f);  // δѡ��ʱΪ��͸����ɫ

                // ����һ���߿������壬���ڱ�ʾ����ı߽�
                Gizmos.DrawCube(
                    transform.position + new Vector3(ChunkData.chunkSize / 2f, ChunkData.chunkHeight / 2f, ChunkData.chunkSize / 2f),
                    new Vector3(ChunkData.chunkSize, ChunkData.chunkHeight, ChunkData.chunkSize)
                );
            }
        }
    }
#endif
}
