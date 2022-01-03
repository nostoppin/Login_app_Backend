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

    [SerializeField] private Text g_alertText;

    [SerializeField] private Button g_loginButton;

   

    public void m_onButtonClick()
    {
        g_alertText.text = "Signing in...";
        g_loginButton.interactable = false;

        StartCoroutine(m_try_logIn());
    }

    private IEnumerator m_try_logIn()
    {
        g_username = g_username_inputField.text;
        g_password = g_password_inputField.text;

        
        if(g_username.Length < 3 || g_username.Length > 20)
        {
            g_alertText.text = g_username + " does not exist" ;
            g_loginButton.interactable = true;
            yield break;
        }
        if(g_password.Length < 3 || g_password.Length > 20)
        {
            g_alertText.text = "Password length too large or too less.";
            g_loginButton.interactable = true;
            yield break;
        }
        
                                                        //access accounts collection(s)
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
            if (request.downloadHandler.text != "wrong password")
            {
                //print(UnityWebRequest.Get(g_username.ToString()));
                g_loginButton.interactable = false;

                //convert JSON to utf8 readable
                AccountLink g_returnedAccount = JsonUtility.FromJson<AccountLink>(request.downloadHandler.text);

                g_alertText.text = $"{ g_returnedAccount._id } Welcome";
            }
            else
            {
                g_alertText.text = "wrong password";
                g_loginButton.interactable = true;
            }
        }
        else
        {
            g_alertText.text = "Error connecting, please try again later.";
        }
        //print(g_username+" "+g_password);

        yield return null;
    }
}
