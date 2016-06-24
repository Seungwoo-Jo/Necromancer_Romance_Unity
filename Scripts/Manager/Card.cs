using UnityEngine;
using System.Collections;


public class Card
{
    // Name
    public string m_Name;
    public string m_KoreanName;

    // Status
    public int m_Level;

    public float m_MaxHp;
    public float m_MaxAp;

    public float m_Hp;
    public float m_Ap;

    public float m_Atk;
    public float m_Def;
    public float m_Spd;

    public int m_Attribute;

    public bool m_Bought;

    // Growth Value
    public float m_MaxHpGrowth;
    public float m_MaxApGrowth;

    public float m_AtkGrowth;
    public float m_DefGrowth;

    // Skill
    public string[] m_SkillName;

    // Explain
    public string m_Home;
    public string m_Explain;

    // Chat
    public string[] m_Chat;

    // Sprite
    public Sprite m_SlotSprite;
    public Sprite m_FullSprite;
    public Sprite m_IllustSprite;

    // Code
    public int m_Code;
}
