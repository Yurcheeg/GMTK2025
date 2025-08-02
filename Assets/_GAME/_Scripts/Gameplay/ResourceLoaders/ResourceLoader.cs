using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceLoader<T> where T : UnityEngine.Object
{
    public List<T> Items { get; private set; }

    public ResourceLoader(string path) => Items = Resources.LoadAll<T>(path).ToList();
}
