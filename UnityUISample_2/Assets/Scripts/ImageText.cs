using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageText : MonoBehaviour
{
    public Button m_btn = null;
    public Text m_txt = null;
    public Image m_image = null;
    public Image m_icon = null;
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
            m_image.color = Color.red * 0.85f;
            m_icon.color = Color.blue;
        }
        else
        {
            m_image.color = Color.white;
            m_icon.color = Color.black;
        }
    }
}
