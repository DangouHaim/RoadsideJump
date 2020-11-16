using System;
using UnityEngine;

[Serializable]
public abstract class AutoSaveBase : IAutoSavable
{
    public event EventHandler<EventArgs> OnUpdate = (s, e) => {};

    public AutoSaveBase()
    {
        OnUpdate += (s, e) =>
        {
            SaveManager.Save(s);
        };
    }

    public void Save(object sender)
    {
        OnUpdate.Invoke(sender, new EventArgs());
    }
}
