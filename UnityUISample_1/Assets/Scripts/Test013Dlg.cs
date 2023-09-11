using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CScore
{
    public string name;
    public int scoreKor;
    public int scoreEng;
    public int scoreMath;

    public CScore(string kname, int kscoreKor, int kscoreEng, int kscoreMath)
    {
        this.name = kname;
        this.scoreKor = kscoreKor;
        this.scoreEng = kscoreEng;
        this.scoreMath = kscoreMath;
    }

    public int Sum { get { return scoreEng + scoreKor + scoreMath; } }
    public float Average { get { return (float)Sum/3f; } }
}
public class Test013Dlg : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClick_Result);
        m_btnClear.onClick.AddListener(OnClick_Clear);
        m_btnAdd.onClick.AddListener(OnClick_Add);
        OnClick_Clear();   
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
        int math = int.Parse (m_infiScoreMath.text);
        if (kor < 0 || eng < 0 || math < 0)
            return;
        if (kor > 100 || eng > 100 || math > 100)
            return;
        CScore kscore = new CScore(name,kor, eng, math);
        m_listScore.Add(kscore);
        m_txtList.text += string.Format("{0}({1}, {2}, {3})\n",name, kor, eng, math);
        m_infiName.text = string.Empty;
        m_infiScoreEng.text = string.Empty;
        m_infiScoreKor.text = string.Empty;
        m_infiScoreMath.text = string.Empty;
    }
    public void OnClick_Result()
    {
        m_listScore.Sort((a,b) => a.Sum > b.Sum ? 1 : -1);
        for(int i = 0; i < m_listScore.Count; i++)
        {
            CScore kscore = m_listScore[i];
            string s = string.Format("{0} : 국어({1}), 영어({2}), 수학({3})\n",kscore.name,kscore.scoreKor,kscore.scoreEng,kscore.scoreMath);
            s += string.Format("합계 : {0}, 평균 : {1:00.00}\n-------------------\n",kscore.Sum,kscore.Average);
            m_txtResult.text += s;
        }
    }
    public void OnClick_Clear()
    {
        m_infiName.text = string.Empty;
        m_infiScoreEng.text = string.Empty;
        m_infiScoreKor.text = string.Empty;
        m_infiScoreMath.text = string.Empty;
        m_listScore.Clear();
        m_txtResult.text = string.Empty;
        m_txtList.text = string.Empty;
    }
}
