using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace MusicGenerator
{
    class Player
    {
        private ChannelMessageBuilder channelBuilder;
        private TempoChangeBuilder tempoBuilder;
        private Sequencer s;
        private Track track;

        public void Play(Block block,SettingsGeneratorBase settingsGenerator)
        {

            channelBuilder = new ChannelMessageBuilder();
            tempoBuilder = new TempoChangeBuilder();
            s = new Sequencer();
            s.Sequence = new Sequence();
            track=new Track();

            tempoBuilder.Tempo = settingsGenerator.GetTempo();
            tempoBuilder.Build();
            track.Insert(0, tempoBuilder.Result);

            channelBuilder.MidiChannel = 1;

            channelBuilder.Command = ChannelCommand.ProgramChange;
            channelBuilder.Data1 = settingsGenerator.GetGeneralMidiInstrument();
            channelBuilder.Data2 = 0;
            channelBuilder.Build();
            track.Insert(0, channelBuilder.Result);

            block.GenerateBlocks();
            block.Getlength();
            block.DefineStartAndEndIndex();
            block.SetGlobalBlockStartIndex(0);
            block.SetGlobalBlockEndIndex(0);
            block.Play(track,channelBuilder);

            s.Sequence.Add(track);
            //s.Sequence.Add(track1);

            s.Sequence.Save("testxx.mid");
            s.Start();

        }
    }
}
