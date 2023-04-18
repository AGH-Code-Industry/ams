using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    public UnityEvent GamePaused;
    public UnityEvent GameResumed;

    public GameObject PauseCanvas;
    public GameObject GameCanvas;


    public bool _isPaused = false;

    void Start()
    {
        PauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPaused = !_isPaused;
            if (_isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        GamePaused.Invoke();
        PauseCanvas.SetActive(true);
        GameCanvas.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameResumed.Invoke();
        PauseCanvas.SetActive(false);
        GameCanvas.SetActive(true);
    }
}
