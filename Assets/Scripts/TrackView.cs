using System;
using System.Collections.Generic;
using System.Linq;

public class TrackView
{
    private DateTime startTime;
    private List<NoteBlock> blocks;

    public TrackView()
    {
        blocks = new List<NoteBlock>();

        for (int i = 0; i < 100; i++)
        {
            blocks.Add(new NoteBlock()
            {
                Time = i,
                Position = i % 7,
                Type = i % 4 == 0 ? 0 : 1
            });
        }
    }

    public List<NoteBlock> GetBlocks()
    {
        return blocks;
    }

    public double GetTime()
    {
        return (DateTime.UtcNow - startTime).TotalSeconds;
    }

    public void HitBlock(NoteBlock block)
    {
        Console.WriteLine(string.Format("Block @{0} hit!", block.Position));
    }

    public void Start()
    {
        startTime = DateTime.UtcNow;
    }

    public void Stop()
    {

    }
}