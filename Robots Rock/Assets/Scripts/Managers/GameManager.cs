using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Set in Inspector
    public RobotController playerController;
    public Pitcher pitcher;
    public Text messageText;
    public Text promptText;
    //

    public bool IsGameOver { get; private set; }
    public bool PlayerWins { get; private set; }

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
        while (true)
        {
            if(Input.anyKeyDown)
            {
                promptText.enabled = false;
                messageText.text = "READY?";
                yield return new WaitForSeconds(3.0f);
                break;
            }
            yield return null;
        }
    }

    private IEnumerator GameplayRoutine()
    {
        playerController.enabled = true;
        pitcher.enabled = true;

        messageText.text = "GO!";
        yield return new WaitForSeconds(1.0f);
        messageText.text = "";

        while (!IsGameOver)
        {
            yield return null;
        }
    }

    private IEnumerator EndGameRoutine()
    {
        playerController.enabled = false;
        pitcher.enabled = false;

        messageText.text = PlayerWins ? "YOU WIN!" : "YOU LOSE.";
        promptText.enabled = true;

        while (true)
        {
            if (Input.anyKeyDown)
            {
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
}
