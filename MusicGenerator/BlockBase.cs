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
 
        public abstract BlockBase Generate();

        public abstract void GenerateBlocks();

        public int Length;

        public SettingsGeneratorBase SettingsGenerator;


        public abstract BlockBase Clone();

        public abstract List<BlockBase> GetNotes();
    }
}
