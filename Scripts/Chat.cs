using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Chat : MonoBehaviour
{
    private Card m_Card;
    private int m_PrevChat = 1;
    private int m_CurChat = 0;
    private Text m_Text;
    private Image m_Character;

	void Awake ()
    {
        m_Text = GetComponent<RectTransform>().Find("Bubble/Text").GetComponent<Text>();
        m_Character = GetComponent<RectTransform>().Find("Character").GetComponent<Image>();

        int code = Random.Range(0, GameManager.Instance.m_HaveCards.Count);

        int i = 0;
        foreach(DictionaryEntry pair in GameManager.Instance.m_HaveCards)
        {
            Card value = (Card)pair.Value;

            Debug.Log(value.m_Name);

            if(i == code)
            {
                m_Card = value;
                break;
            }

            i++;
        }

        m_Character.sprite = m_Card.m_IllustSprite;

        NewChat();
	}
	

	void Update ()
    {
	
	}

    public void NewChat()
    {
        while(true)
        {
            m_CurChat = Random.Range(0, m_Card.m_Chat.Length);

            if(m_CurChat != m_PrevChat)
            {
                m_Text.text = m_Card.m_Chat[m_CurChat];
                m_PrevChat = m_CurChat;
                break;
            }
        }
    }
}
