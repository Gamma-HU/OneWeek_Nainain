using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class DecorationManager : MonoBehaviour
{
    public List<GameObject> decorations = new();
    public List<GameObject> panels = new();
    [SerializeField] GameObject viewContent;
    [SerializeField] GameObject testPrefab;
    [SerializeField] GameObject decolationPanel;
    public List<GameObject> AddDecolation(GameObject decoration)
    {
        decorations.Add(decoration);
        GameObject panel = Instantiate(decolationPanel, viewContent.transform);
        panel.GetComponent<Image>().sprite = decoration.GetComponent<SpriteRenderer>().sprite;
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
    private void SetView()
    {
        foreach (var decolation in decorations)
        {
            decolation.transform.SetParent(viewContent.transform);
        }
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
