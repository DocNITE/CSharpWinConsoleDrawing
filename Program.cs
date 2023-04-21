// System namespace
using System;
using System.Media;

//SoundPlayer playSound ( string wavFile )  
//{  
//    SoundPlayer simpleSound = new SoundPlayer ( wavFile );  
//    try  
//    {  
//        simpleSound.SoundLocation = wavFile;  
//        simpleSound.Load();  
//        simpleSound.Play();  
//    }  
//    catch ( Exception ex )  
//    {  
//, wavFile, ex.Message, ex.StackTrace );  
//    }  
//    return simpleSound;
//}  
//playSimpleSound(AppDomain.CurrentDomain.BaseDirectory+@"\test.wav");

// Инициализируем экран.
Engine.Screen.Initialize(160, 40);
// Инициализируем игру перед запуском.
Game.Controller.Initialize();
// Главный цикл, где все и работает.
while (true) {
    Game.Controller.Update();
    Game.Controller.Render();

    Engine.Screen.Draw();
    Engine.Screen.RestoreBuffer();

    if (Console.KeyAvailable) {
        ConsoleKeyInfo input = Console.ReadKey();
        var running = Game.Controller.KeyHandle(input);

        if (!running) {
            return 0;
        }
    }
}