
using System.Collections;
using UnityEngine;


namespace GameSystem
{
    public class WeaponSprayer : MonoBehaviour
    {
        [SerializeField] private bool CHEAT_SPRAY = false;
        [SerializeField] private float arc;
        [SerializeField] private float speed = 1f;

        [SerializeField]
        [Range(-1, 1)] private float startMinMax = 0;

        private void OnEnable()
        {

            if(!CHEAT_SPRAY) if (!LevelManager.Instance) return;

            StartCoroutine(SprayRoutine());

            IEnumerator SprayRoutine()
            {
                if (startMinMax == 0) startMinMax = 1;
                var halfArc = arc * 0.5f;
                var startingAngle = transform.localRotation.eulerAngles.z;
                while (true)
                {
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, startingAngle + (startMinMax * halfArc * Mathf.Cos(Time.time * speed))));
                    yield return null;
                }
            }
        }
    }
}

