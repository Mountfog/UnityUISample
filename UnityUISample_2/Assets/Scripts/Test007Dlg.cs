using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test007Dlg : MonoBehaviour
{
    public Scrollbar m_scrollbar = null;
    public Text m_txtResult = null;
    public Button m_btnOk = null;
    public Button m_btnClear = null;

    bool m_isMove = false;
    float fTime = 0;

    private void Start()
    {
        m_scrollbar.onValueChanged.AddListener((float value) => PrintText(value));
        m_btnOk.onClick.AddListener(OnClick_Ok);
        m_btnClear.onClick.AddListener(OnClick_Clear);
    }
    public void OnClick_Ok()
    {
        m_isMove = true;
    }
    public void OnClick_Clear()
    {
        m_isMove=false;
        m_scrollbar.value = 0;
    }
    private void Update()
    {
        if (!m_isMove) return;
        if (m_scrollbar.value >= 1.0f) return;
        fTime += Time.deltaTime;
        
        if(fTime >= 1)
        {
            fTime = 0;
            m_scrollbar.value += 0.05f;
            if (m_scrollbar.value >= 1.0f)
                m_scrollbar.value = 1.0f;
            PrintText(m_scrollbar.value);
        }
    }
    void PrintText(float value)
    {
        m_txtResult.text = string.Format("{0:0.00}", value);
        float a = m_scrollbar.value;
        m_txtResult.color = new Color(m_txtResult.color.r, m_txtResult.color.g, m_txtResult.color.b, a);
        bool isMove = false;


        if ((isMove || !isMove) || (isMove && !isMove))
        {

        }
    }
}
