using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class chat : MonoBehaviour
{
    private TransportTCP m_transport;

    private ChatState m_state = ChatState.HOST_TYPE_SELECT;
    private string m_hostAddress = "";
    private const int m_port = 50765;

    private string m_sendComment = "";
    private string m_prevComment = "";
    private string m_chatMessage = "";
    private List<string>[] m_message;






    private static int CHAT_MEMBER_NUM = 2;

    enum ChatState
    {
        HOST_TYPE_SELECT = 0,
        CHATTING,
        LEAVE,
        ERROR,
    }
    // Start is called before the first frame update
    void Start()
    {
        IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        System.Net.IPAddress hostAddress = hostEntry.AddressList[0];
        Debug.Log(hostEntry.HostName);
        m_hostAddress = hostAddress.ToString();

        GameObject obj = new GameObject("NetWork");
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_state)
        {
            case ChatState.HOST_TYPE_SELECT:
                for(int i = 0; i < CHAT_MEMBER_NUM; ++i){
                    m_message[i] = new List<string>();
                }
                break;
        }
    }
}
