using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test008Dlg : MonoBehaviour
{
    public Dropdown m_dropDown = null;
    public Button m_btnOk = null;
    public Button m_btnClear = null;
    public Text m_txtResult = null;

    List<string> m_listCities = new List<string>() { "서울","전주","청주","광주","부산","대구","울산"};

    // Start is called before the first frame update
    void Start()
    {
        m_dropDown.onValueChanged.AddListener(OnValueChanged2);
        m_btnOk.onClick.AddListener(OnClick_Ok);
        m_btnClear.onClick.AddListener(OnClick_Clear);
        Initialize();
    }
    
    void Initialize()
    {
        m_dropDown.options.Clear();
        for (int i = 0; i < m_listCities.Count; i++)
        {
            m_dropDown.options.Add(new Dropdown.OptionData());
            m_dropDown.options[i].text = m_listCities[i];
        }
    }
    public void OnValueChanged2(int value)
    {
        OnValueChanged1();
    }
    public void OnValueChanged1()
    {
        //dropdown
    }



    public void OnValueChanged(int value)
    {
        string city = m_dropDown.options[value].text;
        m_txtResult.text = string.Format("{0} : {1}",value,city);
    }
    public void OnClick_Ok()
    {
        int value = m_dropDown.value;
        string city = m_dropDown.options[value].text;
        m_txtResult.text = string.Format("당신이 이동할 도시는 <color=#FF0000>{0}</color>",city);
    }
    public void OnClick_Clear()
    {
        m_dropDown.value = 0;
        m_txtResult.text  =string.Empty;
    }
}
