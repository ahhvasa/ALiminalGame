using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManadger : MonoBehaviour
{
    private readonly List<IUpdateHandler> _updatables = new();

    public void Register(IUpdateHandler updatable)
    {
        if (!_updatables.Contains(updatable))
            _updatables.Add(updatable);
    }

    public void Unregister(IUpdateHandler updatable)
    {
        _updatables.Remove(updatable);
    }

    void Update()
    {
        for (int i = 0; i < _updatables.Count; i++)
        {
            _updatables[i].Update();
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < _updatables.Count; i++)
        {
            _updatables[i].FixedUpdate();
        }
    }

}
