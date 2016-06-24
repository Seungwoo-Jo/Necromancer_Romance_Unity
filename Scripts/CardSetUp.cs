using UnityEngine;
using System.Collections;

public class CardSetUp : ScriptableObject
{
    // Name
    public string m_Name;

    // Status
    public int m_Level;

    public float m_MaxHp;
    public float m_MaxAp;

    public float m_Hp;
    public float m_Ap;

    public float m_Atk;
    public float m_Def;
    public float m_Spd;

    public float m_Attribute;

    public bool m_Bought;

    // Growth Value
    public float m_MaxHpGrowth;
    public float m_MaxApGrowth;

    public float m_AtkGrowth;
    public float m_DefGrowth;

    // Skill
    public Skill[] m_Skill;

    // Explain
    public string m_Home;
    public string m_Explain;

    // Chat
    public string[] m_Chat;
	
}
