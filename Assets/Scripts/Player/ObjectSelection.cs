using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    public float reachDistance = 10f;

    public Transform handPos, carriedObject;

    public bool carryingObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!carryingObject)
        {
            Physics.Raycast(ray, out hit, reachDistance, 3);

            if (hit.transform.CompareTag("Duckling"))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.yellow);

                // interact with duck;
            }

            if (hit.transform.CompareTag("Food") || hit.transform.CompareTag("InterestingObject"))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.cyan);

                if (Input.GetMouseButton(0))
                {
                    carriedObject = hit.transform;
                    carriedObject.GetComponent<Rigidbody>().useGravity = false;
                    carriedObject.GetComponent<Rigidbody>().isKinematic = true;

                    carriedObject.parent = handPos;
                    carriedObject.position = handPos.position;

                    carryingObject = true;
                }

            }
        }
        else if (Input.GetMouseButton(1))
        {
            carriedObject.GetComponent<Rigidbody>().useGravity = true;
            carriedObject.GetComponent<Rigidbody>().isKinematic = false;

            carriedObject.parent = null;
            carriedObject = null;
            carryingObject = false;
        }
    }

        
}
