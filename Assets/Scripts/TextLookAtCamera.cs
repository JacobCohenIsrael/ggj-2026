using Reflex.Attributes;
using UnityEngine;

namespace Overcrowded
{
    public class TextLookAtCamera : MonoBehaviour
    {
        [Inject] private Camera _camera;

        private void LateUpdate()
        {
            var direction = transform.position - _camera.transform.position;
            var pos = transform.position + direction;
            transform.LookAt(pos);
        }
    }
}