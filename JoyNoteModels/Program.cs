using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoyNoteModels
{
    class Program
    {

        private static TrackView trackView = new TrackView();

        static TrackView GetTrackView()
        {
            return trackView;
        }

        static void Main(string[] args)
        {

            var trackView = GetTrackView();

            trackView.Start();

            // get 10 seconds ahead
            var time = trackView.GetTime();
            var blocks = trackView.GetBlocks(time, time + 10);

            Console.WriteLine("Track at time {0} --------------", time);
            foreach (var block in blocks)
            {
                Console.WriteLine(string.Format("@{0} {1} {2}", block.Time, block.Position, block.Type));
            }

            // sleep for 1500 ms
            System.Threading.Thread.Sleep(1500);

            // get next 10 seconds ahead and hit first block
            time = trackView.GetTime();
            blocks = trackView.GetBlocks(time, time + 10);

            Console.WriteLine("Track at time {0} --------------", time);
            foreach (var block in blocks)
            {
                Console.WriteLine(string.Format("@{0} {1} {2}", block.Time, block.Position, block.Type));
            }

            trackView.HitBlock(blocks.First());
        }
    }
}
