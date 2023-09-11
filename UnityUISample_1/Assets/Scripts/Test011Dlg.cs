using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster
{
    public string name;
    public int health;
    public Monster(string kname, int khealth)
    {
        name = kname;
        health = khealth;
    }
}
public class Test011Dlg : MonoBehaviour
{
    public InputField m_infiName = null;
    public InputField m_infiHealth = null;
    public Text m_txtResult = null;
    public Text m_TxtList = null;
    public Button m_btnAdd = null;
    public Button m_btnResult = null;
    public Button m_btnClear = null;
    List<Monster> m_monsters = new List<Monster>();

    public void Start()
    {
        m_btnClear.onClick.AddListener(OnClick_Clear);
        m_btnAdd.onClick.AddListener(OnClick_Add);
        m_btnResult.onClick.AddListener(OnClick_Result);

        OnClick_Clear();
    }
    public void OnClick_Add()
    {
        if(m_monsters.Count >= 4 || string.IsNullOrEmpty(m_infiHealth.text) || string.IsNullOrEmpty(m_infiName.text))
        {
            return;
        }
        
        string name = m_infiName.text;
        int health = int.Parse(m_infiHealth.text);
        if(health < 0 || health > 200)
        {
            return;
        } 
        Monster kmonster = new Monster(name, health);
        m_monsters.Add(kmonster);
        m_TxtList.text += string.Format("[{0}:{1}], ",name,health);
        m_infiHealth.text = string.Empty;
        m_infiName.text = string.Empty;
    }
    public void OnClick_Result()
    {
        m_txtResult.text = string.Empty;
        foreach(Monster kmonster in m_monsters)
        {
            kmonster.health -= 80;
            if(kmonster.health < 0) kmonster.health = 0;
            m_txtResult.text += string.Format("Name = {0}, HP = {1}\n",kmonster.name, kmonster.health);
        }
    }
    public void OnClick_Clear()
    {
        m_monsters.Clear();
        m_txtResult.text = string.Empty;
        m_TxtList.text = string.Empty;
        m_infiHealth.text = string.Empty;
        m_infiName.text = string.Empty;
    }
}
