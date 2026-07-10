using UnityEngine;
using System.Linq;

public class RoomObjectWall : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;

    public void SetTexture(Room hostRoom, Material material)
    {
        var meshRenderer = meshRenderers.OrderBy(part => (part.transform.position - hostRoom.transform.position).sqrMagnitude)
                .FirstOrDefault();

        meshRenderer.material = material;
    }
}