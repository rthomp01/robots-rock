using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Set in Inspector
    [Header("Game Settings")]
    public GameLoopConfig gameLoopConfig;
    public RobotController playerController;
    public Pitcher pitcher;

    [Space()]
    [Header("HUD")]
    public GameObject HUD;
    public Text messageText;
    public Text promptText;
    public Image titlePanel;
    public Pause pausePanel;

    [Space()]
    [Header("SFX")]
    public SFXPlayer sfxPlayer;
    public AudioClip submitClip;
    //

    public bool IsGameOver { get; private set; }
    public bool PlayerWins { get; private set; }

    [Space()]
    public UnityEvent gameplayStartEvent;
    public UnityEvent gameoverEvent;

    Coroutine messageRoutine;

    private void Start()
    {
        StartCoroutine(Game());
    }

    private IEnumerator Game()
    {
        yield return StartCoroutine(TitleRoutine());
        yield return StartCoroutine(GameplayRoutine());
        yield return StartCoroutine(EndGameRoutine());

        SceneManager.LoadScene(0);
    }

    private IEnumerator TitleRoutine()
    {
        yield return new WaitForSeconds(gameLoopConfig.titlePreActiveDelay);

        promptText.gameObject.SetActive(true);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                sfxPlayer.PlayOneShot(submitClip);
                titlePanel.gameObject.SetActive(false);
                promptText.gameObject.SetActive(false);
                pausePanel.IsPausable = true;
                SetMessageText("READY?", gameLoopConfig.readyWaitDelay);
                yield return new WaitForSeconds(gameLoopConfig.readyWaitDelay);
                break;
            }
            yield return null;
        }
    }

    private IEnumerator GameplayRoutine()
    {
        playerController.enabled = true;
        pitcher.enabled = true;

        SetMessageText("GO!", gameLoopConfig.goMessageActiveDelay);

        if(gameplayStartEvent != null)
        {
            gameplayStartEvent.Invoke();
        }

        yield return new WaitForSeconds(gameLoopConfig.goMessageActiveDelay);

        while (!IsGameOver)
        {
            yield return null;
        }
    }

    private IEnumerator EndGameRoutine()
    {
        pausePanel.IsPausable = false;
        playerController.enabled = false;
        pitcher.enabled = false;

        titlePanel.gameObject.SetActive(true);
        string gameOverMessage = PlayerWins ? "YOU WIN" : "YOU LOSE";
        SetMessageText(gameOverMessage, 0.0f);

        if (gameoverEvent != null)
        {
            gameoverEvent.Invoke();
        }

        yield return new WaitForSeconds(gameLoopConfig.gameOverMessageDelay);

        promptText.gameObject.SetActive(true);

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                sfxPlayer.PlayOneShot(submitClip);
                yield return new WaitForSeconds(1.0f);
                break;
            }
            yield return null;
        }
    }

    public void SetGameOver(bool victory)
    {
        PlayerWins = victory;
        IsGameOver = true;
    }

    /// <summary>
    /// Shows a message on screen for specified duration.
    /// </summary>
    /// <param name="text">Text to display in message.</param>
    /// <param name="duration">Time on screen. Use 0.0f for persistent message</param>
    void SetMessageText(string text, float duration)
    {
        if (messageRoutine != null)
        {
            StopCoroutine(messageRoutine);
        }

        messageRoutine = StartCoroutine(ShowMessageRoutine(text, duration));
    }

    IEnumerator ShowMessageRoutine(string text, float messageDuration)
    {
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 0.0f);
        messageText.text = text;
        messageText.GetComponent<Animation>().Play("MessageFadeIn");

        yield return new WaitForSeconds(messageDuration);

        if (messageDuration != 0.0f)
        {
            messageText.GetComponent<Animation>().Play("MessageFadeOut");
        }
    }
}
