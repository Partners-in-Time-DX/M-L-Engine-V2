using UnityEngine;

namespace Player.Overworld.Mario.Helpers
{
    public class MovementHelper
    {
        private readonly MarioOverworldPlayerController _ctx;

        public MovementHelper(MarioOverworldPlayerController ctx)
        {
            _ctx = ctx;
        }

        public void HandleMovement(Vector3 newMove)
        {
            float rotateAngle = Mathf.Atan2(_ctx.CharacterMove.x, _ctx.CharacterMove.y) * Mathf.Rad2Deg + _ctx.Cam.eulerAngles.y;
            _ctx.transform.rotation = Quaternion.Euler(0f, rotateAngle, 0f);
            
            newMove = Quaternion.Euler(0f, rotateAngle, 0f) * Vector3.forward;

            if (_ctx.CharacterMove.magnitude > 0.1f)
            {
                _ctx.MoveAngle = rotateAngle;
            
                newMove = newMove * _ctx.MoveSpeed * Time.deltaTime;
            
                _ctx.MarioController.Move(newMove);      
            }
        }
    }
}