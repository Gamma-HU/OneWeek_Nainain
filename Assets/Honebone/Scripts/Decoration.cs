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
    [SerializeField] DecoStatus decoStatus;
}
