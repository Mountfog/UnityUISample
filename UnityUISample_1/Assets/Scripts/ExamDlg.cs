using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExamDlg : MonoBehaviour
{
    [SerializeField] InputField m_infiName = null;
    [SerializeField] InputField m_infiPhoneNum = null;
    [SerializeField] InputField m_infiCity = null;
    [Space(10f)]
    [SerializeField] Button m_btnAdd = null;
    [SerializeField] Button m_btnPrintAll = null;
    [SerializeField] Button m_btnClear = null;
    [SerializeField] Button m_btnLoad = null;
    [SerializeField] Button m_btnSave = null;
    [Space(10f)]
    [SerializeField] Button m_btnSearch = null;
    [SerializeField] Dropdown m_dropDownSearch = null;
    [SerializeField] InputField m_infiSearch = null;
    [Space(10f)]
    [SerializeField] Text m_txtResult = null;


    List<Person> m_listPersonData = new List<Person>();
    // Start is called before the first frame update
    void Start()
    {
        OnClick_Clear();
        m_btnAdd.onClick.AddListener(OnClick_Add);
        m_btnClear.onClick.AddListener(OnClick_Clear);
        m_btnPrintAll.onClick.AddListener(() => OnClick_PrintAll(m_listPersonData));
        m_btnLoad.onClick.AddListener(OnClick_Load);
        m_btnSave.onClick.AddListener(OnClick_Save);
        m_btnSearch.onClick.AddListener(OnClick_Search);
    }
    public void OnClick_Add()
    {
        if (!PossibleCheck())
            return;
        string name = m_infiName.text;
        string num = m_infiPhoneNum.text;
        string city = m_infiCity.text;
        Person kperson = new Person(name,num, city);
        m_listPersonData.Add(kperson);
        m_infiName.text = string.Empty;
        m_infiPhoneNum.text = string.Empty;
        m_infiCity.text = string.Empty;
    }
    bool PossibleCheck()
    {
        string phone = m_infiPhoneNum.text;
        if (phone.Length != 13)
            return false;
        if (!char.IsDigit(phone[0])) return false;
        if (!char.IsDigit(phone[1])) return false;
        if (!char.IsDigit(phone[2])) return false;
        if (phone[3] != '-') return false;
        if (!char.IsDigit(phone[4])) return false;
        if (!char.IsDigit(phone[5])) return false;
        if (!char.IsDigit(phone[6])) return false;
        if (!char.IsDigit(phone[7])) return false;
        if (phone[8] != '-') return false;
        if (!char.IsDigit(phone[9])) return false;
        if (!char.IsDigit(phone[10])) return false;
        if (!char.IsDigit(phone[11])) return false;
        if (!char.IsDigit(phone[12])) return false;
        if (IsEmpty(m_infiPhoneNum) || IsEmpty(m_infiName) || IsEmpty(m_infiCity))
            return false;
        else
            return true;
    }
    bool IsEmpty(InputField field)
    {
        return string.IsNullOrEmpty(field.text);
    }
    public void OnClick_PrintAll(List<Person> listPerson)
    {
        m_txtResult.text = "순번  이름     전화번호       도시";
        m_txtResult.text += "\n=========================\n";
        List<Person> a = listPerson.OrderBy(x => x.name).ToList();
        listPerson = a;
        for(int i = 0; i < listPerson.Count; i++)
        {
            Person kperson = listPerson[i];
            string name = kperson.name;
            string phonenum = kperson.phoneNum;
            string city = kperson.city;
            m_txtResult.text += string.Format("  {0}   {1}  {2} {3}\n",i+1 ,name ,phonenum ,city);
            m_txtResult.text += "--------------------------------------------\n";
        }
    }
    public void OnClick_Clear()
    {
        m_listPersonData.Clear();
        m_txtResult.text = string.Empty;
        m_infiCity.text = string.Empty;
        m_infiName.text = string.Empty;
        m_infiPhoneNum.text = string.Empty;
        m_infiSearch.text = string.Empty;
    }
    public void OnClick_Save()
    {
        FileStream fs = new FileStream("examData.txt", FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        sw.Flush();
        sw.WriteLine(m_listPersonData.Count);
        for(int i = 0; i < m_listPersonData.Count; i++)
        {
            Person kperson = m_listPersonData[i];
            sw.WriteLine(kperson.name);
            sw.WriteLine(kperson.phoneNum);
            sw.WriteLine(kperson.city);
        }
        sw.Close();
        fs.Close();
    }
    public void OnClick_Load()
    {
        FileStream fs = new FileStream("examData.txt", FileMode.Open, FileAccess.Read);
        m_listPersonData.Clear();
        m_txtResult.text = string.Empty;
        StreamReader sr = new StreamReader(fs);
        int count = int.Parse(sr.ReadLine());
        for(int i=0;i<count; i++) 
        {
            string name = sr.ReadLine();
            string phoneNum = sr.ReadLine();    
            string city = sr.ReadLine();
            Person kperson = new Person(name, phoneNum, city);
            m_listPersonData.Add(kperson);
        }
        OnClick_PrintAll(m_listPersonData);
        sr.Close();
        fs.Close();
    }
    public void OnClick_Search()
    {
        if (IsEmpty(m_infiSearch)) return;

        List<Person> personSearch = new List<Person>();
        string search = m_infiSearch.text;
        foreach(Person kperson in  m_listPersonData)
        {
            if (IsSearch("이름"))
            {
                string name = kperson.name;
                if (name.Contains(search))
                {
                    personSearch.Add(kperson);
                }
            }
            else if (IsSearch("번호"))
            {
                string phoneNum = kperson.phoneNum;
                if (phoneNum.Contains(search))
                {
                    personSearch.Add(kperson);
                }
            }
            else if (IsSearch("도시"))
            {
                string city = kperson.city;
                if (city.Contains(search))
                {
                    personSearch.Add(kperson);
                }
            }
        }
        OnClick_PrintAll(personSearch);
    }
    bool IsSearch(string s)
    {
        int value = m_dropDownSearch.value;
        return m_dropDownSearch.options[value].text == s;
    }
    public class Person
    {
        public string name;
        public string phoneNum;
        public string city;

        public Person(string kname, string knum, string kcity)
        {
            name = kname;
            phoneNum = knum;
            city = kcity;
        }
    }
}
