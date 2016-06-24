using UnityEngine;
using System.Collections;

public class ScriptController : MonoBehaviour {

    private ScriptActivator m_ScriptActivator;

    void Start()
    {
        m_ScriptActivator = GetComponentInChildren<ScriptActivator>();
    }

    public void ScriptStart(int scriptNum)
    {
        if(m_ScriptActivator != null) {
            m_ScriptActivator.ScriptActivate(scriptNum);
        }
    }
}
