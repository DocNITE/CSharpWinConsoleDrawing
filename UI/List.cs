using System;

namespace Engine.UI;

// GUI ENGINE

class ListButton {
    public int x;
    public int y;
    public string text;

    public delegate void EventHandler();

    public event EventHandler onClick;

    public ListButton(int _y, int _x, string _text = "") {
        y = _y;
        x = _x;
        text = _text;
    }
    // Нормальное состояние кнопки. Когда она не фокусируется
    public void DrawNormal() {
        Screen.SetText(y, x, text);
    }
    // Рисуется, когда она в фокусе под курсором из VirtualListView
    public void DrawFocusable() {
        Screen.SetText(y, x, "-> "+text, ConsoleColor.Black, ConsoleColor.Yellow);
    }
    // ...
    public void Click() {
        if (onClick == null) return;

        onClick();
    }
}

class ListView {
    public List<ListButton> items = new List<ListButton>();
    public int x;
    public int y;
    public int padding;
    public bool visible;

    public ListButton? focused = null;

    public ListView(int _y = 5, int _x = 5, int _padding = 1) {
        x = _x;
        y = _y;
        padding = _padding;
    }

    public void SetVisible(bool toggle) {
        visible = toggle;

        if (visible)
            Restore();
    }

    public void Restore() {
        if (items.Count < 1) return;

        focused = items[0];
    }

    public void Click() {
        if (focused == null) return;

        focused.Click();
    }

    public void Draw() {
        if (!visible) return;

        var _y = 0;
        foreach (var item in items)
        {
            item.y = y + _y;
            item.x = x;
            if (focused == item)
                item.DrawFocusable();
            else
                item.DrawNormal();

            _y += 1 + padding;
        }
    }

    public void KeyPressed(ConsoleKey key) {
        if (!visible) return;
        if (focused == null) return;

        if (key == ConsoleKey.UpArrow)
            ScrollUp();
        else if (key == ConsoleKey.DownArrow)
            ScrollDown();
        else if (key == ConsoleKey.Enter)
            focused.Click();    
    }

    private void ScrollDown() {
        for(int i = 0; i < items.Count; i++) {
            if (items[i] == focused) {
                if (items.Count-1 < i+1)
                    focused = items[0];
                else
                    focused = items[i+1];

                break;
            }
        }
    }

    private void ScrollUp() {
        for(int i = 0; i < items.Count; i++) {
            if (items[i] == focused) {
                if (i-1 < 0)
                    focused = items[items.Count-1];
                else
                    focused = items[i-1];

                break;
            }
        }
    }
}