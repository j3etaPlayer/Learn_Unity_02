using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    static string nextScene;            // ���� ������ �Ѿ� �� Scene �̸��� ���� �����ϴ� ����

    [SerializeField] private Image progressBar;

    private void Start()
    {
        // �ε� process �������� ���� �ش� ���μ����� ����Ǹ� ���� ������ �̵��Ѵ�.

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
        operation.allowSceneActivation = false;                                 // ���� ������ �ڵ����� ���������� �Ѿ ���ΰ�?   true : �ڵ����� �̵�, false : �̵����� ����
                                                                                // �ε��߿� �ּ����� ���ð��� �ο��Ѵ�.
        float timer = 0f;

        while(!operation.isDone)
        {
            yield return null;      // �����Ӹ��� �Ʒ� ������ �ݿ��Ѵ�.

            if (operation.progress < 0.9f)
            {
                progressBar.fillAmount = operation.progress;                    // ������ �̵��ϴ� ��ŭ �ʾ��Ʈ�� �þ��.
            }

            else
            {
                timer += Time.unscaledDeltaTime;                                // Time.Scale�� �����ų �� �ֱ� ������ �������� ���� �� �� ���� �ð����� �ο�
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);           // 90%���� 100%���� ���� ������ �̹��� ������ ������������ ǥ��

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
