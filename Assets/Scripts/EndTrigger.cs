using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameManager gameManager;
    public Transform player;
    public bool checkit = false;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if(checkit)
        {
            if (player.position.y > transform.position.y)
            {
                // Make him reach!
                checkit = false;
                player.GetComponent<PlayerScript>().isReachedFirstArea = true;
                player.GetComponent<Rigidbody>().isKinematic = true;
                player.transform.position = player.GetComponent<PlayerScript>().StartingPoint.position;
            }
            else
            {
                player.GetComponent<PlayerScript>().isReachedFirstArea = false;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.name == "Player")
        {
            checkit = true;
        }
    }



}
