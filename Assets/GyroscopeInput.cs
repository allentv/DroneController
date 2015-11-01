using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GyroscopeInput : MonoBehaviour
{
	float prevY, currY;
	Text userSensorStatus;
	GameObject canvas;
	const float LOWER_LIMIT = -1.5f;
	const float UPPER_LIMIT = 0.75f;

	// Use this for initialization
	void Start ()
	{
		print("Started GyroscopeInput...\n");
		Input.gyro.enabled = true;
		prevY = currY = 0.0f;
		canvas = GameObject.Find("Canvas");
		userSensorStatus = canvas.GetComponentsInChildren<Text> () [1];
	}
	
	// Update is called once per frame
	void Update ()
	{
		currY = -Input.gyro.rotationRateUnbiased.y;
		transform.Rotate (0, currY, 0);
		// print ("-Input.gyro.rotationRateUnbiased.y => " + (float)Input.gyro.rotationRateUnbiased.y);
		if((currY - prevY) > UPPER_LIMIT) {
			userSensorStatus.text = "Rotate Left";
			StartCoroutine (TestLocal ("RLeft"));
		}
        /*
		else if((currY - prevY) > LOWER_LIMIT && (currY - prevY) < UPPER_LIMIT) {
			userSensorStatus.text = "";
		}
        */
		else if ((currY - prevY) < LOWER_LIMIT) {
			userSensorStatus.text = "Rotate Right";
			StartCoroutine (TestLocal ("RRight"));
		}
	}

	IEnumerator TestLocal(string data){
		var url = "http://localhost:8080/DroneController/save.php";
		var form = new WWWForm();
		form.AddField( "data", data );
		WWW obj = new WWW (url, form);
		yield return obj;
		yield return new WaitForSeconds(0.15f);
		print(obj.text);
	}
}

