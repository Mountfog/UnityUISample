using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test003Dlg : MonoBehaviour
{
    public ToggleGroup toggleGroup = null;
    public List<Toggle> toggles = new List<Toggle>();
    public List<string> fruits = new List<string> {"���","��","������"};
    public Text m_txtResult = null;
    public Button m_btnOk = null;
    public Button m_btnCancel = null;
    // Start is called before the first frame update
    void Start()
    {
        for(int i= 0; i < toggles.Count; i++)
        {
            int idx = i;
            toggles[i].onValueChanged.AddListener((bool kbool) => OnValueChanged(fruits[idx]));
        }
        m_btnOk.onClick.AddListener(OnClick_Ok);
        m_btnCancel.onClick.AddListener(OnClick_Clear);
    }
    public void OnValueChanged(string value)
    {
        m_txtResult.text = value;
    }
    public void OnClick_Ok()
    {
        string s = m_txtResult.text;
        if (s != string.Empty)
        {
            m_txtResult.text = string.Format("����� ������ ������<color=#FF0000>{0}</color>�Դϴ�", s);
        }
        else
        {
            m_txtResult.text = "����� ������ ������ �����ϴ�";
        }
        //asdfasdfasfd
    }
    public void OnClick_Clear()
    {
        toggleGroup.SetAllTogglesOff();
        m_txtResult.text = string.Empty;
    }
}
