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
    public static GameManager instance;
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

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cell = new Cell();
        cell.Initialize(out cellInfo, out Route, TopLeft.position, BottomRight.position);
        Route = AddStartAndGoal(StartPos.position,GoalPos.position,Route);
        WaveStart(1);
    }
    /// <summary>
    /// ゲーム開始
    /// </summary>
    /// <param name="spawnPos">最初の栗饅頭の場所</param>
    public void GameStart(Vector2Int spawnPos)
    {
        ManjuManager.instance.SpawnManju(spawnPos, new List<DecorationParams>(),1);
        Sinryaku_Start();
    }

    private void TogglePhase()
    {
        if (currentPhase == Phase.Preparation)
        {
            ManjuManager.instance.StartBattle();
            currentPhase = Phase.Invasion;
            StartCoroutine(Invasion());
            Debug.Log("Switched to Invasion Phase");
        }
        else
        {
            ManjuManager.instance.EndBattle();
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

    bool spawning;
    IEnumerator Invasion()
    {
        spawning = true;
        EnemyWave[] enemyWaves =  WaveList[currentWave - 1].GetComponent<Waves>().waves.ToArray();
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            yield return new WaitForSeconds(enemyWaves[i].spawnSec);
            EnemyGen((int)enemyWaves[i].enemyType, enemyWaves[i].level);
        }
        //TogglePhase();
        spawning = false;
    }

    public void EndWave()
    {
        if (currentPhase == Phase.Invasion) { TogglePhase(); }
    }

    void EnemyGen(int enemyIndex, int enemylevel)
    {
        GameObject enemy = Instantiate(EnemyPrefabs[enemyIndex], Route[0], Quaternion.identity);
        //enemy.GetComponent<Enemy>().Route = Route;
        //enemy.GetComponent<Enemy>().Level = enemylevel;

        enemy.GetComponent<Honemy>().Init(Route, enemylevel);
        enemies.Add(enemy.GetComponent<Honemy>());

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


    List<Honemy> enemies = new List<Honemy>();
    public void RemoveEnemy(Honemy enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0 && !spawning) { EndWave(); }
    }

    public Honemy GetEnemy()
    {
        if (enemies.Count > 0) { return enemies[0]; }//test
        return null;
    }
    public List<Honemy> GetEnemies() { return enemies; }
}
