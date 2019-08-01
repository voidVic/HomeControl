using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AdapterService: MonoBehaviour
{
    public string authTokenURL = "https://raw.githubusercontent.com/voidVic/devicetest/master/token";
    public static string authBearerToken = "";
    //private const string PhilipsSwitchUrl = "http://localhost:4030/connect/account/87EA9876471B4114BD1AB79635EAED60.MUGAZgl6M5c8CLDcQcWiCpip2uc/philips/device/00-17-88-01-02-57-6f-5d-0b_8.LOr73Ps4jgmNPnr88nQAYVx78GE/action/setSwitch";
    private const string PhilipsSwitchUrl = "https://iota-integration.xfinity.com/philips/connect/account/87EA9876471B4114BD1AB79635EAED60.MUGAZgl6M5c8CLDcQcWiCpip2uc/philips/device/00-17-88-01-02-57-6f-5d-0b_8.LOr73Ps4jgmNPnr88nQAYVx78GE/action/setSwitch";
    private const string LifxSwitchUrl = "https://iota-integration.xfinity.com/lifx/connect/account/87EA9876471B4114BD1AB79635EAED60.K_VrKcdvt1RdcHWoSEMUIQFn3Tg/device/d073d52490a2.YTSI2p3e9qn7V4e7HtB14IEMHYk/setSwitch";
    private const string PhilipsColorUrl = "https://iota-integration.xfinity.com/philips/connect/account/87EA9876471B4114BD1AB79635EAED60.MUGAZgl6M5c8CLDcQcWiCpip2uc/philips/device/00-17-88-01-02-57-6f-5d-0b_8.LOr73Ps4jgmNPnr88nQAYVx78GE/action/setColor";
    private const string LifxColorUrl = "https://iota-integration.xfinity.com/lifx/connect/account/87EA9876471B4114BD1AB79635EAED60.K_VrKcdvt1RdcHWoSEMUIQFn3Tg/device/d073d52490a2.YTSI2p3e9qn7V4e7HtB14IEMHYk/setColor";

    private const string green = "#FFFFFF";
    private const string red = "#FFF1E0";
    private const string blue = "#00C4FF";

    public void SetSwitch(string switchValue, string deviceName)
    {
        WWWForm form = new WWWForm();
        form.AddField("value", switchValue);

        string deviceUrl = deviceName == "lifx" ? LifxSwitchUrl : PhilipsSwitchUrl;
        _ = StartCoroutine(postUnityWebRequest(deviceUrl, form));
    }

    public void SetColor(string color, string deviceName)
    {
        WWWForm form = new WWWForm();
        color = color == "Red" ? red : color == "Green" ? green : blue;
        form.AddField("value", color);

        string deviceUrl = deviceName == "lifx" ? LifxColorUrl : PhilipsColorUrl;
        _ = StartCoroutine(postUnityWebRequest(deviceUrl, form));
    }

    public void GetAuthToken()
    {
        if (authBearerToken != "") return;

        _ = StartCoroutine(GetUnityWebRequest_AuthToken());
    }

    IEnumerator postUnityWebRequest(string url, WWWForm form)
    {
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        uwr.SetRequestHeader("Authorization", getAuthBearerToken());
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }


    IEnumerator GetUnityWebRequest_AuthToken()
    {
        UnityWebRequest www = UnityWebRequest.Get(authTokenURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            authBearerToken = "Bearer " + www.downloadHandler.text.ToString();
            authBearerToken = authBearerToken.Replace(System.Environment.NewLine, "");
        }
    }

    private string getAuthBearerToken()
    {
        Debug.Log("auth bearer token:  "  + authBearerToken);
        return authBearerToken;
    }

}
