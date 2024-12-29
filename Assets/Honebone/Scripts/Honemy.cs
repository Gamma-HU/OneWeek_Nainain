using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public void Init(int level)
        {
            maxHP *= Mathf.Pow(level, 2).ToInt();
            HP = maxHP;
            slowedSpeed = moveSpeed / 2f;
        }
    }
    [SerializeField] EnemyStatus status;
    [SerializeField] GameObject particle_smoke;
    [SerializeField] ParticleSystem particle_slow;
    [SerializeField] GameObject canvas;
    [SerializeField] Image HPBar; 

    float moveSpeed;
    bool slowed;
    float slowTimer;

    bool stuned;
    float stunTimer;


    public void Init(Vector3[] r, int level)
    {
        status.Init(level);
        Route = r;
        Level = level;
        moveSpeed = status.moveSpeed;
    }

    public bool CheckAlive() { return !status.dead; }

    public bool Damage(int DMG)
    {
        if (status.dead) { return true; }//���łɎ��S���Ă���ꍇ�̓X�L�b�v
        status.HP -= DMG;
        Debug.Log($"{DMG}�_���[�W");
        HPBar.fillAmount = 1f * status.HP / status.maxHP;


        if (status.HP <= 0)
        {
            status.dead = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            canvas.SetActive(false);
            particle_slow.Stop();
            Instantiate(particle_smoke, transform);

            GameManager.instance.RemoveEnemy(this);
            Destroy(gameObject,3f);
        }
        return status.HP <= 0;//���̍U���ŎE��������Ԃ�
    }
    public void Slow()
    {
        slowed = true;
        slowTimer = 0;
        moveSpeed = status.slowedSpeed;
        particle_slow.Play();
    }

    public void Stun()
    {
        stuned = true;
        stunTimer = 0;
        //particle_slow.Play();
    }




    GameManager.EnemyType enemyType;
    public Vector3[] Route;
    //public float Speed;
    //public int Hp;
    public int Level;
    private int currentTargetIndex = 0; // ���݂̃��[�g�C���f�b�N�X
    //private void Awake()
    //{
    //    switch (enemyType)
    //    {
    //        case (GameManager.EnemyType)0:
    //            Hp = 10 * (int)Mathf.Pow(Level, 2);
    //            Speed = 1;
    //            break;
    //        case (GameManager.EnemyType)1:
    //            Hp = 10 * (int)Mathf.Pow(Level, 2);
    //            Speed = 1.5f;
    //            break;
    //        case (GameManager.EnemyType)2:
    //            Hp = 20 * (int)Mathf.Pow(Level, 2);
    //            Speed = 1;
    //            break;
    //        case (GameManager.EnemyType)3:
    //            Hp = 15 * (int)Mathf.Pow(Level, 2);
    //            Speed = 2;
    //            break;
    //        case (GameManager.EnemyType)4:
    //            Hp = 60 * (int)Mathf.Pow(Level, 2);
    //            Speed = 0.8f;
    //            break;
    //        default:
    //            Hp = 10 * (int)Mathf.Pow(Level, 2);
    //            Speed = 1;
    //            break;
    //    }
    //}
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

        if (stuned)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= 1)
            {
                stunTimer = 0;
                stuned = false;
                //particle_slow.Stop();
            }
        }




        // �^�[�Q�b�g�����݂��Ȃ��ꍇ�͉������Ȃ�
        if (Route == null || Route.Length == 0)
            return;

        // ���݂̃^�[�Q�b�g���W
        Vector3 targetPosition = Route[currentTargetIndex];

        // ���݂̈ʒu����^�[�Q�b�g�ւ̕������v�Z
        Vector3 direction = (targetPosition - transform.position).normalized;

        // �ړ����x�𒲐����Ď��̈ʒu���v�Z
        Vector3 nextPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        // �^�[�Q�b�g�ɋ߂Â����ꍇ�A���̃^�[�Q�b�g�Ɉڍs
        if (Vector3.Distance(transform.position, targetPosition) < moveSpeed * Time.deltaTime)
        {
            transform.position = targetPosition; // �^�[�Q�b�g�ɃX�i�b�v

            // ���̃^�[�Q�b�g�ɐi�ށi���[�v������ꍇ�͏�����ǉ��j
            currentTargetIndex++;
            if (currentTargetIndex >= Route.Length)
            {
                Destroy(gameObject);
                GameManager.instance.RemoveEnemy(this);
            }
        }
        else
        {
            // �^�[�Q�b�g�Ɍ������Ĉړ�
            if (!stuned) transform.position = nextPosition;
        }
    }
}
