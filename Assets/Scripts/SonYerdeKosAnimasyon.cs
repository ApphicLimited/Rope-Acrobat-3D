using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonYerdeKosAnimasyon : MonoBehaviour
{
    public GameManager Gm;
     
    public Rigidbody rb;
    
    Vector2 currentSwipe;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    public float NereyeKadar;
    public string Nerede;
    public int slot;
    private void Start()
    {
        Gm= FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        getir();

        void OnCollisionEnter(Collision collision)
        {
            print("sa    " + collision.collider.tag);
            if (collision.collider.tag == "Player")
            {

                Gm.anim.Stop();
                Gm.anim.clip = Gm.clips[4];
                Gm.anim.Play();
                collision.transform.position = Vector3.Lerp(collision.transform.position, new Vector3(0, 16f, 1.05f), 3f);
                rb.AddForce(0, 0, 200 * Time.deltaTime);
                
            }
        } }

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

    IEnumerator OrtayaAl()
    {
        yield return new WaitForSeconds(.6f);
        Nerede = "ortada";
        NereyeKadar = 0;
    }
    PlayerScript ps = new PlayerScript();
    void getir()
    {

     /*   if (Input.GetMouseButton(0))
        {
            if (ps.JumpingPoint)
            {
                if (Input.mousePosition.x > Screen.width / 2)
                {
                    GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, .5f), ForceMode.Impulse);

                }
                else
                {
                    GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -.5f), ForceMode.Impulse);
                }

            }    */
           
            
        }
        /*
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            currentSwipe.Normalize();

          
           
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("left swipe");

                if (slot > 1 && Nerede == "ortada")
                {
                    NereyeKadar = -27;
                    StartCoroutine(OrtayaAl());
                    slot -= 1;
                    Nerede = "yan";

                }

            }
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                if (slot < 4 && Nerede == "ortada")
                {
                    NereyeKadar = 27;
                    StartCoroutine(OrtayaAl());
                    slot += 1;
                    Nerede = "yan";

                    Debug.Log("right swipe");
                }
            }
        }*/
    }



