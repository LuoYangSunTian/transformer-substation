using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHandle : MonoBehaviour
{

    public GameObject box;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        limitation();
    }

    private void limitation()
    {
        if (box)
        {
            Vector3 boxSize = box.GetComponent<BoxCollider>().size;
            Vector3 boxCenter = box.GetComponent<BoxCollider>().center;
            Vector3 min = box.transform.position + boxCenter - boxSize * 0.5f;
            Vector3 max = box.transform.position + boxCenter + boxSize * 0.5f;

            Vector3 cameraP = transform.position;

            if (cameraP.x < min.x)
            {
                cameraP.x = min.x;
            }
            else if (cameraP.x > max.x)
            {
                cameraP.x = max.x;
            }

            if (cameraP.y < min.y)
            {
                cameraP.y = min.y;
            }
            else if (cameraP.y > max.y)
            {
                cameraP.y = max.y;
            }

            if (cameraP.z < min.z)
            {
                cameraP.z = min.z;
            }
            else if (cameraP.z > max.z)
            {
                cameraP.z = max.z;
            }

            transform.position = cameraP;
        }

    }
}