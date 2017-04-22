using System;
using System.Collections.Generic;
using UnityEngine;
using YIMEngine;
using YouMe;

public class IMInternaelManager:
    YIMEngine.LoginListen,
    YIMEngine.MessageListen,
    YIMEngine.ChatRoomListen,
    YIMEngine.DownloadListen,
    YIMEngine.ContactListen,
    YIMEngine.AudioPlayListen,
    YIMEngine.LocationListen {

    private static IMInternaelManager _instance;
    public static IMInternaelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new IMInternaelManager();
            }
            return _instance;
        }
    }

    private IMUser _lastLoginUser;
    public IMUser LastLoginUser{
        get{
            return _lastLoginUser;
        }
    }

    private IMInternaelManager(){
        var youmeObj = new GameObject("__YIMGameObjectV2__");
        GameObject.DontDestroyOnLoad(youmeObj);
        youmeObj.hideFlags = HideFlags.DontSave;
        youmeObj.AddComponent<YIMBehaviour>();

        YIMEngine.IMAPI.Instance().SetLoginListen(this);
        YIMEngine.IMAPI.Instance().SetMessageListen(this);
        YIMEngine.IMAPI.Instance().SetDownloadListen(this);
        YIMEngine.IMAPI.Instance().SetChatRoomListen(this);
        YIMEngine.IMAPI.Instance().SetContactListen(this);
        YIMEngine.IMAPI.Instance().SetAudioPlayListen(this);
        YIMEngine.IMAPI.Instance().SetLocationListen(this);
    }

    private class YIMBehaviour:MonoBehaviour{

        void OnApplicationQuit()
        {
            #if UNITY_EDITOR
            IMAPI.Instance().Logout();
            #else
            IMAPI.Instance().UnInit();
            #endif
        }

        void OnApplicationPause(bool isPause)
        {
            if (isPause)
            {
                IMAPI.Instance().OnPause();
            }
            else
            {
                IMAPI.Instance().OnResume();
            }
        }

    }
    
    
    #region YouMeLoginListen implementation

    public void OnLogin(YIMEngine.ErrorCode errorcode, string iYouMeID)
    {
        if(errorcode == YIMEngine.ErrorCode.Success){
            _lastLoginUser = new IMUser(iYouMeID);
        }
        if( IMClient.Instance.ConnectListener!=null ){
            IMConnectEvent e = new IMConnectEvent(Conv.ErrorCodeConvert(errorcode),errorcode ==0 ? 
            ConnectEventType.CONNECTED:ConnectEventType.CONNECT_FAIL,iYouMeID);
            IMClient.Instance.ConnectListener(e);
        }
    }

    public void OnLogout()
    {
       if( IMClient.Instance.ConnectListener!=null ){
            IMConnectEvent e = new IMConnectEvent(YouMe.ErrorCode.SUCCESS,ConnectEventType.DISCONNECTED,"");
            IMClient.Instance.ConnectListener(e);
        }
    }

    #endregion


    #region YouMeIMMessageListen implementation

    public void OnSendMessageStatus(ulong iRequestID, YIMEngine.ErrorCode errorcode)
    {
        
    }

    public void OnRecvMessage(MessageInfoBase message)
    {
       
    }

    public void OnRecvNewMessage(YIMEngine.ChatType chatType,string targetID){
        
    }

    public void OnSendAudioMessageStatus(ulong iRequestID, YIMEngine.ErrorCode errorcode, string strText, string strAudioPath, int iDuration)
    {
        
    }

    //获取消息历史纪录回调
    public void OnQueryHistoryMessage(YIMEngine.ErrorCode errorcode, string targetID, int remain, List<YIMEngine.HistoryMsg> messageList)
    {
        
    }
        

    //语音上传后回调
    public void OnStopAudioSpeechStatus(YIMEngine.ErrorCode errorcode, ulong iRequestID, string strDownloadURL, int iDuraton, int iFileSize, string strLocalPath, string strText)
    {
       
    }
    public void OnStartSendAudioMessage(ulong iRequestID,  YIMEngine.ErrorCode errorcode,string strText,string strAudioPath,int iDuration){
        
    }

   

    #endregion

    #region OnJoinGroupRequest implementation

    public void OnJoinRoom(YIMEngine.ErrorCode errorcode, string iChatRoomID)
    {
        if( IMClient.Instance.ChannelEventListener!=null ){
            ChannelEventType et = errorcode == YIMEngine.ErrorCode.Success ? ChannelEventType.JOIN_SUCCESS : ChannelEventType.JOIN_FAIL;
            IMClient.Instance.ChannelEventListener(new ChannelEvent( Conv.ErrorCodeConvert(errorcode),et,iChatRoomID ));
        }
    }
    public void OnLeaveRoom(YIMEngine.ErrorCode errorcode, string iChatRoomID)
    {
        if( IMClient.Instance.ChannelEventListener!=null ){
            ChannelEventType et = errorcode == YIMEngine.ErrorCode.Success ? ChannelEventType.LEAVE_SUCCESS : ChannelEventType.LEAVE_FAIL;
            IMClient.Instance.ChannelEventListener(new ChannelEvent( Conv.ErrorCodeConvert(errorcode),et,iChatRoomID ));
        }
    }
    #endregion

    #region DownloadListen implementation
    public void OnDownload(ulong iRequestID, YIMEngine.ErrorCode errorcode, string strSavePath)
    {
        
    }
    #endregion

    #region ContactListen implementation
    public void OnGetContact(List<string> contactLists){
       
    }
    public void OnGetUserInfo(YIMEngine.ErrorCode code, IMUserInfo userInfo){
        
    }
    public void OnQueryUserStatus(YIMEngine.ErrorCode code, string userID, UserStatus status){
        
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

    #region YIMEngine.AudioPlayListen implementation
    public void OnPlayCompletion(YIMEngine.ErrorCode errorcode,string path){
        
    }
    #endregion
}