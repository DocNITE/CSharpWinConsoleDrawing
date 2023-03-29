// System namespace
using System;
using System.Media;
// Our engine namespace
using Engine;
/*
void playSimpleSound ( string wavFile )  
{  
    SoundPlayer simpleSound = new SoundPlayer ( wavFile );  
    try  
    {  
        simpleSound.SoundLocation = wavFile;  
        simpleSound.Load ( );  
        simpleSound.Play (  );  
    }  
    catch ( Exception ex )  
    {  
        Console.WriteLine ( "Wav File Trouble: {0} \r\n {1} \r\n {2}", wavFile, ex.Message, ex.StackTrace );  
    }  
}  
playSimpleSound(AppDomain.CurrentDomain.BaseDirectory+@"\test.wav");
*/
// Entry point

VirtualScreen vScreen = new VirtualScreen(180, 45); //180 45
// Инициализируем игру перед запуском.
Game.Manager.Initialize(vScreen);
// Главный цикл, где все и работает.
while (true) {
    Game.Manager.PreProcess(vScreen);

    vScreen.Draw();
    vScreen.RestoreBuffer();

    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo input = Console.ReadKey();
        var running = Game.Manager.Process(vScreen, input);

        if (!running)
        {
            return 0;
        }
    }
}