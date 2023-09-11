using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human
{
    public int health = 0;
    public int power = 0;
    public Human(int khealth,int kpower)
    {
        health = khealth;
        power = kpower;
    }
}
public class TestHomework : MonoBehaviour
{
    public Text m_txtResult = null;
    public Button m_btnResult = null;
    public Button m_btnClear = null;
    public Human m_Master = null;
    public Human m_Enemy = null;


    private void Start()
    {
        m_txtResult.text = string.Empty;
        m_btnResult.onClick.AddListener(OnClick_Result);
        m_btnClear.onClick.AddListener(OnClick_Clear);
    }
    public void OnClick_Result()
    {
        m_Master = new Human(5000,100);
        m_Enemy = new Human(2000, 200);
        m_txtResult.text = string.Format("[�⺻ HP={0}, Attack={1}]\n", m_Master.health,m_Master.power);
        m_txtResult.text += string.Format("masterHp = {0}\n",m_Master.health);
        m_txtResult.text += "[������ 50 ����]\n";
        m_Master.health -= 50;
        m_txtResult.text += string.Format("masterHp = {0}\n", m_Master.health);
        m_txtResult.text += "---------------------------------------------\n";
        m_txtResult.text += string.Format("[�� HP={0}, Attack={1}]\n", m_Enemy.health, m_Enemy.power);
        m_txtResult.text += string.Format("EnemyHP = {0}\n", m_Enemy.health);
        m_txtResult.text += "[���� �����Ϳ��� ���� ����]\n";
        m_Enemy.health -= m_Master.power;
        m_txtResult.text += string.Format("EnemyHP = {0}\n", m_Enemy.health);
        m_txtResult.text += "---------------------------------------------\n";
        m_txtResult.text += "[�������� HP 100��ŭ ���� ��]\n";
        m_Master.health += 100;
        m_txtResult.text += string.Format("masterHp = {0}\n", m_Master.health);
        m_txtResult.text += "[���� HP 200��ŭ ���� ��]\n";
        m_Enemy.health += 200;
        m_txtResult.text += string.Format("EnemyHP = {0}\n", m_Enemy.health);
    }
    /*
     * [�⺻ HP=5000, Attack=100]
masterHP = 5000
[������ 50 ����]
masterHP = 4950
------------------------------------------------
[�� HP=2000, Attack=200���� ����]
EnemyHP = 2000
[���� �����Ϳ��� ���� ����]
EnemyHP = 1900
------------------------------------------------
[�������� HP 100��ŭ ���� ��]
MasterHP = 5050
[���� HP 200��ŭ ���� ��]
EnemyHP = 2100
     */
    public void OnClick_Clear()
    {
        m_txtResult.text = string.Empty;
    }
}
