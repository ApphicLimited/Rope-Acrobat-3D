using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerScript : MonoBehaviour
{
    public GameManager Gm;
    public bool isReachedFirstArea = false;
    public bool isReachedJumpingPoint = false;
    public Transform JumpingPoint;
    public Transform StartingPoint;
    public Animation animation;
    public GameObject cube;
    public GameObject parashut;
    private float screenCenterX;
    // Start is called before the first frame update
    void Start()
    {


        Gm = FindObjectOfType<GameManager>();

        animation = transform.GetChild(0).GetComponent<Animation>();
        StartCoroutine(JumpingPointSet());
        cube = GameObject.Find("Cube");

        screenCenterX = Screen.width * 0.5f;
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
        if (isReachedFirstArea == true && isReachedJumpingPoint == false)
        {
            if (Vector3.Distance(transform.position, JumpingPoint.position) > 1)
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

                JumpingPoint = GameObject.Find("paractueBut").transform;
                Image trans = JumpingPoint.GetComponent<Image>();

                Color c = trans.color;
                c.a = 1;
                trans.color = c;
                StartCoroutine(paraschutWait());
               
            }
        } 
    }
    int x=10;
    private void FixedUpdate()
    {
        if (jumpControl == true && animationControl!=true)
        {
            if (Input.mousePosition.x > screenCenterX)
            {
                // Debug.Log("Left");

                //   this.gameObject.GetComponent<RigidBody>()=AddForce(0, 0, 1);
                this.gameObject.GetComponent<Rigidbody>().AddForce(0, 0, -5*Time.deltaTime,ForceMode.Impulse);
            }
            else if (Input.mousePosition.x < screenCenterX)
            {
                // Debug.Log("Right");

                this.gameObject.GetComponent<Rigidbody>().AddForce(0, 0, 5*Time.deltaTime, ForceMode.Impulse);
            }
        }

    }
    private bool jumpControl;
    private bool animationControl;
    IEnumerator paraschutWait()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }
  public  GameObject left, right;
    public void paractueBut()
    {
        if (isReachedJumpingPoint==true)
        {
            Debug.Log("jump işlemi");
            Time.timeScale = 1;
            parashut.SetActive(true);

            jumpControl = true;
            GameObject parasütButton = GameObject.Find("paractueBut");
            parasütButton.SetActive(false);
          
        }
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "PuanTablo")
        {
             print("skk");
            Gm.anim.Stop();
            Gm.anim.clip = Gm.clips[3];
            Gm.anim.Play();
            Invoke("Gulme", 2f);
        }
    }
    public void Gulme()
    {
        print("Gülme hareketi başladı");
        Gm.anim.Stop();
        Gm.anim.clip = Gm.clips[6];
        Gm.anim.Play();
        animationControl = true;
       
    }
}
