using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace MusicGenerator
{
    class SettingsGenerator:SettingsGeneratorBase
    {
        public override int GetVelocityStart()
        {
            return 127;
        }

        public override int GetVelocityEnd()
        {
            return 0;
        }

        public override int GetLenght()
        {
            return random.Next(1, 7);
        }

        public override int GetNumberOfBlockForDuplicate()
        {
            return 2;
        }

        public override int GetNoteName()
        {
            return random.Next(40, 90);
        }

        public override int GetGeneralMidiInstrument()
        {
            return (int) GeneralMidiInstrument.ElectricPiano1;
        }

        public override int GetTempo()
        {
            return  (int)(1 / 150.0 * 60000000);
        }
    }
}
