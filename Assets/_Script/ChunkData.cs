using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ChunkData �����ڴ洢���飨chunk�������ݣ������������͡��ߴ硢λ�õ���Ϣ
public class ChunkData
{
    // �洢��������ÿ��λ�õķ������ͣ���СΪ chunkSize * chunkHeight * chunkSize
    public BlockType[] blocks;

    // �����ˮƽ��С����ʾ������ x �� z �����ϵĳ��ȣ�Ĭ��Ϊ 16
    public int chunkSize = 16;

    // ����ĸ߶ȣ���ʾ������ y �����ϵĸ߶ȣ�Ĭ��Ϊ 100
    public int chunkHeight = 100;

    // ��ǰ�������ڵ���������ã������������е�������������ݽ���
    public World worldReference;

    // ��������������ϵ�е�λ�ã�ʹ�� Vector3Int ��ʾ��ά��������
    public Vector3Int worldPosition;

    // ��Ǹ������Ƿ�����޸Ĺ�����ʼֵΪ false
    public bool modifiedByThePlayer = false;

    // ���캯�������ڳ�ʼ�� ChunkData ��ʵ��
    public ChunkData(int chunkSize, int chunkHeight, World world, Vector3Int worldPosition)
    {
        // ʹ�ô���� chunkHeight ֵ����������߶�
        this.chunkHeight = chunkHeight;

        // ʹ�ô���� chunkSize ֵ�����������ˮƽ��С
        this.chunkSize = chunkSize;

        // ���õ�ǰ�������ڵ���������
        this.worldReference = world;

        // ���õ�ǰ�����������е�λ��
        this.worldPosition = worldPosition;

        // ��ʼ�� blocks ���飬��СΪ chunkSize * chunkHeight * chunkSize��
        // ���ڴ洢������ÿ�������λ�ú�����
        blocks = new BlockType[chunkSize * chunkHeight * chunkSize];
    }
}
