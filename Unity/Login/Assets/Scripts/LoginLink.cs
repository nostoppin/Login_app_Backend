using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class LoginLink : MonoBehaviour
{
    string g_username;
    string g_password;

    [SerializeField] private string g_auth_endPoint = "http://localhost:12233/account/login";
    [SerializeField] private string g_creation_endPoint = "http://localhost:12233/account/create";

    [SerializeField] private InputField g_username_inputField;
    [SerializeField] private InputField g_password_inputField;

    [SerializeField] private Text g_alertText;

    [SerializeField] private Button g_loginButton;
    [SerializeField] private Button g_createButton;



    public void m_onButtonClick()
    {
        g_alertText.text = "Logging in...";
        m_buttonsAreActive_switch(false);

        StartCoroutine(m_logIn());
    }

    public void m_onCreate()
    {
        g_alertText.text = "Signing up...";
        m_buttonsAreActive_switch(false);

        StartCoroutine(m_create());
    }
                                            //---LOG IN---//
    private IEnumerator m_logIn()
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
        WWWForm data_form = new WWWForm();
        data_form.AddField("r_username", g_username);
        data_form.AddField("r_password", g_password);

        //access accounts collection(s)
        UnityWebRequest request = UnityWebRequest.Post(g_auth_endPoint, data_form);

        //access accounts collection(s)

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
            if (request.downloadHandler.text != "invalid credentials")
            {
                //print(UnityWebRequest.Get(g_username.ToString()));
                m_buttonsAreActive_switch(false);

                //convert JSON to utf8 readable
                //AccountLink g_returnedAccount = JsonUtility.FromJson<AccountLink>(request.downloadHandler.text);
                //g_alertText.text = $"{ g_returnedAccount._id } Welcome";

                g_alertText.text = "Welcome " + g_username;
            }
            else
            {
                g_alertText.text = "invalid credentials";
                m_buttonsAreActive_switch(true);
            }
        }
        else
        {
            g_alertText.text = "Error connecting, please try again later.";

            m_buttonsAreActive_switch(true);
        }
        //print(g_username+" "+g_password);

        yield return null;
    }
                                    //---CREATE---//
    private IEnumerator m_create()
    {
        g_username = g_username_inputField.text;
        g_password = g_password_inputField.text;


        if (g_username.Length < 3 || g_username.Length > 20)
        {
            g_alertText.text = g_username + " does not exist";
            g_loginButton.interactable = true;
            yield break;
        }
        if (g_password.Length < 3 || g_password.Length > 20)
        {
            g_alertText.text = "Password length too large or too less.";
            g_loginButton.interactable = true;
            yield break;
        }

        WWWForm data_form = new WWWForm();
        data_form.AddField("r_username", g_username);
        data_form.AddField("r_password", g_password);

        //access accounts collection(s)
        UnityWebRequest request = UnityWebRequest.Post(g_creation_endPoint, data_form);
        var handler = request.SendWebRequest();

        float start_time = 0f;

        while (!handler.isDone)
        {
            start_time += Time.deltaTime;

            if (start_time > 5f)
            {
                break;
            }
            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.downloadHandler.text != "invalid credentials" && request.downloadHandler.text != "Username already exists")
            {
                //print(UnityWebRequest.Get(g_username.ToString()));
                m_buttonsAreActive_switch(false);
                //convert JSON to utf8 readable
                //AccountLink g_returnedAccount = JsonUtility.FromJson<AccountLink>(request.downloadHandler.text);

                //g_alertText.text = $"{ g_returnedAccount._id } Welcome";

                g_alertText.text = "Welcome " + g_username + ", account created successfully";
            }
            else
            {
                g_alertText.text = "invalid credentials";
            }
        }
        else
        {
            g_alertText.text = "Error connecting, please try again later.";
        }
        //print(g_username+" "+g_password);
        m_buttonsAreActive_switch(true);

        yield return null;
    }

    private void m_buttonsAreActive_switch(bool l_value)
    {
        g_loginButton.interactable = l_value;
        g_createButton.interactable = l_value;
    }
}
