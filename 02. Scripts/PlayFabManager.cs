// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using PlayFab;
// using PlayFab.ClientModels;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;


// public class PlayFabManager : MonoBehaviour
// {
//     public InputField EmailInput, PasswordInput, UsernameInput;
//     public string myID;
//     public string nickName;
//     public int tier,settingimg,settingbackground;
//     public int[] book=new int[96];
//     public int[] background = new int[11];
//     public int[] settingstuff = new int[20];
//     public float[] settingstuffx = new float[20];
//     public float[] settingstuffy = new float[20];
//     public string tmp;
//     bool[] check = new bool[8];
//     public void Login()
//     {
//         for (int i = 0; i < 8; i++)
//             check[i] = false;
//         var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
//         PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        
//     }

//     public void Register()
//     {
//         var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text };
//         PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
//     }
//     public void SetData()
//     {
//         string newbook = "0";
//         for (int i = 0; i < 95; i++)
//         {
//             newbook += ",0";
//         }
//         string setting_stuff = "-1";
//         for (int i = 0; i < 19; i++)
//         {
//             setting_stuff += ",-1";
//         }
//         string setting_stuffxy = "0";
//         for (int i = 0; i < 19; i++)
//         {
//             setting_stuffxy += ",0";
//         }
//         var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() {
//             { "Tier","0"},
//             { "SettingImg","0" },
//             { "Book", newbook},
//             { "Background","1,1,1,1,1,1,1,1,1,1,1"},
//             { "SettingBackground","0" },
//             { "SettingStuff", setting_stuff},
//             { "SettingStuffx", setting_stuffxy},
//             { "SettingStuffy", setting_stuffxy},
//         } };
//         PlayFabClientAPI.UpdateUserData(request, (result) => print("data save success"), (error) => print("data save fail"));
//     }

//     public void GetDataTier()
//     {
//         var request = new GetUserDataRequest() { PlayFabId = myID };
//         PlayFabClientAPI.GetUserData(request, (result)=> {
//             tmp = result.Data["Tier"].Value;
//             tier = int.Parse(tmp);
//             check[0] = true;
//         }, (error) => print("data load fail"));

//     }

//     public bool GetDataSettingImg()
//     {
//         bool success = false;
//         var request = new GetUserDataRequest() { PlayFabId = myID };
//         PlayFabClientAPI.GetUserData(request, (result) => {
//             tmp = result.Data["SettingImg"].Value;
//             settingimg = int.Parse(tmp);
//             check[1] = true;
//         }, (error) => print("data load fail"));

//         return success;

//     }
//     public bool GetDataBook()
//     {
//         bool success = false;
//         var request = new GetUserDataRequest() { PlayFabId = myID };
//         PlayFabClientAPI.GetUserData(request, (result) => {
//             string[] split_text;
//             tmp = result.Data["Book"].Value;
//             split_text = tmp.Split(',');
//             for(int i=0;i<96;i++)
//             {
//                 book[i]=int.Parse(split_text[i]);
//             }
//             check[2] = true;
//         }, (error) => print("data load fail"));

//         return success;

//     }

//     public bool GetDataBackground()
//     {
//         bool success = false;
//         var request = new GetUserDataRequest() { PlayFabId = myID };
//         PlayFabClientAPI.GetUserData(request, (result) => {
//             string[] split_text;
//             tmp = result.Data["Background"].Value;
//             split_text = tmp.Split(',');
//             for (int i = 0; i < 11; i++)
//             {
//                 background[i] = int.Parse(split_text[i]);
//             }
//             check[3] = true;
//         }, (error) => print("data load fail"));

//         return success;

//     }

//     public bool GetDataSettingBackground()
//     {
//         bool success = false;
//         var request = new GetUserDataRequest() { PlayFabId = myID };
//         PlayFabClientAPI.GetUserData(request, (result) => {
//             tmp = result.Data["SettingBackground"].Value;
//             settingbackground = int.Parse(tmp);
//             check[4] = true;
//         }, (error) => print("data load fail"));

//         return success;

//     }

//     public bool GetDataSettingStuff()
//     {
//         bool success = false;
//         var request = new GetUserDataRequest() { PlayFabId = myID };
//         PlayFabClientAPI.GetUserData(request, (result) => {
//             string[] split_text;
//             tmp = result.Data["SettingStuff"].Value;
//             split_text = tmp.Split(',');
//             for (int i = 0; i < 20; i++)
//             {
//                 settingstuff[i] = int.Parse(split_text[i]);
//             }
//             check[5] = true;
//         }, (error) => print("data load fail"));

//         return success;

//     }

//     public bool GetDataSettingStuffx()
//     {
//         bool success = false;
//         var request = new GetUserDataRequest() { PlayFabId = myID };
//         PlayFabClientAPI.GetUserData(request, (result) => {
//             string[] split_text;
//             tmp = result.Data["SettingStuffx"].Value;
//             split_text = tmp.Split(',');
//             for (int i = 0; i < 20; i++)
//             {
//                 settingstuffx[i] = float.Parse(split_text[i]);
//             }
//             check[6] = true;
//         }, (error) => print("data load fail"));

//         return success;

//     }
//     public void GetDataSettingStuffy()
//     {
//         var request = new GetUserDataRequest() { PlayFabId = myID };
//         PlayFabClientAPI.GetUserData(request, (result) => {
//             string[] split_text;
//             tmp = result.Data["SettingStuffy"].Value;
//             split_text = tmp.Split(',');
//             for (int i = 0; i < 20; i++)
//             {
//                 settingstuffy[i] = float.Parse(split_text[i]);
//             }
//             check[7] = true;
//         }, (error) => print("data load fail"));
//     }

//     public void GetAccountinfo()
//     {
//         var request = new GetAccountInfoRequest() { PlayFabId = myID };

//     }

//     void OnLoginSuccess(LoginResult result)
//     {
//         print("로그인 성공");
//         myID = result.PlayFabId;

        

//         GetAccountinfo();
//         GetDataTier();
//         GetDataBackground();
//         GetDataBook();
//         GetDataSettingBackground();
//         GetDataSettingImg();
//         GetDataSettingStuff();
//         GetDataSettingStuffx();
//         GetDataSettingStuffy();
              
//     }
//     void OnLoginFailure(PlayFabError error) => print("로그인 실패");
//     void OnRegisterSuccess(RegisterPlayFabUserResult result)
//     {
//         print("Register success");
//         var request = new UpdateUserTitleDisplayNameRequest() { DisplayName = UsernameInput.text };
//         PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdateSuccess, OnDisplayNameUpdateFailure);
//     }
//     void OnRegisterFailure(PlayFabError error) => print("Register fail");

//     void OnDisplayNameUpdateSuccess(UpdateUserTitleDisplayNameResult result) => print("DisplayNameUpdate success");
//     void OnDisplayNameUpdateFailure(PlayFabError error) => print("DisplayNameUpdate fail");


//     private void Awake()
//     {
//         DontDestroyOnLoad(gameObject);
//     }

//     private void Update()
//     {
//         if (check[0] && check[1] && check[2] && check[3] && check[4] && check[5]&& check[6] && check[7])
//         {
//             SceneManager.LoadScene("main scene");
//         }
//     }
// }
