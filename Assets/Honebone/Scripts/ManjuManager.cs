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
        if (cellPos.OutOfBoard())
        {
            Debug.Log("盤外です");
            return;
        }
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

    public void RemoveManju(Vector2Int pos)
    {
        if (manjuList[pos.VectorToInt()] == null) { Debug.Log("error"); }
        manjuList[pos.VectorToInt()] = null;
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

    //============[ナイナイン]===============
    [SerializeField] GameObject option_nine;
    Kurimanju selected_nine;
    
    public void Nainain_ShowOption(Kurimanju m)
    {
        selected_nine = m;
        option_nine.SetActive(true);
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        option_nine.transform.position = pos;
    }
    public void Nainain_Abort()
    {
        selected_nine = null;
        option_nine.SetActive(false);
    }
    public void Nainain_Vertical()
    {
        Vector2Int origin = selected_nine.Status().position;
        List<DecorationParams> decos = selected_nine.GetDecorations();
        int level = selected_nine.Status().level;
        for(int i = -8; i <= 8; i++)
        {
            SpawnManju(new Vector2Int(origin.x, origin.y + i), decos, level);
        }
        Nainain_Abort();
    } 
    public void Nainain_Horizontal()
    {
        Vector2Int origin = selected_nine.Status().position;
        List<DecorationParams> decos = selected_nine.GetDecorations();
        int level = selected_nine.Status().level;
        for (int i = -8; i <= 8; i++)
        {
            SpawnManju(new Vector2Int(origin.x + i, origin.y), decos, level);
        }
        Nainain_Abort();
    }
    public void Nainain_Around()
    {
        Vector2Int origin = selected_nine.Status().position;
        List<DecorationParams> decos = selected_nine.GetDecorations();
        int level = selected_nine.Status().level;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                SpawnManju(new Vector2Int(origin.x + i, origin.y + j), decos, level);
            }
        }
        Nainain_Abort();
    }

}
