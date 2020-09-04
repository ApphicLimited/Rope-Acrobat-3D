using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyVerticalMove : MonoBehaviour
{
    public float speed;
    public bool MoveRight;
    public float righlimit, leftlimit;
    public GameManager gameManager;
    public GameObject GameOverUI;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (MoveRight)
        {
            transform.Translate(0, 0, 2 * Time.deltaTime * speed);
            if (transform.position.y <= righlimit)
            {
                MoveRight = false;
            }
        }
        else
        {
            transform.Translate(0, 0, -2 * Time.deltaTime * speed);
            if (transform.position.y >= leftlimit)
            {
                MoveRight = true ;
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.GameOver();




        }
    }
}
