using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Honebone_Test : MonoBehaviour
{
    public bool debug;
    [SerializeField] List<Vector2> manjuPos;
    [SerializeField] GameObject manju;
    [SerializeField] List<GameObject> equip;
    //[SerializeField] List<Enemy> enemyTest;
    [SerializeField] GraphicRaycaster raycaster;
    public static Honebone_Test instance;

    List<Kurimanju> majuList = new List<Kurimanju>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //foreach(Vector2 pos in manjuPos)
        //{
        //    var m = Instantiate(manju, pos, Quaternion.identity);
        //    m.GetComponent<Kurimanju>().Init(pos,new List<DecorationParams>());
        //    m.GetComponent<Kurimanju>().StartBattle();
        //    majuList.Add(m.GetComponent<Kurimanju>());
        //}

        //foreach(Enemy enemy in enemyTest)
        //{
        //    enemy.GetComponent<Honemy>().Init(null,1);
        //}
    }
    private void Update()
    {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ManjuManager.instance.SpawnManju(new Vector2Int(4, 3), new List<DecorationParams>(), 1);
                ManjuManager.instance.SpawnManju(new Vector2Int(3, 3), new List<DecorationParams>(), 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //ManjuManager.instance.GetManjuList().Choice().Equip(equip.Choice(), 1);
                ManjuManager.instance.GetManjuList()[0].Equip(equip[0], 1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ManjuManager.instance.GetManjuList()[0].Combine(ManjuManager.instance.GetManjuList()[1]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                FindObjectOfType<GameManager>().GameStart(new Vector2Int(4, 3));
            }

            //if (Input.GetMouseButtonUp(0))//左クリックを話した時
            //{
            //    EventSystem ev = EventSystem.current;
            //    PointerEventData ped = new PointerEventData(ev);
            //    ped.position = Input.mousePosition;
            //    List<RaycastResult> rr = new List<RaycastResult>();
            //    raycaster.Raycast(ped, rr);

            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    Physics2D.RaycastAll(ray)


            //    foreach (RaycastResult result in rr)//カーソルに重なってる前オブジェクトに対して
            //    {
            //        if (result.gameObject.GetComponent<Kurimanju>())//CharaEqButton上でボタン離したなら
            //        {
            //            Debug.Log("栗饅頭をクリック");
            //            break;
            //        }
            //    }
            //}

            if (Input.GetMouseButtonDown(0))
            {
                // マウス位置を取得し、ワールド座標に変換
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Raycastを実行して、全てのヒットしたオブジェクトを取得
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

                // ヒットしたオブジェクトをログ出力
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.GetComponent<Kurimanju>())
                    {
                        ManjuManager.instance.Nainain_ShowOption(hit.collider.GetComponent<Kurimanju>());
                    }
                }

                // ヒットがなかった場合
                if (hits.Length == 0)
                {
                    Debug.Log("No objects hit.");
                }
            }
        }    
    }

    //public void RemoveEnemy(Enemy enemy)
    //{
    //    enemyTest.Remove(enemy);
    //}

    //public Enemy GetEnemy()
    //{
    //    if (enemyTest.Count > 0) { return enemyTest[0]; }
    //    return null;
    //}
    //public List<Enemy> GetEnemies() { return enemyTest; }
}
