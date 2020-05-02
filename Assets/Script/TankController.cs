using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {

    public GameObject turret;
    public GameObject gun;
    public float gunAngleMax;// = 345;
    public float gunAngleMin;// = 5;
    public float forwardSpeed;// = 15;
    public float rotSpeed;// = 1.0f;
    public float smooth;// = 1.0f;

    float movementInputValue = 0.0f;
    float turnInputValue = 0.0f;
    /*-----------audio----------------------*/
    public AudioSource movementAudio;
    public AudioClip engineStop;
    public AudioClip engineMoving;
    float originalPitch;
    /*-----------particle-------------------*/
    //public ParticleSystem[] par;
    private void Start()
    {
        originalPitch = movementAudio.pitch;
    }
    private void Update()
    {
        //move
        turnInputValue = Input.GetAxis("Horizontal");
        movementInputValue = Input.GetAxis("Vertical");
        Vector3 mv = new Vector3(0, 0, movementInputValue) * Time.deltaTime * forwardSpeed;
        this.transform.Translate(mv, Space.Self);
        if (turnInputValue != 0)
        {
            Vector3 mh = new Vector3(turnInputValue, 0, 0.1f) * Time.deltaTime;
            this.transform.Translate(mh, Space.Self);//rotateSpeed * h/Mathf.PI
            //print(rotSpeed * h);
            this.transform.Rotate(Vector3.up * Time.deltaTime, rotSpeed * turnInputValue);
        }
        //rotation
        //float rh = Input.GetAxis("rHorizontal");
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 dir = Vector3.right * -1;//對x軸轉
            gun.transform.Rotate(gunRotateAngle(dir), Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 dir = Vector3.right;
            gun.transform.Rotate(gunRotateAngle(dir), Space.Self);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 dir = Vector3.forward * -1;//對z軸轉
            turret.transform.Rotate(dir, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 dir = Vector3.forward;
            turret.transform.Rotate(dir, Space.Self);
        }
        EngineAudio();
    }
    Vector3 gunRotateAngle(Vector3 axis)
    {
        Vector3 dir = axis;
        Quaternion angle = Quaternion.Euler(gun.transform.localEulerAngles);

        angle.eulerAngles += dir;
        //print(angle.eulerAngles + "  " + dir);
        if (angle.eulerAngles.x >= gunAngleMax|| angle.eulerAngles.x <= gunAngleMin)
            return dir;
        else
            return Vector3.zero;
    }
    private void EngineAudio()
    {
        // If there is no input (the tank is stationary)...
        if (Mathf.Abs(movementInputValue) < 0.1f && Mathf.Abs(turnInputValue) < 0.1f)
        {
            // ... and if the audio source is currently playing the driving clip...
            if (movementAudio.clip == engineMoving)
            {
                // ... change the clip to idling and play it.
                movementAudio.clip = engineStop;
                movementAudio.pitch = Random.Range(originalPitch - 0.2f, originalPitch + 0.2f);
                movementAudio.Play();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (movementAudio.clip == engineStop) 
            {
                // ... change the clip to driving and play.
                movementAudio.clip = engineMoving;
                movementAudio.pitch = Random.Range(originalPitch - 0.2f, originalPitch + 0.2f);
                movementAudio.Play();
            }
        }
    }
}
