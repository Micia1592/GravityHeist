using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    public float totalTime = 600f; //10 min in sseconds
    private float currentTime;
    private bool isTimerRunning = false;
    public GameObject canvasToToggle;

    public Text timerText;

    public GameObject player;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("player gameobject cannot be found in the scene");
        }
    }

    void Start()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
    }
    void Update()
    {
        if (isTimerRunning)
        {
            if (currentTime > 0f)

            {
                currentTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                TimerEnded();
            }
        }
    }
    void UpdateTimerDisplay()
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void StartTimer()
    {
        isTimerRunning = true;

    }
    void TimerEnded()
    {
        isTimerRunning = false;
        canvasToToggle.SetActive(true);

        if (player != null)
        {
            player.SetActive(false);
        }
    }
}
