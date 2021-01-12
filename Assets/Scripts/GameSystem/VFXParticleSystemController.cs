
using System.Collections;
using UnityEngine;

namespace GameSystem {

    [RequireComponent(typeof(ParticleSystem))]
    public class VFXParticleSystemController : VFXController, ISceneObject
    {
        private ParticleSystem vfx = null;

        protected override void Awake()
        {
            base.Awake();

            vfx = GetComponent<ParticleSystem>();
            waitForSeconds = new WaitForSeconds(vfx.main.duration);
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