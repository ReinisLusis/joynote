public class NoteBlock
{
    public double Time { get; set; }

    public int Note { get; set; }

    public int Octave { get; set; }

    public bool IsGoodBlock { get { return Octave == 3; } }

    public string NoteName { get { return noteNames[Note]; } }

    private static string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    // note
    // C  C# D  D# E  F  F# G  G# A  A# B
    // 0  1  2  3  4  5  6  7  8  9  10 11
}