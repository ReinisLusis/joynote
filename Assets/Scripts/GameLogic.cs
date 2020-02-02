using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public GameObject BlockPrefab;
    public GameObject GroundPrefab;
    public int BlockSpacing = 3;
    public Material GoodBlockMaterial;
    public Material BadBlockMaterial;
    public GameObject PlayerGameObject;

    private List<NoteBlock> blocks;
    private MIDIToCSVReader refScript;

    private DateTime startTime;
    private DateTime audioTime;
    private bool hasStartedAudio;
    private AudioManager audioManager;
    private PlayerMovement playerMovement;
    void Start()
    {
        refScript = GetComponent<MIDIToCSVReader>();
        audioManager = GetComponent<AudioManager>();
        playerMovement = PlayerGameObject.GetComponent<PlayerMovement>();

        startTime = DateTime.UtcNow;
        audioTime = startTime + TimeSpan.FromSeconds(3);
        hasStartedAudio = false;

        blocks = refScript.GetBlocks();

        foreach (var block in blocks)
        {
            var newBlock = Instantiate(BlockPrefab, GetBlockPosition(block), Quaternion.identity);
            newBlock.GetComponent<Renderer>().material = block.IsGoodBlock ? GoodBlockMaterial : BadBlockMaterial;

            newBlock.AddComponent<NoteBlock>();
            var cBlock = newBlock.GetComponent<NoteBlock>();
            cBlock.Time = block.Time;
            cBlock.Note = block.Note;
            cBlock.Octave = block.Octave;

            newBlock.tag = block.IsGoodBlock ? "Block" : "BadBlock";
        }

        for (var i = 0; i < 30; i++)
        {
            Instantiate(GroundPrefab, new Vector3(0, -10, i * 1200), Quaternion.Euler(-90, 90, 0));
        }
    }

    void Update()
    {
        var time = GetGameTime();
        playerMovement.UpdateForwardPosition(time);
    }

    public float GetGameTime()
    {
        if (!hasStartedAudio)
        {
            var time = DateTime.UtcNow;
            if (time < audioTime)
            {
                return Convert.ToSingle((time - audioTime).TotalSeconds);
            }
            else
            {
                hasStartedAudio = true;
                audioManager.StartAudio();
                return 0;
            }
        }
        else
        {
            return audioManager.GetAudioTime();
        }
    }

    private Vector3 GetBlockPosition(NoteBlock block)
    {
        // Debug.Log(string.Format("time {0:0.000}, octave {1}, note {2}", block.Time / 10.0, block.Octave, block.NoteName));

        var z = System.Convert.ToSingle(block.Time) * 10;

        // MAP
        // A  D# B 
        // G  F  E
        // C  F# D 

        switch (block.Note)
        {
            case 0: return new Vector3(-BlockSpacing, -BlockSpacing, z);  // C
            case 1: return new Vector3(0, 0, z);  // C#
            case 2: return new Vector3(BlockSpacing, -BlockSpacing, z);  // D
            case 3: return new Vector3(0, BlockSpacing, z);  // D#
            case 4: return new Vector3(BlockSpacing, 0, z);  // E
            case 5: return new Vector3(0, 0, z);  // F
            case 6: return new Vector3(0, -BlockSpacing, z);  // F#
            case 7: return new Vector3(-BlockSpacing, 0, z);  // G
            case 8: return new Vector3(0, 0, z);  // G#
            case 9: return new Vector3(-BlockSpacing, BlockSpacing, z);  // A
            case 10: return new Vector3(0, 0, z); // A#
            case 11: return new Vector3(BlockSpacing, BlockSpacing, z); // B
        }

        return new Vector3(0, 0, 0);
    }

}
