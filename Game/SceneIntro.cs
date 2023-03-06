using Engine;
using Game.Resources;

namespace Game.Scenes;

class SceneIntro : Scene {

    string[] text;
    int stage = 0;

    public SceneIntro() {
        text = new string[] {
            " Ты ощущаешь легкий порыв ветра,",
            "идущий из открытого портала...",
            "", 

            " Тебе холодно... Ты дрожишь...",
            "но ты не хочешь уходить...",
            "",

            " Вскоре, ты стал улавливать своим слухом легкий цокот",
            "маленьких ножек. Это 'нечто' стало легонько мурлыкать...",
            "",

            " И ты, в слабом свете от твоего фонаря, посреди бетонного",
            "подземелья, замечаешь кошку... Обычную кошку, что?",
            "",

            " Что она тут делает? Впрочем, это",
            "наврядли должно тебя волновать.",
            "",

            " Ты решился погладить кошку...",
            "",
            ""

        };
    }
    // Используется в основном для рендеринга картинки.
    public override void PreProcess(VirtualScreen scrCtx) {
        for(int i = 0; i < 3; i++) {
            scrCtx.SetText(i+4, 4, text[i+stage], ConsoleColor.Yellow);
        }

        if (stage >= 9) {
            for(int i = 0; i < Resource.TextureCatNormal.Length; i++) {
                scrCtx.SetText(i+13, 61, Resource.TextureCatNormal[i], ConsoleColor.Yellow);
            }
        }

        stage += 3;

        if (stage + 3 > text.Length) {
            Manager.gameState = EGameState.GAMEPLAY;
            Manager.gameScene = new SceneGameplay();
            return;
        }
    }
    // Используется во время нажатия на кнопки
    public override void Process(VirtualScreen scrCtx, ConsoleKeyInfo input) {

    }
}