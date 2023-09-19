using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Test005Dlg : MonoBehaviour
{
    public Slider m_sliderR = null;
    public Slider m_sliderG = null;
    public Slider m_sliderB = null;
    public Text m_txtResult = null;
    public Text m_txt_SliderR = null;
    public Text m_txt_SliderG = null;
    public Text m_txt_SliderB = null;
    public Button m_btnOk = null;
    public Button m_btnClear = null;
    // Start is called before the first frame update
    void Start()
    {
        m_sliderR.onValueChanged.AddListener((float f) => OnValueChanged());
        m_sliderG.onValueChanged.AddListener((float f) => OnValueChanged());
        m_sliderB.onValueChanged.AddListener((float f) => OnValueChanged());
        m_btnOk.onClick.AddListener(OnClick_Ok);
        m_btnClear.onClick.AddListener(OnClick_Clear);
    }
    public void OnValueChanged()
    {
        byte byteR = (byte)m_sliderR.value;
        byte byteG = (byte)m_sliderG.value;
        byte byteB = (byte)m_sliderB.value;
        m_txt_SliderR.text = byteR.ToString();
        m_txt_SliderG.text = byteG.ToString();
        m_txt_SliderB.text = byteB.ToString();
        m_txtResult.text = string.Format("({0}, {1}, {2})", byteR, byteG, byteB);
        m_txtResult.color = new Color32(byteR, byteG, byteB,255);
    }

    public void OnClick_Ok()
    {
        byte byteR = (byte)m_sliderR.value;
        byte byteG = (byte)m_sliderG.value;
        byte byteB = (byte)m_sliderB.value;
        m_txtResult.text = string.Format("현재 색상은 ({0}, {1}, {2})",byteR,byteG,byteB);
    }
    public void OnClick_Clear()
    {
        m_sliderR.value = 0;
        m_sliderG.value = 0;
        m_sliderB.value = 0;
        m_txtResult.color = new Color(0, 0, 0);
    }
}
