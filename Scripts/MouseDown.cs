using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseDown : MonoBehaviour, IPointerDownHandler
{
    public bool m_UseEvent = true;

    public UnityEvent m_MouseDown;

    void Start()
    {
        m_UseEvent = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(m_UseEvent)
        {
            //Debug.Log(gameObject.name + " Mouse Down");
            m_MouseDown.Invoke();
        }
    }
}
