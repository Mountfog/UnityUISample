using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Test014Dlg : MonoBehaviour
{

    public GameObject m_itemPrefab = null;
    List<StudentData> m_students = new List<StudentData>();
    List<CItemText014> m_listItem = new List<CItemText014>();
    CItemText014 m_curItem = null;
    public InputField m_infiNum = null;
    public InputField m_infiName = null;
    public InputField m_infiKor = null;
    public InputField m_infiEng = null;
    public InputField m_infiMath = null;
    public Button m_btnAdd = null;
    public Button m_btnEdit = null;
    public Button m_btnRemove = null;
    public Button m_btnRemoveAll = null;
    public Button m_btnFileSave = null;
    public Button m_btnFileLoad = null;
    public ScrollRect m_scrollRect = null;

    void Start()
    {
        m_btnAdd.onClick.AddListener(OnClick_Add);
        m_btnEdit.onClick.AddListener(OnClick_Edit);
        m_btnRemove.onClick.AddListener(OnClick_Remove);
        m_btnRemoveAll.onClick.AddListener(OnClick_RemoveAll);
        m_btnFileLoad.onClick.AddListener(OnClick_FileLoad);
        m_btnFileSave.onClick.AddListener(OnClick_FileSave);
    }

    public void OnClick_Add()
    {
        if (!IsInputGood())
            return;

        int num = int.Parse(m_infiNum.text);
        string name = m_infiName.text;
        int kor = int.Parse(m_infiKor.text);
        int eng = int.Parse(m_infiEng.text);
        int math = int.Parse(m_infiMath.text);

        StudentData kdata = new StudentData(num,name, kor, eng, math);
        GameObject go = Instantiate(m_itemPrefab, m_scrollRect.content);
        CItemText014 kitem = go.GetComponent<CItemText014>();
        kitem.Initialize(kdata);
        m_students.Add(kdata);
        m_listItem.Add(kitem);
        go.GetComponent<Button>().onClick.AddListener(()=>OnClick_Item(kitem));
        ClearInputField();
        ItemSort();
    }
    public void OnClick_Edit()
    {
        if (!IsInputGood()) return;
        if (m_curItem == null) return;
        int num = int.Parse(m_infiNum.text);
        string name = m_infiName.text;
        int kor = int.Parse(m_infiKor.text);
        int eng = int.Parse(m_infiEng.text);
        int math = int.Parse(m_infiMath.text);
        m_students.Remove(m_curItem.curData);
        StudentData kdata = new StudentData(num, name, kor, eng, math);
        m_curItem.Initialize(kdata);
        m_students.Add(kdata);
        ClearInputField();
        ItemSort();
    }
    public void OnClick_Remove()
    {
        if (m_curItem == null) return;
        m_students.Remove(m_curItem.curData);
        m_listItem.Remove(m_curItem);
        Destroy(m_curItem.gameObject);
        ClearInputField();
        ItemSort();
    }
    public void OnClick_RemoveAll()
    {
        for(int i = m_listItem.Count - 1; i >= 0; i--)
        {
            CItemText014 item = m_listItem[i];
            m_students.Remove(item.curData);
            m_listItem.Remove(item);
            Destroy(item.gameObject);
        }
        ClearInputField();

    }
    public void OnClick_FileSave()
    {
        StreamWriter sw = new StreamWriter("saveinfo2.txt");
        sw.Flush();
        sw.WriteLine(m_students.Count);
        for(int i = 0; i < m_students.Count; i++)
        {
            StudentData data = m_students[i];
            int knum = data.id;
            string kname = data.name;
            int kkor = data.scoreKor;
            int keng = data.scoreEng;
            int kmath = data.scoreMath;
            string s = string.Format("{0} {1} {2} {3} {4}",knum,kname,kkor,keng,kmath);
            sw.WriteLine(s);
        }
        sw.Close();
    }
    public void OnClick_FileLoad()
    {
        StreamReader sr = new StreamReader("saveinfo2.txt");
        int count = int.Parse(sr.ReadLine());
        OnClick_RemoveAll();
        for (int i = 0; i < count; i++)
        {
            string[] s = sr.ReadLine().Split(" ");
            int knum = int.Parse(s[0]);
            string kname = s[1];
            int kkor = int.Parse(s[2]);
            int keng = int.Parse(s[3]);
            int kmath = int.Parse(s[4]);
            StudentData data = new StudentData(knum,kname,kkor,keng,kmath);
            GameObject go = Instantiate(m_itemPrefab, m_scrollRect.content);
            CItemText014 kitem = go.GetComponent<CItemText014>();
            kitem.Initialize(data);
            m_students.Add(data);
            m_listItem.Add(kitem);
            go.GetComponent<Button>().onClick.AddListener(() => OnClick_Item(kitem));
        }
        sr.Close();
        ItemSort();
    }
    public void OnClick_Item(CItemText014 item)
    {
        m_curItem = item;
        StudentData data = item.curData;
        m_infiNum.text = data.id.ToString();
        m_infiName.text = data.name;
        m_infiKor.text = data.scoreKor.ToString();
        m_infiEng.text = data.scoreEng.ToString();
        m_infiMath.text = data.scoreMath.ToString();
    }
    public void ClearInputField()
    {
        m_infiNum.text = string.Empty;
        m_infiName.text = string.Empty;
        m_infiKor.text = string.Empty;
        m_infiEng.text = string.Empty;
        m_infiMath.text = string.Empty;
    }
    public bool IsInputGood()
    {
        if (IsInputNull(m_infiNum))
            return false;
        if (IsInputNull(m_infiName))
            return false;
        if (IsInputNull(m_infiKor))
            return false;
        if (IsInputNull(m_infiEng))
            return false;
        if (IsInputNull(m_infiMath))
            return false;
        if (IsParsable(m_infiNum))
            return false;
        if (IsParsable(m_infiKor))
            return false;
        if (IsParsable(m_infiEng))
            return false;
        if (IsParsable(m_infiMath))
            return false;
        return true;
    }
    public bool IsInputNull(InputField infi)
    {
        return string.IsNullOrEmpty(infi.text);
    }
    public bool IsParsable(InputField infi)
    {
        int a = 0;
        return !int.TryParse(infi.text, out a);
    }

    public void ItemSort()
    {
        m_students.Sort((a, b) => a.Sum > b.Sum ? 1 : -1);
        for(int i=0;i < m_students.Count;i++)
        {
            CItemText014 itemText = m_listItem[i];
            itemText.curData = m_students[i];
            itemText.RefreshData();
        }
    }

    public class StudentData
    {
        public int id;
        public string name;
        public int scoreKor;
        public int scoreEng;
        public int scoreMath;

        public StudentData(int kid, string kname, int kscoreKor, int kscoreEng, int kscoreMath)
        {
            Initialize(kid, kname, kscoreKor, kscoreEng, kscoreMath);
        }
        public void Initialize(int kid, string kname, int kscoreKor, int kscoreEng, int kscoreMath)
        {
            id = kid;
            name = kname;
            scoreKor = kscoreKor;
            scoreEng = kscoreEng;
            scoreMath = kscoreMath;

        }

        public int Sum { get { return scoreKor + scoreEng + scoreMath; } }
        public float Average { get { return (float)Sum / 3f; } }
    }
}
