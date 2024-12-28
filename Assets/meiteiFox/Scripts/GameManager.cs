using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        normal,
        endless
    }
    /// <summary>
    /// 現在のモードを表示させたい
    /// </summary>
    public static GameMode mode = GameMode.normal;
    Cell cell;
    Cell.CellInfo[] cellInfo;
    Vector3[] Route;
    [SerializeField] Transform StartPos, GoalPos, TopLeft, BottomRight;
    [SerializeField] float GizmoSize;
    [SerializeField] GameObject[] WaveList;
    int currentWave;
    [SerializeField] GameObject[] EnemyPrefabs;
    public enum Phase
    {
        Preparation,
        Invasion
    }
    public Phase currentPhase;
    
    public enum EnemyType
    {
        purin,
        tiramisu,
        choco,
        creampuffs,
        cake
    };
    void Start()
    {
        cell = new Cell();
        cell.Initialize(out cellInfo, out Route, TopLeft.position, BottomRight.position);
        Route = AddStartAndGoal(StartPos.position,GoalPos.position,Route);
        WaveStart(1);
    }
    private void TogglePhase()
    {
        if (currentPhase == Phase.Preparation)
        {
            currentPhase = Phase.Invasion;
            StartCoroutine(Invasion());
            Debug.Log("Switched to Invasion Phase");
        }
        else
        {
            currentPhase = Phase.Preparation;
            if (mode == GameMode.normal && currentWave == 20)
            {

            } else
            {
                WaveStart(currentWave + 1);
            }
            Debug.Log("Switched to Preparation Phase");
        }
    }
    public void WaveStart(int wave)
    {
        currentPhase = Phase.Preparation;
        currentWave = wave;
        //waveスタートのアニメーションもつけたい
    }
    public void Sinryaku_Start()
    {
        if (currentPhase != Phase.Invasion)
        {
            TogglePhase();
        }
    }
    IEnumerator Invasion()
    {
        EnemyWave[] enemyWaves =  WaveList[currentWave - 1].GetComponent<Waves>().waves.ToArray();
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            yield return new WaitForSeconds(enemyWaves[i].spawnSec);
            EnemyGen((int)enemyWaves[i].enemyType, enemyWaves[i].level);
        }
        TogglePhase();
    }
    void EnemyGen(int enemyIndex, int enemylevel)
    {
        GameObject enemy = Instantiate(EnemyPrefabs[enemyIndex], Route[0], Quaternion.identity);
        enemy.GetComponent<Enemy>().Route = Route;
        enemy.GetComponent<Enemy>().Level = enemylevel;

    }

    // Vector3配列の最初と最後にStartPos, GoalPosを追加する関数
    public Vector3[] AddStartAndGoal(Vector3 startPos, Vector3 goalPos, Vector3[] targets)
    {
        if (targets == null || targets.Length == 0)
        {
            targets = new Vector3[] { startPos, goalPos };
            return targets;
        }
        else
        {
            Vector3[] updatedTargets = new Vector3[targets.Length + 2];
            updatedTargets[0] = startPos;
            for (int i = 0; i < targets.Length; i++)
            {
                updatedTargets[i + 1] = targets[i];
            }
            updatedTargets[updatedTargets.Length - 1] = goalPos;
            return updatedTargets;
        }
    }
    void OnDrawGizmos()
    {
        if (Route == null || Route.Length == 0)
            return;

        foreach (var target in Route)
        {
            // 正方形のギズモを描画
            Gizmos.color = Color.green;
            GizmoSize = 0.5f;
            Gizmos.DrawLine(target + new Vector3(-GizmoSize, -GizmoSize, 0), target + new Vector3(GizmoSize, -GizmoSize, 0));
            Gizmos.DrawLine(target + new Vector3(GizmoSize, -GizmoSize, 0), target + new Vector3(GizmoSize, GizmoSize, 0));
            Gizmos.DrawLine(target + new Vector3(GizmoSize, GizmoSize, 0), target + new Vector3(-GizmoSize, GizmoSize, 0));
            Gizmos.DrawLine(target + new Vector3(-GizmoSize, GizmoSize, 0), target + new Vector3(-GizmoSize, -GizmoSize, 0));
        }
    }
}
