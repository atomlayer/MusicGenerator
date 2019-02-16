namespace MusicGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //example

            //Definition of structure 
            SettingsGeneratorBase sg =new SettingsGenerator4();
            Block blockLevel1 = new Block(sg, new Note(sg), 4);
            Block blockLevel2 = new Block(sg,blockLevel1, 40);
            Block blockLevel3 = new Block(sg, blockLevel2, 5);
            Block blockLevel4 = new Block(sg, blockLevel3, 4);

            //Create a Midi File
            Player player =new Player();
            player.Play(blockLevel4,sg);
        }
    }
}
