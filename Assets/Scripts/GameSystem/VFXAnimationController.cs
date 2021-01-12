using System.Collections;
using UnityEngine;

namespace GameSystem
{
    [RequireComponent(typeof(Animation))]
    public class VFXAnimationController : VFXController
    {
        private Animation vfx = null;

        protected override void Awake()
        {
            base.Awake();

            vfx = GetComponent<Animation>();
            waitForSeconds = new WaitForSeconds(vfx.clip.length);
        }

        protected override IEnumerator Play_Routine()
        {
            vfx.Play();

            yield return waitForSeconds;

            gameObject.transform.parent = UtilityPooler.Instance.transform;
            gameObject.SetActive(false);
        }
    }
}
