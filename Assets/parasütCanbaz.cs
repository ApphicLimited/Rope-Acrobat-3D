using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parasütCanbaz : MonoBehaviour
{ public GameManager Gm;
      private bool planeControl = true;
    private void OnTriggerEnter(Collider other)
    {
       
     if (planeControl == true)
        { 
        if (other.name == "PuanGroundKırmızı")
        {
            Debug.Log("10 Puan");
            Gm.anim.Stop();
            Gm.anim.clip = Gm.clips[3];
            Gm.anim.Play();
                Gm.CompleteLevel();
                Invoke("Gulme", 2f);
        }

        else if (other.name == "PuanGroundPembe")
        {
            Debug.Log("20 Puan");
            Gm.anim.Stop();
            Gm.anim.clip = Gm.clips[3];
            Gm.anim.Play();
                Gm.CompleteLevel();
                Invoke("Gulme", 2f);
        }

        else if (other.name == "PuanGroundMor")
        {
            Debug.Log("30 Puan");
            Gm.anim.Stop();
            Gm.anim.clip = Gm.clips[3];
            Gm.anim.Play();
                Gm.CompleteLevel();
                Invoke("Gulme", 2f);
        }
        else if (other.name == "PuanGroundTurkuaz")
        {
            Debug.Log("40 Puan");
            Gm.anim.Stop();
            Gm.anim.clip = Gm.clips[3];
            Gm.anim.Play();
                Gm.CompleteLevel();
                Invoke("Gulme", 2f);
        }

        else if (other.name == "PuanGroundYesil")
        {
            Debug.Log("50 Puan");
            Gm.anim.Stop();
            Gm.anim.clip = Gm.clips[3];
            Gm.anim.Play();
            Invoke("Gulme", 2f);
                Gm.CompleteLevel();
                planeControl = false;
        }

        }}
} 

