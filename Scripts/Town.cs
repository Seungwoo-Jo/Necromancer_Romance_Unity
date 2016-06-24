using UnityEngine;
using System.Collections;

public class Town : MonoBehaviour {

    public GameObject m_TownChat;

    void Start()
    {
        if(GameManager.Instance.m_TownTut)
        {
            m_TownChat.SetActive(false);
            GetComponent<ScriptController>().ScriptStart(1);
            GameManager.Instance.m_TownTut = false;
        }
        else if(GameManager.Instance.m_InnTut)
        {
            m_TownChat.SetActive(false);
            GetComponent<ScriptController>().ScriptStart(4);
            GameManager.Instance.m_InnTut = false;
        }
        else
        {
            m_TownChat.SetActive(true);
        }

        if(GameManager.Instance.m_Lost) {
            Lost();
        }

        Restore();
    }

	public void Restore()
    {
        GameManager.Instance.Restore();
    }

    public void Lost()
    {
        GameManager.Instance.m_Party[0] = "Nero";
        GameManager.Instance.m_Party[1] = "";
        GameManager.Instance.m_Party[2] = "";
        GameManager.Instance.m_Party[3] = "";

        GameManager.Instance.m_Lost = false;
    }
}
