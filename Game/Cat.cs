using Engine;
using Game.Resources;

namespace Game.Scenes;

enum ECatReaction {
    NORMAL = 0,
    HAPPY = 1,
    SURPRISED = 2,
    ANGRY = 3,
    SLEEP = 4
}

static class Cat {
    static public int x = 61;
    static public int y = 13;
    static string[] catTexture = Resource.TextureCatNormal;
    static public ECatReaction reaction = ECatReaction.NORMAL;

    static public int happyValue = 0;
    static public int surpriseValue = 0;
    static public int angryValue = 0;

    static public void Draw(VirtualScreen scrCtx) {
        for(int i = 0; i < catTexture.Length; i++) {
            scrCtx.SetText(i+y, x, catTexture[i], ConsoleColor.Yellow);
        }
    }

    static public void SetReaction(ECatReaction react) {
        switch (react)
        {
            case ECatReaction.HAPPY:
                catTexture = Resource.TextureCatHappy;
                break;
            case ECatReaction.SURPRISED:
                catTexture = Resource.TextureCatSurprised;
                break;
            case ECatReaction.ANGRY:
                catTexture = Resource.TextureCatWhat;
                break;
            case ECatReaction.SLEEP:
                catTexture = Resource.TextureCatSleep;
                break;
            default:
                catTexture = Resource.TextureCatNormal;
                break;
        }
    }
}