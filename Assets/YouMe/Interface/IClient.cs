using System;

namespace YouMe
{
    public interface IClient
    {
        /**
        Base API
         */
        IClient Initialize(string appKey,string secretKey,Config config);
        // void Login(string userID,string token,Action<LoginEvent> callback);
        // void JoinChannel(IChannel channel);
        // void LeaveChannel(IChannel channel);

        /**
        Extention API
         */
        void ReConnect();
        // void JoinMultiChannel(IChannel[] channel);
        // void LeaveAllChannel();
        // void SwitchChannel(IChannel channel);
        // void Logout();

        /**
        Config API
         */
        IClient SetDebug(bool isDebug);
        IClient SetLogLevel(LogLevel logLevel);

        //信息查询
        // IChannel[] GetCurrentChannels();
        IUser GetCurrentUserID();
        ConnectStatus GetCurrentStatus();

    }

}