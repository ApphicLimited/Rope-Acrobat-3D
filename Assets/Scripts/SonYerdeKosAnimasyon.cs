using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonYerdeKosAnimasyon : MonoBehaviour
{
    public GameManager Gm;
    public Rigidbody rb;
    public float horizontalMoveSpeed = 30f;
    public float horizontalmove = 0;
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
    }
        void OnCollisionEnter(Collision collision)
        {
            print("hello    " + collision.collider.tag);
            if (collision.collider.tag == "Player")
            {

                Gm.anim.Stop();
                Gm.anim.clip = Gm.clips[4];
                Gm.anim.Play();
            print("kosmaya basladi");
                collision.transform.position = Vector3.Lerp(collision.transform.position, new Vector3(0, 16f, 1.05f), 3f);
            print("addforce veriliyor ");
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

    IEnumerator OrtayaAl()
    {
        yield return new WaitForSeconds(.6f);
        Nerede = "ortada";
        NereyeKadar = 0;
    }
    
    void getir()
    {
        if (Gm.ensonasama)
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x > Screen.width / 2)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z- 0.5f);
                    print("Sag tarafa tiklandi");

                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
                    print("Sol tarafa tiklandi");
                }

            }
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


}
