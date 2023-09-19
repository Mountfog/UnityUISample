using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test004Dlg : MonoBehaviour
{
    public Slider m_slider = null;
    public Text m_txtResult = null;
    public Button m_btnOk = null;
    public Button m_btnClear = null;

    // Start is called before the first frame update
    void Start()
    {
        m_slider.onValueChanged.AddListener((float value)=>OnValueChanged(value));
        m_btnOk.onClick.AddListener(OnClick_Ok);
        m_btnClear.onClick.AddListener(OnClick_Clear);
    }
    public void OnValueChanged(float value)
    {
        m_txtResult.text = value.ToString();
    }

    public void OnClick_Ok()
    {
        m_txtResult.text = string.Format("현재 진행된 값은 <color=#FF0000>{0}</color>입니다.",m_slider.value);
    }
    public void OnClick_Clear()
    {
        m_txtResult.text = string.Empty;
        m_slider.value = 0;
    }
}
