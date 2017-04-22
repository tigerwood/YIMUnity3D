using UnityEngine;
using YouMe;

public class IMTest : MonoBehaviour {

    IMClient IM;

    // Use this for initialization
    void Start () {
        IMClient.Instance.Initialize("appkey", "secretkey", ServerZone.China);
        IMClient.Instance.ConnectListener = OnConnect;
        IMClient.Instance.ChannelEventListener = OnJoinChannel;
        IMClient.Instance.ReceiveMessageListener = OnReceiveMessage;
        IMClient.Instance.FinishSendAudioListener = OnFinishSendAudio;

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
        TextMessage msg = IMClient.Instance.SendTextMessage("userid", ChatType.Private, "hello cat");
    }

	void OnReceiveMessage(IMMessage msg){
		if(msg.messageType == MessageType.AUDIO){
            var audioMsg = (AudioMessage)msg;
            ShowMsg(audioMsg);
            audioMsg.Download((code,audiMsgObj)=>{
                audiMsgObj.PlayAudioInQueue();
            });
        }else if(msg.messageType == MessageType.TEXT){
            ShowMsg(msg);
        }
	}


    public void StartRecord(){
        IMClient.Instance.StartRecordAudio("userid",ChatType.Private);
    }

    public void StopRecordAndSend(){
        IMClient.Instance.StopRecordAndSendAudio("");
    }
    

    public void OnFinishSendAudio(ErrorCode code,AudioMessage audioMsg){

    }

	void ShowMsg(IMMessage msg){
		
	}
}
