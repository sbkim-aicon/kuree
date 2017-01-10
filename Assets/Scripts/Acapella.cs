using System.Diagnostics;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEngine.UI;
using AOT;

public class Acapella : MonoBehaviour {
	static Stopwatch sw = new Stopwatch();
	
	#if UNITY_IOS

	// TTS functions
	[DllImport ("__Internal")]
	private static extern string init(callback_event event_callback);

	[DllImport ("__Internal")]
	private static extern int loadvoice(string voiceid);

	[DllImport ("__Internal")]
	private static extern void speak(string text);

	[DllImport ("__Internal")]
	private static extern void stop();

	// Event callback from TTS
	public delegate void callback_event(int type, int param1, int param2, int param3, int param4);
	
	[MonoPInvokeCallback(typeof(callback_event))]
	static void event_callback(int type, int param1, int param2, int param3, int param4) {
		if (type == 0) { // didFinishSpeaking 
//			Buddy.Instance.acapellaFinished = true;
			Console.Out.WriteLine (" text " + param2 + " - finshed normally : " + param1);
		}
		if (type == 1) { // willSpeakWord
			sw.Stop ();
			Console.Out.WriteLine ("Acapella speak returned elapsed " + sw.Elapsed);
			Console.Out.WriteLine (" word [" + param1 + "-" + param2 + "]");
		}
		if (type == 2) { // willSpeakViseme
			Console.Out.WriteLine (" viseme [" + param1 + "]");
		}
	}
		
	// Button functions
	public void tts_init() {
//		Buddy.Instance.acapellaFinished = false;
		string voicelist = init(new callback_event(Acapella.event_callback));
		if (voicelist == null)
			return;
		
		// voicelist contains voiceid separaed by : 
		string[] voices = voicelist.Split(':');
			
		Console.Out.WriteLine("voices list length: " + voices.Length);
		
		for (int index = 0; index < voices.Length; index++) {
			Console.Out.WriteLine("voices : " + voices[index]);
		} 

		if (voices.Length >= 1)
			loadvoice(voices[0]);
	}

	public void tts_speak(string text) {
		sw.Start ();
		Console.Out.WriteLine ("Acapella speak started");
		speak(text);
	}

	public void tts_stop() {
		stop ();
	}
	#elif UNITY_ANDROID
	public void tts_init()
	{}

	public void tts_speak(string text)
	{}
	#elif UNITY_WEBGL
	public void tts_init()
	{
		Buddy.Instance.acapellaFinished = false;

//		Console.Out.WriteLine ("voices list length: ");
	}

	public void tts_speak(string text)
	{
		sw.Start ();
//		Console.Out.WriteLine ("Acapella speak started");
	}
#endif

}