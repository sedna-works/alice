using Godot;

using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Alice.UI;

public partial class GameUI : Node
{
    private class UIConfig
    {
        public List<ViewInfo> views;
    }

    public static GameUI Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }


    private static readonly string CONFIG_PATH = "res://config/ui_config.json";

    private UIConfig uiConfig;

    private Dictionary<ViewLayer, Control> layerDict = [];
    private Dictionary<string, BaseView> viewDict = [];

    private void LoadConfig()
    {
        string configText = GD.Load<FileAccess>(CONFIG_PATH).GetAsText();
        uiConfig = JsonSerializer.Deserialize<UIConfig>(configText);
    }

    public static T ShowWithoutRegister<T>(Action startCallback = null, Action endCallback = null) where T : BaseView
    {
        throw new NotImplementedException();
    }

    internal static T Register<T>(T view) where T : BaseView
    {
        string viewName = view.GetType().Name;
        if (Instance.viewDict.ContainsKey(viewName))
        {
            GD.PrintErr($"View {viewName} is already registered.");
            return null;
        }
        Instance.viewDict[viewName] = view;
        return view;
    }

    internal static T UnRegister<T>(T view) where T : BaseView
    {
        string viewName = view.GetType().Name;
        if (!Instance.viewDict.ContainsKey(viewName))
        {
            GD.PrintErr($"View {viewName} is not registered.");
            return null;
        }
        Instance.viewDict.Remove(viewName);
        return view;
    }
}
