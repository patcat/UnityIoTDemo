using UnityEngine;
using System.Collections;

public class IoTSkybox : MonoBehaviour {
	public Material clearSky;
	public Material cloudySky;
	
	IEnumerator AdjustSkyToWeather() {
		while (true) {
			string weatherUrl = "http://api.openweathermap.org/data/2.5/weather?zip=2000,au";
			
			WWW weatherWWW = new WWW (weatherUrl);
			yield return weatherWWW;
			Debug.Log (weatherWWW.text);
			JSONObject tempData = new JSONObject (weatherWWW.text);
			
			JSONObject weatherDetails = tempData["weather"];
			string WeatherType = weatherDetails[0]["main"].str;
			Debug.Log (WeatherType);
			
			if (WeatherType == "Clear") {
				RenderSettings.skybox = clearSky;
			} else if (WeatherType == "Clouds" || WeatherType == "Rain") {
				RenderSettings.skybox = cloudySky;
			}
			
			yield return new WaitForSeconds(60);
		}
	}
	
	void Start () {
		StartCoroutine (AdjustSkyToWeather());
	}
}
