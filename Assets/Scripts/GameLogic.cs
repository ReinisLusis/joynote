using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject BlockPrefab;
    public int BlockSpacing = 3;
    private TrackView trackView;
    private List<NoteBlock> blocks;

    void Start()
    {
        trackView = new TrackView();
        blocks = trackView.GetBlocks();
        foreach (var block in blocks)
        {
            Instantiate(BlockPrefab, GetBlockPosition(block), Quaternion.identity);
        }
    }

    private Vector3 GetBlockPosition(NoteBlock block)
    {
        var z = System.Convert.ToSingle(block.Time) * 10;

        //   c d 
        // b   e 
        // a g f

        int y = 0;
        int x = 0;
        var blockPosition = block.Position;

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
