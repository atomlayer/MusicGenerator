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

        public override BlockBase Clone()
        {
            return new Block(SettingsGenerator,_prototypeOfChildren,_countOfChildren)
            {
                ChildBlocks = ChildBlocks.Select(n=>n.Clone()).ToList(),
                Length = Length
            };
        }

        public override List<BlockBase> GetNotes()
        {
           return ChildBlocks.SelectMany(n=>n.GetNotes()).ToList();
        }
    }
}
