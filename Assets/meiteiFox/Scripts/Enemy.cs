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
    private int currentTargetIndex = 0; // 現在のルートインデックス
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
        // ターゲットが存在しない場合は何もしない
        if (Route == null || Route.Length == 0)
            return;

        // 現在のターゲット座標
        Vector3 targetPosition = Route[currentTargetIndex];

        // 現在の位置からターゲットへの方向を計算
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 移動速度を調整して次の位置を計算
        Vector3 nextPosition = transform.position + direction * Speed * Time.deltaTime;

        // ターゲットに近づいた場合、次のターゲットに移行
        if (Vector3.Distance(transform.position, targetPosition) < Speed * Time.deltaTime)
        {
            transform.position = targetPosition; // ターゲットにスナップ

            // 次のターゲットに進む（ループさせる場合は条件を追加）
            currentTargetIndex++;
            if (currentTargetIndex >= Route.Length)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // ターゲットに向かって移動
            transform.position = nextPosition;
        }
    }
}