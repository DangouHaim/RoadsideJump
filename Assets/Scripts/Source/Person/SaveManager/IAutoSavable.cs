﻿using System;

public interface IAutoSavable
{
    event EventHandler<EventArgs> OnPropertyChanged;
}