using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honebone_Test : MonoBehaviour
{
    public bool debug;
    [SerializeField] List<Vector2> manjuPos;
    [SerializeField] GameObject manju;
    [SerializeField] GameObject equip;
    [SerializeField] List<Enemy> enemyTest;
    public static Honebone_Test instance;

    List<Kurimanju> majuList = new List<Kurimanju>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach(Vector2 pos in manjuPos)
        {
            var m = Instantiate(manju, pos, Quaternion.identity);
            m.GetComponent<Kurimanju>().Init(pos,new List<DecorationParams>());
            m.GetComponent<Kurimanju>().StartBattle();
            majuList.Add(m.GetComponent<Kurimanju>());
        }
    }
    private void Update()
    {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                majuList[0].Equip(equip, 1);
            }
        }    
    }

    public Enemy GetEnemy()
    {
        return enemyTest[0];
    }
    public List<Enemy> GetEnemies() { return enemyTest; }
}
