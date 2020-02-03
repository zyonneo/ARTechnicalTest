using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LookCam : MonoBehaviour
{


    private Transform lookCamera;
    float mdistance,prev=0;
    bool dist = false;

    float minDistance = 0.5f;
    float maxDistance = 6f;

    float minScale = 0.5f;
    float maxScale = 3f;

    public TMP_Text datatxt;
    // Start is called before the first frame update
    void Start()
    {
        lookCamera = Camera.main.transform;

        //prev = mdistance;

    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.LookAt(lookCamera);
        transform.rotation = Quaternion.LookRotation(transform.position - lookCamera.position);

        mdistance = Vector3.Distance(Camera.main.transform.position, transform.position);
        var scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(minDistance, maxDistance, mdistance));
        transform.localScale = new Vector3(scale, scale, scale);

        //if (mdistance > 0.5f && mdistance < 1f)
        //{ 
        //    transform.localScale=new Vector3(0.5f,0.5f,0.5f);
        //}
        //else if(mdistance > 1f && mdistance < 1.5f)
        //{
        //    transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //}
        //else if(mdistance > 1.5f && mdistance < 2f)
        //{
        //    transform.localScale = new Vector3(2.5f,2.5f,2.5f);
        //}
        //else if (mdistance > 2f && mdistance < 3f)
        //{
        //    transform.localScale = new Vector3(4f,4f,4f);
        //}

        //mdistance = Vector3.Distance(Camera.main.transform.position, transform.position);

        datatxt.text = mdistance.ToString();
        Debug.Log("Local distance "+mdistance.ToString());
        //datatxt.text = transform.localScale.ToString();


    }
}
