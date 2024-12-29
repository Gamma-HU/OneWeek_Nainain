using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    GameManager.EnemyType enemyType;
    public Vector3[] Route;
    public float Speed;
    public int Hp;
    public int Level;
    private int currentTargetIndex = 0; // ���݂̃��[�g�C���f�b�N�X
    private void Awake()
    {
        switch (enemyType)
        {
            case (GameManager.EnemyType)0:
                Hp = 10 * (int)Mathf.Pow(Level, 2);
                Speed = 1; 
                break;
            case (GameManager.EnemyType)1:
                Hp = 10 * (int)Mathf.Pow(Level, 2);
                Speed = 1.5f;
                break;
            case (GameManager.EnemyType)2:
                Hp = 20 * (int)Mathf.Pow(Level, 2);
                Speed = 1;
                break;
            case (GameManager.EnemyType)3:
                Hp = 15 * (int)Mathf.Pow(Level, 2);
                Speed = 2;
                break;
            case (GameManager.EnemyType)4:
                Hp = 60 * (int)Mathf.Pow(Level, 2);
                Speed = 0.8f;
                break;
            default:
                Hp = 10 * (int)Mathf.Pow(Level, 2);
                Speed = 1;
                break;
        }
    }
    private void Update()
    {
        // �^�[�Q�b�g�����݂��Ȃ��ꍇ�͉������Ȃ�
        if (Route == null || Route.Length == 0)
            return;

        // ���݂̃^�[�Q�b�g���W
        Vector3 targetPosition = Route[currentTargetIndex];

        // ���݂̈ʒu����^�[�Q�b�g�ւ̕������v�Z
        Vector3 direction = (targetPosition - transform.position).normalized;

        // �ړ����x�𒲐����Ď��̈ʒu���v�Z
        Vector3 nextPosition = transform.position + direction * Speed * Time.deltaTime;

        // �^�[�Q�b�g�ɋ߂Â����ꍇ�A���̃^�[�Q�b�g�Ɉڍs
        if (Vector3.Distance(transform.position, targetPosition) < Speed * Time.deltaTime)
        {
            transform.position = targetPosition; // �^�[�Q�b�g�ɃX�i�b�v

            // ���̃^�[�Q�b�g�ɐi�ށi���[�v������ꍇ�͏�����ǉ��j
            currentTargetIndex++;
            if (currentTargetIndex >= Route.Length)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // �^�[�Q�b�g�Ɍ������Ĉړ�
            transform.position = nextPosition;
        }
    }
}