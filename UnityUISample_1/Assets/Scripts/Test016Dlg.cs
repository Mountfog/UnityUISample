using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

public class Test016Dlg : MonoBehaviour
{
    public InputField m_ifName = null;
    public InputField m_ifScoreKor = null;
    public InputField m_ifScoreEng = null;
    public InputField m_ifScoreMath = null;
    public Button m_btnAdd = null;
    public Button m_btnResult = null;
    public Button m_btnClear = null;
    public Button m_BtnSave = null;
    public Button m_BtnLoad = null;
    public Text m_txtResult = null;
    public Text m_txtList = null;
    List<CScore> m_listScore = new List<CScore>();

    // Start is called before the first frame update
    void Start()
    {
        OnClick_Clear();
        m_btnAdd.onClick.AddListener(OnClick_Add);
        m_btnResult.onClick.AddListener(OnClick_Result);
        m_btnClear.onClick.AddListener(OnClick_Clear);
        m_BtnSave.onClick.AddListener(SaveInfo);
        m_BtnLoad.onClick.AddListener(LoadInfo);
    }
    public void OnClick_Add()
    {
        int math = 0, kor = 0, eng = 0;
        if (PossibleCheck(ref kor, ref eng, ref math))
            return;
        string name = m_ifName.text;
        CScore kscore = new CScore(name, kor, eng, math);
        m_listScore.Add(kscore);
        m_txtList.text += string.Format("{0}({1}, {2}, {3})\n", name, kor, eng, math);
        m_ifName.text = string.Empty;
        m_ifScoreEng.text = string.Empty;
        m_ifScoreKor.text = string.Empty;
        m_ifScoreMath.text = string.Empty;
    }
    public void OnClick_Result()
    {
        m_listScore.Sort((a,b)=> a.Sum >  b.Sum ? 1 : -1);
        string s = string.Empty;
        int sumKor = 0, sumEng = 0, sumMath = 0;
        for (int i = 0; i < m_listScore.Count; i++)
        {
            sumKor += m_listScore[i].scoreKor;
            sumEng += m_listScore[i].scoreEng;
            sumMath += m_listScore[i].scoreMath;
        }
        ResultText(ref s, sumKor, sumEng, sumMath);
        m_txtResult.text = s;
    }
    public void OnClick_Clear()
    {
        m_ifName.text = string.Empty;
        m_ifScoreEng.text = string.Empty;
        m_ifScoreKor.text = string.Empty;
        m_ifScoreMath.text = string.Empty;
        m_listScore.Clear();
        m_txtList.text = string.Empty;
        m_txtResult.text = string.Empty;
    }

    public void ResultText(ref string s, int sumKor, int sumEng, int sumMath)
    {
        if (m_listScore.Count < 3)
            return;
        for (int i = 0; i < 3; i++)
        {
            CScore kscore = m_listScore[i];
            s += string.Format("{0}등 : {1} ", i + 1, kscore.name);
            s += string.Format("(국어({0}), 영어({1}), 수학({2}))\n", kscore.scoreKor, kscore.scoreEng, kscore.scoreMath);
            s += string.Format("합계 : {0}, 평균 : {1:00.00}\n", kscore.Sum, kscore.Average);
            s += "-----------------\n";
        }
        s += "\n";
        s += string.Format("[과목별 통계]\n국어 합계 : {0} 평균 : {1:00.00}\n", sumKor, (float)sumKor / 3f);
        s += string.Format("영어 합계 : {0} 평균 : {1:00.00}\n", sumEng, (float)sumEng / 3f);
        s += string.Format("수학 합계 : {0} 평균 : {1:00.00}\n", sumMath, (float)sumMath / 3f);
    }
    bool PossibleCheck(ref int kor, ref int eng, ref int math)
    {
        if(IsEmpty(m_ifName) || IsEmpty(m_ifScoreKor) || IsEmpty(m_ifScoreEng) || IsEmpty(m_ifScoreMath))
        {
            m_txtResult.text = "값이 입력되지 않았습니다";
            return true;
        }
        bool kkor = int.TryParse(m_ifScoreKor.text, out kor);
        bool keng = int.TryParse(m_ifScoreEng.text, out eng);
        bool kmath = int.TryParse(m_ifScoreMath.text, out math);
        if (!kmath || !kkor || !keng)
        {
            m_txtResult.text = "숫자를 입력해 주십시오";
            return true;
        }
        if ((kor < 0 || eng < 0 || math < 0) || (kor > 100 || eng > 100 || math > 100))
        {
            m_txtResult.text = "범위 안의 숫자를 입력해 주십시오";
            return true;
        }
        return false;
    }

    bool IsEmpty(InputField infi)
    {
        return (string.IsNullOrEmpty(infi.text));
    }
    public void SaveInfo()
    {
        FileStream fs = new FileStream("saveinfo.txt",FileMode.OpenOrCreate,FileAccess.Write); 
        StreamWriter sr = new StreamWriter(fs);
        sr.Flush();
        sr.WriteLine(m_listScore.Count);
        for(int i=0; i<m_listScore.Count; i++)
        {
            CScore score = m_listScore[i];
            sr.WriteLine(score.name);
            sr.WriteLine(score.scoreKor);
            sr.WriteLine(score.scoreEng);
            sr.WriteLine(score.scoreMath);
        } 
        sr.Close();
        fs.Close();
    }
    public void LoadInfo()
    {
        try
        {
            FileStream fs = new FileStream("saveinfo.txt",FileMode.OpenOrCreate,FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            m_listScore.Clear();
            m_txtList.text = string.Empty;
            m_txtResult.text = string.Empty;
            int scoreNum = int.Parse(sr.ReadLine());
            for(int i=0; i < scoreNum; i++)
            {
                string name = sr.ReadLine();
                int kor = int.Parse(sr.ReadLine());
                int eng = int.Parse(sr.ReadLine());
                int math = int.Parse(sr.ReadLine());
                CScore score = new CScore(name, kor, eng, math);
                m_listScore.Add(score);
                m_txtList.text += string.Format("{0}({1}, {2}, {3})\n", name, kor, eng, math);
            }
            sr.Close();
            fs.Close();
            OnClick_Result();
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
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
