using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotMouse : MonoBehaviour
{

    public Transform StartPoint;
    public Transform EndPoint;
    public Transform MiddlePoint;

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private float ropeSegLen = 0.25f;
    private int segmentLength = 25;
    private float lineWidth = 0.2f;
    //Sling shot 
    private bool moveToMouse = false;
    private Vector3 mousePositionWorld;
    private int indexMousePos;
    RaycastHit hit;
    public Vector3 firstMiddlePoint;
    bool bagliydi = false;
    Vector3 changeAmount2;
    Vector3 changeAmount3;
    Ray ray;
    float ratio;

    // Use this for initialization
    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = StartPoint.position;

        for (int i = 0; i < segmentLength; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }

        StartCoroutine(CreateCollider());
    }

    IEnumerator CreateCollider()
    {
        yield return new WaitForSeconds(0.5f);
        firstMiddlePoint = MiddlePoint.transform.position;
        BoxCollider col = transform.gameObject.AddComponent<BoxCollider>();
        col.size = new Vector3(8, 0.25f, 0.25f);
        float lineLength = Vector3.Distance(StartPoint.transform.position, EndPoint.transform.position); // length of line
        Vector3 midPoint = (StartPoint.transform.position + EndPoint.transform.position) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();




        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    this.moveToMouse = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (this.moveToMouse == true)
            {
                this.moveToMouse = false;
                changeAmount3 = changeAmount2 * ratio * 250;
                GetComponent<BoxCollider>().enabled = false;
                bagliydi = true;

            }

        }
        else if (this.moveToMouse == true && Input.GetMouseButton(0))
        {
            GameObject.Find("MainCamera").GetComponent<SmoothFollow>().enabled = false;
            MiddlePoint.position = this.mousePositionWorld;
            GameObject.Find("Player").transform.position = MiddlePoint.position;
            GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Player").GetComponent<Rigidbody>().useGravity = false;
        }

        if (bagliydi && Vector3.Distance(GameObject.Find("Player").transform.position, firstMiddlePoint) < 0.05f)
        {
            bagliydi = false;
            Firlat(changeAmount3);
        }
        if (bagliydi)
        {
            GameObject.Find("Player").transform.position = Vector3.Lerp(GameObject.Find("Player").transform.position, firstMiddlePoint, 0.75f);

        }

        Vector3 screenMousePos = Input.mousePosition;
        float xStart = StartPoint.position.x;
        float xEnd = EndPoint.position.x;
        this.mousePositionWorld = Camera.main.ScreenToWorldPoint(new Vector3(screenMousePos.x, screenMousePos.y, 10));
        float currX = this.mousePositionWorld.x;

        ratio = (currX - xStart) / (xEnd - xStart);
        if (ratio > 0)
        {
            this.indexMousePos = (int)(this.segmentLength * ratio);
        }


    }

    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(0.6f);
        GetComponent<BoxCollider>().enabled = true;
    }

    void Firlat(Vector3 force)
    {
        MiddlePoint.position = firstMiddlePoint;
        GameObject.Find("Player").GetComponent<Rigidbody>().useGravity = true;
        StartCoroutine(ActivateCollider());
        GameObject.Find("Player").GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        GameObject.Find("MainCamera").GetComponent<SmoothFollow>().enabled = true;
    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    private void Simulate()
    {
        // SIMULATION
        Vector3 forceGravity = new Vector3(0f, -1f, 0f);

        for (int i = 1; i < this.segmentLength; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector3 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to First Point 
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = this.StartPoint.position;
        this.ropeSegments[0] = firstSegment;


        //Constrant to Second Point 
        RopeSegment endSegment = this.ropeSegments[this.ropeSegments.Count - 1];
        endSegment.posNow = this.EndPoint.position;
        this.ropeSegments[this.ropeSegments.Count - 1] = endSegment;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector3 changeDir = Vector3.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }


            Vector3 changeAmount = changeDir * error;
            changeAmount2 = new Vector3(0, changeAmount.x / 2.5f, changeAmount.z * -1.3f);
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }

            if (this.moveToMouse && indexMousePos > 0 && indexMousePos < this.segmentLength - 1 && i == indexMousePos)
            {
                RopeSegment segment = this.ropeSegments[i];
                RopeSegment segment2 = this.ropeSegments[i + 1];
                segment.posNow = new Vector3(this.mousePositionWorld.x, this.mousePositionWorld.y, this.mousePositionWorld.z);
                segment2.posNow = new Vector3(this.mousePositionWorld.x, this.mousePositionWorld.y, this.mousePositionWorld.z);
                this.ropeSegments[i] = segment;
                this.ropeSegments[i + 1] = segment2;
            }
        }
    }

    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector3 posNow;
        public Vector3 posOld;

        public RopeSegment(Vector3 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.name == "Player")
        {
            GameObject.Find("Player").GetComponent<Rigidbody>().velocity = Vector3.zero;
            GameObject.Find("Player").GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            other.transform.position = MiddlePoint.position;
        }
    }
}
