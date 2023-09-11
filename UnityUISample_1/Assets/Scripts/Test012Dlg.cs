using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cscore
{
    public string name = string.Empty;
    public int score_kor = 0;
    public int score_eng = 0;
    public int score_math = 0;

    public Cscore(string kname, int kscore_kor, int kscore_eng, int kscore_math)
    {
        name = kname;
        score_kor = kscore_kor;
        score_eng = kscore_eng;
        score_math = kscore_math;
    }

    public int Sum { get { return score_kor + score_eng + score_math; } }
    public float Average { get { return (float)Sum / 3f; } }
}
public class Test012Dlg : MonoBehaviour
{
    public Button m_btnResult = null;
    public Button m_btnClear = null;
    public Text m_txtResult = null;

    public InputField m_infiName = null;
    public InputField m_infiKor = null;
    public InputField m_infiEng = null;
    public InputField m_infiMath = null;

    Cscore curScore = null;
    // Start is called before the first frame update
    void Start()
    {
        OnClick_Clear();
        m_btnResult.onClick.AddListener(OnClick_Result);
        m_btnClear.onClick.AddListener(OnClick_Clear);
    }

    public void OnClick_Result()
    {
        string name = m_infiName.text;
        if (string.IsNullOrEmpty(m_infiName.text) || string.IsNullOrEmpty(m_infiKor.text)||string.IsNullOrEmpty(m_infiEng.text)||string.IsNullOrEmpty(m_infiMath.text))
            return;
        int scoreKor = int.Parse(m_infiKor.text);
        int scoreEng = int.Parse(m_infiEng.text);
        int scoreMath = int.Parse(m_infiMath.text);
        if(scoreEng < 0 || scoreKor < 0 || scoreMath < 0)
            return;
        if(scoreEng > 100 || scoreKor > 100 || scoreMath > 100) 
            return;
        curScore = new Cscore(name,scoreKor,scoreEng,scoreMath);
        string s = string.Format("이름 : {0}\n국어 점수 : {1} 영어 점수 : {2} 수학 점수 : {3}\n",curScore.name,curScore.score_kor, curScore.score_eng, curScore.score_math);
        s += string.Format("합계 : {0} 평균 : {1:00.00}", curScore.Sum, curScore.Average);
        m_txtResult.text = s;
    }
    public void OnClick_Clear()
    {
        curScore = null;
        m_txtResult.text = string.Empty;
    }
}
