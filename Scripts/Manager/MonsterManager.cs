using UnityEngine;
using System.Collections;
using System.IO;

public class MonsterManager
{
    private static MonsterManager instance;

    public static MonsterManager Instance
    {
        get
        {
            if(instance == null) {
                instance = new MonsterManager();
                instance.Init();
            }
            return instance;
        }
    }

    public Hashtable m_Monsters;

    void Init()
    {
        m_Monsters = new Hashtable();
    }

    public void Load()
    {
        StreamReader sr;

        for(int i = 0; i < 8; i++)
        {
            TextAsset ta = Resources.Load("Monster_" + i.ToString()) as TextAsset;
            //sr = new StreamReader(Application.dataPath + "/Resources/Monster_" + i.ToString() + ".json");

            Monster _NewMonster = JsonUtility.FromJson<Monster>(ta.text);
            _NewMonster.m_Sprite = Resources.Load<Sprite>("Monsters/Monster_" + _NewMonster.m_Name);
            _NewMonster.m_Code = i;

            //sr.Close();

            m_Monsters.Add(_NewMonster.m_Name, _NewMonster);

            Debug.Log(_NewMonster.m_Name + " Load Complete");
        }
    }
}
