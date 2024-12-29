using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManjuManager : MonoBehaviour
{
    [SerializeField] float cellDist = 7.57f;
    [SerializeField] GameObject manju;
    [SerializeField] List<Vector2Int> enemyRoute;

    Kurimanju[] manjuList = new Kurimanju[81];
    public static ManjuManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void SpawnManju(Vector2Int cellPos,List<DecorationParams> decos,int level)
    {
        if (manjuList[cellPos.VectorToInt()] != null)
        {
            Debug.Log("すでにその場所に饅頭があります");
            return;
        }
        if (enemyRoute.Contains(cellPos)) return;

        var m = Instantiate(manju,transform);
        m.transform.localPosition = new Vector3(cellPos.x * cellDist, cellPos.y * cellDist, 0);
        Kurimanju kurimanju = m.GetComponent<Kurimanju>();
        kurimanju.Init(cellPos, decos,level);
        manjuList[cellPos.VectorToInt()] = kurimanju;
    }

    public List<Kurimanju> GetManjuList()
    {
        List<Kurimanju> list = new List<Kurimanju>();
        foreach(Kurimanju manju in manjuList)
        {
            if (manju != null)
            {
                list.Add(manju);
            }
        }
        return list;
    }

    public void StartBattle()
    {
        foreach (Kurimanju manju in GetManjuList())
        {
            manju.StartBattle();
        }
    }
    public void EndBattle()
    {
        foreach (Kurimanju manju in GetManjuList())
        {
            manju.EndBattle();
        }
    }
}
