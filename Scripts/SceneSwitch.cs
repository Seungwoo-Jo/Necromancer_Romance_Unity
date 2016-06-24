using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitch : MonoBehaviour {

    private string m_SceneName;
	
	void Start () {
	
	}

    public void SetNextScene(string scene)
    {
        m_SceneName = scene;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(m_SceneName);
    }

    public void LoadData()
    {
        GameManager.Instance.Load();
    }
}
