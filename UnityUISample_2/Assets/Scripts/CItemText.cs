using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CItemText : MonoBehaviour
{
    public Button m_btn = null;
    public Text m_txt = null;
    public int m_index = 0;

    public void Initialize(string name, int idx) //�̴ϼȶ�����
    {

        m_txt.text = name;
        m_index = idx;
        //�����ÿ� ����� �Լ�
    }
    public void SetColor(bool bColor)
    {
        if (bColor)
        {
            m_txt.color = Color.red;
        }
        else
        {
            m_txt.color = Color.black;
        }
    }
}
