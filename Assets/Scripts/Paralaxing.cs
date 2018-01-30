using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaxing : MonoBehaviour {

	public Transform[] backgrounds; 			//Array of all the back and forgrounds to be parallaxed
	private float[] paralaxScales; 			//Camera speed and background speed
	public float smoothing = 1f;				//Smoothing of parallaxing. Has to be above 0

	private Transform cam; 						//main camera transform refrence
	private Vector3 previousCamPos;				//the position of the camera in the previous frame

	//called before start.
	void Awake () {
		//camera reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		previousCamPos = cam.position;

		//assigining coresponding paralaxScales
		paralaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++) {
			paralaxScales[i] = backgrounds [i].position.z * -1;
		}
	}
	
	// Update is called once per frame
	void Update () {

		//each background
		for (int i = 0; i < backgrounds.Length; i++) {
			float paralax = (previousCamPos.x - cam.position.x) * paralaxScales [i];

			float backgroundTargetPosX = backgrounds [i].position.x + paralax;

			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			//fade between current pos and the target pos
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		//set previousCamPos to the cameras position at the end of the frame
		previousCamPos = cam.position;
	}
}
