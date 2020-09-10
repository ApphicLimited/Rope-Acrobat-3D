using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParasutAcil : MonoBehaviour
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMove(position, 0.5f);
        transform.DOLocalRotate(rotation, 0.5f);
        transform.DOScale(scale, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
