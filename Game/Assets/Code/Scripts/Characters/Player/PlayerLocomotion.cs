using System;
using UnityEngine;

namespace Characters.Player
{
    [DisallowMultipleComponent]
    public class PlayerLocomotion : MonoBehaviour
    {
        public void FaceTowards(FaceDirection faceDirection)
        {
            Transform cachedTransform = transform;
            Vector3 localScale = cachedTransform.localScale;
            
            localScale = faceDirection switch
            {
                FaceDirection.West => new Vector3(-Mathf.Abs(localScale.x), localScale.y, localScale.z),
                FaceDirection.East => new Vector3(Mathf.Abs(localScale.x), localScale.y, localScale.z),
                _ => throw new ArgumentOutOfRangeException(nameof(faceDirection), faceDirection, null)
            };
            
            cachedTransform.localScale = localScale;
        }
    }
}