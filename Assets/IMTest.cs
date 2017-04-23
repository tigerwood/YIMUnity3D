using UnityEngine;
using YouMe;

public class IMTest : MonoBehaviour {

    IMClient IM;

    // Use this for initialization
    void Start () {
        IM = IMClient.Instance;
        IM.Initialize("appkey", "secretkey", ServerZone.China);
        IM.ConnectListener = OnConnect;
        IM.ChannelEventListener = OnJoinChannel;
        IM.ReceiveMessageListener = OnReceiveMessage;

    }

	void StartGame(){
        IM.Connect("userid","token");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnConnect(IConnectEvent connectEvent){
		if(connectEvent.EventType == ConnectEventType.CONNECTED ){
			IM.JoinChannel(new IMChannel("5678"));
		}else if(connectEvent.EventType == ConnectEventType.OFF_LINE){
            IM.ReConnect();
        }
	}

	void OnJoinChannel(ChannelEvent joinEvent){
		if(joinEvent.EventType == ChannelEventType.JOIN_SUCCESS){
            Debug.LogError("join room success");
        }
	}

	void SendTextMessage(){
        TextMessage msg = IMClient.Instance.SendTextMessage("userid", ChatType.PrivateChat, "hello cat",(code,message)=>{
            if(code == ErrorCode.SUCCESS){
                Log.e("发送文本消息成功："+message.content);
            }else{
                Log.e("发送文本消息失败："+message.content);
            }
        });
    }

	void OnReceiveMessage(IMMessage msg){
		if(msg.messageType == MessageType.AUDIO){
            var audioMsg = (AudioMessage)msg;
            ShowMsg(audioMsg);
            audioMsg.Download((code,audiMsgObj)=>{
                audiMsgObj.PlayAudio(null);
            });
        }else if(msg.messageType == MessageType.TEXT){
            ShowMsg(msg);
        }
	}

    public void StartRecord(){
        var recordAudioMsg = IMClient.Instance.StartRecordAudio("userid",ChatType.PrivateChat,"",true,(code,audioMessage)=>{
            if(code == ErrorCode.SUCCESS){
                if(audioMessage.sendStatus == SendStatus.Sending){
                    Log.e("录音完成，发送语音消息中...");
                    audioMessage.PlayAudio(null);//可以播放了
                }else if(audioMessage.sendStatus == SendStatus.Sended){
                    Log.e("语音消息发送成功。");
                    audioMessage.PlayAudio(null);//也可以播放
                }
            }else{
                Log.e("启动录音失败，错误码："+code.ToString());
            }
        });
    }

    public void StopRecordAndSend(){
        IMClient.Instance.StopRecordAndSendAudio();
    }
    

    public void OnFinishSendAudio(ErrorCode code,AudioMessage audioMsg){

    }

	void ShowMsg(IMMessage msg){
		
	}
}
