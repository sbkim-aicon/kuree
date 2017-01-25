using System;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding.Serialization.JsonFx;

public class HexConnect : MonoBehaviour 
{

	private const string HexagramURL = "https://hex-os-buddy-jr-api.herokuapp.com";
	private const string chat = "/v1/users/sbkim/chats/buddy";
	private const string AccesseKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6InNia2ltIiwiZXhwaXJlcyI6IjIwMTctMTItMDdUMTc6MzU6MjEuMjc3WiJ9.4xbKA0cmIXyhiztPpfGPtmTibutJPZNFD_-EN5rqD9k";
	public tmp_controller tmpCtr;

	public static void SendMessage(string message, Action<HttpWebResponse> responseAction)
	{			
		Debug.Log ("SendMessage " + (HexagramURL + chat));
		Debug.Log (Time.time);
		string json = JsonWriter.Serialize (new ChatBotMessage (message));
		var httpWebRequest = (HttpWebRequest)WebRequest.Create (HexagramURL + chat);
		httpWebRequest.ContentType = "application/json";
		httpWebRequest.Method = "POST";
		httpWebRequest.Headers ["access-token"] = AccesseKey;

		using (var streamWriter = new StreamWriter (httpWebRequest.GetRequestStream ())) 
		{
			
			streamWriter.Write (json);
			Debug.Log ("StreamWriter");
		}

		DoWithResponse (httpWebRequest, responseAction);
	}

	private static void DoWithResponse(HttpWebRequest request, Action<HttpWebResponse> responseAction)
	{
		Debug.Log ("DoWithResponse");
		Action wrapperAction = () => {
			request.BeginGetResponse (new AsyncCallback ((iar) => {
				try {
					Debug.Log ("DoWithResponse");
					var response = (HttpWebResponse)((HttpWebRequest)iar.AsyncState).EndGetResponse (iar);
					responseAction(response);
				} catch (Exception e) {
					Debug.Log ("DoWithResponse");
					responseAction (null);
				}
			}), request);
		};
		wrapperAction.BeginInvoke (new AsyncCallback ((iar) => {
			Debug.Log ("BeginInvoke");
			var action = (Action)iar.AsyncState;
			action.EndInvoke (iar);
		}), wrapperAction);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
