using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class Test017Dlg : MonoBehaviour
{
    public InputField m_infiName = null;
    public InputField m_infiScoreKor = null;
    public InputField m_infiScoreEng = null;
    public InputField m_infiScoreMath = null;
    public Button m_btnResult = null;
    public Button m_btnClear = null;
    public Button m_btnAdd = null;
    public Button m_btnFileLoad = null;
    public Button m_BtnFileSave = null;
    public Text m_txtResult = null;
    public Text m_txtList = null;
    List<CScore> m_listScore = new List<CScore>();
    


    void Start()
    {
        OnClick_Clear();
        m_btnAdd.onClick.AddListener(OnClick_Add);
        m_btnClear.onClick.AddListener(OnClick_Clear);
        m_btnFileLoad.onClick.AddListener(LoadInfo);
        m_BtnFileSave.onClick.AddListener(SaveInfo);
        m_btnResult.onClick.AddListener(OnClick_Result);
    }
    public void OnClick_Add()
    {
        int math = 0, kor = 0, eng = 0;
        if (PossibleCheck(ref kor, ref eng, ref math))
            return;
        string name = m_infiName.text;
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
        m_txtResult.text = "No Name  Kor Eng Math <종합>";
        m_txtResult.text += "\n====================\n";
        m_listScore.Sort((a,b)=>a.Sum < b.Sum ? 1 : -1);
        for(int i = 0; i < m_listScore.Count; i++)
        {
            string s = ResultText(m_listScore[i],i);
            m_txtResult.text += s;
        }
    }
    string ResultText(CScore score, int i)
    {
        string gradeKor = Grade(score.scoreKor);
        string gradeEng = Grade(score.scoreEng);
        string gradeMath = Grade(score.scoreMath);
        string gradeSum = Grade(score.Average);
        string s = string.Format("{0}   {1}   {2}   {3}    {4}        <{5}>\n",i+1,score.name,gradeKor,gradeEng,gradeMath,gradeSum);
        return s;
    }
    string Grade(float score)
    {
        if (score >= 90) return "A";
        else if (score >= 80) return "B";
        else if (score >= 70) return "C";
        else if (score >= 60) return "D";
        else return "F";
    }
    public void OnClick_Clear()
    {
        m_listScore.Clear();
        m_infiName.text = string.Empty;
        m_infiScoreKor.text = string.Empty;
        m_infiScoreEng.text = string.Empty;
        m_infiScoreMath.text = string.Empty;
        m_txtList.text = string.Empty;
        m_txtResult.text = string.Empty;
    }
    public void SaveInfo()
    {
        FileStream fs = new FileStream("saveinfo.txt", FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sr = new StreamWriter(fs);
        sr.Flush();
        sr.WriteLine(m_listScore.Count);
        for (int i = 0; i < m_listScore.Count; i++)
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

    }
    bool PossibleCheck(ref int kor, ref int eng, ref int math)
    {
        if (IsEmpty(m_infiName) || IsEmpty(m_infiScoreKor) || IsEmpty(m_infiScoreEng) || IsEmpty(m_infiScoreMath))
        {
            m_txtResult.text = "값이 입력되지 않았습니다";
            return true;
        }
        bool kkor = int.TryParse(m_infiScoreKor.text, out kor);
        bool keng = int.TryParse(m_infiScoreEng.text, out eng);
        bool kmath = int.TryParse(m_infiScoreMath.text, out math);
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

    public bool IsEmpty(InputField er)
    {
        return string.IsNullOrEmpty(er.text);
        
    }


    public class CScore
    {
        public string name = "";
        public int scoreKor { get; private set; } = 0;
        public int scoreEng { get; private set; } = 0;
        public int scoreMath { get; private set; } = 0;

        public CScore(string kname, int kkor, int keng, int kmath)
        {
            name = kname;
            scoreKor = kkor;
            scoreEng = keng;
            scoreMath = kmath;
        }
        public int Sum
        {
            get { return scoreKor + scoreEng + scoreMath; }
        }
        public float Average
        {
            get {return (float)Sum / 3f;  }
        }
    }
}
