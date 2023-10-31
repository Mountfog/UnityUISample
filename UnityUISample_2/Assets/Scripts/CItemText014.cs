using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Test014Dlg;

public class CItemText014 : MonoBehaviour
{
    public Text m_textNum = null;
    public Text m_textName = null;
    public Text m_textKor = null;
    public Text m_textEng = null;
    public Text m_textMath = null;
    public Text m_textSum = null;
    public Text m_textAverage = null;
    public Test014Dlg.StudentData curData = null;

    public void Initialize(Test014Dlg.StudentData studentData)
    {
        curData = studentData;
        m_textNum.text = curData.id.ToString();
        m_textName.text = curData.name;
        m_textKor.text = curData.scoreKor.ToString();
        m_textEng.text = curData.scoreEng.ToString();
        m_textMath.text = curData.scoreMath.ToString();
        m_textSum.text = curData.Sum.ToString();
        m_textAverage.text = string.Format("{0:00.00}", curData.Average);

    }
    public void RefreshData()
    {
        m_textNum.text = curData.id.ToString();
        m_textName.text = curData.name;
        m_textKor.text = curData.scoreKor.ToString();
        m_textEng.text = curData.scoreEng.ToString();
        m_textMath.text = curData.scoreMath.ToString();
        m_textSum.text = curData.Sum.ToString();
        m_textAverage.text = string.Format("{0:00.00}", curData.Average);
    }
}
