using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioSource goodTrack;
	public AudioSource badTrack;
    public AudioSource hitTrack;

	//public float badTrackOffset;
	public float balance;
    public float hitVolume;
    float hitDuration = 1.0f / 16;

    // Start is called before the first frame update
    void Start()
    {
		
	}

	public void StartAudio()
	{
        goodTrack.time = 35.0f;

        goodTrack.Play();

        if (badTrack.clip) {
			badTrack.Play();
		}

        if (hitTrack.clip)
        {
            hitTrack.Play();
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
        else
        {
            hitVolume = 2.0f;
        }
    }

	// Update is called once per frame
	void Update()
    {
        badTrack.timeSamples = goodTrack.timeSamples;
        hitTrack.timeSamples = goodTrack.timeSamples;

        goodTrack.volume = balance;
		badTrack.volume = 1 - balance;
        hitTrack.volume = hitVolume > 0 ? 1.0f : 0.0f; 

        // restore balance to 1 in 1 second
        balance = System.Math.Min(1.0f, balance + Time.deltaTime);

        // fade out hit track
        hitVolume = System.Math.Max(0.0f, hitVolume - Time.deltaTime / hitDuration);
    }
}
