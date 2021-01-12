
using UnityEngine;


namespace GameSystem.Weapons
{
    
    public class SimpleGunRotationTester : MonoBehaviour
    {
        public float speed;
        private void OnEnable()
        {
            GetComponent<Rotator>().Rotate(speed, Rotation.ClockWise);
        }
    }

}
