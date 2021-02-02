using UnityEngine;

public class TabButton : MonoBehaviour
{
    private TabButtonGroup _group;
    public GameObject Panel { get; private set; }
   

    public void SetPanel(TabButtonGroup group, GameObject panel)
    {
        _group = group;
        Panel = panel;
    }

    public void SetPanelActive()
    {
        _group.SetActivePanel(this);
    }    
}
