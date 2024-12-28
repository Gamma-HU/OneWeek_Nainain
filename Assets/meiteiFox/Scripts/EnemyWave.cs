using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public GameManager.EnemyType enemyType;
    public int level;        // 敵のレベル
    [Tooltip("直前の敵が出現してからの経過時間")]
    public int spawnSec;
    public EnemyWave(GameManager.EnemyType enemyType, int level, int spawnSec)
    {
        this.enemyType = enemyType;
        this.level = level;
        this.spawnSec = spawnSec;
    }
}