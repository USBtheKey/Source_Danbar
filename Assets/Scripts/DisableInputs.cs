
using UnityEngine;

namespace GameSystem
{
    public class DisableInputs : MonoBehaviour
    {
        public void Disable()
        {
            InputSystem.InputsHandler.DisableInputs();
        }
    }
}
