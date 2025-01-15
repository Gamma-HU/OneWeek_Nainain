using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSwitch : MonoBehaviour
{
    [SerializeField] GameObject itemTab;
    [SerializeField] GameObject decoTab;
    [SerializeField] GameObject itemPanel;
    [SerializeField] GameObject decoPanel;
    public void SwitchTabItem()
    {
        itemTab.GetComponent<Image>().color = Color.gray;
        decoTab.GetComponent<Image>().color = Color.white;
        itemPanel.SetActive(true);
        decoPanel.SetActive(false);
    }
    public void SwitchTabDeco()
    {
        itemTab.GetComponent<Image>().color = Color.white;
        decoTab.GetComponent<Image>().color = Color.gray;
        itemPanel.SetActive(false);
        decoPanel.SetActive(true);
    }
}
