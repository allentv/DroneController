using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class AccelerometerInput : MonoBehaviour {

	float prevX, prevZ, currX, currZ;
    const float DISPLACEMENT = 0.01f;
	Text userSensorStatus, userLocationSensorStatus;
	GameObject canvas;

	// Use this for initialization
	void Start () {
		print("Started AccelerometerInput...\n");
        //Input.acceleration.enabled = true;
		prevX = currX = 0.0f;
		prevZ = currZ = 0.0f;
		canvas = GameObject.Find("Canvas");
		userSensorStatus = canvas.GetComponentsInChildren<Text> () [0];
        userLocationSensorStatus = canvas.GetComponentsInChildren<Text> () [2];
	}
	
	// Update is called once per frame
	void Update () {
		currX = Input.acceleration.x;
		currZ = -Input.acceleration.z;
		//transform.Translate (currX, 0, currZ);
		/*
        if (currZ - prevZ > 0.2f ) {
			userSensorStatus.text = "Moved Left";
			transform.Translate (0, 0, 0.015f);
		}
        
		if (currX - prevX > -0.12f ) {
			userSensorStatus.text = "Moved Right";
			transform.Translate (0.015f, 0, 0);
		} else {
			userSensorStatus.text = "Moved Left";
			transform.Translate (-0.015f, 0, 0);
		}
        */
        if (currX > 0) {
            userSensorStatus.text = "Moved Right";
			transform.Translate (DISPLACEMENT, 0, 0);
			StartCoroutine(TestLocal("Right"));
        } else  {
            userSensorStatus.text = "Moved Left";
			transform.Translate (-DISPLACEMENT, 0, 0);
			StartCoroutine(TestLocal("Left"));
        }
        if (currZ > 0) {
            userLocationSensorStatus.text = "Moved Back";
			transform.Translate (0, 0, DISPLACEMENT);
			StartCoroutine(TestLocal("Back"));
        } else {
            userLocationSensorStatus.text = "Moved Front";
			transform.Translate (0, 0, -DISPLACEMENT);
			StartCoroutine(TestLocal("Front"));
        }
		prevX = currX;
		prevZ = currZ;
		//print ("Acceleration X: " + Input.acceleration.x + " Acceleration Y: " + Input.acceleration.y + " Acceleration Z: " + Input.acceleration.z);
	}

	IEnumerator TestLocal(string data){
		var url = "http://localhost:8080/DroneController/save.php";
		var form = new WWWForm();
		form.AddField( "data", data );
		WWW obj = new WWW (url, form);
		yield return obj;
		yield return new WaitForSeconds(0.35f);
		print(obj.text);
	}
}
