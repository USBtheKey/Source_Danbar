using GameSystem.GameSceneManagement;
using System.Collections;
using UnityEngine;

namespace GameSystem
{
    public class LockOn_VFX : MonoBehaviour, ISceneObject 
    {
        private const float endRotationZ = -180f;
        private SpriteRenderer[] sRends;
        private const float timePeriod = 0.5f;
        private const float startScale = 20f;
        private AudioSource audio;

        private void Awake()
        {
            audio = GetComponent<AudioSource>();
            sRends = GetComponentsInChildren<SpriteRenderer>();
        }

        private void OnEnable()
        {
            if (!LevelManager.Instance) return;


            transform.up = Vector3.down;

            ResetObj();

            StopAllCoroutines();
            StartCoroutine(PlayAnimation());

            IEnumerator PlayAnimation()
            {
                float currentAlpha = 0;
                float currentScale = startScale;


                while (true)
                {
                    if (this.transform.eulerAngles.z < Mathf.Abs(endRotationZ))
                    {
                        this.transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.eulerAngles), Quaternion.Euler(new Vector3(0, 0, endRotationZ)), (Mathf.Abs(endRotationZ) / timePeriod) * Time.deltaTime);
                    }

                    if (currentAlpha < 1)
                    {
                        foreach (SpriteRenderer sprite in sRends)
                        {
                            currentAlpha = Mathf.MoveTowards(currentAlpha, 1, (1 / timePeriod) * Time.deltaTime);
                            sprite.color = new Color(0, 1, 0, currentAlpha);
                        }
                    }

                    if (currentScale > 1)
                    {
                        currentScale = Mathf.MoveTowards(currentScale, 1, (startScale / timePeriod) * Time.deltaTime);
                        transform.localScale = Vector3.one * currentScale;
                    }

                    if (currentScale >= 1 && transform.eulerAngles.z >= Mathf.Abs(endRotationZ) && currentAlpha >= 1)
                    {
                        audio.Play();
                        OnTargetLocked?.Invoke();
                        yield break;
                    }
                    else
                    {
                        yield return null;
                    }
                }
            }
        }
        private void Start()
        {
            GameSceneLoader.OnStartLoadingNextScene += DisableObj;
        }

        private void OnDisable()
        {
            if (!LevelManager.Instance) return;
            transform.parent = UtilityPooler.Instance.transform;
        }

        private void ResetObj()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            gameObject.transform.localScale = Vector3.one * startScale;
        }

        public void DisableObj()
        {
            gameObject.SetActive(false);
        }

        public event System.Action OnTargetLocked;
    }
}

