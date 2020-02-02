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

	public void StartAudio()
	{
		goodTrack.Play();
		if (badTrack.clip) {
			badTrack.Play();
		}
	}

	public float GetAudioTime()
	{
		return (float)goodTrack.timeSamples / (float)goodTrack.clip.frequency;
	}

	void OnGUI()
	{
		//balance = EditorGUILayout.Slider(balance, 0, 1);
	}

    public void BlockHit(NoteBlock block)
    {
        if (!block.IsGoodBlock)
        {
            balance = 0;
        }
    }

	// Update is called once per frame
	void Update()
    {
        badTrack.timeSamples = goodTrack.timeSamples;

        goodTrack.volume = balance;
		badTrack.volume = 1 - balance;

        // restore balance to 1 in 1 second
        balance = System.Math.Min(1.0f, balance + Time.deltaTime);
	}
}
