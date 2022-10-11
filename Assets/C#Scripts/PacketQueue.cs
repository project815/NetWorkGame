using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

public class PacketQueue : MonoBehaviour
{

    struct PacketInfo
    {
        public int offset;
        public int size;
    };

    //stream 타입은 일련의 바이트를 일관성 있게 다루는 공통 기반을 제공한다.
    //stream의 사전적 의미는 '흐름'인데, 프로그래밍에서 사용될 때는 일반적으로 '바이트 데이터의 흐름'을 의미한다.
    //MemoryStream은 Stream 추상클래스를 상속받은 타입입니다.
    //메모리에 바이트 데이터를 순서대로 읽고 쓰는 작업을 수행하는 클래스. > 이를 이용 데이터를 직렬화/ 역직렬화하는 것이 가능하다.
    //
    private MemoryStream m_streamBuffer;
    private List<PacketInfo> m_offsetList;
    private int m_offset = 0;

    public PacketQueue()
    {
        m_streamBuffer = new MemoryStream();
        m_offsetList = new List<PacketInfo>();
    }

    public int Enqueue(byte[] data, int size)
    {
        PacketInfo info = new PacketInfo();
        info.offset = m_offset;
        info.size = size;

        //패킷 저장 정보를 보존.
        m_offsetList.Add(info);

        //패킷 데이터를 보존
        m_streamBuffer.Position = m_offset;
        m_streamBuffer.Write(data, 0, size);
        //Write함수 매개변수
        //1. buffer : 데이터를 쓸 버퍼.
        //2. offset : 현재 스트림으로 바이트를 복사하기 시작할 Buffer의 바이트 오프셋(0부터 시작)
        //3. count : 쓸 최대 바이트 수.
        m_streamBuffer.Flush();
        //Flush함수의 의미 : 버퍼에 있는 데이터를 모두 처리해라
        //즉, 파일로 치면 임시 메모리에 있는 데이터를 모두 디스크에 써라는 의미.
        //Write()함수를 cpu가 처리했더라도 모든 데이터가 물리적으로 디스크에 전부 저장되지 않을 수 있다.
        //일부는 버퍼라는 임시 저장소에 쓰기를 대기할 수 있다.
        //따라서 Write()한 파일을 다시 읽거나 할 때는 반드시 Flush()를 쓰는 것이 좋다.
        m_offset += size;

        return size;
    }

    public int Dequeue(ref byte[] buffer, int size)
    {
        if(m_offsetList.Count <= 0)
        {
            return -1;
        }
        PacketInfo info = m_offsetList[0];

        //패킷으로부터 해당하는 패킷 데이터를 가져옴.
        int dataSize = Math.Min(size, info.size);
        m_streamBuffer.Position = info.offset;
        int recvSize = m_streamBuffer.Read(buffer, 0, dataSize);
        //반환값 : 버퍼로 읽어온 총 바이트 수. (int32)
        //현재 많은 바이트를 쓸 수 없는 경우 버퍼에 할당된 바이트 수보다 작을 수 있으며(?), 메모리 스트림의 끝에 도달하면 0이 될 수 있다.

        //큐 데이터를 추출했음으로 선두 요소를 삭제.
        if(m_offsetList.Count == 0)
        {
            Clear();
            m_offset = 0;
        }
        return recvSize;
    }

    public void Clear()
    {
        byte[] buffer = m_streamBuffer.GetBuffer();
        //반환값 : Byte[]
        //이 스트림을 만든 바이트 배열이거나 현재 인스턴스의 생성 도중 MemoryStream 생성자에 바이트 배열이 제공되지 않는 경우 내부 배열.

        //버퍼에는 사용되지 않을 수 있는 할당된 바이트가 포함되어 있습니다.
        //예를 들어 문자열 "test"가 개체에 MemoryStream 기록되면 반환된 GetBuffer 버퍼의 길이는 4가 아닌 256이고 252바이트는 사용되지 않습니다.
        //버퍼의 데이터만 가져오려면 메서드를 ToArray 사용합니다.
        //그러나 ToArray 메모리에 데이터의 복사본을 만듭니다.
        Array.Clear(buffer, 0, buffer.Length);
        //배열의 각 요소 형식의 기본값으로 요소의 범위를 설정.
        //1. array : 포함된 요소를 지울 배열
        //2. 지울 요소의 범위의 시작 인덱스
        //3. 지울 요소의 개수
    }
}
