using UnityEngine;
using TMPro;

public class TabButton : MonoBehaviour
{
    private TabButtonGroup _group;
    private TextMeshProUGUI _text => GetComponentInChildren<TextMeshProUGUI>();
    public GameObject Panel { get; private set; }
   

    public void SetPanel(TabButtonGroup group, GameObject panel)
    {
        _group = group;
        Panel = panel;
        _text.text = panel.name;
    }

    public void SetPanelActive()
    {
        _group.SetActivePanel(this);
    }    
}
