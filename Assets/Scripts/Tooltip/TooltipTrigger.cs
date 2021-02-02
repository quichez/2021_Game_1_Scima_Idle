using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Header { get; private set; }
    public string Content { get; private set; }
    [SerializeField] private bool _enabled = false;

    public void EnableToolTip(bool enb)
    {
        //Debug.Log("why call here?");
        _enabled = enb;
    }
    public void SetToolTipText(string header, string content)
    {
        Header = header;
        Content = content;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_enabled)
            TooltipSystem.Show(Content, Header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

}
