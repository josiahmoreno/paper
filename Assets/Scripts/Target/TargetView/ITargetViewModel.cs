using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetViewModel 
{
    bool IsTargeted { get; }

    event EventHandler<bool> OnTargeted; 
}
