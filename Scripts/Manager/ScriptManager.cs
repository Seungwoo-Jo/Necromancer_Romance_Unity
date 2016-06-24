using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class ScriptManager {

    private static ScriptManager instance;

    public static ScriptManager Instance
    {
        get
        {
            if(instance == null) {
                instance = new ScriptManager();
                instance.Init();
            }
            return instance;
        }
    }
    

    public struct Script
    {
        public int m_Group;
        public Sprite m_Illust;
        public string m_Name;

        public bool m_bRight;

        public string m_Text;
    };

    public Hashtable m_ScriptIllust;
    public List<Script[]> m_Script;

    private int m_TotalNumberOfScript;

    void Init()
    {
        m_ScriptIllust = new Hashtable();
        m_Script = new List<Script[]>();
    }

    public void Load()
    {
        TextAsset ta = Resources.Load("Script") as TextAsset;

        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/Script.txt", Encoding.Default);

        

        string input = sr.ReadLine();
        int NumOfIllust = int.Parse(input);

        for(int i = 0; i < NumOfIllust; i++)
        {
            input = sr.ReadLine();
            m_ScriptIllust.Add(input, Resources.Load<Sprite>("Cards/Illust/Illust_" + input));
            Debug.Log(input + " Illust Load Complete!");
        }

        m_TotalNumberOfScript = int.Parse(sr.ReadLine());

        // 스크립트 읽기
        for(int i = 0; i < m_TotalNumberOfScript; i++)
        {
            int groupNum = int.Parse(sr.ReadLine());
            int numberOfLines = int.Parse(sr.ReadLine());

            Script[] script = new Script[numberOfLines];

            for(int j = 0; j < numberOfLines; j++)
            {
                script[j].m_Group = groupNum;

                input = sr.ReadLine();
                script[j].m_Illust = (Sprite)m_ScriptIllust[input];
                script[j].m_Name = input;

                input = sr.ReadLine();
                if(input.Equals("R")) {
                    script[j].m_bRight = true;
                }
                else {
                    script[j].m_bRight = false;
                }
                
                input = sr.ReadLine();
                
                input = input.Replace("\n", System.Environment.NewLine);

                script[j].m_Text = input;

                Debug.Log(input);
            }

            m_Script.Add(script);
        }

        sr.Close();
    }

    public string EngToKor(string engName)
    {
        string kor;

        if(engName.Equals("Nero")) {
            kor = "네로";
        }
        else if(engName.Equals("John")) {
            kor = "존";
        }
        else if(engName.Equals("Shoa")) {
            kor = "쇼아";
        }
        else if(engName.Equals("God")) {
            kor = "신";
        }
        else if(engName.Equals("Renata")) {
            kor = "레나타";
        }
        else if(engName.Equals("None")) {
            kor = " ";
        }
        else {
            kor = "error";
        }

        return kor;
    }
}
