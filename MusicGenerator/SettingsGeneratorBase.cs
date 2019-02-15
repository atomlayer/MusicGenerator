using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGenerator
{
    abstract class SettingsGeneratorBase
    {
        public Random random= new Random();
        public abstract int GetVelocityStart();
        public abstract int GetVelocityEnd();
        public abstract int GetLenght();
        public abstract int GetNumberOfBlockForDuplicate();

        public abstract int GetNoteName();

        public abstract int GetGeneralMidiInstrument();

        public abstract int GetTempo();
    }
}
