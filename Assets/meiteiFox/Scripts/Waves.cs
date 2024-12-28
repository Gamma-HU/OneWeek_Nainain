using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public List<EnemyWave> waves;
    private void Start()
    {
        if (waves == null || waves.Count == 0)
        {
            waves = new List<EnemyWave>()
            {
                new EnemyWave(GameManager.EnemyType.purin, 1, 0),
                new EnemyWave(GameManager.EnemyType.purin, 1, 2)
            };
        }
    }
}
