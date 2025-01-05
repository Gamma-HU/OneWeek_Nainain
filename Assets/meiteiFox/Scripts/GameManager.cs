using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEditor.Tilemaps;

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
    [SerializeField] GameObject InfoCanvas;
    bool IsSettingKurimanju = false;
    [SerializeField] GameObject highlightPanel;
    Vector2 nearestCellPos;
    float cellSize;
    Vector3 mousePosition;
    Vector2 gridOffset;
    [SerializeField] Transform Map;
    
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

        for (int i = 0; i < Enum.GetNames(typeof(Decoration.DecoStatus.Rarity)).Length; i++)
        {
            decorations.Add(new List<GameObject>());
        }
        foreach(GameObject deco in decorationDataBase)
        {
            decorations[(int)deco.GetComponent<Decoration>().Status().rarity].Add(deco);
        }
    }

    void Start()
    {
        cell = new Cell();
        cell.Initialize(out cellInfo, out Route, TopLeft.position, BottomRight.position);
        Route = AddStartAndGoal(StartPos.position,GoalPos.position,Route);
        WaveStart(1);
        highlightPanel = Instantiate(highlightPanel);
        nearestCellPos = Vector2.zero;
        cellSize = 0.94625f;
        gridOffset = Map.transform.position;

    }
    private void Update()
    {
        // マウスのワールド座標を取得
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (mousePosition.x > TopLeft.transform.position.x - cellSize/3 && mousePosition.x < BottomRight.transform.position.x + cellSize/3)
        {
            if (mousePosition.y < TopLeft.transform.position.y + cellSize/3 && mousePosition.y > BottomRight.transform.position.y - cellSize/3)
            {
                HighlightNearestCell();
                if (IsSettingKurimanju)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        int x = Math.Abs((int)((nearestCellPos.x - gridOffset.x - Map.localScale.x / 2) / cellSize));
                        int y = Math.Abs((int)((nearestCellPos.y - gridOffset.y - Map.localScale.y / 2) / cellSize));
                        Debug.Log(x.ToString());
                        Debug.Log(y.ToString());

                        ManjuManager.instance.SpawnManju(new Vector2Int(x,y), new List<DecorationParams>(), 1);
                    }
                }
            }
        }
    }
    private void HighlightNearestCell()
    {
        // マウス位置に最も近いセルを計算
        float nearestX = Mathf.Round((mousePosition.x - gridOffset.x) / cellSize) * cellSize + gridOffset.x;
        float nearestY = Mathf.Round((mousePosition.y - gridOffset.y) / cellSize) * cellSize + gridOffset.y;

        // ハイライトパネルをそのセルに移動
        if (highlightPanel != null)
        {
            highlightPanel.transform.position = new Vector3(nearestX, nearestY, 0);
        }
        nearestCellPos = new Vector2(nearestX, nearestY);
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
        if (currentWave == 1)
        {
            SetInitialKurimanju();
        }
    }
    void SetInitialKurimanju()
    {
        GameObject infoCanvas = Instantiate(InfoCanvas);
        infoCanvas.GetComponent<InfoCanvasController>().SetInfoString("最初に配置する自機の場所を選んでください");
        IsSettingKurimanju = true;
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
    public List<GameObject> decorationDataBase;
    public int dropDecorationChance;//敵を倒した時に小食品をおとすかくりつ
    public List<float> dropDecorationWeight;//装飾品ドロップ時のレアリティの重み

    List<List<GameObject>> decorations = new List<List<GameObject>>();

    /// <summary>ランダムな装飾品を返す(レアリティを考慮)</summary>
    public GameObject GetRandomDeco()
    {
        return decorations[dropDecorationWeight.ChoiceWithWeight()].Choice();
    }

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
