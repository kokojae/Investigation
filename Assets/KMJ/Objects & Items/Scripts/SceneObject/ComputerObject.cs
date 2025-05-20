using UnityEngine;

public class ComputerObject : SceneObject
{
    [SerializeField] private Material changedMat = null;
    [SerializeField] private Renderer MonitorRenderer = null;

    protected override bool ZoomInteract()
    {
        if (MonitorRenderer != null)
        {
            if (changedMat != null)
            {
                MonitorRenderer.material = changedMat;
            }
            else
            {
                Debug.LogError("ComputerObject.changedMat is null!!");
            }
        }
        else
        {
            Debug.LogError("ComputerObject.MonitorRenderer is null!!");
        }

        return false;
    }
}
