using Cube;
using DefaultNamespace;
using UnityEngine;

namespace DragAndDrop
{
    public class DraggingCube : DraggingItem<CubeController>
    {
        public override void NotifyDropFailed()
        {
            Value.DestroyCube();
            UITextLogger.Instance.LogText(TextProvider.Get("explode_miss"));
        }
    }
}