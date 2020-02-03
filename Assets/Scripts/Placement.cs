using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
public class Placement : MonoBehaviour
{

    public GameObject objectToPlace;
   

    private ARRaycastManager arRaycastManger;
    private ARPlaneManager planeManager;

    public GameObject ARDefPlane;
    public Material shadowMat; 

    static List<ARRaycastHit> Hits = new List<ARRaycastHit>();

    GameObject spawnedObject;



    bool onTouchHold = false;
    //Vector2 touchPosition;

    float period = 0f;
    float nextperiod = 1f;

    public Text tt;
    public Text valuetxt;

    void Awake()
    {
     arRaycastManger=GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    bool TryGetPosition(out Vector2 touchPosition)
    {

        if (Input.touchCount > 0)
        {

            touchPosition = Input.GetTouch(0).position;
            return true;

        }


        touchPosition = default;
        return false;

    }

    // Start is called before the first frame update
    void Start()
    {
        tt.gameObject.SetActive(false);
        // ARDefPlane.GetComponent<MeshRenderer>().material = shadowMat;

       // objectToPlace.transform.GetChild(1).gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {


       
        if (Input.touchCount == 1)
        {

            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = touch.position;

                Ray ray = Camera.main.ScreenPointToRay(touchPos);
                RaycastHit hit;
               
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.tag == "ArcadeGame")
                    {
                        

                        if (period > nextperiod)
                        {
                            onTouchHold = true;
                            period = 0f;
                            valuetxt.text = "Drag and move";
                        }

                        period = period + Time.deltaTime;
                        //tt.gameObject.SetActive(true);
                       // tt.text = period.ToString();
                    }

                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                onTouchHold = false;
                valuetxt.text = "Cannot move, Hold 1 sec to move";

            }

        }


        if (!TryGetPosition(out Vector2 touchPosition))
            return;


       


        if (arRaycastManger.Raycast(touchPosition, Hits, TrackableType.PlaneWithinPolygon))
        {

            var hitpose = Hits[0].pose;
           // var hitlast = Hits[Hits.Count - 1].pose;

            if (spawnedObject == null)
            {

                spawnedObject = Instantiate(objectToPlace, hitpose.position, hitpose.rotation);
                spawnedObject.transform.GetChild(1).gameObject.SetActive(true);
                TurnoffDetection();
            }

            else 
            {

                //  spawnedObject.transform.position = hitpose.position;
                if (onTouchHold == true)
                {

                    spawnedObject.transform.position = hitpose.position;
                    spawnedObject.transform.rotation = hitpose.rotation;


                }
                else 
                {

                   // spawnedObject.transform.position = hitlast.position;

                }

            }


        }
        
    }


    void TurnoffDetection()
    {
        Debug.Log("Name of object "+objectToPlace.transform.GetChild(1).gameObject.name);
        objectToPlace.transform.GetChild(1).gameObject.SetActive(true);
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }

    }


}
