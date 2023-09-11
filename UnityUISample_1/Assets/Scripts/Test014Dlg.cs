using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Test014Dlg : MonoBehaviour
{
    public InputField m_infiName = null;
    public InputField m_infiScoreKor = null;
    public InputField m_infiScoreEng = null;
    public InputField m_infiScoreMath = null;
    public Button m_btnAdd = null;
    public Button m_btnResult = null;
    public Button m_btnClear = null;
    public Text m_txtResult = null;
    public Text m_txtList = null;
    List<CScore> m_listScore = new List<CScore>();

    void Start()
    {
        OnClick_Clear();
        m_btnAdd.onClick.AddListener(OnClick_Add);
        m_btnResult.onClick.AddListener (OnClick_Result);
        m_btnClear.onClick.AddListener(OnClick_Clear);
    }

    public void OnClick_Add()
    {
        if (m_listScore.Count >= 3)
            return;
        if (string.IsNullOrEmpty(m_infiName.text) || string.IsNullOrEmpty(m_infiScoreKor.text) || string.IsNullOrEmpty(m_infiScoreEng.text) || string.IsNullOrEmpty(m_infiScoreMath.text))
            return;
        string name = m_infiName.text;
        int kor = int.Parse(m_infiScoreKor.text);
        int eng = int.Parse(m_infiScoreEng.text);
        int math = int.Parse(m_infiScoreMath.text);
        if (kor < 0 || eng < 0 || math < 0)
            return;
        if (kor > 100 || eng > 100 || math > 100)
            return;
        CScore kscore = new CScore(name, kor, eng, math);
        m_listScore.Add(kscore);
        m_txtList.text += string.Format("{0}({1}, {2}, {3})\n", name, kor, eng, math);
        m_infiName.text = string.Empty;
        m_infiScoreEng.text = string.Empty;
        m_infiScoreKor.text = string.Empty;
        m_infiScoreMath.text = string.Empty;
    }
    public void OnClick_Result()
    {
        if (m_listScore.Count < 3) return;
        m_listScore.Sort((a, b) => a.Sum < b.Sum ? 1 : -1);
        string s = string.Empty;
        int sumKor = 0, sumEng = 0, sumMath = 0;
        for(int i = 0; i < m_listScore.Count; i++)
        {
            sumKor += m_listScore[i].scoreKor;
            sumEng += m_listScore[i].scoreEng;
            sumMath += m_listScore[i].scoreMath;
        }
        ResultText(ref s, sumKor,sumEng,sumMath);
        m_txtResult.text = s;
    }
    public void ResultText(ref string s, int sumKor, int sumEng, int sumMath)
    {
        for (int i = 0; i < 3; i++)
        {
            CScore kscore = m_listScore[i];
            s += string.Format("{0}등 : {1} ", i + 1, kscore.name);
            s += string.Format("(국어({0}), 영어({1}), 수학({2}))\n", kscore.scoreKor, kscore.scoreEng, kscore.scoreMath);
            s += string.Format("합계 : {0}, 평균 : {1:00.00}\n", kscore.Sum, kscore.Average);
        }
        s += "=======================\n";
        s += string.Format("[과목별 통계]\n국어 합계 : {0} 평균 : {1:00.00}\n", sumKor, (float)sumKor/3f);
        s += string.Format("영어 합계 : {0} 평균 : {1:00.00}\n", sumEng, (float)sumEng / 3f);
        s += string.Format("수학 합계 : {0} 평균 : {1:00:00}\n", sumMath, (float)sumMath / 3f);
    }
    public void OnClick_Clear()
    {
        m_infiName.text = string.Empty;
        m_infiScoreEng.text = string.Empty;
        m_infiScoreKor.text = string.Empty;
        m_infiScoreMath.text = string.Empty;
        m_listScore.Clear();
        m_txtList.text = string.Empty;
        m_txtResult.text = string.Empty;
    }



    public class CScore
    {
        public string name = string.Empty;
        public int scoreKor = 0;
        public int scoreEng = 0;
        public int scoreMath = 0;
        public CScore(string kname, int kscoreKor, int kscoreEng, int kscoreMath)
        {
            name = kname;
            scoreKor = kscoreKor;
            scoreEng = kscoreEng;
            scoreMath = kscoreMath;
        }
        public int Sum { get { return scoreKor + scoreEng + scoreMath; } }
        public float Average { get { return (float)Sum / 3; } }
    }
}
