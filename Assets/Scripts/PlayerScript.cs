using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameManager Gm;
    public bool isReachedFirstArea = false;
    public bool isReachedJumpingPoint = false;
    public Transform JumpingPoint;
    public Transform StartingPoint;
    public Animation animation;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
       
        
            Gm = FindObjectOfType<GameManager>();
        
        animation = transform.GetChild(0).GetComponent<Animation>();
        StartCoroutine(JumpingPointSet());
        cube = GameObject.Find("Cube");
    }

    IEnumerator JumpingPointSet()
    {
        yield return new WaitForSeconds(2);
        JumpingPoint = GameObject.Find("JumpingPoint").transform;
        StartingPoint = GameObject.Find("StartingPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReachedFirstArea == true && isReachedJumpingPoint == false)
        {
            if(Vector3.Distance(transform.position, JumpingPoint.position) > 1)
            {
                animation.Play("PlayerKos");
                transform.position = Vector3.Lerp(transform.position, JumpingPoint.position, 0.03f);
            }
            else
            {
                isReachedJumpingPoint = true;
                Physics.gravity = new Vector3(0, -2, 0);
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce(new Vector3(5, 5, 0), ForceMode.Impulse);
                
                Gm.anim.clip = Gm.clips[5];
                Gm.anim.Play();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "PuanTablo")
        {
            if(collision.collider.name== "PuanGroundKırmızı")
            {
                print("skk");
                Gm.anim.Stop();
                Gm.anim.clip = Gm.clips[3];
                Gm.anim.Play();
                Invoke("Gulme", 2f);
                print("80");
                
                Invoke("completed", 5f);
            }
            else if(collision.collider.name== "PuanGroundSarı")
            {
                print("skk");
                Gm.anim.Stop();
                Gm.anim.clip = Gm.clips[3];
                Gm.anim.Play();
                Invoke("Gulme", 2f);
                print("90");
                
                Invoke("completed", 5f);
            }
            else if(collision.collider.name== "PuanGroundYesil")
            {
                print("skk");
                Gm.anim.Stop();
                Gm.anim.clip = Gm.clips[3];
                Gm.anim.Play();
                Invoke("Gulme", 2f);
                print("100");

                Invoke("completed", 5f);


            }
           
           
        }
    }
    public void Gulme()
    {
        Gm.confetti.SetActive(true);
        print("Gülme hareketi başladı");
        Gm.anim.Stop();
        Gm.anim.clip = Gm.clips[6];
        Gm.anim.Play();
    }

    public void completed()
    {
        Gm.CompleteLevel();
    }
}
