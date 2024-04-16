using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    private Vector3 offset;
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;
    
    // Start is called before the first frame update
    private void Start() {
        offset = transform.position - target.position;
    }
    // Update is called once per frame
    void Update()
    {
        //Vector3 movementFollow = new Vector3(target.position.x + 5f, 0, target.position.z + 5f);
        //transform.position= movementFollow * 5 * Time.deltaTime;
        transform.LookAt(target);
        Vector3 newPos = target.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
    }

}
