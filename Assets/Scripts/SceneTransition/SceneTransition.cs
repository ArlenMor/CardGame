using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace SceneTransition
{
    public class SceneTransition : MonoBehaviour
    {
        public TextMeshProUGUI loadingProgress;
        public Image loadingProgressBar;

        private Animator anim;
        private static bool shouldPlayAnimation = false;
        private static SceneTransition instance;
        private AsyncOperation loadingSceneOperation;

        public static void SwitchToScene(string sceneName)
        {
            instance.anim.SetTrigger("sceneClosing");

            instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
            instance.loadingSceneOperation.allowSceneActivation = false;
        }
        void Start()
        {
            instance = this;

            anim = GetComponent<Animator>();

            if (shouldPlayAnimation)
                anim.SetTrigger("sceneOpening");
        }

        private void Update()
        {
            if (loadingSceneOperation != null)
            {
                //loadingProgress.text = Mathf.RoundToInt(loadingSceneOperation.progress/0.9f * 100) + "%";
                loadingProgressBar.fillAmount = loadingSceneOperation.progress / 0.9f;
            }
        }

        public void InAnimationOver()
        {
            shouldPlayAnimation = true;
            loadingSceneOperation.allowSceneActivation = true;
        }
    }
}

