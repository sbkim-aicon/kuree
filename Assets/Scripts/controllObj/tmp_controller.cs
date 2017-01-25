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
	public int sendSttCount, replySttCount;
	
	/* private */
	[SerializeField]bool m_IsRecording;
	[SerializeField]bool m_LastResultWasFianl;
	[SerializeField]bool m_WaitingForLastFinalResultOfSession;
	[SerializeField]bool m_IsResponse;
	[SerializeField] bool m_RecordStart, m_RecordStop, m_RequestStt, m_ReplyStt, m_RequestHex, m_ReplyHex, m_Acapela;
	[SerializeField]string m_PreviousFinalResults;

	[SerializeField] private string tts_ResultText;
	[SerializeField] private string hex_ResultText;
	[SerializeField] private ILowLevelSpeechRecognition _speechRecognition;
	/*[SerializeField]*/ private SpeechRecognitionModule _speechModule;
	[SerializeField] private ChatUIController _chatUICtr;
//	[SerializeField] private SpeechRecognitionModule module;                    

	[SerializeField] Text Tmp, my;
	[SerializeField] AllUIController aUICtr;
	[SerializeField] Acapella acapella;
	[SerializeField] public Text[] text;
	float delayTime, requestTime;
	bool jungleMove, hexSendChk, isRuntimeRecording;
	bool showTimeTxt;

	public float timechk;
	public SpeechRecognitionModule speechModule
	{
		get
		{
			if (_speechModule != null)
				return _speechModule;
			else 
			{
				_speechModule = (SpeechRecognitionModule)_speechRecognition;
				return _speechModule;
			}
		}
	}

	/* protected */

	/* public */
	public Text botText{get{return Tmp;}}
	public Text userText{get{return my;}}
	public ChatUIController chatUICtr{ get { return _chatUICtr; } }

	/* private method */
	void Start()
	{
		delayTime = 0;
		requestTime = 0;
		_speechRecognition = SpeechRecognitionModule.Instance;
		_speechRecognition.SpeechRecognizedSuccessEvent += SpeechRecognizedSuccessEventHandler;
		_speechRecognition.SpeechRecognizedFailedEvent += SpeechRecognizedFailedEventHandler;
		_speechModule = (SpeechRecognitionModule)_speechRecognition;
//		HexConnect.SendMessage("Hello", (response)=>hexagramResponseCallback(response));
	}

	void Update()
	{	
		if(m_ReplyStt)
		{
			DisplayTimeCheck (Time.deltaTime, 3);
		}

		if (m_RequestHex) {
			DisplayTimeCheck (Time.deltaTime, 4);
		}

		if (m_ReplyHex) {
			DisplayTimeCheck (Time.deltaTime, 5);
		}
		
		if (m_IsResponse) {
			DisplayTimeCheck(Time.deltaTime, 5);
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

		if (hexSendChk) {
//			Debug.Log ("hex Sned Check : " + hexSendChk);
			MessageSendToHex ();
		}
	}

	private void SpeechRecognizedSuccessEventHandler(RecognitionResponse obj)
	{
		//		Debug.Log ("Success");
		m_ReplyStt = false;
		replySttCount++;
		text [8].text = "Reply Index : " + replySttCount.ToString();
//		DisplayTimeCheck(Time.deltaTime, 3);
		Debug.Log (Time.time);
		if(obj != null && obj.results.Length > 0)
		{
			m_PreviousFinalResults += obj.results [0].alternatives [0].transcript + " ";

			tts_ResultText = m_PreviousFinalResults;
//			my.text = obj.results [0].alternatives [0].transcript;
			if (jungleMove) {
				if (tts_ResultText.Contains ("ok") || tts_ResultText.Contains("yes")) {
//					SceneManager.LoadScene ("scene_adventure");
					aUICtr.FadeChangUI ("adventure");
				} else {
					jungleMove = false;
				}
			} else {
				
			}
		}
	}

	private void SpeechRecognizedFailedEventHandler(string obj)
	{
		m_ReplyStt = false;
		replySttCount++;
		text [8].text = "Reply Index : " + replySttCount.ToString();
		Debug.Log ("Failed : " + obj);
		#if UNITY_IPHONE && !UNITY_EDITOR
		acapella.tts_stop ();
		acapella.tts_init ();
		acapella.tts_speak (Tmp.text);
		#endif
	}

	Action<HttpWebResponse> hexagramResponseCallback(HttpWebResponse hexagramResponse)
	{
		m_ReplyHex = false;
		Debug.Log ("Response");
		string txt = hexagramResponse != null ? "SUCCESS" : "FAILED";
		Debug.Log ("Hexagram returned voice " + txt);
		if (hexagramResponse != null) {
			var result = new StreamReader (hexagramResponse.GetResponseStream ()).ReadToEnd ();

			var msg = JSONNode.Parse (result);
			var msgObject = msg [0];
			var msgOOB = msgObject ["oob"];
			string operation = "";

			result = msgObject ["body"] + ":OOB=" + operation;

			hex_ResultText = msgObject ["body"];
		} else {
			hex_ResultText = "";
		}
		m_IsResponse = true;

		if (hex_ResultText.Contains ("jungle")) {
			jungleMove = true;
		} else {
			jungleMove = false;
		}

//		switch (hex_ResultText) {
//		case "Awesome! Let's head out! There's a entire jungle waiting to be explored!":
//			jungleMove = true;
//			break;
//		default:
//			jungleMove = false;
//			break;
//		}

		return null;
	}

	void ShowText()
	{
		DisplayTimeCheck(Time.deltaTime, 6);
//		Debug.Log (Time.time);
		m_IsResponse = false;
		delayTime = 0;
		Tmp.text = hex_ResultText;
		#if UNITY_IPHONE && !UNITY_EDITOR
		acapella.tts_stop ();
		acapella.tts_init ();
		acapella.tts_speak (hex_ResultText);
		#endif
	}

	private void StartRecording()
	{
		sendSttCount = 0;
		text [7].text = "Request Index : " + sendSttCount.ToString();
		replySttCount = 0;
		text [8].text = "Reply Index : " + replySttCount.ToString ();
		timechk = 0;
		showTimeTxt = false;
		DisplayTimeCheck(Time.deltaTime, 0);
		if (!m_IsRecording) 
		{
			Debug.Log ("Start Recording");
			m_IsRecording = true;
			m_WaitingForLastFinalResultOfSession = false;
			m_LastResultWasFianl = false;
			m_PreviousFinalResults = "";
			tts_ResultText = m_PreviousFinalResults;
			_speechModule.isRuntimeDetection = true;
			_speechRecognition.StartRuntimeRecord();
		}
	}

	private void StopRecording()
	{
		DisplayTimeCheck(Time.deltaTime, 1);
		showTimeTxt = true;
		if (m_IsRecording) 
		{
			Debug.Log ("Stop Recording");
			m_IsRecording = false;
			if (m_LastResultWasFianl) 
			{

			}
			else 
			{
				hexSendChk = true;
				isRuntimeRecording = true;
				DisplayTimeCheck (Time.deltaTime, 2);
				m_ReplyStt = true;
				_speechRecognition.StopRuntimeRecord();
			}
		}
	}

	private void MessageSendToHex()
	{
		if (sendSttCount > 0) {
			if (sendSttCount == replySttCount) {
				if (m_ReplyStt)
					m_ReplyStt = false;
				DisplayTimeCheck (Time.deltaTime, 4);
				my.text = tts_ResultText;
				m_ReplyHex = true;
				HexConnect.SendMessage (tts_ResultText, (response) => hexagramResponseCallback (response));
				m_PreviousFinalResults = "";
				tts_ResultText = m_PreviousFinalResults;
				hexSendChk = false;
				requestTime = 0;
			} else if (sendSttCount < replySttCount) {
				if (m_ReplyStt)
					m_ReplyStt = false;
			}
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
		DisplayTimeCheck (Time.deltaTime, 4);
		HexConnect.SendMessage (":reset", (response)=> hexagramResponseCallback(response));
	}

	public void DisplayTimeCheck(float time, int textIdx)
	{
		timechk += time;
		switch (textIdx) {
		case 0:
			text [textIdx].text = "Record Start : " + timechk;
			text [++textIdx].text = "Record Stop : ";
			text [++textIdx].text = "STT Send : ";
			text [++textIdx].text = "STT Respon : ";
			text [++textIdx].text = "Hex Send : ";
			text [++textIdx].text = "Hex Respon : ";
			text [++textIdx].text = "Acapela : ";
			break;
		case 1:
			text [textIdx].text = "Record Stop : " + timechk;
			break;
		case 2:
			text [textIdx].text = "STT Send : " + timechk;
//			if (showTimeTxt)
//				text [textIdx].text = "STT Send : " + timechk;
//			else
//				text [textIdx].text = "STT Send : ";
			break;
		case 3:
//			if (showTimeTxt)
				text [textIdx].text = "STT Respon : " + timechk;
//			else
//				text [textIdx].text = "STT Respon : ";
			break;
		case 4:
//			if (showTimeTxt)
				text [textIdx].text = "Hex Send : " + timechk;
//			else
//				text [textIdx].text = "Hex Send : ";
			break;
		case 5:
//			if (showTimeTxt)
				text [textIdx].text = "Hex Respon : " + timechk;
//			else
//				text [textIdx].text = "Hex Respon : ";
			break;
		case 6:
//			if (showTimeTxt)
				text [textIdx].text = "Acapela : " + timechk;
//			else
//				text [textIdx].text = "Acapela : ";
			break;
		}
	}
}