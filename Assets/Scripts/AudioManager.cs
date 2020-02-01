using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioSource goodTrack;
	public AudioSource badTrack;
	//public float badTrackOffset;
	public float balance;
	
	// Start is called before the first frame update
	void Start()
    {

	}

	void OnGUI()
	{
		//balance = EditorGUILayout.Slider(balance, 0, 1);
	}

	// Update is called once per frame
	void Update()
    {
		goodTrack.volume = balance;
		badTrack.volume = 1 - balance;
	}
}
