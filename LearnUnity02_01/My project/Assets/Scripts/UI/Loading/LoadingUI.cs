using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    static string nextScene;            // 다음 씬으로 넘어 갈 Scene 이름의 값을 저장하는 변수

    [SerializeField] private Image progressBar;

    private void Start()
    {
        // 로딩 process ㅈ진행을 통해 해당 프로세스가 종료되면 다음 씬으로 이동한다.

        StartCoroutine(LoadSceneProcess());
    }
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;

        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator LoadSceneProcess()
    {
        yield return new WaitForSeconds(0.3f);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;                                 // 씬이 끝날때 자동으로 다음씬으로 넘어갈 것인가?   true : 자동으로 이동, false : 이동하지 않음
                                                                                // 로딩중에 최소한의 대기시간을 부여한다.
        float timer = 0f;

        while(!operation.isDone)
        {
            yield return null;      // 프레임마다 아래 내용을 반영한다.

            if (operation.progress < 0.9f)
            {
                progressBar.fillAmount = operation.progress;                    // 실제로 이동하는 만큼 필어마운트가 늘어난다.
            }

            else
            {
                timer += Time.unscaledDeltaTime;                                // Time.Scale을 변경시킬 수 있기 때문에 스케일을 변경 할 수 없는 시간값을 부여
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);           // 90%에서 100%까지 가는 사이의 이미지 증가를 선형보간으로 표시

                if (progressBar.fillAmount >= 1f)
                {
                    yield return new WaitForSeconds(1f);
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
