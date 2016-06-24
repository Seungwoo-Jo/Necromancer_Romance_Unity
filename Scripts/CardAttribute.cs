using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardAttribute : MonoBehaviour {

    public Sprite[] m_AttributeImage;
    public string[] m_AttributeExplain;

    public Sprite SetAttributeSprite(int attribute)
    {
        if(attribute >= 0 && attribute < 3) {
            return m_AttributeImage[attribute];
        }
        return null;
    }

    public string SetAttributeExplain(int attribute)
    {
        if(attribute >= 0 && attribute < 3) {
            return m_AttributeExplain[attribute];
        }
        return null;
    }
}
