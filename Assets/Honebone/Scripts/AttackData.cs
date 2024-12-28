using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData : ScriptableObject
{
    //����̓{�c
    public GameObject projectile;

    [Header("�^�[�Q�b�g��ǔ����邩/�����]�����x")] public float followTargetSpeed;
    [Header("���݂̃v���C���[�̈ʒu��ǔ����邩 false�Ȃ甭�ˎ��̏ꏊ��")] public bool followCurrentTarget;

    [Header("���̔��˂Ŏˏo����e��")] public int pellets = 1;
    [Header("�����_���ȕ����ɔ��˂��邩")] public bool fireRandomly;
    [Header("+-(spread/2)���̃u����������")] public float spread;
    [Header("spread��ɓ��Ԋu�ɔ��˂��邩")] public bool equidistant;

    //[Header("���ˉ�")] public float fireRounds = 1;
    //[Header("���ˉ񐔂�2�ȏ�̎��ɎQ�� 1�����˂��邲�Ƃ̃C���^�[�o��[s] 0�Ȃ瓯������")] public float fireRate;

    [Header("x:min y:max min�`max�̊ԂŃ����_���Ɍ��܂�")]
    public Vector2 projectileSpeed;

    public bool infinitePenetration;
    public int penetration;
    public float projectileDuration = 1f;
}
