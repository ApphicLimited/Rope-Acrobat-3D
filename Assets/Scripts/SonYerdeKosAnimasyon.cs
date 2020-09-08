using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonYerdeKosAnimasyon : MonoBehaviour
{
    public GameManager Gm;
     
    public Rigidbody rb;
    public float forwardForce = 2000f;
    private void Start()
    {
        Gm= FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("sa    "+ collision.collider.tag);
        if (collision.collider.tag == "Player")
        {
            //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
            Gm.anim.Stop();
            Gm.anim.clip = Gm.clips[4];
            Gm.anim.Play();
            collision.transform.position = Vector3.Lerp(collision.transform.position, new Vector3(0, 16f, 1.05f), 3f);
            rb.AddForce(0, 0, 200 * Time.deltaTime);
        }
    }
    
    /* private void on(Collider collision)
     {
     FindObjectOfType<GameManager>();
         if (collision.collider.tag == "sonYer")
         {
             rb.AddForce(0, 0, forwardForce * Time.deltaTime);


             FindObjectOfType<GameManager>().EndGame();

             Gm.anim.Stop();
             Gm.anim.clip = Gm.clips[4];
             Gm.anim.Play();
         }
     }*/



}
