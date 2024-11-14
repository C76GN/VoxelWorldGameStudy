using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BlockType ö�ٱ�ʾ��Ϸ�в�ͬ���͵ķ���
public enum BlockType
{
    // �޷��飬��ʾ�հ׻򲻿ɼ�������
    Nothing,

    // �������飬ͨ��������ҿɽ�������򣬲��ᱻ��Ⱦ
    Air,

    // �ݵط��飬���������вݣ��ײ�������
    Grass_Dirt,

    // �������飬ͨ�����ڵ������²�
    Dirt,

    // ʯͷ���ǵĲݵط��飬�����ǲݣ��ײ���ʯͷ
    Grass_Stone,

    // ʯͷ���飬ͨ������ɽ������£�Ӳ�Ƚϴ�
    Stone,

    // ���ɷ��飬��ʾ��ľ�����ɲ���
    TreeTrunk,

    // ͸����Ҷ���飬��ʾ��ľ����Ҷ���֣�����͸�����ֹ���
    TreeLeafesTransparent,

    // ʵ����Ҷ���飬��ʾ��ľ����Ҷ���֣���͸��
    TreeLeafsSolid,

    // ˮ���飬ͨ������ˮ�壬��������Ϊ��͸��
    Water,

    // ɳ�ӷ��飬ͨ�����ں�̲��ɳĮ����
    Sand
}
