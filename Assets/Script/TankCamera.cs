using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCamera : MonoBehaviour {

    public Transform mainCamera;
    public Transform stay;
    public Transform moveForward;
    public Transform turnLeft;
    public Transform turnRight;
    
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 forward = stay.forward;
        Vector3 pos = stay.position;
        if (v > 0) 
        {
            forward = (forward + moveForward.forward) / 2;
            pos = (pos + moveForward.position) / 2;
        }
        if (h > 0)//right
        {
            forward = (forward + turnRight.forward) / 2;
            pos = (pos + turnRight.position) / 2;
        }
        else if (h < 0)//left
        {
            forward = (forward + turnLeft.forward) / 2;
            pos = (pos + turnLeft.position) / 2;
        }
        mainCamera.forward = Vector3.Slerp(mainCamera.forward, forward, 0.03f);
        mainCamera.position = Vector3.Slerp(mainCamera.position, pos, 0.03f);
    }
}
