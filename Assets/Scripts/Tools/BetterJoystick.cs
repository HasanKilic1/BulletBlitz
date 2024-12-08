using UnityEngine;

namespace BetterGames.Tools
{
    public class BetterJoystick : MonoBehaviour
    {
        [Header(" Elements ")]
        [SerializeField] private RectTransform joystickOutline;
        [SerializeField] private RectTransform knob;
        [SerializeField] float moveFactor;
        private Vector3 clickedPosition;
        private bool canControl;

        private Vector3 movement;

        private void Update()
        {
            if (canControl)
            {
                ControlJoystick();
            }
        }

        public void TouchZoneClickCallback()
        {
            clickedPosition = Input.mousePosition;
            joystickOutline.position = clickedPosition;
            ShowJoystick();
        }

        private void ShowJoystick()
        {
            joystickOutline.gameObject.SetActive(true);
            canControl = true;
        }

        private void HideJoystick()
        {
            joystickOutline.gameObject.SetActive(false);
            canControl = false;
            movement = Vector3.zero;
        }

        private void ControlJoystick()
        {
            Vector3 currentPosition = Input.mousePosition;
            Vector3 direction = currentPosition - clickedPosition;

            float moveMagnitude = direction.magnitude * moveFactor / Screen.width;
            moveMagnitude = Mathf.Min(moveMagnitude , joystickOutline.rect.width / 2);

            Vector3 move = direction.normalized * moveMagnitude;
            Vector3 targetPosition = clickedPosition + move;

            knob.position = targetPosition;
            movement = move;

            if(Input.GetMouseButtonUp(0))
            {
                HideJoystick();
            }
        }


        /// <summary>
        /// Returns the Joystick's movement value
        /// </summary>
        public Vector3 GetMovementVector => movement;
    }

}
