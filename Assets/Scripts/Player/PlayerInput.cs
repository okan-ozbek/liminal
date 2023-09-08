using UnityEngine;

namespace Player
{
    public class PlayerInput
    {
        public Vector3 MovementDirection { get; private set; }
        
        public bool PressedJump { get; private set; }
        public bool PressedSprint { get; private set; }

        public void OnUpdate()
        {
            MovementDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

            PressedJump = Input.GetKey(KeyCode.Space);
            PressedSprint = Input.GetKey(KeyCode.LeftShift);
        }
    }
}