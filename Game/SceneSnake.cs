using Engine;
using Game.Resources;

namespace Game.Scenes;

class BaseObject
{
    public int oldX;
    public int oldY;
    private int _x;
    private int _y;
    public int x
    {
        get => _x;
        set
        {
            oldX = _x;
            _x = value;
        }
    }
    public int y
    {
        get => _y;
        set
        {
            oldY = _y;
            _y = value;
        }
    }
    public char tex;

    public bool canCollide;
    public bool canEat;

    public BaseObject(int _x, int _y, char _tex)
    {
        oldX = _x;
        oldY = _y;
        x = _x;
        y = _y;
        tex = _tex;
    }
}

class Snake : BaseObject
{
    public List<BaseObject> tail = new List<BaseObject>();
    public Snake(int _x, int _y, char _tex) : base(_x, _y, _tex)
    {
        canCollide = false;
        canEat = false;

    }

    public void addTail(BaseObject obj)
    {
        Map.objs.Add(obj);
        if (tail.Count > 0)
        {
            obj.x = tail[tail.Count - 1].oldX;
            obj.y = tail[tail.Count - 1].oldY;
        }
        else
        {
            obj.x = this.oldX;
            obj.y = this.oldY;
        }
        tail.Add(obj);
    }

    public void updateTails()
    {
        if (this.oldX == this.x && this.oldY == this.y)
            return;

        for (int i = 0; i < tail.Count; i++)
        {
            if (i == 0)
            {
                tail[i].x = this.oldX;
                tail[i].y = this.oldY;
            }
            else
            {
                tail[i].x = tail[i - 1].oldX;
                tail[i].y = tail[i - 1].oldY;
            }
        }
    }
}

class Wall : BaseObject
{
    public Wall(int _x, int _y, char _tex) : base(_x, _y, _tex)
    {
        canCollide = true;
        canEat = false;
    }
}

class Food : BaseObject
{
    public Food(int _x, int _y, char _tex) : base(_x, _y, _tex)
    {
        canCollide = false;
        canEat = true;
    }
}

static class Map
{
    static public List<BaseObject> objs = new List<BaseObject>();
}

class SceneSnake : Scene {
    static public string[] mapInfo = new string[] {
     "##########",
     "#    @   #",
     "#        #",
     "#   ##   #",
     "#  ####  #",
     "#   ##   #",
     "#        #",
     "#        #",
     "##########"
    };
    Snake snake = new Snake(4,4,'@');
    Food food = new Food(0, 0, '$');

    int timer = 50;
    int currTimer = 0;

    ConsoleKey? currKey = null;
  
    static void getEmptyTile(Food food)
    {
        Random rnd = new Random();
        bool posIsDone = false;
        while (!posIsDone)
        {
            int posX = rnd.Next(1, 23);
            int posY = rnd.Next(1, 23);
            foreach (var obj in Map.objs)
            {
                if (obj.x == posX || obj.y == posY)
                    continue;
            }
            posIsDone = true;
            food.x = posX;
            food.y = posY;
        }
    }

    static void make3x3wall(int x, int y)
    {
      for(int _y = 0; _y < 3; _y++)
     {
        for(int _x = 0; _x < 3; _x++)
         {
          var wall = new Wall(x+_x, y+_y, '#');
           Map.objs.Add(wall);
         }
      }
    }
    
    public SceneSnake() {
        for (int y = 0; y < 24; y++)
        {
            for (int x = 0; x < 24; x++)
            {
                if (y == 0 || y == 24 - 1)
                {
                    var wall = new Wall(x, y, '#');
                    Map.objs.Add(wall);
                }
                else if (x == 0 || x == 24 - 1)
                {
                    var wall = new Wall(x, y, '#');
                    Map.objs.Add(wall);
                }

                if (y == 6 && x == 6)
                {
                  make3x3wall(x, y);
                }
                  else if (y == 16 && x == 16)
                {
                  make3x3wall(x, y);
                }
            }
        }
        
        Map.objs.Add(snake);
        Map.objs.Add(food);
        getEmptyTile(food);
    }
    // Используется в основном для рендеринга картинки.
    public override void Render(VirtualScreen scrCtx) {
        foreach (var obj in Map.objs)
        {
            // draw object
            scrCtx.SetPixel(obj.y, obj.x, obj.tex);
        }
    }
    public override void Update(VirtualScreen scrCtx) {
      // take objects
            if (currTimer <= timer)
            {
                currTimer++;
                return;
            }
            else {
                currTimer = 0;
            }

            bool foodEaten = false;
            foreach (var obj in Map.objs)
            {
                // game logic
                if (snake.x == obj.x && snake.y == obj.y && obj.canCollide)
                {
                    Console.Clear();
                    Console.Write("Game over!");
                    Console.ReadKey(true);
                    return;
                }
                else if (snake.x == obj.x && snake.y == obj.y && obj.canEat)
                {
                    foodEaten = true;
                    getEmptyTile(food);
                }
            }

            if (foodEaten)
            {
                var tail = new Wall(0, 0, '@');
                snake.addTail(tail);
            }

            if (currKey == null)
                return;

            if (currKey == ConsoleKey.W)
                { snake.y--; snake.x = snake.x; }
            else if (currKey == ConsoleKey.S)
                { snake.y++; snake.x = snake.x; }
            else if (currKey == ConsoleKey.D)
                { snake.y = snake.y; snake.x++; }
            else if (currKey == ConsoleKey.A)
                { snake.y = snake.y; snake.x--; }

            snake.updateTails();
    }
    // Используется во время нажатия на кнопки
    public override void KeyHandle(VirtualScreen scrCtx, ConsoleKeyInfo input) {
        currKey = input.Key;
    }
}