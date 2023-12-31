using Player.Enums;
using Statics;
using UnityEngine;

namespace Player
{
    [RequireComponent(
        typeof(Animator)
    )]
    public class PlayerAnimatorManager : MonoBehaviour
    {
        private const float BlendTime = 0.1f;
        
        public Animator Animator { get; private set; }

        private int _horizontal;
        private int _vertical;
        
        public const float StartWalk = 0.0f;
        public const float StartRun = 0.5f;
        public const float StartSprint = 2.0f;
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();

            _horizontal = Animator.StringToHash(AnimatorEnum.Horizontal.ToString());
            _vertical = Animator.StringToHash(AnimatorEnum.Vertical.ToString());
        }

        public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
        {
            Animator.SetBool(AnimatorEnum.isInteracting.ToString(), isInteracting);
            Animator.CrossFade(targetAnimation, 0.2f);
        }
        
        public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool setSprintAnimation)
        {
            #region Snapping Movement
            horizontalMovement = GeneralAnimatorManager.Snap(
                horizontalMovement,
                new[] { new Vector2(0.0f, 0.55f), new Vector2(0.55f, 1.0f) },
                new[] { 0.5f, 1.0f },
                true
            );

            verticalMovement = GeneralAnimatorManager.Snap(
                verticalMovement,
                new[] { new Vector2(0.0f, 0.55f), new Vector2(0.55f, 1.0f) },
                new[] { 0.5f, 1.0f },
                true
            );
            #endregion

            if (setSprintAnimation)
            {
                verticalMovement = 2.0f;
            }
            
            Animator.SetFloat(_horizontal, horizontalMovement, BlendTime, Time.deltaTime);
            Animator.SetFloat(_vertical, verticalMovement, BlendTime, Time.deltaTime);
        }

        
    }
}
