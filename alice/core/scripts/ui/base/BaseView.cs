using Godot;
using System;
using System.Collections.Generic;

namespace Alice.UI;

public partial class BaseView : Control
{
    protected ViewType viewType;
    public ViewType ViewType => viewType;

    protected ViewInfo viewInfo;
    protected List<BaseView> ChildViews = [];

    public ViewLayer Layer => viewType switch
    {
        ViewType.Hud => ViewLayer.HudLayer,
        ViewType.Top => ViewLayer.TopLayer,
        _ => ViewLayer.WindowLayer,
    };

    public event Action OnShowEvent;
    public event Action OnHideEvent;
    public event Action OnCloseEvent;
    public event Action OnFocusEvent;
    public event Action OnRefreshEvent;
    public event Action OnDestroyEvent;

    public override void _Ready()
    {
    }

    # region View Control

    public virtual void Show(Action startCallback = null, Action endCallback = null)
    {
        startCallback?.Invoke();

        GameUI.Register(this);
        this.Visible = true;
        OnShowEvent?.Invoke();

        endCallback?.Invoke();
    }

    public virtual void Hide(Action startCallback = null, Action endCallback = null)
    {
        startCallback?.Invoke();

        this.Visible = false;
        OnHideEvent?.Invoke();

        endCallback?.Invoke();
    }

    public virtual void Close(Action startCallback = null, Action endCallback = null)
    {
        startCallback?.Invoke();

        GameUI.UnRegister(this);
        this.QueueFree();
        OnCloseEvent?.Invoke();

        endCallback?.Invoke();
    }

    public virtual void CloseWithoutUnregister(Action startCallback = null, Action endCallback = null)
    {
        startCallback?.Invoke();

        this.QueueFree();
        OnCloseEvent?.Invoke();

        endCallback?.Invoke();
    }

    protected virtual void OnDestroy()
    {
        OnDestroyEvent?.Invoke();
    }

    protected virtual void ShowChildWindow<T>(T childView) where T : BaseView
    {
        var child = GameUI.ShowWithoutRegister<T>();
        if (child != null)
        {
            child.OnCloseEvent += () => ChildViews.Remove(child);
            ChildViews.Add(child);
        }
    }

    protected virtual void HideAllChildWindows(Action startCallback = null, Action endCallback = null)
    {
        startCallback?.Invoke();

        foreach (var child in ChildViews)
        {
            child.Hide();
        }

        endCallback?.Invoke();
    }

    protected virtual void CloseAllChildWindows(Action startCallback = null, Action endCallback = null)
    {
        startCallback?.Invoke();
        foreach (var child in ChildViews)
        {
            child.Close();
        }
        ChildViews.Clear();
        endCallback?.Invoke();
    }

    # endregion
}
