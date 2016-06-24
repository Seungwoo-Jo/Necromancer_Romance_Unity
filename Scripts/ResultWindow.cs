using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultWindow : MonoBehaviour
{
    public Image m_Icon;
    public Text m_Text;

    public Sprite m_Encount;
    public string m_EncountMessage;

    public Sprite m_Trap;
    public string m_TrapMessage;

    public Sprite m_Treasure;
    public string m_TreasureMessage;

    public Sprite m_Win;
    public string m_WinMessage;

    public Sprite m_Lose;
    public string m_LoseMessage;

    public void Encount()
    {
        m_Icon.sprite = m_Encount;
        m_Text.text = m_EncountMessage;
    }

    public void Trap()
    {
        m_Icon.sprite = m_Trap;
        m_Text.text = m_TrapMessage;
    }

    public void Treasure()
    {
        m_Icon.sprite = m_Treasure;
        m_Text.text = m_TreasureMessage;
    }

    public void Win()
    {
        m_Icon.sprite = m_Win;
        m_Text.text = m_WinMessage;
    }

    public void Lose()
    {
        m_Icon.sprite = m_Lose;
        m_Text.text = m_LoseMessage;
    }
}
