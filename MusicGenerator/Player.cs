using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace MusicGenerator
{

    class Player
    {

        public void PlayNote(Note note, int startIndex, int endIndex)
        {
            channelBuilder.Command = ChannelCommand.NoteOn;
            channelBuilder.Data1 = note.NoteName;
            channelBuilder.Data2 = note.VelocityStart;
            channelBuilder.Build();
            track.Insert(startIndex, channelBuilder.Result);

            channelBuilder.Command = ChannelCommand.NoteOff;
            channelBuilder.Data1 = note.NoteName;
            channelBuilder.Data2 = note.VelocityEnd;
            channelBuilder.Build();
            track.Insert(endIndex, channelBuilder.Result);

        }

        private ChannelMessageBuilder channelBuilder;
        private TempoChangeBuilder tempoBuilder;
        private Sequencer s;
        private Track track;

        public void Play2(Block block,SettingsGeneratorBase settingsGenerator)
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

            List<Note> notes = block.GetNotes().Cast<Note>().ToList();

            int index = 0;
            foreach (var note in notes)
            {
                int endIndex = index + note.Length;
                PlayNote(note,index,endIndex);
                index = ++endIndex;
            }


            s.Sequence.Add(track);
            //s.Sequence.Add(track1);

            s.Sequence.Save("testxx.mid");
            s.Start();

        }


        public void Play(Block block, SettingsGeneratorBase settingsGenerator)
        {

            channelBuilder = new ChannelMessageBuilder();
            tempoBuilder = new TempoChangeBuilder();
            s = new Sequencer();
            s.Sequence = new Sequence();


            track = new Track();

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

            List<Note> notes = block.GetNotes().Cast<Note>().ToList();

            int index = 0;
            foreach (var note in notes)
            {
                int endIndex = index + note.Length;
                PlayNote(note, index, endIndex);
                index = ++endIndex;
            }

            s.Sequence.Add(track);

            s.Sequence.Save("testxx.mid");
            s.Start();

        }
    }
}
