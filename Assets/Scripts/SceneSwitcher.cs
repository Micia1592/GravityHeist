using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public int sceneNumber;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        sceneNumber = 0;
    }
    public void SceneChange()
    {
        SceneManager.LoadScene(sceneNumber);
        Debug.Log("Scene Change" + sceneNumber);
    }
}
