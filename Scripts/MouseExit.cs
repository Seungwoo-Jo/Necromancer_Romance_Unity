using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseExit : MonoBehaviour, IPointerExitHandler
{
    public bool m_UseEvent = true;

    public UnityEvent m_MouseExit;

    void Start()
    {
        m_UseEvent = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(m_UseEvent)
        {
            //Debug.Log(gameObject.name + " Mouse Exit");
            m_MouseExit.Invoke();
        }
    }
}
