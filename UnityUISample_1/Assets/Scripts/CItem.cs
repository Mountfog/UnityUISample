using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CItem : MonoBehaviour
{
    public delegate void DelegateFunc(int kselect);
    public DelegateFunc m_onBtnClick = null;
    public Student m_curStudent = null;

    public Text m_txtID = null;
    public Text m_txtName = null;
    public Text m_txtScoreKor = null;
    public Text m_txtScoreEng = null;
    public Text m_txtScoreMath = null;
    public Text m_txtSum = null;
    public Text m_txtAverage = null;    
    public void AddLinster(DelegateFunc func)
    {
        m_onBtnClick = new DelegateFunc(func);
    }
    private void OnClick_Host(int k)
    {
        if (m_onBtnClick != null)
            m_onBtnClick(k);
    }
    public void Initialize(Student kstudent, int idx)
    {
        m_curStudent = kstudent;
        SetInfo(m_curStudent);
        GetComponent<Button>().onClick.AddListener(()=>OnClick_Host(idx));
    }
    public void SetInfo(Student kstudent)
    {
        m_txtID.text = kstudent.id.ToString();
        m_txtName.text = kstudent.name;
        m_txtScoreKor.text = kstudent.scoreKor.ToString();
        m_txtScoreEng.text = kstudent.scoreEng.ToString();
        m_txtScoreMath.text = kstudent.scoreMath.ToString();
        m_txtSum.text = kstudent.Sum.ToString();
        m_txtAverage.text = kstudent.Average.ToString();
    }
    public void OnSelectedColor(bool kbool)
    {
        GetComponent<Image>().color = (kbool) ? Color.green * 0.5f : Color.white;
    }
}
