using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exceptions {
    
}
public class ResourcesNotFoundException : Exception
{
    public ResourcesNotFoundException(){}
    public ResourcesNotFoundException(string message) : base(message){}
}
