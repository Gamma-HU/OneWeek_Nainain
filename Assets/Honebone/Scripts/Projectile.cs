using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Kurimanju attacker;
    Attack attack;

    Transform tf;
    protected Enemy target;
    Transform targetTF;
    protected Vector3 targetPos;
    Vector3 targetPosDiff;
    float projectileSpeed;
    float followTargetSpeed;

    List<int> hitEnemies;
    int hitCount;
    bool disabled;
    public void Init(Kurimanju k, Enemy e, Attack a)
    {
        attacker = k;
        attack = a;
        target = e;
        targetTF = target.GetComponent<Transform>();
        targetPos = targetTF.position;

        projectileSpeed = Random.Range(attack.projectileSpeed.x, attack.projectileSpeed.y);
        //followTargetSpeed = turretData.followTargetSpeed;

        tf = transform;
        hitEnemies = new List<int>();

        StartCoroutine(CountDown());
    }
    void FixedUpdate()
    {
        //if (followTargetSpeed > 0)//追尾弾
        //{
        //    if (turretData.followCurrentTarget)//現在のプレイヤーの位置を追尾する場合は、ターゲットの位置を常に更新
        //    {
        //        targetPos = targetTF.position;
        //    }
        //    targetPosDiff = (targetPos - tf.position);
        //    Vector2 dis = targetPosDiff;
        //    if (dis.magnitude < 0.5f) { followTargetSpeed = 0; }//ターゲットの位置に到着したら追尾停止

        //    float rot = (Mathf.Atan2(targetPosDiff.y, targetPosDiff.x) * Mathf.Rad2Deg) - tf.localEulerAngles.z - 90;
        //    if (rot < -180) { rot += 360; }
        //    tf.Rotate(0, 0, Mathf.Clamp(rot, followTargetSpeed * -0.5f, followTargetSpeed * 0.5f));
        //}

        transform.Translate(Vector3.up * projectileSpeed / 50f);
    }

    void DestroyPJTL(bool expired)
    {
        disabled = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        AtTheEnd(expired);


        Destroy(gameObject, 1f);
    }
    /// <summary>expired:時間切れによる破壊か</summary>
    public virtual void AtTheEnd(bool expired) { }//消滅時誘発

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(attack.projectileDuration);
        if (!disabled) { DestroyPJTL(true); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hitCount <= attack.penetration && !hitEnemies.Contains(collision.GetInstanceID()))
            {
                hitCount++;
                hitEnemies.Add(collision.GetInstanceID());
                Enemy hit = collision.GetComponent<Enemy>();
                //Enemy.EnemyStatus enemyStatus = hit.GetEnemyStatus();//攻撃処理
                //int DMG = attack.DMG;


                //hit.Damage(DMG, attacker.transform, execute);
                if (hitCount >= attack.penetration + 1 && !attack.infinitePenetration)//ヒット数が貫通数+1より多くなり、無限に貫通しないなら
                {
                    if (!disabled) { DestroyPJTL(false); }
                }
            }

        }
    }
}
