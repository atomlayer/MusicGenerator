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
        public override void Play(Track track, ChannelMessageBuilder channelBuilder)
        {
            channelBuilder.Command = ChannelCommand.NoteOn;
            channelBuilder.Data1 = NoteName; 
            channelBuilder.Data2 = VelocityStart; 
            channelBuilder.Build();
            track.Insert(GlobalBlockStartIndex, channelBuilder.Result);

            channelBuilder.Command = ChannelCommand.NoteOff;
            channelBuilder.Data1 = NoteName; 
            channelBuilder.Data2 = VelocityEnd; 
            channelBuilder.Build();
            track.Insert(GlobalBlockEndIndex, channelBuilder.Result);
            Trace.WriteLine($"start {GlobalBlockStartIndex} end {GlobalBlockEndIndex} ");
        }

        

        public override void SetGlobalBlockStartIndex(int highestBlockStartIndex)
        {
            GlobalBlockStartIndex = BlockStartIndex+highestBlockStartIndex;
        }

        public override void SetGlobalBlockEndIndex(int highestBlockEndIndex)
        {
            GlobalBlockEndIndex =BlockEndIndex+highestBlockEndIndex;
        }

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

        public override int Getlength()
        {
            return Length;
        }

        public override BlockBase Clone()
        {
            return new Note(SettingsGenerator){Length = Length,NoteName = NoteName,VelocityStart = VelocityStart,VelocityEnd = VelocityEnd};
        }


        public int NoteName;

        public int VelocityStart;

        public int VelocityEnd;

        public Note(SettingsGeneratorBase settingsGenerator) : base(settingsGenerator)
        {
        }
    }
}
