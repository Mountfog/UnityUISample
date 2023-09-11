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
            Message("적절한 몸무게의 범위가 아닙니다");
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
            Message("기존의 동물의 몸무게를 업데이트했습니다");
            return;
        }
        if(m_animals.Count >= 2)
        {
            Message("동물 수를 넘었습니다");
        }
        if(!isIn && m_animals.Count < 2)
        {
            Animal kanimal = new Animal(kname,kweight);
            Message("동물 추가 완료");
            m_animals.Add(kanimal);
        }
        m_infiName.text = string.Empty;
        m_infiWeight.text = string.Empty;
    }
    public void OnClick_Result()
    {
        if(m_animals.Count == 0)
        {
            Message("동물이 하나도 입력되지 않았습니다");
            return;
        }
        if (m_animals.Count == 1)
        {
            m_txtResult.text = string.Format("{0}의 몸무게는 {1}kg입니다", m_animals[0].name, m_animals[0].weight);
            return;
        }
        if (m_animals.Count == 2)
        {
            float sum = m_animals[0].weight + m_animals[1].weight;
            m_txtResult.text = string.Format("{0}와 {1}의 몸무게의 합은 {2}kg입니다", m_animals[0].name, m_animals[1].name, sum);
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
