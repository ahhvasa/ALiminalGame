using UnityEngine;

public class RotationRemapper : MonoBehaviour
{
    public Transform target;

    public Axis targetForward = Axis.ZPositive;
    public Axis targetUp = Axis.YPositive;

    public Axis selfForward = Axis.ZPositive;
    public Axis selfUp = Axis.YPositive;

    public enum Axis
    {
        XPositive, XNegative,
        YPositive, YNegative,
        ZPositive, ZNegative
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 forward = GetAxis(target, targetForward);
        Vector3 up = GetAxis(target, targetUp);

        Quaternion targetRotation = Quaternion.LookRotation(forward, up);

        Quaternion selfOffset =
            Quaternion.Inverse(Quaternion.LookRotation(
                AxisVector(selfForward),
                AxisVector(selfUp)));

        transform.rotation = targetRotation * selfOffset;
    }

    static Vector3 GetAxis(Transform t, Axis axis)
    {
        switch (axis)
        {
            case Axis.XPositive: return t.right;
            case Axis.XNegative: return -t.right;
            case Axis.YPositive: return t.up;
            case Axis.YNegative: return -t.up;
            case Axis.ZPositive: return t.forward;
            case Axis.ZNegative: return -t.forward;
        }

        return t.forward;
    }

    static Vector3 AxisVector(Axis axis)
    {
        switch (axis)
        {
            case Axis.XPositive: return Vector3.right;
            case Axis.XNegative: return Vector3.left;
            case Axis.YPositive: return Vector3.up;
            case Axis.YNegative: return Vector3.down;
            case Axis.ZPositive: return Vector3.forward;
            case Axis.ZNegative: return Vector3.back;
        }

        return Vector3.forward;
    }
}