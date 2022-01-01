using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class LoginLink : MonoBehaviour
{
    string g_username;
    string g_password;

    [SerializeField] private string g_auth_endPoint = "http://localhost:12233/account";
    [SerializeField] private InputField g_username_inputField;
    [SerializeField] private InputField g_password_inputField;

    public void m_onButtonClick()
    {
        StartCoroutine(m_try_logIn());
    }

    private IEnumerator m_try_logIn()
    {
        g_username = g_username_inputField.text;
        g_password = g_password_inputField.text;

        UnityWebRequest request = UnityWebRequest.Get($"{g_auth_endPoint}?r_username={g_username}&r_password={g_password}");
        var handler = request.SendWebRequest();

        float start_time = 0f;

        while(!handler.isDone)
        {
            start_time += Time.deltaTime;

            if(start_time > 5f)
            {
                break;
            }
            yield return null;
        }

        if(request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Unable to connect, try again later");
        }
        print(g_username+" "+g_password);

        yield return null;
    }
}
