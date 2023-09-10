using System.Collections.Generic;
using Player.Enums;
using UnityEngine;

namespace Player
{
    [RequireComponent(
        typeof(Animator)
    )]
    public class PlayerAnimatorManager : MonoBehaviour
    {
        private const float BlendTime = 0.1f;
        
        private Animator _animator;

        private int _horizontal;
        private int _vertical;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();

            _horizontal = Animator.StringToHash(AnimatorEnum.Horizontal.ToString());
            _vertical = Animator.StringToHash(AnimatorEnum.Vertical.ToString());
        }
        
        public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
        {
            #region Snapping Movement
            horizontalMovement = Snap(
                horizontalMovement, 
                new[] { new Vector2(0.0f, 0.55f), new Vector2(0.55f, 1.0f) }, 
                new[] { 0.5f, 1.0f }, 
                true
            ) ?? horizontalMovement;
            
            verticalMovement = Snap(verticalMovement, 
                new[] { new Vector2(0.0f, 0.55f), new Vector2(0.55f, 1.0f) }, 
                new[] { 0.5f, 1.0f }, 
                true
            ) ?? verticalMovement;
            #endregion
            
            _animator.SetFloat(_horizontal, horizontalMovement, BlendTime, Time.deltaTime);
            _animator.SetFloat(_vertical, verticalMovement, BlendTime, Time.deltaTime);
        }

        private static float? Snap(float floatToSnap, IList<Vector2> conditions, IReadOnlyList<float> results, bool includeNegative)
        {
            for (int index = 0; index < conditions.Count; index++)
            {
                if (floatToSnap > conditions[index].x && floatToSnap < conditions[index].y)
                {
                    return results[index];
                }

                if (!includeNegative) continue;
                
                if (floatToSnap < conditions[index].x && floatToSnap > conditions[index].y)
                {
                    return results[index];
                }
            }

            return null;
        }
    }
}
