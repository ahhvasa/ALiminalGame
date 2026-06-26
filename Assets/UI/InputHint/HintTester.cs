using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HintTester : MonoBehaviour
{
    public InputHintInfo hint1;
    public InputHintInfo hint2;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            InputHintManadger.Instance.ShowHint(hint1);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            InputHintManadger.Instance.RemoveHint(hint1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            InputHintManadger.Instance.ShowHint(hint2);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            InputHintManadger.Instance.RemoveHint(hint2);
        }
    }
}
