using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace MusicGenerator
{
    class SettingsGenerator4:SettingsGeneratorBase
    {
        public override int GetVelocityStart()
        {
            return random.Next(100,127);
        }

        public override int GetVelocityEnd()
        {
            return random.Next(0,30);
        }

        public override int GetLenght()
        {
            return random.Next(1, 15);
        }

        public override int GetNumberOfBlockForDuplicate()
        {
            return 2;
        }

        int Parabola(int x, int noteName,int y)
        {
            return (int) (- Math.Pow(x - noteName,2)/ 5.0   + y);
        }

        private int previousNote=70;

        public override int GetNoteName()
        {
            List<int> randData=new List<int>();
            for (int i = 30; i < 90; i++)
            {
                int fun = Parabola(i, previousNote, 30);
                if(fun>0)
                    randData.AddRange(Enumerable.Repeat(i,fun));
            }
            int noteName=randData[random.Next(randData.Count - 1)];
            previousNote = noteName;
            return noteName;
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
