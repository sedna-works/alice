namespace Alice.UI;

public enum ViewType
{
    Hud,
    FullWindow,
    Window,
    Top,
}

public enum ViewLayer
{
    HudLayer = 0,
    WindowLayer = 100,
    TopLayer = 500,
}

public class ViewInfo
{
    public string Name;
    public string RootPath;
}
