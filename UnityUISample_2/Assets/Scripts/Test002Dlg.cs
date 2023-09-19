using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test002Dlg : MonoBehaviour
{
    public Toggle m_toggleApple = null;
    public Toggle m_togglePear = null;
    public Toggle m_toggleOrange = null;
    public Text m_txtResult = null;
    public Button m_btnOk = null;
    public Button m_btnCancel = null;

    // Start is called before the first frame update
    void Start()
    {
        m_toggleApple.onValueChanged.AddListener((bool kbool) => OnValueChanged_Apple(kbool));
        m_togglePear.onValueChanged.AddListener((bool kbool) => OnValueChanged_Pear(kbool));
        m_toggleOrange.onValueChanged.AddListener((bool kbool) => OnValueChanged_Orange(kbool));
        m_txtResult.text = string.Empty;
        m_btnOk.onClick.AddListener(OnClick_Ok);
        m_btnCancel.onClick.AddListener(OnClick_Clear);
    }

    public void OnValueChanged_Apple(bool kbool)
    {
        m_txtResult.text = kbool ? "���" : string.Empty;
    }
    public void OnValueChanged_Pear(bool kbool)
    {
        m_txtResult.text = kbool ? "��" : string.Empty;
    }
    public void OnValueChanged_Orange(bool kbool)
    {
        m_txtResult.text = kbool ? "������" : string.Empty;
    }

    public void OnClick_Ok()
    {
        string s = string.Empty;
        if (m_toggleApple.isOn) s += " ���";
        if (m_togglePear.isOn) s += " ��";
        if (m_toggleOrange.isOn) s += " ������";
        if (s != string.Empty)
        {
            m_txtResult.text = string.Format("����� ������ ������<color=#FF0000>{0}</color>�Դϴ�", s);
        }
        else
        {
            m_txtResult.text = "����� ������ ������ �����ϴ�";
        }
    }
    public void OnClick_Clear()
    {
        m_toggleApple.isOn = false;
        m_togglePear.isOn = false;
        m_toggleOrange.isOn = false;
        m_txtResult.text = string.Empty;
    }

}
