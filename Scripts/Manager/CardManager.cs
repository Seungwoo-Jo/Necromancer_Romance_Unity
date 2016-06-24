using UnityEngine;
using System.Collections;
using System.IO;

public class CardManager
{
    private static CardManager instance;

    public static CardManager Instance
    {
        get
        {
            if(instance == null) {
                instance = new CardManager();
                instance.Init();
            }
            return instance;
        }
    }

    public Hashtable m_Cards;

    void Init()
    {
        m_Cards = new Hashtable();
    }

    public void Load()
    {
        StreamReader sr;

        for(int i = 0; i < 13; i++)
        {
            TextAsset ta = Resources.Load("Character_" + i.ToString()) as TextAsset;
            

            //sr = new StreamReader(Application.dataPath + "/Resources/Character_" + i.ToString() + ".json");

            Card _NewCard = JsonUtility.FromJson<Card>(ta.text);
            _NewCard.m_SlotSprite = Resources.Load<Sprite>("Cards/Card_" + _NewCard.m_Name);
            _NewCard.m_FullSprite = Resources.Load<Sprite>("Cards/Full/Full_" + _NewCard.m_Name);
            _NewCard.m_IllustSprite = Resources.Load<Sprite>("Cards/Illust/Illust_" + _NewCard.m_Name);
            _NewCard.m_Code = i;

            //sr.Close();

            m_Cards.Add(_NewCard.m_Name, _NewCard);

            Debug.Log(_NewCard.m_Name + " Load Complete");
        }  
    }
}
