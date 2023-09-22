using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TronTimer : MonoBehaviour
{
    int time = 60;

    public TMP_Text timerText;

    public GameObject Quit;
    public GameObject Restart;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timeCountDown());
    }

    // Update is called once per frame
    void Update()
    {
        Grid.timeLeft = time;

        timerText.text = ("" + time);
        if (time <= 0)
        {
            StopAllCoroutines();

            PlayerDetection.ActivateMenuButtons(Restart, Quit);
        }

    }

    IEnumerator timeCountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            --time;
        }
    }
}


