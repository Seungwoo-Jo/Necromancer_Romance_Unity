using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleLog : MonoBehaviour {

    public Text[] m_Log;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void RegisterLog(string log)
    {
        m_Log[2].text = m_Log[1].text;
        m_Log[1].text = m_Log[0].text;
        m_Log[0].text = log;
    }

    public void Log_Encount(string monsterName)
    {
        string log = "「" + monsterName + "」가 나타났다.";
        RegisterLog(log);
    }

    public void Log_DefeatMonster(string monsterName)
    {
        string log = "「" + monsterName + "」를 물리쳤다.";
        RegisterLog(log);
    }

    public void Log_Attack(string attacker, string victim, float damage)
    {
        string log = "「" + attacker + "」가(이) " + "「" + victim + "」에게 " + ((int)damage).ToString() + "의 피해를 입혔다.";
        RegisterLog(log);
    }

    public void Log_Trap(string victim, float damage)
    {
        string log = "「" + victim + "」가(이) 함정으로 " + ((int)damage).ToString() + "의 피해를 입었다.";
        RegisterLog(log);
    }

    public void Log_GetGold(int gold)
    {
        string log = "파티는 " + gold.ToString() + "골드를 얻었다.";
        RegisterLog(log);
    }

    public void Log_GetSoul(int soul)
    {
        string log = "파티는 영혼 " + soul.ToString() + "개를 얻었다.";
        RegisterLog(log);
    }

    public void Log_Die(string deadman)
    {
        string log = "「" + deadman + "」은(는) 사망했다.";
        RegisterLog(log);
    }
}
