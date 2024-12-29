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
            m.GetComponent<Kurimanju>().Init(pos,new List<DecorationParams>(),1);
            m.GetComponent<Kurimanju>().StartBattle();
            majuList.Add(m.GetComponent<Kurimanju>());
        }

        foreach(Enemy enemy in enemyTest)
        {
            enemy.GetComponent<Honemy>().Init(null,1);
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

    public void RemoveEnemy(Enemy enemy)
    {
        enemyTest.Remove(enemy);
    }

    public Enemy GetEnemy()
    {
        if (enemyTest.Count > 0) { return enemyTest[0]; }
        return null;
    }
    public List<Enemy> GetEnemies() { return enemyTest; }
}
