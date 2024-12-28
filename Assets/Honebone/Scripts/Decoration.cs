using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    [System.Serializable]
    public class DecoStatus
    {
        public string decoName;
        [TextArea(3, 10)] public string decoInfo;
        [TextArea(3, 10)] public string flavorText;
        public bool canStack;
        public int rank = 1;
        public enum rarity { common, rare, superRare }
    }
    [SerializeField]protected DecoStatus decoStatus;
    public DecoStatus Status() { return decoStatus; }

    protected Kurimanju manju;

    public void Init(Kurimanju m,int rank)
    {
        manju = m;
        decoStatus.rank = rank;
        OnInit();
    }
    public void AddRank(int add)
    {
        decoStatus.rank += add;
        OnAddRank(add);
    }

    public virtual void OnAttack(Enemy target,Attack atk,bool normalATK) { }

    public virtual void OnInit() { }
    public virtual void OnAddRank(int add) { }
    
}
