using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlace : MonoBehaviour
{
    public GameObject ObjectToInstantiate;

    private GameObject SpawnedObject;

    private ARRaycastManager aRRaycastManager;

 

    static List<ARRaycastHit> Hits = new List<ARRaycastHit>();

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        
    }


    bool GetTouchPos(out Vector2 TouchPosition)
    {
        if (Input.touchCount > 0)
        {
            TouchPosition = Input.GetTouch(0).position;
            return true;
        }

        TouchPosition = default;
        return false;
    }



    // Update is called once per frame
    void Update()
    {
        if (!GetTouchPos(out Vector2 TouchPosition))
        {
            return;
        }

        if(aRRaycastManager.Raycast(TouchPosition, Hits, TrackableType.PlaneWithinPolygon))
        {
            var HitPos = Hits[0].pose;

            if(SpawnedObject == null)
            {
                SpawnedObject = Instantiate(ObjectToInstantiate, HitPos.position, HitPos.rotation);
            }
            else
            {
                SpawnedObject.transform.position = HitPos.position;
            }
        }
    }


  
}
