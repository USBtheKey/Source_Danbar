using System;
using System.Collections;
using UnityEngine;

public class WeaponScan : MonoBehaviour
{
    [SerializeField] private float targetAngle;
    [SerializeField] private float speed = 1;
    [SerializeField] private bool useNegativeDegrees = false;

    private void OnEnable()
    {
        var startAngle = transform.localEulerAngles.z;
        var deltaAngle = targetAngle - startAngle;
        
        StartCoroutine(ScanRoutine());

        IEnumerator ScanRoutine()
        {
            while (true)
            {
                var newAngle = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, targetAngle, speed);

                if(useNegativeDegrees) transform.localRotation = Quaternion.Euler(new Vector3(0, 180, newAngle));
                else transform.localRotation = Quaternion.Euler(new Vector3(0, 0, newAngle));

                if (Math.Abs(startAngle - newAngle) >= deltaAngle) transform.localEulerAngles = transform.forward * startAngle;

                yield return null;
            }
        }
    }
}
