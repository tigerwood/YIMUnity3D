using System;

namespace YouMe
{
    public interface IClient
    {
        /**
        Base API
         */
        IClient Initialize(string appKey,string secretKey,ServerZone zone = ServerZone.China);
        void Connect(string userID,string token="");
        // void JoinChannel(IChannel channel);
        // void LeaveChannel(IChannel channel);

        Action<IConnectEvent> ConnectListener{set;get;}
        Action<ChannelEvent> ChannelEventListener{set;get;}

        /**
        Extention API
         */
        void ReConnect();
        // void JoinMultiChannel(IChannel[] channel);
        void LeaveAllChannel();
        // void SwitchChannel(IChannel channel);
        void Disconnect();

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