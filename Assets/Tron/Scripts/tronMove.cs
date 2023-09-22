using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tronMove : MonoBehaviour
{
    public GameObject deathEffect;

    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    public int playerNum;
    public float speed = 16;

    public GameObject wallPrefab;

    Collider2D wall;
    public bool MenuL;
    public bool MenuR;

    Vector2 lastWallEnd;

    void Start()
    {
        if(MenuL && !MenuR)
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
        }
        else if(!MenuL && MenuR)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        }

        spawnWall();
    }


    // Update is called once per frame
    void Update()
    {
        if(WinTracker.playerDeaths == 2)
        {
            WinTracker.winningPlayer = playerNum;
        }

        if (Input.GetKeyDown(upKey))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            spawnWall();
        }
        else if (Input.GetKeyDown(downKey))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
            spawnWall();
        }
        else if (Input.GetKeyDown(rightKey))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            spawnWall();
        }
        else if (Input.GetKeyDown(leftKey))
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
            spawnWall();
        }

        fitColliderBetween(wall, lastWallEnd, transform.position);
    }

    void spawnWall()
    {
        lastWallEnd = transform.position;

        GameObject g = (GameObject)Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
    }

    void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        co.transform.position = a + (b - a) * 0.5f;

        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
            co.transform.localScale = new Vector2(dist + 1, 1);
        else
            co.transform.localScale = new Vector2(1, dist + 1);
    }

    private void OnTriggerEnter2D(Collider2D co)
    {
        if (co != wall && co.gameObject.tag == "item")
        {
            Destroy(co.gameObject);
            StartCoroutine(SpeedBoost());
        }

        if(co != wall && co.gameObject.tag != "item")
        {
            StartCoroutine(DestroyPlayer());
            print("Player lost:" + name);

            deathEffect.SetActive(true);
        }
    }

    IEnumerator DestroyPlayer()
    {
        if(!MenuL && !MenuR)
        {
            WinTracker.playerDeaths += 1;
        }

        yield return new WaitForSeconds(0.8f);

        Destroy(gameObject);
    }

    IEnumerator SpeedBoost()
    {
        speed += 5;

        yield return new WaitForSeconds(5f);

        speed -= 5;
    }


}
