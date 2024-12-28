using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honebone_Test : MonoBehaviour
{
    [SerializeField] List<Vector2> manjuPos;
    [SerializeField] GameObject manju;
    [SerializeField] List<Enemy> enemyTest;
    public static Honebone_Test instance;

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
            
        }
    }

    public Enemy GetEnemy()
    {
        return enemyTest[0];
    }
    public List<Enemy> GetEnemies() { return enemyTest; }
}
