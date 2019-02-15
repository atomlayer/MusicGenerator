using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace MusicGenerator
{
    abstract class BlockBase
    {
        protected BlockBase(SettingsGeneratorBase settingsGenerator)
        {
            this.SettingsGenerator = settingsGenerator;
        }
        public abstract void Play(Track track, ChannelMessageBuilder channelBuilder);

        public int BlockStartIndex;
        public int BlockEndIndex;

        public int GlobalBlockStartIndex;
        public int GlobalBlockEndIndex;

        public abstract void SetGlobalBlockStartIndex(int highestBlockStartIndex);

        public abstract void SetGlobalBlockEndIndex(int highestBlockEndIndex);

        public abstract BlockBase Generate();

        public abstract void GenerateBlocks();

        public int Length;

        public SettingsGeneratorBase SettingsGenerator;

        public abstract int Getlength();


        public abstract BlockBase Clone();
    }
}
