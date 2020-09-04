using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinManager : MonoBehaviour
{   public GameObject Coinsobj;
    public AudioClip coinSound;
    public GameObject coinpoint;

    public Text score;
    private int ScoreValue = 0;
    private void Start()
    {
      
        print(this.name);
    }
   

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            collision.gameObject.SetActive(false);
            Instantiate(Coinsobj, collision.transform.position, Quaternion.identity);
            ScoreValue += 1;
            StartCoroutine(CoinEffect());            
            
            SetScore();
        }
    }

    IEnumerator CoinEffect()
    {
        coinpoint.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        coinpoint.SetActive(false);

    }
    void SetScore()
    {
        score.text = " " + ScoreValue;
    }
}
