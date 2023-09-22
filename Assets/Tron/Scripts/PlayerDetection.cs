using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public GameObject[] winScreen;
    public GameObject restartButton;
    public GameObject quitButton;
    int playerDeathCount = 0;
    int WinningPlayer = 0;
    int addedWin = 0;

    void Start()
    {
        Time.timeScale = 1;
        addedWin = 1;
    }

    void Update()
    {
        playerDeathCount = WinTracker.playerDeaths;
        WinningPlayer = WinTracker.winningPlayer;

        if (playerDeathCount >= 3)
        {
            WinTracker.playerWins[WinningPlayer] += addedWin;
            addedWin = 0;

            winScreen[WinningPlayer].SetActive(true);

            StartCoroutine(PlayerDetection.ActivateMenuButtons(quitButton, restartButton));
        }
    }

    public static IEnumerator ActivateMenuButtons(GameObject restart, GameObject quit)
    {
            WinTracker.playerDeaths = 0;

            Time.timeScale = 0.2f;

            yield return new WaitForSeconds(0.8f);

            Time.timeScale = 1f;

            yield return new WaitForSeconds(0.5f);

            quit.SetActive(true);

            yield return new WaitForSeconds(0.4f);

            restart.SetActive(true);

    }
}
