using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class MIDIToCSVReader : MonoBehaviour
{
    public TextAsset textAsset;
    private List<NoteBlockRaw> blocks;
    // Start is called before the first frame update
    void Start()
    {
        blocks = new List<NoteBlockRaw>();
        string fs = textAsset.text;
        string[] fLines = Regex.Split(fs, "\n|\r|\r\n");

        for (int i = 0; i < fLines.Length; i++)
        {

            string valueLine = fLines[i];
            string[] values = Regex.Split(valueLine, ",");

            if (values.Length <= 5)
            {
                continue;
            }

            int timeVal = 0;
            int.TryParse(values[1], out timeVal);

            string eventVal = values[2].ToString().Trim();

            int typeVal = 0;
            int.TryParse(values[3], out typeVal);

            int positionVal = 0;
            int.TryParse(values[4], out positionVal);

            int velocityVal = 0;
            int.TryParse(values[5], out velocityVal);

            if (eventVal == "Note_on_c")
            {
                // note
                // C  C# D  D# E  F  F# G  G# A  A# B
                // 0  1  2  3  4  5  6  7  8  9  10 11

                // 41 = F, C2 -> 36
                // middle / neutral - F

                // C2 octave - good blocks

                int note = positionVal % 12;
                int octave = positionVal / 12;


                // Debug.Log(string.Format("note {0} @ {1}", positionVal, timeVal));

                var noteBlock = new NoteBlockRaw()
                {
                    Time = timeVal / 192.0, // event time 10 == 1 sec
                    Note = note,
                    Octave = octave,
                };
                blocks.Add(noteBlock);
            }
        }
    }

    public List<NoteBlockRaw> GetBlocks()
    {
        return blocks;
    }
}
