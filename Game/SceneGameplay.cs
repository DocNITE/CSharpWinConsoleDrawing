using Engine;
using Game.Resources;
namespace Game.Scenes;

class SceneGameplay : Scene {
    public VirtualListView itemList;
    private string turnInfo = "";
    private string catStatus = "";

    string logo = "@Doruton@";
    int kX = 5;
    int kY = 5;
    int vecX = 1;
    int vecY = 1;
    int timer = 5;
    int currTime = 0;
    bool toright = true;

    public SceneGameplay() {
        itemList = new VirtualListView(4, 4, 1);

        var petButton = new VirtualButton(0, 0, "Погладить кошку");
        petButton.onClick += OnPet;
        itemList.items.Add(petButton);
        var playButton = new VirtualButton(0, 0, "Поиграть с кошкой");
        playButton.onClick += OnPlay;
        itemList.items.Add(playButton);
        var hitButton = new VirtualButton(0, 0, "Ударить кошку");
        hitButton.onClick += OnHit;
        itemList.items.Add(hitButton);
        itemList.SetVisible(true);
    }

    public void OnPet() {
        Cat.happyValue += 25;
        turnInfo = "Вы гладите кошку...";
        catStatus = "Кошка в ответ довольно мурчит.";
        Cat.SetReaction(ECatReaction.HAPPY);
    }
    public void OnPlay() {
        Cat.surpriseValue += 25;
        turnInfo = "Вы играете с кошкой...";
        catStatus = "Кошка выглядит удивленной.";
        Cat.SetReaction(ECatReaction.SURPRISED);
    }
    public void OnHit() {
        Cat.angryValue += 25;
        turnInfo = "Вы избиваете кошку...";
        catStatus = "Кошка на вас шипит.";
        Cat.SetReaction(ECatReaction.ANGRY);
    }

    public override void PreProcess(VirtualScreen scrCtx) {
        if (Cat.angryValue>=100 || Cat.surpriseValue >= 100 || Cat.happyValue >= 100) {
            if (itemList.visible)
                itemList.SetVisible(false);

            Cat.SetReaction(ECatReaction.SLEEP);
            Cat.x = 41;
            Cat.Draw(scrCtx);

            if (Cat.angryValue >= 100)
                scrCtx.SetText(4, 4, "Вы избили кошку, и она показав вам фак упала замертво...", ConsoleColor.Red);
            else if (Cat.surpriseValue >= 100)
                scrCtx.SetText(4, 4, "Кошка вдоволь наигралась и она устала...", ConsoleColor.Magenta);
            else if (Cat.happyValue >= 100)
                scrCtx.SetText(4, 4, "От счастья, кошка прилегла спинкой вниз, показывая пузико...", ConsoleColor.Green);
            // exit from app
            Manager.gameState = EGameState.EXIT;
        } else {
            scrCtx.SetText(6, 45, turnInfo, ConsoleColor.Yellow);
            scrCtx.SetText(7, 45, catStatus, ConsoleColor.Yellow);

            scrCtx.SetText(23, 2, "Счастье - " + Cat.happyValue.ToString() + "%", ConsoleColor.Green);
            scrCtx.SetText(23, 17, "Удивление - " + Cat.surpriseValue.ToString() + "%", ConsoleColor.Magenta);
            scrCtx.SetText(23, 36, "Отвращение - " + Cat.angryValue.ToString() + "%", ConsoleColor.Red);
            Cat.Draw(scrCtx);
        }

        if (currTime >= timer)
        {
            //main logic
            if (vecX >= 1)
            {
                kX++;
            }
            if (vecY >= 1) 
            { 
                kY++;
            }

            if (vecX <= -1)
            {
                kX--;
            }
            if (vecY <= -1) 
            { 
                kY--; 
            }

            if (kX >= scrCtx.width-2)
            {
                vecX = -1;
            }
            if (kY >= scrCtx.height-2)
            {
                vecY = -1;
            }

            if (kX <= 1)
            {
                vecX = 1;
            }
            if (kY <= 1)
            {
                vecY = 1;
            }

            //maing lovoas dend

            currTime = 0;

        } else
        {
            currTime++;
        }
        scrCtx.SetText(kY, kX, logo);

        itemList.Draw(scrCtx);
    }
    public override void Process(VirtualScreen scrCtx, ConsoleKeyInfo input) {
        itemList.KeyPressed(input.Key);
    }
}