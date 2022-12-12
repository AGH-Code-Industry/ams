using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMechanism
{
    bool Activate(IInteractable activator);
}
