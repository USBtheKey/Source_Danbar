using System.Collections;
using UnityEngine;

namespace GameSystem.Weapons
{
    public class Rotator : MonoBehaviour
    {

#if UNITY_EDITOR
        [SerializeField] private bool CHEAT_ROTATE = false;
#endif
        [SerializeField] private float speed = 0;
        [SerializeField] private int rotDirection = 0;
        private Coroutine rotateCoroutine;

#if UNITY_EDITOR
        private void OnEnable()
        {
            if (CHEAT_ROTATE)
            {
                StopAllCoroutines();
                rotateCoroutine = null;
                Rotate();
            }
        }
#endif

        public float Speed 
        { 
            get => speed; 
            set => speed = value  < 0 ? 0: value ; 
        }

        public void Rotate()
        {
            if (rotateCoroutine != null) return;

            rotateCoroutine = StartCoroutine(RotateRoutine());

            IEnumerator RotateRoutine()
            {
                while (true)
                {
                    this.transform.Rotate(transform.forward * speed * -rotDirection * Time.deltaTime);
                    yield return null;
                }
            }
        }

        public void Rotate(float speed, Rotation direction)
        {
            if (rotateCoroutine != null) return;

            this.speed = speed;

            if (direction == Rotation.None)
            {
                rotDirection = 0;
            }
            else if(direction == Rotation.ClockWise)
            {
                rotDirection = 1;
            }
            else //Rotation.CounterClockWise
            {
                rotDirection = -1;
            }

            rotateCoroutine = StartCoroutine(RotateRoutine());

            IEnumerator RotateRoutine()
            {
                while (true)
                {
                    this.transform.Rotate(transform.forward * speed * -rotDirection * Time.deltaTime);
                    yield return null;
                }
            }
        }

        public void Stop()
        {
            if (rotateCoroutine != null)
            {
                StopCoroutine(rotateCoroutine);
                rotateCoroutine = null;
            }
        }

        public void Reverse() => rotDirection = -rotDirection;
    }
}
