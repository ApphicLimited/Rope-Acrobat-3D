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
    public GameObject Parachute;
    private float screenCenterX;
    // Start is called before the first frame update
    void Start()
    {
       // PlayerPrefs.DeleteAll();

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
                GameObject.Find("CameraPosition").transform.eulerAngles = new Vector3(0, 90, 0);
                transform.DORotate(new Vector3(0, 180, 0), 0.5f);
                transform.DOMove(JumpingPoint.position, 2.5f);
            }
            else
            {
                isReachedJumpingPoint = true;
                Physics.gravity = new Vector3(0, -1, 0);
               gameObject.GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce(new Vector3(7.5f, 7.5f, 0), ForceMode.Impulse);
                jumpControl = true;
                Gm.anim.clip = Gm.clips[5];
              Gm.anim.Play();

                StartCoroutine(OpenParachute());
            }
        } 
    }

    IEnumerator OpenParachute()
    {
        yield return new WaitForSeconds(2.5f);
        Parachute.SetActive(true);
        GameObject.Find("MainCamera").GetComponent<SmoothFollow>().distance = 20;
        GetComponent<TrailRenderer>().enabled = false;
    }



    int x=10;
    private void FixedUpdate()
    {
        if (jumpControl == true)
        {
            if (Input.GetMouseButton(0))
            {

                if (Input.mousePosition.x > Screen.width / 2)
                {
                    GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, .5f), ForceMode.Impulse);

                }
                else
                {
                    GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -.5f), ForceMode.Impulse);
                }


            }

        }
    }
    private bool jumpControl;
    public  bool animationControl;
    IEnumerator paraschutWait()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }

    public void paractueButton()
    {
      
    }


    private bool planeControl=true;

  


    public void Gulme()
    {
        print("Gülme hareketi başladı");
        Gm.anim.Stop();
        Gm.anim.clip = Gm.clips[6];
        Gm.anim.Play();
       animationControl = true;
       
    }
}
