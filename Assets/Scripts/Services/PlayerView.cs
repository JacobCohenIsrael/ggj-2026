using UnityEngine;

namespace Overcrowded.Services
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform mesh;
        [SerializeField] private CapsuleCollider capsule;

        private void Awake()
        {
#if UNITY_EDITOR
            mesh.gameObject.SetActive(true);
#endif
        } 
    }
}