# Windows 메시지 후킹

#### 메시지 훅

Window 운영체제는 GUI를 제공하고 이는 Event Driven 방식으로 동작합니다.

키보드/ 마우스를 이용하여 메뉴 선택, 버튼 선택, 마우스 이동, 창 크기 변경, 창 위치 이동 등의 작업은 모두 이벤트입니다.

이런 이벤트가 발생할 때 OS는 미리 정의된 메시지를 해당 응용 프로그램으로 통보합니다. 응용 프로그램은 해당 메시지를 분석하여 필요한 작업을 진행하는 것 입니다. 

메시지 : 이벤트가 발생할때 OS가 응용 프로그램에게 통보하는 것!

-> 응용 프로그램은 이런 메시지를 분석하여 필요한 작업을 진행

메시지 훅은 이런 메시지를 중간에서 엿보는 것입니다.



OS Message Queue ---------------------------------------------------------------------------------> Application Message Queue

​                                                                        message 

1.키보드 입력 이벤트가 발생하면 WM_KEYDOWN 메시지가 [OS Message queue]에 추가됩니다.

2.OS는 어느 응용 프로그램에서 이벤트가 발생했는지 파악해서 [OS Message queue]에서 메시지를 꺼내어 해당 응용 프로그램의 [Application Message queue]에 추가합니다.

3.응용 프로그램(메모장)은 자신의 [application message queue]를 모니터링 하고 있다가 WM_KEYDOWN메시지가 추가된 걸 확인하고 해당 event handler를 호출합니다.



키보드 메시지 훅이 설치되어있다면!!!

OS 메시지 큐와 응용 프로그램 메시지 큐 사이에 설치된 훅 체인에 있는 키보드 메시지 훅들이 응용 프로그램보다 먼저 해당 메시지를 볼 수 가 있습니다. 

(같은 키보드 메시지 훅이라도 여러 개를 동시에 설치 할 수 있습니다. 이러한 훅은 설치 순서대로 호출되기 때문에 이를 훅 체인이라고 말합니다.)



이러한 메시지 훅 기능은 Window 운영체제에서 제공하는 기본기능이며 대표적인 프로그램으로 SPY++가 있습니다.

(SPY++은 막강한 메시지 후킹 프로그램으로서 운영체제에서 오고가는 모든 메시지를 볼 수 있습니다.)



#### SetWindowsHookEx()

메시지 훅은 SetWindowsHookEx() API를 사용해서 간단히 구현 할 수 있습니다.

```c++
HHOOK SetWindowsHookEx(
	int idHook, 		//hook type
	HOOKPROC lpfn,		//hook procedure
	HINSTANCE hMod,		//위 hook Procedure가 속해 있는 DLL 핸들
	DWORD dwThreadId	//hook을 걸고 싶은 thread의 ID

);
```

hook procedure은 운영체제가 호출해주는 콜백 함수입니다.

메시지 훅을 걸 때 hook procedure는 dll 내부에 존재해야 하며, 그 DLL의 인스턴스 핸들이 바로 hMod입니다.



이 함수를 이용해 훅을 설치해 놓으면, 어떤 프로세스에서 해당 메시지가 발생했을 때 운영체제가 해당 DLL 파일을 해당 프로세스에 강제로 인젝션 하고 등록된 hook procedure를 호출 합니다. 