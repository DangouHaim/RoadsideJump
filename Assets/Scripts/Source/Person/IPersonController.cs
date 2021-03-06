﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersonController
{
    void Idle();
    void Die();
    void Respawn();
    void Move();
    bool IsAlive();
}
