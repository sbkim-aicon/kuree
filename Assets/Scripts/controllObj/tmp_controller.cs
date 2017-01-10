using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FrostweepGames.SpeechRecognition.Google.Cloud;
using SimpleJSON;

public class tmp_controller : MonoBehaviour 
{
	/* private */
	[SerializeField]bool m_IsRecording;
	[SerializeField]bool m_LastResultWasFianl;
	[SerializeField]bool m_WaitingForLastFinalResultOfSession;
	[SerializeField]bool m_IsResponse;
	[SerializeField]string m_PreviousFinalResults;

	[SerializeField] private string tts_ResultText;
	[SerializeField] private string hex_ResultText;
	[SerializeField] private ILowLevelSpeechRecognition _speechRecognition;
//	[SerializeField] private SpeechRecognitionModule module;

	[SerializeField] Text Tmp, my;
	[SerializeField] AllUIController aUICtr;
	[SerializeField] Acapella acapella;
	float delayTime;
	bool jungleMove;

	/* protected */

	/* public */
	public Text botText{get{return Tmp;}}
	public Text userText{get{return my;}}

	/* private method */
	void Start()
	{
		delayTime = 0;
		_speechRecognition = SpeechRecognitionModule.Instance;
		_speechRecognition.SpeechRecognizedSuccessEvent += SpeechRecognizedSuccessEventHandler;
		_speechRecognition.SpeechRecognizedFailedEvent += SpeechRecognizedFailedEventHandler;
//		HexConnect.SendMessage("Hello", (response)=>hexagramResponseCallback(response));
	}

	void Update()
	{
		if (m_IsResponse) {
			ShowText ();
		} else if(!string.IsNullOrEmpty(Tmp.text) && !m_IsResponse) {
			if (delayTime > 8) {
				delayTime = 0;
				Tmp.text = "";
				my.text = "";
			} else {
				delayTime += Time.deltaTime;
			}
		}
	}

	private void SpeechRecognizedSuccessEventHandler(RecognitionResponse obj)
	{
//		Debug.Log ("Success");
		if(obj != null && obj.results.Length > 0)
		{
			m_PreviousFinalResults = obj.results [0].alternatives [0].transcript;

			tts_ResultText = m_PreviousFinalResults;
			my.text = tts_ResultText;
			if (jungleMove) {
				if (tts_ResultText.Contains ("ok")) {
//					SceneManager.LoadScene ("scene_adventure");
					aUICtr.FadeChangUI("adventure");
				} else {
					jungleMove = false;
				}
			}
			else
				HexConnect.SendMessage (tts_ResultText, (response)=> hexagramResponseCallback(response));
		}
	}

	private void SpeechRecognizedFailedEventHandler(string obj)
	{
		acapella.tts_stop ();
		Debug.Log ("Failed : " + obj);
		Tmp.text = "I did not hear it well, could you tell me again?";
		acapella.tts_init ();
		acapella.tts_speak (Tmp.text);
	}

	Action<HttpWebResponse> hexagramResponseCallback(HttpWebResponse hexagramResponse)
	{
		Debug.Log ("Response");
		string txt = hexagramResponse != null ? "SUCCESS" : "FAILED";
		Debug.Log ("Hexagram returned voice " + txt);
		if (hexagramResponse != null) 
		{
			var result = new StreamReader (hexagramResponse.GetResponseStream ()).ReadToEnd ();

			var msg = JSONNode.Parse (result);
			var msgObject = msg [0];
			var msgOOB = msgObject ["oob"];
			string operation = "";

			result = msgObject ["body"] + ":OOB=" + operation;

			hex_ResultText = msgObject ["body"];
		}
		m_IsResponse = true;

		switch (hex_ResultText) {
		case "Awesome! Let's head out! There's a entire jungle waiting to be explored!":
			jungleMove = true;
			break;
		default:
			jungleMove = true;
			break;
		}

		return null;
	}

	void ShowText()
	{
		acapella.tts_stop ();
		m_IsResponse = false;
		Tmp.text = hex_ResultText;
		acapella.tts_init ();
		acapella.tts_speak (hex_ResultText);
	}

	private void StartRecording()
	{
		if (!m_IsRecording) 
		{
			Debug.Log ("Start Recording");
			m_IsRecording = true;
			m_WaitingForLastFinalResultOfSession = false;
			m_LastResultWasFianl = false;
			m_PreviousFinalResults = "";
			tts_ResultText = m_PreviousFinalResults;
			_speechRecognition.StartRecord ();
		}
	}

	private void StopRecording()
	{
		if (m_IsRecording) 
		{
			Debug.Log ("Stop Recording");
			m_IsRecording = false;
			if (m_LastResultWasFianl) 
			{

			}
			else 
			{
				m_WaitingForLastFinalResultOfSession = true;
				_speechRecognition.StopRecord ();
			}
		}
	}

	private void SaveTextResult()
	{
		Debug.Log ("Save a Text!");
//		m_LastResultWasFianl = result.IsFinal;
		Debug.Log ("result Text Alternatives ");
		if (m_LastResultWasFianl) 
		{
//			m_PreviousFinalResults += result.TextAlternatives [0].Text;
			tts_ResultText = m_PreviousFinalResults;
			Tmp.text = tts_ResultText;
			if (m_WaitingForLastFinalResultOfSession) 
			{
				m_WaitingForLastFinalResultOfSession = false;
			}
		}
		else
		{
//			m_ResultText = m_PreviousFinalResults + result.TextAlternatives [0].Text;
			Tmp.text = tts_ResultText;
		}
	}

	/* protected method */

	/* public method */
	public void RecordingButtonClicked()
	{
		if (m_IsRecording) 
		{
//			module.StopRecord ();
			StopRecording ();
		} 
		else 
		{
//			module.StartRecord ();
			StartRecording ();
		}
	}

	public void ResetButtonClicked()
	{
		Debug.Log ("Rest!");

		HexConnect.SendMessage (":reset", (response)=> hexagramResponseCallback(response));
	}
}
