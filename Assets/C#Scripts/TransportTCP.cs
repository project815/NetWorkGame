// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using System.Net;
// using System.Net.Sockets;
// using System.Threading;

// public class TransportTCP : MonoBehaviour
// {   
//     //
//     //소켓 접속 관련
//     //

//     //리스닝 소켓.
//     private Socket m_listener = null;

//     //클라이언트 접속용 소켓.
//     private Socket m_socket = null;

//     //송신 버퍼
//     private PacketQueue m_sendQueue;
//     //수신 버퍼
//     private PacketQueue m_recvQueue;

//     //서버 플래그
//     private bool m_isServer = false;
//     //접속 플래그
//     private bool m_isConnected = false;

//     //
//     //이번트 관련 멤버 변수.
//     //

//     //이벤트 알림 델리게이트
//     //델리게이트라는 것은 함수에 대한 참조.
//     //하나의 델리게이트로 여러 함수들에 접근해 실행할 수 있다.
//     public delegate void EventHandler(NetEventState state);
//     private EventHandler m_handler;

//     //
//     //스레드 관련 멤버 변수.
//     //

//     //스레드 실행 플래그.

//     protected bool m_threadLoop = false;
//     protected Thread m_thread = null;
//     private static int s_mtu = 1400;


//     // Start is called before the first frame update
//     void Start()
//     {
//         m_sendQueue = new PacketQueue();
//         m_recvQueue = new PacketQueue();
//     }

//     // 대기 시작.
//     public bool StartServer(int port, int connectionNum)
//     {
//         Debug.Log("StartServer called.!");
//         //리스닝 소켓을 생성.
//         try{
//             //소켓을 생성.
//             m_listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//             // 사용할 포트 번호를 할당.
//             m_listener.Bind(new IPEndPoint(IPAddress.Any, port));
//             //System.Net.Socket에 내장된 인터페이스.
//             //대기.
//             m_listener.Listen(connectionNum);
//         }
//         catch{
//             Debug.Log("StartServer fail");
//             return false;
//         }

//         m_isServer = true;

//         return LaunchThread();
//     }

//     public void 
   






//     //Thread
//     //스레드는 하나의 프로세스 컴퓨
//     //프로그램이 실행되서 돌아가고 있는 상태(컴퓨터가 어떤 일을 하고 있는 상태) > 프로세스.
//     //동시성(Concurrency) : 여러 작업을 돌아가면서 일부분씩 진행 - 진행중인 작업을 바꾸는 것을 Context Switching >> 빠르게 돌아감으로써 동시에 진행되는 것처럼 보임.
//     //병렬성(Parallelism) : 프로세서 하나에 여러개의 코어 달려서 각각 동시에 진행(듀얼코어, 쿼드코어, 옥타코어)멀티코어 프로세서가 달린 컴퓨터의 방식
//     //cpu의 발열 문제로 인한 발전의 한계를 여러 프로세스를 나누는 방식으로 사용하고 있다.
//     //한 프로세스 내에서도 여러 갈래의 작업들을 해야한다. >>   


//     //프로세서와 프로세스는 서로 다르다.
//     //Processor(프로세서)는 하드웨어적인 측면에서 "컴퓨터 내에서 프로그램을 수행하는 하드웨어 유닛"이다.
//     //https://blogger.pe.kr/422
//     //https://soccom.tistory.com/59

//    bool LaunchThread()
//    {
//         try{
//             m_threadLoop = true;
//             m_thread = new Thread(new ThreadStart(Dispatch));
//             m_thread.Start();
//         }
//         catch{
//             Debug.Log("Cannot launch thread");
//             return false;
//         }

//         return true;
//    }

//    //스레드 측의 송수신 처리.
//    public void Dispatch()
//    {
//         Debug.Log("Dispatch thread started");

//         while(m_threadLoop)
//         {
//             AcceptClient();

//         }
//    }

//    void AcceptClient()
//    {
//         if(m_listener != null && m_listener.Poll(0, SelectMode.SelectRead))
//         //리스닝 소켓이 존재, 
//         //P0ll()
//         //보통 Receive() 명령과 같은 블로킹 네트워크 함수를 실행하고자 할 때, 명령어 실행 전에 소켓을 확인할 수 있는 기능이 있어야 한다.
//         //매개변수
//         //1. microSeconds int32 : 응답을 기다릴 시간(마이크로 초)
//         //2. SelectMode
//         //   1. SelectRead : Listen(int32)이 호출, 연결이 보류 // 데이터 읽기 // 연결닫기나 재설정이거나 종료 >> true 이외 false
//         //   2. SelectWrite : Connect(EndPoint)가 처리 중이고 연결에 성공 // 데이터 읽기 >> true 이외 false
//         //   3. SelectError : 차단되지 않는 Connect(EndPoint)가 처리 중이고 연결에 실패하면 ture (?)
//         //                      OutofBandInline이 설정되지 않고  out of band데이터를 사용할 수 있으면 true 이외 false
//         // 즉, listen함수가 호출되었음. >> true
//         {

//         }
//    }
// }
