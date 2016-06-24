using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class ScriptActivator : MonoBehaviour {

    public Text m_CharacterName;
    public Text m_Chat;
    public Image m_LeftIllustMold;
    public Image m_RightIllustMold;

    public GameObject[] m_DisableOnScripting;

    private int m_CurScriptNum;
    private int m_CurLine;

    public UnityEvent m_ScriptActivateEvent;
    public UnityEvent m_ScriptEndEvent;

	void Awake ()
    {
        m_CurLine = -1;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 720.0f);
	}
	

    public void ScriptActivate(int scriptNum)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 0.0f);

        m_CurScriptNum = scriptNum;
        m_CurLine = 0;

        m_CharacterName.text = ScriptManager.Instance.m_Script[m_CurScriptNum][m_CurLine].m_Name;
        m_CharacterName.text = ScriptManager.Instance.EngToKor(m_CharacterName.text);
        m_Chat.text = ScriptManager.Instance.m_Script[m_CurScriptNum][m_CurLine].m_Text;

        if(ScriptManager.Instance.m_Script[m_CurScriptNum][m_CurLine].m_bRight)
        {
            m_RightIllustMold.sprite = ScriptManager.Instance.m_Script[m_CurScriptNum][m_CurLine].m_Illust;
            m_LeftIllustMold.color = Color.grey;
            m_RightIllustMold.color = Color.white;
        }
        else
        {
            m_LeftIllustMold.sprite = ScriptManager.Instance.m_Script[m_CurScriptNum][m_CurLine].m_Illust;
            m_LeftIllustMold.color = Color.white;
            m_RightIllustMold.color = Color.grey;
        }

        foreach(GameObject obj in m_DisableOnScripting)
        {
            obj.SetActive(false);
        }

        m_ScriptActivateEvent.Invoke();
    }

    public void NextScript()
    {
        m_CurLine++;;

        if(m_CurLine < ScriptManager.Instance.m_Script[m_CurScriptNum].Length)
        {
            m_CharacterName.text = ScriptManager.Instance.m_Script[m_CurScriptNum][m_CurLine].m_Name;
            m_CharacterName.text = ScriptManager.Instance.EngToKor(m_CharacterName.text);
            m_Chat.text = ScriptManager.Instance.m_Script[m_CurScriptNum][m_CurLine].m_Text;

            if(ScriptManager.Instance.m_Script[m_CurScriptNum][m_CurLine].m_bRight)
            {
                m_RightIllustMold.sprite = ScriptManager.Instance.m_Script[m_CurScriptNum][m_CurLine].m_Illust;
                m_LeftIllustMold.color = Color.grey;
                m_RightIllustMold.color = Color.white;
            }
            else
            {
                m_LeftIllustMold.sprite = ScriptManager.Instance.m_Script[m_CurScriptNum][m_CurLine].m_Illust;
                m_LeftIllustMold.color = Color.white;
                m_RightIllustMold.color = Color.grey;
            }
        }
        else
        {
            ScriptEnd();
        } 
    }

    public void ScriptEnd()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 720.0f);

        foreach(GameObject obj in m_DisableOnScripting)
        {
            obj.SetActive(true);
        }

        m_ScriptEndEvent.Invoke();
    }
}
