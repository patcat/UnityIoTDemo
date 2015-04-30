using UnityEngine;
using System.Collections;

public class IoTLight : MonoBehaviour {
	public string token = "9c0f99363411f0fd2c650ce1bbd8c0a5a3d4cd2e";
	public string deviceId = "55ff6a065075555341101787";
	public Light sceneLight;

	IEnumerator AdjustLightWithSensor() {
		while (true) {
			string lightUrl = "https://api.spark.io/v1/devices/" + deviceId + "/light?access_token=" + token;
			
			WWW lightWWW = new WWW (lightUrl);
			yield return lightWWW;
			Debug.Log (lightWWW.text);
			JSONObject lightData = new JSONObject (lightWWW.text);
			
			float light = lightData ["result"].n;
			
			sceneLight.intensity = light / 255;
			
			yield return new WaitForSeconds (10);
		}
	}

	void Start () {
		sceneLight = GetComponent<Light>();
		
		StartCoroutine (AdjustLightWithSensor());
	}
}
