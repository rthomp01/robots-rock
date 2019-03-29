using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    public UnityEvent pauseEvent;
    public UnityEvent unPauseEvent;

    public bool IsPausable { get; set; }

    void Start()
    {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (IsPausable && Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pausePanel.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);

        if (pauseEvent != null)
        {
            pauseEvent.Invoke();
        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);

        if (unPauseEvent != null)
        {
            unPauseEvent.Invoke();
        }
    }
}
