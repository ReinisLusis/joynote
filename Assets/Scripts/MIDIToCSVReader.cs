using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class MIDIToCSVReader : MonoBehaviour
{
	public TextAsset textAsset;
	private ArrayList timeOccupied = new ArrayList(); // occupied event times

	// Start is called before the first frame update
	void Start()
	{
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

			if (
				// Check if event time is already occupied
				!timeOccupied.Contains(timeVal)

				// Only note_on_c; FIXME: returns
				&& eventVal == "Note_on_c"

			// velocity above 0 means it has a volume
			//&& velocityVal > 30
			)
			{
				timeOccupied.Add(timeVal);

				//TODO: NoteBlocks

				if (velocityVal < 47)
				{
					//TODO: Good blocks
					//var noteBlock = new NoteBlock()
					//{
					//	Time = timeVal, // event time
					//	Position = typeVal, // Channel
					//	Type = positionVal % 4 == 0 ? 0 : 1 // Note tone
					//};
				}
				else
				{
					//TODO: Bad blocks
				}
			}
		}
	}
}
