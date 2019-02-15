using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace MusicGenerator
{
    class Note:BlockBase
    {
     

        public override BlockBase Generate()
        {
            Note note =new Note(SettingsGenerator);
            note.Length = SettingsGenerator.GetLenght();
            note.VelocityStart = SettingsGenerator.GetVelocityStart();
            note.VelocityEnd = SettingsGenerator.GetVelocityEnd();
            note.NoteName = SettingsGenerator.GetNoteName();
            return note;
        }

        public override void GenerateBlocks()
        {
            
        }


        public override BlockBase Clone()
        {
            return new Note(SettingsGenerator){Length = Length,NoteName = NoteName,VelocityStart = VelocityStart,VelocityEnd = VelocityEnd};
        }

        public override List<BlockBase> GetNotes()
        {
            return new List<BlockBase>(){this};
        }


        public int NoteName;

        public int VelocityStart;

        public int VelocityEnd;

        public Note(SettingsGeneratorBase settingsGenerator) : base(settingsGenerator)
        {
        }
    }
}
