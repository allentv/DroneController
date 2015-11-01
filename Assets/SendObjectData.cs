using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;


public class SendObjectData : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print("Started sphere import...\n");
		// StartCoroutine(DownloadData());
		// 	StartCoroutine(UploadData());
		for (int i=0; i<4; i++) {
			//StartCoroutine (TestLocal (i));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator TestLocal(int index){
		var url = "http://localhost:8080/DroneController/save.php";
		var form = new WWWForm();
		form.AddField( "data", index );
		WWW obj = new WWW (url, form);
		yield return obj;
		yield return new WaitForSeconds(1f);
		print(obj.text);
	}

	/*
	IEnumerator UploadData(string someData="")
	{
		var url = "http://pikoproject.com/save1.php";
		var form = new WWWForm();
		form.AddField( "data", "{'var1': '100'}" );
		// form.AddField( "var2", "value2");
		var postw = new WWW( url, form );
		// wait for request to complete
		yield return postw;
		yield return new WaitForSeconds(1f);

		// and check for errors
		if (postw.error == null)
		{
			// request completed!
			print ("Response: " + postw.text);
		} else {
			// something wrong!
			Debug.Log("WWW Error: " + postw.error);
		}
	}

	IEnumerator DownloadData()
	{
		WWW w = new WWW (
			"http://pikoproject.com/fetch.php"
		);
		
		yield return w;

		print("Waiting for sphere definitions\n");
		// Add a wait to make sure we have the definitions
		yield return new WaitForSeconds(1f);
		print("Received sphere definitions\n");

		// Extract the spheres from our JSON results
		ExtractData(w.text);
	}

	void ExtractData(string json)
	{
		// Create a JSON object from the text stream
		JSONObject jo = new JSONObject (json);
		print (jo.list[0]);
	}
	*/
}
