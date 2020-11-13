using System;
using UnityEngine;

[Serializable]
public abstract class AutoSaveBase : IAutoSavable
{
    public event EventHandler<EventArgs> OnPropertyChanged = (s, e) => {};

    public AutoSaveBase()
    {
        OnPropertyChanged += (s, e) =>
        {
            SaveManager.Save(s);
        };
    }

    public void Save(object sender)
    {
        OnPropertyChanged.Invoke(sender, new EventArgs());
    }
}
