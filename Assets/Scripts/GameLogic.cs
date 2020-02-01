using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLogic : MonoBehaviour
{
    public GameObject BlockPrefab;
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
        Debug.Log(string.Format("time {0:0.000}, octave {1}, note {2}", block.Time / 10.0, block.Octave, block.NoteName));

        var z = System.Convert.ToSingle(block.Time) * 10;

        // MAP
        // C  F# D 
        // G  F  E
        // A  D# B  
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
