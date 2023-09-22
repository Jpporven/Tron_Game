using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TronUI : MonoBehaviour
{
    public TMP_Text[] wins;
    public string quitSceneName;
    public string restartSceneName;
    public GameObject fadeOut;
    public GameObject fadeIn;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < wins.Length; i++)
        {
            wins[i].text = ("" + WinTracker.playerWins[i]);
        }

        StartCoroutine(toggleOffFadeIn());
    }

    public void Quit()
    {
        StartCoroutine(loadNextScene(quitSceneName));
    }

    public void Restart()
    {
        StartCoroutine(loadNextScene(restartSceneName));
    }

    IEnumerator loadNextScene(string scene)
    {
        fadeOut.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(scene);
    }

    IEnumerator toggleOffFadeIn()
    {
        yield return new WaitForSeconds(2f);

        fadeIn.SetActive(false);
    }
}
