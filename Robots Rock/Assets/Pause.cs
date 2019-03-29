using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    public UnityEvent pauseEvent;
    public UnityEvent unPauseEvent;

    public 
    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeSelf)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);

                if(pauseEvent != null)
                {
                    pauseEvent.Invoke();
                }
            }
            else
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);

                if(unPauseEvent != null)
                {
                    unPauseEvent.Invoke();
                }
            }
        }
    }
    private void PauseGame()
    {

    }
    private void ContinueGame()
    {

    }
}
