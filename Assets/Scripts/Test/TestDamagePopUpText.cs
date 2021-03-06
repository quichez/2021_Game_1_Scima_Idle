using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestDamagePopUpText : MonoBehaviour
{
    private TextMeshPro text => GetComponent<TextMeshPro>();

    private void Start()
    {
        StartCoroutine(FadeAway());
        Destroy(gameObject,1.0f);
    }
    public void Setup(BigNumber damage)
    {
        text.text = damage.ToString();
    }

    private IEnumerator FadeAway()
    {
        float timer = 0.0f;
        while (timer < 0.95f)
        {
            timer += Time.deltaTime;
            text.color = Color.Lerp(text.color, new Color(1,1,1,0), Time.deltaTime * 2.0f);
            yield return new WaitForEndOfFrame();
        }
    }
}
