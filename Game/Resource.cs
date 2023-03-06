/* 
\_                     _/
  \_                 _/
    \_             _/
      \ _________ /
       |         |
       |   ###   |
       |  #####  |
       |   ###   |
       |_________|
     _/           \_
   _/               \_
 _/                   \_
/                       \

 /\_/\
( o.o )
 > ^ <

|\---/|
| o_o |
 \_^_/

      |\      _,,,---,,_
ZZZzz /,`.-'`'    -.  ;-;;,_
     |,4-  ) )-,_. ,\ (  `'-'
    '---''(_/--'  `-'\_)  Он уснул...

 /\_/\
( o o )
==_Y_==
  `-'

  /\_/\  (
 ( ^.^ ) _)
   \"/  (
 ( | | )
(__d b__)

Спрайты брал отсюда:
https://www.asciiart.eu/animals/cats

Пока что работает крайне отвратительно.
Надо будет создать адекватную систему текстур,
ввидет структуры или класса, и написать какой нибудь
инструмент для его редактирования. Но на первое время пойдет.
*/

namespace Game.Resources;

static class Resource {
  static public string[] TextureCatHappy = new string[] {
     "  /\\_/\\  ( ",
     " ( ^.^ ) _)",
     "   \\'/  (  ",
     " ( | | )   ",
     "(__d b__) "
  };

  static public string[] TextureCatWhat = new string[] {
     "  /\\_/\\  ( ",
     " ( o o ) _)",
     " ==_Y_==(  ",
     " ( `-' ) ",
     "(__d b__) "
  };

  static public string[] TextureCatNormal = new string[] {
     "  /\\_/\\  ( ",
     " ( o.o ) _)",
     "  > ^ < (  ",
     " ( | | )   ",
     "(__d b__) "
  };

  static public string[] TextureCatSurprised = new string[] {
     " |\\---/| ( ",
     " | o_o | _)",
     "  \\_^_/ (  ",
     " ( | | )   ",
     "(__d b__) "
  };

  static public string[] TextureCatSleep = new string[] {
    "      |\\      _,,,---,,_             ",
    "ZZZzz /,`.-'`'    -.  ;-;;,_         ",
    "     |,4-  ) )-,_. ,\\ (  `'-'        ",
    "    '---''(_/--'  `-'\\_)  Он уснул..."
  };
}