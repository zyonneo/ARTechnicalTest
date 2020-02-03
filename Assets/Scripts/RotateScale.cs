using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScale : MonoBehaviour
{


    public float rotspeed=8f;
	public float perspectiveZoomSpeed = 0.05f;
	// public Camera arCamera;

	public GameObject billboard;

    float modeldist;
	

	// Start is called before the first frame update
	void Start()
    {
		billboard.SetActive(false);

	}

    // Update is called once per frame
    void Update()
    {
		


        modeldist = Vector3.Distance(Camera.main.transform.position,transform.position);
        Debug.Log("Distance " + modeldist);
        if (modeldist < 1.5)
        {
            Camera.main.transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Inside if statement ");
        }
        else
        {
            Camera.main.transform.GetChild(0).gameObject.SetActive(false);

        }

		if (Input.touchCount > 0 && Input.touchCount<2 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            transform.Rotate(Vector3.up, -touchDeltaPosition.x * rotspeed * Time.deltaTime);
           // transform.Rotate(Vector3.right, touchDeltaPosition.y * rotspeed * Time.deltaTime);

        }


		if (Input.touchCount == 2)
		{



			
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


			float scale = deltaMagnitudeDiff * perspectiveZoomSpeed * Time.deltaTime;


			if (scale > 0)
			{
				if (transform.localScale.x > 1 && transform.localScale.y > 1 && transform.localScale.z > 1)
					transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
				//if(transform.localScale.x>1 && transform.localScale.y > 1 && transform.localScale.z > 1)
				

			}
			if (scale < 0)
			{

				transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
			}

		}


	}


	public void Showinfo()
	{
		if(billboard.gameObject.activeInHierarchy)
		billboard.SetActive(false);
		else
		billboard.SetActive(true);
	}

}
