using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public abstract class RandomResourceLoader<TItem, TSubject> : MonoBehaviour where TItem : Object where TSubject : Object
{
    [SerializeField] private string _path;

    [SerializeField] private List<TSubject> _subjects;
    
    private ResourceLoader<TItem> _loader;

    protected abstract void Set(TItem item, TSubject subject);

    private void Awake()
    {
        if (_path == null)
            throw new MissingReferenceException();

        _loader = new(_path);

        Load();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Load();
    }
#endif
    protected virtual void Load()
    {
        if (_loader.Items == null || _loader.Items.Count == 0)
        {
            Debug.LogWarning("no items");
            return;
        }

        if (_subjects == null || _subjects.Count == 0)
        {
            Debug.LogWarning("no subjects");
            return;
        }

        foreach (TSubject subject in _subjects)
        {
            int index = Random.Range(0, _loader.Items.Count);
            Set(_loader.Items[index], subject);
        }
    }
}
