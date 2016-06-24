using UnityEngine;
using System.Collections;
using System.IO;

public class SkillManager {

    private static SkillManager instance;

    public static SkillManager Instance
    {
        get
        {
            if(instance == null) {
                instance = new SkillManager();
                instance.Init();
            }
            return instance;
        }
    }

    public Hashtable m_Skills;

    void Init()
    {
        m_Skills = new Hashtable();
    }

    public void Load()
    {
        StreamReader sr;

        for(int i = 0; i < 26; i++)
        {
            TextAsset ta = Resources.Load("Skill_" + i.ToString()) as TextAsset;
            //sr = new StreamReader(Application.dataPath + "/Resources/Skill_" + i.ToString() + ".json");

            Skill _NewSkill = JsonUtility.FromJson<Skill>(ta.text);

            _NewSkill.m_Code = i;

            //sr.Close();

            m_Skills.Add(_NewSkill.m_Name, _NewSkill);

            Debug.Log(_NewSkill.m_Name + " Load Complete");
        }
    }

    public Skill GetSkill(string name)
    {
        Skill skill = (Skill)m_Skills[name];

        return skill;
    }
}

