using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabButtonGroup : MonoBehaviour
{
    public TabButton buttonPrefab;
    private GameObject[] _panels => GameObject.FindGameObjectsWithTag("Panel");
    private TabButton[] _tabs;


    void Start()
    {
        _tabs = new TabButton[_panels.Length];
        foreach (GameObject panel in _panels)
        {
            TabButton clone = Instantiate(buttonPrefab,transform);
            clone.SetPanel(this, panel.gameObject);
            _tabs[clone.transform.GetSiblingIndex()] = clone;
            if (panel.transform.GetSiblingIndex() == 0)
                panel.gameObject.SetActive(true);
            else
                panel.gameObject.SetActive(false);
        }
    }

    public void SetActivePanel(TabButton tab)
    {
        foreach (GameObject obj in _panels)
        {
            obj.SetActive(false);
        }
        tab.Panel.SetActive(true);
    }
}
