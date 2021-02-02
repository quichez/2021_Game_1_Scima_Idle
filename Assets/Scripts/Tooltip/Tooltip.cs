using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    TextMeshProUGUI _header => transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    TextMeshProUGUI _content => transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    LayoutElement _layoutElement => GetComponent<LayoutElement>();

    public int characterWrapLimit;
    public float fixThisPlease;
    public RectTransform rectTransform => GetComponent<RectTransform>();

    private void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = _header.text.Length;
            int contentLength = _content.text.Length;

            _layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;

            transform.position = Input.mousePosition;
        }

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = fixThisPlease;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
            _header.gameObject.SetActive(false);
        else
        {
            _header.gameObject.SetActive(true);
            _header.text = header;
        }

        _content.text = content;

        int headerLength = _header.text.Length;
        int contentLength = _content.text.Length;

        _layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;

        //transform.position = Input.mousePosition;
    }
}
