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
    private TrackView trackView;
    private List<NoteBlock> blocks;
    MIDIToCSVReader refScript;

    DateTime startTime;
    DateTime audioTime;
    bool hasStartedAudio;

    void Start()
    {
        refScript = GetComponent<MIDIToCSVReader>();

        startTime = DateTime.UtcNow;
        audioTime = startTime + TimeSpan.FromSeconds(3);
        hasStartedAudio = false;

        // trackView = new TrackView();
        blocks = refScript.GetBlocks();
        Debug.Log(blocks.Count);
        foreach (var block in blocks)
        {
            var newBlock = Instantiate(BlockPrefab, GetBlockPosition(block), Quaternion.identity);
            newBlock.GetComponent<Renderer>().material = block.Type == 1 ? GoodBlockMaterial : BadBlockMaterial;
        }
    }

    public double GetGameTime()
    {
        if (!hasStartedAudio)
        {
            var time = DateTime.UtcNow;
            if (time < audioTime)
            {
                return (time - audioTime).TotalSeconds;
            }
            else
            {
                hasStartedAudio = true;
                var audioManager = GetComponent<AudioManager>();
                audioManager.StartAudio();
                return 0.0;
            }
        }
        else
        {
            var audioManager = GetComponent<AudioManager>();
            return audioManager.GetAudioTime();
        }
    }

    private Vector3 GetBlockPosition(NoteBlock block)
    {
        var z = System.Convert.ToSingle(block.Time) * 10;

        //   c d 
        // b   e 
        // a g f
        Debug.Log(string.Format("pos {0}, time {1}, type {2}", block.Position, block.Time, block.Type));
        int y = 0;
        int x = 0;
        var blockPosition = block.Position % 7;

        if (blockPosition < 2)
        {
            y = BlockSpacing;
        }
        else if (blockPosition > 2 && blockPosition < 7)
        {
            y = -BlockSpacing;
        }

        if (blockPosition > 0 && blockPosition < 4)
        {
            x = BlockSpacing;
        }
        else if (blockPosition > 4)
        {
            x = -BlockSpacing;
        }

        return new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
