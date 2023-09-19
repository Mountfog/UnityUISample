using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Test017_1Dlg : MonoBehaviour
{
    [SerializeField] InputField m_infiID = null;
    [SerializeField] InputField m_infiName = null;
    [SerializeField] InputField m_infiKor = null;
    [SerializeField] InputField m_infiEng = null;
    [SerializeField] InputField m_infiMath = null;

    [SerializeField] Button m_btnDelete = null;
    [SerializeField] Button m_btnEdit = null;
    [SerializeField] Button m_btnClear = null;
    [SerializeField] Button m_btnAdd = null;
    [SerializeField] Button m_btnFileLoad = null;
    [SerializeField] Button m_BtnFileSave = null;

    [SerializeField] ScrollRect m_scrollRect = null;
    [SerializeField] GameObject m_prefabItem = null;

    List<Student> m_studentsData = new List<Student>();
    List<CItem> m_prefabItems = new List<CItem>();
    CItem m_curSelected = null;

    // Start is called before the first frame update
    void Start()
    {
        OnClick_Clear();
        m_btnAdd.onClick.AddListener(OnClick_Add);
        m_btnEdit.onClick.AddListener(OnClick_Edit);
        m_btnClear.onClick.AddListener(OnClick_Clear);
        m_btnDelete.onClick.AddListener(OnClick_Delete);
        m_btnFileLoad.onClick.AddListener(OnClick_FileLoad);
        m_BtnFileSave.onClick.AddListener(OnClick_FileSave);
    }
    public void OnClick_Add()
    {
        int id = 0 , math = 0, kor = 0, eng = 0;
        if (PossibleCheck(ref id, ref kor, ref eng, ref math))
            return;
        string name = m_infiName.text;
        Student kstudent = new Student(id, name, kor, eng, math);

        m_studentsData.Add(kstudent);
        m_infiID.text = string.Empty;
        m_infiName.text = string.Empty;
        m_infiEng.text = string.Empty;
        m_infiKor.text = string.Empty;
        m_infiMath.text = string.Empty;
        ScrollRectUpdate();
    }
    public void OnClick_Edit()
    {
        if (m_curSelected is null) return;
        int id = 0, math = 0, kor = 0, eng = 0;
        if (PossibleCheck(ref id, ref kor, ref eng, ref math))
            return;
        Student kstudent = m_curSelected.m_curStudent;
        m_studentsData.Remove(kstudent);
        string name = m_infiName.text;
        Student newStudent = new Student(id, name, kor, eng, math);
        m_studentsData.Add(newStudent);
        ScrollRectUpdate();
    }
    public void OnClick_Delete()
    {
        if (m_curSelected is null) return;
        Student kstudent = m_curSelected.m_curStudent;
        m_studentsData.Remove(kstudent);
        ScrollRectUpdate();
    }
    public void OnClick_FileSave()
    {
        FileStream fs = new FileStream("saveinfo1.txt", FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sr = new StreamWriter(fs);
        sr.Flush();
        sr.WriteLine(m_studentsData.Count);
        for (int i = 0; i < m_studentsData.Count; i++)
        {
            Student kstudent = m_studentsData[i];
            sr.WriteLine(kstudent.id);
            sr.WriteLine(kstudent.name);
            sr.WriteLine(kstudent.scoreKor);
            sr.WriteLine(kstudent.scoreEng);
            sr.WriteLine(kstudent.scoreMath);
        }
        sr.Close();
        fs.Close();
    }
    public void OnClick_FileLoad()
    {
        try
        {
            FileStream fs = new FileStream("saveinfo1.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            m_studentsData.Clear();
            int scoreNum = int.Parse(sr.ReadLine());
            for (int i = 0; i < scoreNum; i++)
            {
                int id = int .Parse(sr.ReadLine()); 
                string name = sr.ReadLine();
                int kor = int.Parse(sr.ReadLine());
                int eng = int.Parse(sr.ReadLine());
                int math = int.Parse(sr.ReadLine());
                Student kstudent = new Student(id, name, kor, eng, math);
                m_studentsData.Add(kstudent);
            }
            sr.Close();
            fs.Close();
            ScrollRectUpdate();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    public void OnClick_Clear()
    {
        m_infiID.text = string.Empty;
        m_infiName.text = string.Empty;
        m_infiKor.text = string.Empty;
        m_infiEng.text = string.Empty;
        m_infiMath.text = string.Empty;
        m_studentsData.Clear();
        ScrollRectUpdate();
    }
    public void ScrollRectUpdate()
    {
        foreach(Transform child in m_scrollRect.content)
        {
            Destroy(child.gameObject);
            m_prefabItems.Remove(child.GetComponent<CItem>());
        }
        m_studentsData.Sort((a, b) => a.id > b.id ? 1 : -1);
        for(int i = 0; i < m_studentsData.Count; i++)
        {
            GameObject go = Instantiate(m_prefabItem,m_scrollRect.content);
            CItem item = go.GetComponent<CItem>();
            Student kstudent = m_studentsData[i];
            int idx = i;
            item.Initialize(kstudent, idx);
            item.AddLinster(OnClick_ItemSelect);
            item.OnSelectedColor(false);
            m_prefabItems.Add(item);
        }
    }
    public void OnClick_ItemSelect(int idx)
    {
        foreach(var item in m_prefabItems)
        {
            item.OnSelectedColor(false);
        }
        m_curSelected = m_prefabItems[idx];
        Student curStudent = m_curSelected.m_curStudent;
        m_curSelected.OnSelectedColor(true);
        m_infiID.text = curStudent.id.ToString();
        m_infiName.text = curStudent.name;
        m_infiKor.text = curStudent.scoreKor.ToString();
        m_infiEng.text = curStudent.scoreEng.ToString();
        m_infiMath.text = curStudent.scoreMath.ToString();
    }
    bool PossibleCheck(ref int id, ref int kor, ref int eng, ref int math)
    {
        if (IsEmpty(m_infiName) || IsEmpty(m_infiKor) || IsEmpty(m_infiEng) || IsEmpty(m_infiMath) || IsEmpty(m_infiID))
        {
            return true;
        }
        bool kID = int.TryParse(m_infiID.text, out id);
        bool kkor = int.TryParse(m_infiKor.text, out kor);
        bool keng = int.TryParse(m_infiEng.text, out eng);
        bool kmath = int.TryParse(m_infiMath.text, out math);
        if (!(kmath && kkor && keng && kID))
        {
            return true;
        }
        if ((kor < 0 || eng < 0 || math < 0) || (kor > 100 || eng > 100 || math > 100))
        {
            return true;
        }
        return false;
    }
    public bool IsEmpty(InputField er)
    {
        return string.IsNullOrEmpty(er.text);

    }
}
public class Student
{
    public int id;
    public string name;
    public int scoreKor;
    public int scoreEng;
    public int scoreMath;

    public int Sum { get { return scoreKor + scoreEng + scoreMath; } }
    public float Average { get { return (float)Sum / 3f; } }

    public Student(int kid, string kname, int kscoreKor, int kscoreEng, int kscoreMath)
    {
        id = kid;
        name = kname;
        scoreKor = kscoreKor;
        scoreEng = kscoreEng;
        scoreMath = kscoreMath;
    }
}
