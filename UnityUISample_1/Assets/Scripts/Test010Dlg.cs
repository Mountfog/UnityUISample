using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Animal
{
    public string name;
    public float weight;
    public Animal(string kname, float kweight)
    {
        name = kname;
        weight = kweight;
    }
}
public class Test010Dlg : MonoBehaviour
{
    public InputField m_infiName = null;
    public InputField m_infiWeight = null;
    public Text m_txtResult = null;
    public Button m_btnAdd = null;
    public Button m_btnResult = null;
    public Button m_btnClear = null;
    List<Animal> m_animals = new List<Animal>();


    // Start is called before the first frame update
    void Start()
    {
        m_btnAdd.onClick.AddListener(OnClick_Add);
        m_btnResult.onClick.AddListener(OnClick_Result);
        m_btnClear.onClick.AddListener(OnClick_Clear);
    }

    public void OnClick_Add()
    {
        string kname = m_infiName.text;
        float kweight = float.Parse(m_infiWeight.text);
        if(kweight < 0 || kweight > 2000)
        {
            Message("������ �������� ������ �ƴմϴ�");
            return;
        }
        bool isIn = false;
        foreach(Animal a in m_animals)
        {
            if(a.name == kname)
            {
                a.weight = kweight;
                isIn = true;
                break;
            }
        }
        if (isIn)
        {
            Message("������ ������ �����Ը� ������Ʈ�߽��ϴ�");
            return;
        }
        if(m_animals.Count >= 2)
        {
            Message("���� ���� �Ѿ����ϴ�");
        }
        if(!isIn && m_animals.Count < 2)
        {
            Animal kanimal = new Animal(kname,kweight);
            Message("���� �߰� �Ϸ�");
            m_animals.Add(kanimal);
        }
        m_infiName.text = string.Empty;
        m_infiWeight.text = string.Empty;
    }
    public void OnClick_Result()
    {
        if(m_animals.Count == 0)
        {
            Message("������ �ϳ��� �Էµ��� �ʾҽ��ϴ�");
            return;
        }
        if (m_animals.Count == 1)
        {
            m_txtResult.text = string.Format("{0}�� �����Դ� {1}kg�Դϴ�", m_animals[0].name, m_animals[0].weight);
            return;
        }
        if (m_animals.Count == 2)
        {
            float sum = m_animals[0].weight + m_animals[1].weight;
            m_txtResult.text = string.Format("{0}�� {1}�� �������� ���� {2}kg�Դϴ�", m_animals[0].name, m_animals[1].name, sum);
            return;
        }

    }
    public void OnClick_Clear()
    {
        m_infiName.text = string.Empty;
        m_infiWeight.text = string.Empty;
        m_animals.Clear();
        ResultClear();
    }
    public void Message(string s)
    {
        m_txtResult.text = string.Format("<color=#FF0000>{0}</color>", s);
        Invoke(nameof(ResultClear),1);
    }
    public void ResultClear()
    {
        m_txtResult.text = string.Empty;
    }
}
