using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptNahui : MonoBehaviour
{
    private float currentLocalRotationY;
    // Start is called before the first frame update
    void Start()
    {
        currentLocalRotationY = transform.rotation.y;
        transform.Rotate(0, 60, 0, Space.Self);
        // transform.localRotation = Quaternion.Euler((new Vector3(transform.localRotation.x, 0, transform.localRotation.z)));
        // if (currentLocalRotationY >= 0)
        // {
        //      transform.Rotate(0, currentLocalRotationY - currentLocalRotationY, 0, Space.Self);
        //  }
        //   else
        //    {
        //        transform.Rotate(0, currentLocalRotationY - currentLocalRotationY, 0, Space.Self);
        //    }

        // currentTransform = new Vector3(transform.localRotation.x, 0, transform.localRotation.z); 
        //  transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y - transform.localRotation.y, transform.localRotation.z));
    }

    // Update is called once per frame
    void Update()
    {
      //  if (transform.localRotation.y != 0)
      //  transform.Rotate(Vector3.up * 30f * Time.deltaTime, Space.Self);
    }
}
