using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            SettingsGeneratorBase sg =new SettingsGenerator3();
            Block blockLevel1 = new Block(sg, new Note(sg), 5);
            Block blockLevel2 = new Block(sg,blockLevel1, 3);
            Block blockLevel3 = new Block(sg, blockLevel2, 10);
            Block blockLevel4 = new Block(sg, blockLevel3, 10);
            Block blockLevel5 = new Block(sg, blockLevel4, 10);

            Player player =new Player();
            player.Play(blockLevel5,sg);
        }
    }
}
