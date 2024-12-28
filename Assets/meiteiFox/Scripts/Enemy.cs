using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public Vector3[] Route;
    public float Speed;
    private int currentTargetIndex = 0; // 現在のルートインデックス
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