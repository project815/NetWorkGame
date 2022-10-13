using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class Sequence : MonoBehaviour
{
    private Mode m_mode;
    private string serverAddress;
    private HostType hostType;
    private const int m_port = 50765;
    private TransportTCP m_transport = null;
    private int m_counter = 0;

    public Texture bgTexture;
    public Texture pushTexture;

    private static float WINDOW_WIDTH = 640.0f;
    private static float WINDOW_HIGHT = 480.0f;



    enum Mode{
        SelectHost = 0,
        Connection,
        Game,
        Disconnection,
        Error,
    }
    enum HostType {
        None = 0,
        Server,
        Client,
    } 

    void Awake()
    {
        m_mode = Mode.SelectHost;
        hostType = HostType.None;
        serverAddress = "";

        //NetWork 클래스의 컴포넌트 취득.
        GameObject obj = new GameObject("Network");
        m_transport = obj.AddComponent<TransportTCP>();
        DontDestroyOnLoad(obj); // TCP스크립트가 들어있는 네트워크 오브젝트 생성. 

        //호스트명을 가져옴.
        string hostname = Dns.GetHostName();
        //Dns클래스
        //보통 ip주소를 기억하기 힘들기 떄문에 호스트 명을 사용함.
        //로컬네트워크에서는 컴퓨터명을 호스트명을 하고 
        //인터넷 상에서는 호스트이름과 도메인이름을 사용함.
        //로컬에서는 AlexPC와 같은 호스트명, 인터넷상에서는 www.google.com과 같은 호스트명을 사용함.
        IPAddress[] addList = Dns.GetHostAddresses(hostname);
        //
        // IPv6는 128 비트 주소로서 "fe80::42a:545e:43be:6682%23"와 같이 좀 복잡하게 표기한다. 
        // IPv4 는 32비트 주소를 사용하고 "191.239.213.197" 와 같이 4개의 숫자로 표기하며
        serverAddress = addList[0].ToString();

    }

    // Update is called once per frame
    void Update()
    {
        switch(m_mode)
        {
            case Mode.SelectHost:
                OnUpdateSelectHost();
                break;
        }
    }

    void OnUpdateSelectHost()
    {
        switch(hostType)
        {
            case HostType.Server:
                {
                    bool ret = m_transport.StartServer(m_port, 1);
                    m_mode = ret? Mode.Connection : Mode.Error;
                }
                break;
            case HostType.Client:
                {
                    bool ret = m_transport.Connect(serverAddress, m_port);
                    m_mode = ret? Mode.Connection : Mode.Error;
                }
                break;
            default:
                break;
        }
    }
}
