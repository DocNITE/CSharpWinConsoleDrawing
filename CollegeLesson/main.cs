using System;
using System.Collections.Generic;

class zScreen
{
    char[,] buffer;
    int height;
    int width;
    public zScreen(int h, int w)
    {
        height = h;
        width = w;
        buffer = new char[h, w];

        RestoreBuffer();
    }

    public void SetPixel(int y, int x, char symbol)
    {
        buffer[y, x] = symbol;
    }

    public void Draw()
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);

        string symbols = "";
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                symbols = symbols + buffer[y, x];
            }

            symbols = symbols + "\n";
        }
        Console.Write(symbols);
    }

    public void RestoreBuffer()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                buffer[y, x] = ' ';
            }
        }
    }

}

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

class Program
{
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

    static void Main(string[] args)
    {
        var scrPort = new zScreen(24, 24);
        var snake = new Snake(4,4,'@');
      
        /*
        for (int y = 0; y < mapInfo.Length; y++)
        {
          for (int x = 0; x < mapInfo[y].Length; x++)
          {
            if (mapinfo[y][x] == "#")
            {
              var wall = new Wall(x, y, '#');
              Map.objs.Add(wall);
            }
            else if (mapinfo[y][x] == "@")
            {
              snake = new Snake(4, 4, '@');
            }
          }
        }
      */
        // make map walls OLD METHOD
        
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

        var food = new Food(0, 0, '$');
        Map.objs.Add(food);
        getEmptyTile(food);
        while (true)
        {
            // take objects
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
                // draw object
                scrPort.SetPixel(obj.y, obj.x, obj.tex);
            }

            if (foodEaten)
            {
                var tail = new Wall(0, 0, '@');
                snake.addTail(tail);
            }

            scrPort.Draw();
            scrPort.RestoreBuffer();

            ConsoleKeyInfo input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.W)
            { snake.y--; snake.x = snake.x; }
            else if (input.Key == ConsoleKey.S)
            { snake.y++; snake.x = snake.x; }
            else if (input.Key == ConsoleKey.D)
            { snake.y = snake.y; snake.x++; }
            else if (input.Key == ConsoleKey.A)
            { snake.y = snake.y; snake.x--; }

            snake.updateTails();
        }
    }
}