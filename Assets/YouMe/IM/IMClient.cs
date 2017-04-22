using System;
using YIMEngine;

namespace YouMe
{

    public class IMClient : IClient
    {

        static IMClient _ins;
        public static IMClient Instance{
            get{
                if(_ins==null){
                    _ins = new IMClient();
                }
                return _ins;
            }
        }

        private IMClient(){
            IMManager = IMInternaelManager.Instance;
        }

        public static string FAKE_PAPSSWORD = "123456";

        public IMInternaelManager IMManager;

        // event 
        public Action<IConnectEvent> ConnectListener{set;get;}
        public Action<ChannelEvent> ChannelEventListener{set;get;}
        public Action<IMMessage> ReceiveMessageListener{get;set; }
        // public Action<ErrorCode,IMMessage> SendMessageListener{get;set; }
        public Action<ErrorCode,AudioMessage> StartRecordAudioListener{get;set; }
        public Action<ErrorCode,AudioMessage> StartSendAudioListener{get;set; }
        public Action<ErrorCode,AudioMessage> FinishSendAudioListener{get;set; }

        public IClient Initialize(string appKey, string secretKey, ServerZone zone = ServerZone.China)
        {
            IMAPI.Instance().SetServerZone((YIMEngine.ServerZone)zone);
            IMAPI.Instance().Init(appKey, secretKey);
            return this;
        }

        public void Connect(string userID,string token="")
        {
            // login 
            YIMEngine.ErrorCode code = IMAPI.Instance().Login(userID, FAKE_PAPSSWORD, token);
            if( code!=YIMEngine.ErrorCode.Success && ConnectListener != null ){
                IMConnectEvent e = new IMConnectEvent(Conv.ErrorCodeConvert(code),ConnectEventType.CONNECT_FAIL,userID);
                ConnectListener( e );
            }
        }

        public void Disconnect()
        {
            // logout
            YIMEngine.ErrorCode code = IMAPI.Instance().Logout();
            if( code!=YIMEngine.ErrorCode.Success && ConnectListener != null ){
                IMConnectEvent e = new IMConnectEvent(Conv.ErrorCodeConvert(code),ConnectEventType.DISCONNECTED,"");
                ConnectListener( e );
            }
        }

        public IMChannel[] GetCurrentChannels()
        {
            return new IMChannel[]{};
        }

        public ConnectStatus GetCurrentStatus()
        {
            throw new NotImplementedException();
        }

        public IUser GetCurrentUserID()
        {
            return IMManager.LastLoginUser;
        }

        public void JoinChannel(IMChannel channel)
        {
            var code = IMAPI.Instance().JoinChatRoom( channel.ChannelID );
            if( code!=YIMEngine.ErrorCode.Success && ChannelEventListener!=null ){
                ChannelEventListener(new ChannelEvent( Conv.ErrorCodeConvert(code),ChannelEventType.JOIN_FAIL,channel.ChannelID ));
            }
        }

        public void JoinMultiChannel(IMChannel[] channels)
        {
            for (int i = 0; i < channels.Length;i++){
                var code = IMAPI.Instance().JoinChatRoom(channels[i].ChannelID);
                if( code!=YIMEngine.ErrorCode.Success && ChannelEventListener!=null ){
                    ChannelEventListener(new ChannelEvent( Conv.ErrorCodeConvert(code),ChannelEventType.LEAVE_FAIL,channels[i].ChannelID ));
                }
            }
        }

        public void LeaveAllChannel()
        {
            throw new NotImplementedException();
        }

        public void LeaveChannel(IMChannel channel)
        {
            var code = IMAPI.Instance().LeaveChatRoom( channel.ChannelID );
            if( code!=YIMEngine.ErrorCode.Success && ChannelEventListener!=null ){
                ChannelEventListener(new ChannelEvent( Conv.ErrorCodeConvert(code),ChannelEventType.LEAVE_FAIL,channel.ChannelID ));
            }
        }

        public void ReConnect()
        {
            throw new NotImplementedException();
        }

        public IClient SetDebug(bool isDebug)
        {
            if(isDebug){
                IMAPI.SetMode(0);
            }else{
                IMAPI.SetMode(2);
            }
            return this;
        }

        public IClient SetLogLevel(LogLevel logLevel)
        {

            return this;
        }

        public void SwitchToChannels(IMChannel[] channel)
        {
            LeaveAllChannel();
            JoinMultiChannel(channel);
        }

        public TextMessage SendTextMessage(string reciverID,ChatType chatType, string msgContent ,Action<ErrorCode,IMMessage> onSendCallBack){
            return null;
        }

        /**
        if return null，启动录音是失败的
         */
        public AudioMessage StartRecordAudio(string reciverID,ChatType chatType,string extraMsg,bool recognizeText,Action<IMRecordAudioEvent,ErrorCode,AudioMessage> recordCallback){
            ulong reqID = 0;
            YIMEngine.ErrorCode code = 0;
            if(recognizeText){
                code = IMAPI.Instance().SendAudioMessage(reciverID, (YIMEngine.ChatType)chatType, ref reqID);
            }else{
                code = IMAPI.Instance().SendOnlyAudioMessage(reciverID, (YIMEngine.ChatType)chatType, ref reqID);
            }
            if(code == YIMEngine.ErrorCode.Success){
                var msg = new AudioMessage(reciverID,chatType,extraMsg,recognizeText);
                return msg;
            }
            Log.e("StartRecordAudio fail error:"+code.ToString());
            return null;
        }

        public bool StopRecordAndSendAudio(AudioMessage audioMsg){
            
            return false;
        }
    }
}