using UnityEngine;
using System.Collections;

public class GameManager
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if(instance == null) {
                instance = new GameManager();
                instance.Init();
            }
            return instance;
        }
    }

    public Hashtable m_HaveCards;   // 구매한 카드들
    public string[] m_Party;     // 파티로 지정한 카드 이름

    public int m_Gold;
    public int m_Soul;
    public int m_Escape;

    public int m_EnterInnNum;

    public int m_RecruitCost;

    public float m_Stage;

    public float m_HeroProg;
    public float m_NeroProg;

    public bool m_Lost;
    public bool m_TownTut;
    public bool m_InnTut;

    public int m_Boss;
    public bool m_ClearLupus;
    public bool m_ClearSuccubus;
    public bool m_ClearShoa;
    public bool m_ClearGod;

    void Init()
    {
        m_HaveCards = new Hashtable();
    }

    public void Load()
    {
        CardManager.Instance.Load();
        MonsterManager.Instance.Load();
        SkillManager.Instance.Load();
        ScriptManager.Instance.Load();

        Card nero = (Card)CardManager.Instance.m_Cards["Nero"];
        Card john = (Card)CardManager.Instance.m_Cards["John"];

        m_HaveCards.Add(nero.m_Name, nero);
        m_HaveCards.Add(john.m_Name, john);
        
        m_Party = new string[4];

        m_Party[0] = nero.m_Name;
        m_Party[1] = john.m_Name;
        m_Party[2] = "";
        m_Party[3] = "";

        m_Gold = 1000;
        m_Soul = 1;
        m_Escape = 0;
        m_EnterInnNum = 0;

        m_RecruitCost = 100;

        m_Stage = 1.0f;

        m_HeroProg = 2.0f;
        m_NeroProg = 1.0f;

        m_Lost = false;
        m_TownTut = false;
        m_InnTut = false;

        m_Boss = 0;

        m_ClearLupus = false;
        m_ClearSuccubus = false;
        m_ClearShoa = false;
        m_ClearGod = false;
    }

    public void AddCard(string name)
    {
        Card card = (Card)CardManager.Instance.m_Cards[name];
        m_HaveCards.Add(card.m_Name, card);
    }

    public void AddParty(string name, int slotNum)
    {
        if(slotNum >= 0 || slotNum <= 3)
        {
            m_Party[slotNum] = name;
        }
        else
        {
            Debug.Log("AddParty() Out Of Bound");
        }
    }

    public Card GetParty(string name)
    {
        Card ret = null;

        foreach(string n in m_Party)
        {
            if(n == name) {
                ret = (Card)m_HaveCards[name];
            }
        }

        if(ret == null) {
            Debug.Log(name + " GetParty(string) Return Value is Null");
        }

        return ret;
    }

    public Card GetParty(int slotNum)
    {
        Card ret = null;

        if(slotNum >= 0 && slotNum <= 3)
        {
            if(m_HaveCards.ContainsKey(m_Party[slotNum]))
            {
                ret = (Card)m_HaveCards[m_Party[slotNum]];
            }
        }
        else {
            Debug.Log("GetParty(int) Out Of Bound");
        }

        if(ret == null) {
            //Debug.Log("GetParty(string) Return Value is Null");
        }

        return ret;
    }

    public void Restore()
    {
        foreach(DictionaryEntry pair in m_HaveCards)
        {
            Card card = (Card)pair.Value;

            card.m_Hp = card.m_MaxHp;
            card.m_Ap = card.m_MaxAp;
        }
    }
}

