using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    Cell cell;
    Cell.CellInfo[] cellInfo;
    Vector3[] Route;
    [SerializeField] Transform StartPos, GoalPos, TopLeft, BottomRight;
    [SerializeField] GameObject Enemy1;
    [SerializeField] float GizmoSize;
    void Start()
    {
        cell = new Cell();
        cell.Initialize(out cellInfo, out Route, TopLeft.position, BottomRight.position);
        Route = AddStartAndGoal(StartPos.position,GoalPos.position,Route);

        //テスト
        WaveStart(1);
    }
    public void WaveStart(int wave)
    {
        GameObject enemy = Instantiate(Enemy1, StartPos.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().Route = Route;
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
