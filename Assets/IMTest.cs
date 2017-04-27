using UnityEngine;
using UnityEngine.UI;
using YouMe;

public class IMTest : MonoBehaviour {

    public InputField userIDInput;
    public Button loginButton;
    public Button logoutButton;
    public Text logText;
    public Text recvMsgText;

    public InputField textMsgInput;

    IMClient IM;

    // Use this for initialization
    void Start () {
        IM = IMClient.Instance;
        IM.Initialize("YOUME670584CA1F7BEF370EC7780417B89BFCC4ECBF78", "yYG7XY8BOVzPQed9T1/jlnWMhxKFmKZvWSFLxhBNe0nR4lbm5OUk3pTAevmxcBn1mXV9Z+gZ3B0Mv/MxZ4QIeDS4sDRRPzC+5OyjuUcSZdP8dLlnRV7bUUm29E2CrOUaALm9xQgK54biquqPuA0ZTszxHuEKI4nkyMtV9sNCNDMBAAE=", ServerZone.China);
        IM.ConnectListener = OnConnect;
        IM.ChannelEventListener = OnJoinChannel;
        IM.ReceiveMessageListener = OnReceiveMessage;
        userIDInput.text = Random.Range(10000,999999).ToString();
    }

    public void OnLoginClick(){
        IM.Connect(userIDInput.text,"");
        userIDInput.interactable = false;
    }

    public void OnLogoutClick(){
        IM.Disconnect();
    }

    public void OnSendTextMsgClick(){
        IM.SendTextMessage(userIDInput.text,ChatType.PrivateChat,textMsgInput.text,(code,textMsgObj)=>{
            if (code == ErrorCode.SUCCESS)
            {
                Log2UI("发送：" + textMsgObj.content + " 成功");
            }else{
                Log2UI("发送：" + textMsgObj.content + " 失败");
            }
        });
    }

    public void OnStartRecordVoiceClick(){
        IM.StartRecordAudio(userIDInput.text, ChatType.PrivateChat, "",true, (code, audioMsg) =>
        {
            if(audioMsg.sendStatus == SendStatus.Sending){
                Log2UI("语音发送中");
                Log2UI("开始播放自己发送的语音");
                audioMsg.PlayAudio((audiomsg)=>{
                    Log2UI("完成播放自己发送的语音");
                });
            }else if(audioMsg.sendStatus == SendStatus.Sended){
                Log2UI("语音发送成功");
            }else if(audioMsg.sendStatus == SendStatus.Fail){
                Log2UI("语音发送失败");
            }else if (code != ErrorCode.SUCCESS) { 
                 Log2UI("启动录音失败");
            }
        });
    }
	
    public void OnStopRecordAndSendClick(){
        IMClient.Instance.StopRecordAndSendAudio();
    }

	void OnConnect(IConnectEvent connectEvent){
		if(connectEvent.EventType == ConnectEventType.CONNECTED ){
            userIDInput.interactable = false;
            loginButton.interactable = false;
            logoutButton.interactable = true;
            //进入聊天频道
            IM.JoinChannel(new IMChannel("5678"));
            Log2UI("登陆成功");
            Log2UI("开始进入频道:5678");
        }else if(connectEvent.EventType == ConnectEventType.OFF_LINE){
            IM.ReConnect();
            Log2UI("掉线了");
        }else if(connectEvent.EventType == ConnectEventType.DISCONNECTED){
            userIDInput.interactable = true;
            loginButton.interactable = true;
            logoutButton.interactable = false;
            Log2UI("注销登陆成功");
        }else if(connectEvent.EventType == ConnectEventType.CONNECT_FAIL){
            userIDInput.interactable = true;
            loginButton.interactable = true;
            logoutButton.interactable = false;
            Log2UI("登陆失败，错误码："+connectEvent.Code);
        }
	}

	void OnJoinChannel(ChannelEvent joinEvent){
		if(joinEvent.EventType == ChannelEventType.JOIN_SUCCESS){
            Debug.LogError("join room success");
            Log2UI("进入房间:"+joinEvent.ChannelID +" 成功");
        }else{
            Log2UI("进入房间:"+joinEvent.ChannelID +" 失败，code:"+joinEvent.code);
        }
	}

	void OnReceiveMessage(IMMessage msg){
		if(msg.messageType == MessageType.AUDIO){
            var audioMsg = (AudioMessage)msg;
            ShowMsg(audioMsg);
            Log2UI("下载语音文件");
            audioMsg.Download((code,audiMsgObj)=>{
                Log2UI("播放接收到的语音消息");
                audiMsgObj.PlayAudio(null);
            });
        }else if(msg.messageType == MessageType.TEXT){
            ShowMsg(msg);
        }
	}

	void ShowMsg(IMMessage msg){
        if (msg.messageType == MessageType.TEXT)
        {
            recvMsgText.text = "收到文本："+((TextMessage)msg).content + "\n" + recvMsgText.text;
        }else{
            recvMsgText.text = "收到语音："+((AudioMessage)msg).recognizedText + "\n" + recvMsgText.text;
        }
    }

    void Log2UI(string log){
        logText.text = log + "\n" + logText.text;
    }
}
