using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test006Dlg : MonoBehaviour
{
    public Scrollbar m_ollba = null;
    public Text m_txtResult = null;
    public Button m_btnOk = null;
    public Button m_btnClear = null;
    // Start is called before the first frame update
    void Start()
    {
        m_ollba.onValueChanged.AddListener(OnValueChanged);
        m_btnOk.onClick.AddListener(OnClick_Ok);
        m_btnClear.onClick.AddListener (OnClick_Clear);
    }

    public void OnValueChanged(float f)
    {
        m_txtResult.text = f.ToString();
    }
    //asjdfnwotoiwnroijeir
    public void OnClick_Ok()
    {
        m_txtResult.text = string.Format("현재 진행된 값은 {0:0.00}입니다", m_ollba.value);
    }
    public void OnClick_Clear()
    {
        m_txtResult.text = string.Empty;
    }
}
