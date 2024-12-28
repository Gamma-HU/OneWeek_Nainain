using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStatus
    {
        public int maxHP;
        public float moveSpeed;
        public float slowedSpeed;
        public float slowDuration;

        public bool dead;
        public int HP;

        public void Init()
        {
            HP = maxHP;
        }
    }
    [SerializeField] EnemyStatus status;
    [SerializeField] GameObject particle_smoke;
    [SerializeField] ParticleSystem particle_slow;

    float moveSpeed;
    bool slowed;
    float slowTimer;

    [SerializeField]Enemy enemy;//test

    public void Init()
    {
        status.Init();
        moveSpeed = status.moveSpeed;
    }

    public bool CheckAlive() { return !status.dead; }

    private void Update()
    {
        if (slowed)
        {
            slowTimer += Time.deltaTime;
            if (slowTimer >= status.slowDuration)
            {
                slowTimer = 0;
                slowed = false;
                moveSpeed = status.moveSpeed;
                particle_slow.Stop();
            }
        }
    }

    public bool Damage(int DMG)
    {
        if (status.dead) { return true; }//すでに死亡している場合はスキップ
        status.HP -= DMG;
        Debug.Log($"{DMG}ダメージ");



        if (status.HP <= 0)
        {
            status.dead = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            particle_slow.Stop();
            Instantiate(particle_smoke, transform);

            Honebone_Test.instance.RemoveEnemy(enemy);
            Destroy(gameObject, 3f);
        }
        return status.HP <= 0;//この攻撃で殺したかを返す
    }
    public void Slow()
    {
        slowed = true;
        slowTimer = 0;
        moveSpeed = status.slowedSpeed;
        particle_slow.Play();
    }

   
}
