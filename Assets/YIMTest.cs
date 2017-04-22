using UnityEngine;
using System.Collections.Generic;


//#if UNITY_IOS || UNITY_ANDROID
namespace U3dTest
{
    public class YIMTest : MonoBehaviour,
        YIMEngine.LoginListen,
        YIMEngine.MessageListen,
        YIMEngine.ChatRoomListen,
        YIMEngine.AudioPlayListen,
        YIMEngine.ContactListen,
        YIMEngine.LocationListen
    {

        private Vector2 m_Position = Vector2.zero;
        private string m_InGameLog = "";
        // Use this for initialization
        void Start()
        {
            YIMEngine.IMAPI.Instance().SetLoginListen(this);
            YIMEngine.IMAPI.Instance().SetMessageListen(this);
            YIMEngine.IMAPI.Instance().SetChatRoomListen(this);
            YIMEngine.IMAPI.Instance().SetAudioPlayListen(this);
            YIMEngine.IMAPI.Instance().SetContactListen(this);
            YIMEngine.IMAPI.Instance().SetLocationListen(this);
            YIMEngine.IMAPI.Instance().Init("YOUMEBC2B3171A7A165DC10918A7B50A4B939F2A187D0", "r1+ih9rvMEDD3jUoU+nj8C7VljQr7Tuk4TtcByIdyAqjdl5lhlESU0D+SoRZ30sopoaOBg9EsiIMdc8R16WpJPNwLYx2WDT5hI/HsLl1NJjQfa9ZPuz7c/xVb8GHJlMf/wtmuog3bHCpuninqsm3DRWiZZugBTEj2ryrhK7oZncBAAE=");
        }

        // Update is called once per frame
        void Update()
        {

        }
        void showStatus(string msg)
        {
            m_InGameLog += msg;
            m_InGameLog += "\r\n";
        }
        void OnGUI()
        {

            int inset = Screen.width / 20;
            int space = Screen.width / 30;
            int btnsOneRow = 3;
            int btnWidth = (Screen.width - inset * 2 - space * (btnsOneRow - 1)) / btnsOneRow;
            int btnHeight = btnWidth / 3;

            int labelX = inset;
            int labelY = inset + (btnHeight + space) * 8;
            int labelWidth = Screen.width - labelX * 2;
            int labelHeight = Screen.height - labelY;

            GUI.BeginGroup(new Rect(labelX, labelY, labelWidth, labelHeight));

            m_Position = GUILayout.BeginScrollView(m_Position, GUILayout.Width(labelWidth), GUILayout.Height(labelHeight));
            GUILayout.Label(m_InGameLog);
            GUILayout.EndScrollView();

            GUI.EndGroup();

            if (GUI.Button(new Rect(inset, inset, btnWidth, btnHeight), "login"))
            {
                YIMEngine.ErrorCode errorcode = YIMEngine.IMAPI.Instance().Login("1001", "123456","");
                Debug.Log("login errorcode: " + errorcode);
            }

            if (GUI.Button(new Rect(inset + btnWidth + space, inset, btnWidth, btnHeight), "logout"))
            {
                Debug.Log("logout");
                showStatus("logout");
                YIMEngine.IMAPI.Instance().Logout();
                //VoiceChannelPlugin.ExitChannel();
            }





            if (GUI.Button(new Rect(inset, inset + btnHeight + space, btnWidth, btnHeight), "init"))
            {
                Debug.Log("init");
                showStatus("init");
                YIMEngine.IMAPI.Instance().Init("YOUMEBC2B3171A7A165DC10918A7B50A4B939F2A187D0", "r1+ih9rvMEDD3jUoU+nj8C7VljQr7Tuk4TtcByIdyAqjdl5lhlESU0D+SoRZ30sopoaOBg9EsiIMdc8R16WpJPNwLYx2WDT5hI/HsLl1NJjQfa9ZPuz7c/xVb8GHJlMf/wtmuog3bHCpuninqsm3DRWiZZugBTEj2ryrhK7oZncBAAE=");

                //VoiceChannelPlugin.StartTalking();
            }

            if (GUI.Button(new Rect(inset + btnWidth + space, inset + btnHeight + space, btnWidth, btnHeight), "uninit"))
            {
                Debug.Log("uninit");
                showStatus("uninit");
                YIMEngine.IMAPI.Instance().UnInit();
                //VoiceChannelPlugin.StartTalking();
            }





            if (GUI.Button(new Rect(inset + (btnWidth + space) * 2, inset + (btnHeight + space) * 2, btnWidth, btnHeight), "sendmessage"))
            {
                ulong iRequestID = 0;
                YIMEngine.ErrorCode errorcode = YIMEngine.IMAPI.Instance().SendAudioMessage("1001", YIMEngine.ChatType.PrivateChat, ref iRequestID);
                Debug.Log("sendmessage: RequestID:" + iRequestID + "errorcode: " + errorcode);
                //VoiceChannelPlugin.StartTalking();

            }

            if (GUI.Button(new Rect(inset, inset + (btnHeight + space) * 3, btnWidth, btnHeight), "joinchatroom"))
            {
                Debug.Log("joinchatroom");
                YIMEngine.IMAPI.Instance().JoinChatRoom("1001");

                //VoiceChannelPlugin.StartTalking();
            }

            if (GUI.Button(new Rect(inset + btnWidth + space, inset + (btnHeight + space) * 3, btnWidth, btnHeight), "leavechatroom"))
            {
                Debug.Log("leavechatroom");
                showStatus("leavechatroom");
                YIMEngine.IMAPI.Instance().LeaveChatRoom("1001");
                //VoiceChannelPlugin.StartTalking();
            }

            if (GUI.Button(new Rect(inset + (btnWidth + space) * 2, inset + (btnHeight + space) * 3, btnWidth, btnHeight), "sendcustommessage"))
            {
                Debug.Log("sendcustommessage");
                showStatus("sendcustommessage");
				ulong iRequestID = 0;
                string strText = "112345";

                YIMEngine.ErrorCode errorcode = YIMEngine.IMAPI.Instance().SendCustomMessage("1001", YIMEngine.ChatType.PrivateChat, System.Text.Encoding.UTF8.GetBytes(strText), ref iRequestID);
                //VoiceChannelPlugin.StartTalking();
                Debug.Log("sendmessage: RequestID:" + iRequestID + "errorcode: " + errorcode);
            }

            if (GUI.Button(new Rect(inset, inset + (btnHeight + space) * 4, btnWidth, btnHeight), "Clear"))
            {
                m_InGameLog = "";
            }

            if (GUI.Button(new Rect(inset + btnWidth, inset + (btnHeight + space) * 4, btnWidth, btnHeight), "StopAudio"))
            {
                YIMEngine.IMAPI.Instance().StopAudioMessage("");
            }

            if (GUI.Button(new Rect(inset + (btnWidth + space) * 2, inset + (btnHeight + space) * 4, btnWidth, btnHeight), "filter"))
            {
                int level = 0;
                string strResult = YIMEngine.IMAPI.GetFilterText("这是江泽明de胡锦涛哦的法轮功的水电费水电费", ref level);
                showStatus("result:" + strResult + " level:" + level);
            }


            if (GUI.Button(new Rect(inset, inset + (btnHeight + space) * 5, btnWidth, btnHeight), "QueryUserStatus"))
            {
                YIMEngine.IMAPI.Instance().QueryUserStatus("1001");
                Debug.Log("QueryUserStatus 1001");
            }

            if (GUI.Button(new Rect(inset + btnWidth + space, inset + (btnHeight + space) * 5, btnWidth, btnHeight), "GetAudioCache"))
            {
                string strPath = YIMEngine.IMAPI.Instance().GetAudioCachePath();
                Debug.Log("audio cache path:" + strPath);
            }

            if (GUI.Button(new Rect(inset + (btnWidth + space) * 2, inset + (btnHeight + space) * 5, btnWidth, btnHeight), "PlayAudio"))
            {
                string path = "E:\\test\\bd_1.wav";
                YIMEngine.ErrorCode errorcode = YIMEngine.IMAPI.Instance().StartPlayAudio(path);

                //Debug.Log("errorcode:" + errorcode + " path:" + path);
            }
        }

        #region YIMEngine.LoginListen implementation

        public void OnLogin(YIMEngine.ErrorCode errorcode, string strYouMeID)
        {
            showStatus("OnLogin: errorcode" + errorcode + " contact:" + strYouMeID);
        }

        public void OnLogout()
        {
            showStatus("OnLogout");
        }

        #endregion

        #region YIMEngine.MessageListen implementation
		//获取消息历史纪录回调
		public void OnQueryHistoryMessage(YIMEngine.ErrorCode errorcode, string targetID, int remain, List <YIMEngine.HistoryMsg> messageList)
		{
		}
            
        public void OnSendMessageStatus(ulong iRequestID, YIMEngine.ErrorCode errorcode)
        {
            Debug.Log("OnSendMessageStatus request:" + iRequestID + "errorcode:" + errorcode);
        }
        public void OnStartSendAudioMessage(ulong iRequestID, YIMEngine.ErrorCode errorcode, string strText, string strAudioPath, int iDuration)
        {
            Debug.Log("OnStopSendAudioMessage request:" + iRequestID + "errorcode:" + errorcode);
        }
        public void OnSendAudioMessageStatus(ulong iRequestID, YIMEngine.ErrorCode errorcode, string strText, string strAudioPath, int iDuration)
        {
            Debug.Log("OnSendAudioMessageStatus request:" + iRequestID + "errorcode:" + errorcode + " text:" + strText + " path:" + strAudioPath);
        }
        public void OnStopAudioSpeechStatus(YIMEngine.ErrorCode errorcode, ulong iRequestID,string strDownloadURL,int iDuration,int iFileSize,string strLocalPath,string strText)
        {

        }
        public void OnRecvMessage(YIMEngine.MessageInfoBase message)
        {
            if (message.MessageType == YIMEngine.MessageBodyType.TXT)
            {
                YIMEngine.TextMessage textMsg = (YIMEngine.TextMessage)message;
                Debug.Log("OnRecvMessage text:" + textMsg.Content + " send:" + textMsg.SenderID + "recv:" + textMsg.RecvID);
            }
            else if (message.MessageType == YIMEngine.MessageBodyType.CustomMesssage)
            {
                YIMEngine.CustomMessage customMsg = (YIMEngine.CustomMessage)message;
                Debug.Log("OnRecvMessage custom:" + System.Convert.ToBase64String(customMsg.Content) + " send:" + customMsg.SenderID + "recv:" + customMsg.RecvID);
            }
            else if (message.MessageType == YIMEngine.MessageBodyType.Voice)
            {
                YIMEngine.VoiceMessage voiceMsg = (YIMEngine.VoiceMessage)message;
                Debug.Log("OnRecvMessage voice:" + voiceMsg.Text + " send:" + voiceMsg.SenderID + "recv:" + voiceMsg.RecvID);
                YIMEngine.IMAPI.Instance().DownloadAudioFile(voiceMsg.RequestID, "/sdcard/abc.wav");
            }
        }
        public void OnRecvNewMessage(YIMEngine.ChatType chatType,string targetID)
		{

		}
        public void OnTranslateTextComplete(YIMEngine.ErrorCode errorcode, uint requestID, string text, YIMEngine.LanguageCode destLangCode)
        {

        }
        #endregion

        #region YIMEngine.ChatRoomListen implementation

        public void OnJoinRoom(YIMEngine.ErrorCode errorcode, string strChatRoomID)
        {

        }
        public void OnLeaveRoom(YIMEngine.ErrorCode errorcode, string strChatRoomID)
        {
        }
        #endregion

        #region YIMEngine.AudioPlayListen implementation

        public void OnPlayCompletion(YIMEngine.ErrorCode errorcode, string path)
        {
            Debug.Log("play audio done errorcode:" + errorcode);
        }

        #endregion

        #region YIMEngine.ContactListen implementation

        public void OnGetContact(List<string> contactLists)
        {

        }

        public void OnGetUserInfo(YIMEngine.ErrorCode code, YIMEngine.IMUserInfo userInfo)
        {
            Debug.Log("OnGetUserInfo code:" + code + " userInfo: " + userInfo.ToJsonString());
        }

        public void OnQueryUserStatus(YIMEngine.ErrorCode code, string userID, YIMEngine.UserStatus status)
        {
            Debug.Log("OnQueryUserStatus code:" + code + " userID: " + userID + " status:" + status);
        }

        #endregion


        #region YIMEngine.LocationListen implementation

        public void OnUpdateLocation(YIMEngine.ErrorCode errorcode, YIMEngine.GeographyLocation location)
        {

        }
	    
        public void OnGetNearbyObjects(YIMEngine.ErrorCode errorcode, List<YIMEngine.RelativeLocation> neighbourList)
        {

        }

        #endregion
    }
}
//#endif
