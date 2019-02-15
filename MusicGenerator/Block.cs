using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace MusicGenerator
{
    class Block:BlockBase
    {
        private readonly BlockBase _prototypeOfChildren;
        private readonly int _countOfChildren;

        public List<BlockBase> ChildBlocks=new List<BlockBase>();

        public Block(SettingsGeneratorBase settingsGenerator, BlockBase prototypeOfChildren, int countOfChildren) : base(settingsGenerator)
        {
            _prototypeOfChildren = prototypeOfChildren;
            _countOfChildren = countOfChildren;
        }

        public override void Play(Track track, ChannelMessageBuilder channelBuilder)
        {
            foreach (var childBlock in ChildBlocks)
            {
                childBlock.Play(track,channelBuilder);
            }
        }

        public override void SetGlobalBlockStartIndex(int highestBlockStartIndex)
        {
            GlobalBlockStartIndex = BlockStartIndex + highestBlockStartIndex;
            foreach (var childBlock in ChildBlocks)
            {
                childBlock.SetGlobalBlockStartIndex(GlobalBlockStartIndex);
            }
        }

        public override void SetGlobalBlockEndIndex(int highestBlockEndIndex)
        {
            GlobalBlockEndIndex = BlockEndIndex + highestBlockEndIndex;
            foreach (var childBlock in ChildBlocks)
            {
                childBlock.SetGlobalBlockEndIndex(GlobalBlockEndIndex);
            }
        }

        public override  BlockBase Generate()
        {
            return new Block(SettingsGenerator,_prototypeOfChildren, _countOfChildren);
        }

        public override void GenerateBlocks()
        {
            int numberOfBlockForDuplicate = SettingsGenerator.GetNumberOfBlockForDuplicate();
            if (numberOfBlockForDuplicate < _countOfChildren)
            {
                for (int i = 0; i < _countOfChildren- numberOfBlockForDuplicate; i++)
                {
                    var child = _prototypeOfChildren.Generate();
                    child.GenerateBlocks();
                    ChildBlocks.Add(child);
                }
                var duplicateChild = ChildBlocks.Take(numberOfBlockForDuplicate).Select(n=>n.Clone()).ToList();
                ChildBlocks.AddRange(duplicateChild);
            }
            else
            {
                for (int i = 0; i < _countOfChildren ; i++)
                {
                    var child = _prototypeOfChildren.Generate();
                    child.GenerateBlocks();
                    ChildBlocks.Add(child);
                }
            }
            
        }

        public override int Getlength()
        {
            foreach (var childBlock in ChildBlocks)
            {
                Length += childBlock.Getlength();
            }

            return Length;
        }

        public override BlockBase Clone()
        {
            return new Block(SettingsGenerator,_prototypeOfChildren,_countOfChildren)
            {
                ChildBlocks = ChildBlocks.Select(n=>n.Clone()).ToList(),
                Length = Length
            };
        }

        public void DefineStartAndEndIndex()
        {
            int index=0;
            for (int i = 0; i < ChildBlocks.Count; i++ )
            {
                ChildBlocks[i].BlockStartIndex = index;
                index += ChildBlocks[i].Length;
                ChildBlocks[i].BlockEndIndex = index;
                index++;

                if(ChildBlocks[i] is Block)
                    ((Block)ChildBlocks[i]).DefineStartAndEndIndex();

              
            }

        }
    }
}
