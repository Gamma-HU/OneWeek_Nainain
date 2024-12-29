using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class DecorationManager : MonoBehaviour
{
    // 
    public List<GameObject> decorations = new(); // �n���ꂽ�Q�[���I�u�W�F�N�g���i�[���郊�X�g
    public List<GameObject> panels = new(); // �n���ꂽ�Q�[���I�u�W�F�N�g�����Ƃɐ��������{�^�����i�[���郊�X�g
    [SerializeField] GameObject viewContent;
    [SerializeField] GameObject testPrefab;
    [SerializeField] GameObject decolationPanel; // �{�^���̃v���n�u
    public List<GameObject> AddDecolation(GameObject decoration)
    {
        decorations.Add(decoration);
        GameObject panel = Instantiate(decolationPanel, viewContent.transform);
        panel.GetComponent<Image>().sprite = decoration.GetComponent<SpriteRenderer>().sprite; // �{�^���̉摜���Q�[���I�u�W�F�N�g�����Ƃɐݒ�
        panels.Add(panel);
        return decorations;
    }
    public List<GameObject> RemoveDecolation(GameObject decoration)
    {
        Destroy(panels[decorations.IndexOf(decoration)]);
        panels.Remove(panels[decorations.IndexOf(decoration)]);
        decorations.Remove(decoration);
        return decorations;
    }
    [ContextMenu("AddTest")]
    public void Test()
    {
        AddDecolation(Instantiate(testPrefab));
    }
    [ContextMenu("RemoveTest")]
    public void Test2()
    {
        RemoveDecolation(decorations[0]);
    }
}
