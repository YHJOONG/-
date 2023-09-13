# C# 윈도우 관리 도구(WMI), WMI쿼리를 이용한 시스템 확인 방법

WMI는 윈도즈 관리 도구(Window Management Instrumentation)라고 불리우며, Windows에 대한 Microsoft의 주요 관리 기술이라고 부른다.

쉽게 말해 모든 Windows 리소스를 엑세스, 구성, 관리 및 모니터링 할 수 있는 수단이라 할 수 있다.



#### WMI를 통해 알수 있는 정보들

ALIAS : 로컬 시스템에서 사용 가능한 별칭 액세스

BASEBOARD : 기본 보드(마더 보드 또는 시스템 보드) 관리

BIOS : 기본 입출력 서비스(BIOS) 관리

BOOTCONFIG : 부트 구성 관리

CDROM : CD-ROM 관리

COMPUTERSYSTEM : 컴퓨터 시스템 관리

CPU : CPU 관리

CSPRODUCT : SMBIOS의 컴퓨터 시스템 제품 정보

DATAFILE : DataFile 관리

DCOMAPP : DCOM 응용 프로그램 관리

DESKTOP : 사용자 데스크톱 관리

DESKTOPMONITOR : 데스크톱 모니터 관리

DEVICEMEMORYADDRESS : 장치 메모리 주소 관리

DISKDRIVE : 실제 디스크 드라이브 관리

DISKQUOTA : NTFS 볼륨의 디스크 공간 사용

DMACHANNEL : 직접 메모리 액세스(DMA) 채널 관리

ENVIRONMENT : 시스템 환경 설정 관리

FSDIR : 파일 시스템 디렉터리 항목 관리

GROUP : 그룹 계정 관리

IDECONTROLLER : IDE 컨트롤러 관리

IRQ : 인터럽트 요청(IRQ) 관리

JOB : 일정 서비스를 사용하여 예약된 작업 액세스

LOADORDER : 실행 종속성을 정의하는 시스템 서비스 관리

LOGICALDISK : 로컬 저장 장치 관리

LOGON : LOGON 세션

MEMCACHE : 캐시 메모리 관리

MEMLOGICAL : 시스템 메모리 관리(구성 레이아웃 및 사용 가능한 메모리)

MEMORYCHIP : 메모리 칩 정보.

MEMPHYSICAL : 컴퓨터 시스템의 실제 메모리 관리

NETCLIENT : 네트워크 클라이언트 관리

NETLOGIN : (특정 사용자의) 네트워크 로그인 정보 관리

NETPROTOCOL : 프로토콜(및 네트워크 특성) 관리

NETUSE : 활성 네트워크 연결 관리

NIC : 네트워크 인터페이스 컨트롤러(NIC) 관리

NICCONFIG : 네트워크 어댑터 관리

NTDOMAIN : NT 도메인 관리

NTEVENT : NT 이벤트 로그의 항목

NTEVENTLOG : NT 이벤트 로그 파일 관리

ONBOARDDEVICE : 마더 보드(시스템 보드)에 내장된 일반 어댑터 장치 관리

OS : 설치된 운영 체제 관리

PAGEFILE : 가상 메모리 파일 스와핑 관리

PAGEFILESET : 페이지 파일 설정 관리

PARTITION : 실제 디스크의 파티션된 영역 관리

PORT : I/O 포트 관리

PORTCONNECTOR : 실제 연결 포트 관리

PRINTER : 프린터 장치 관리

PRINTERCONFIG : 프린터 장치 구성 관리

PRINTJOB : 인쇄 작업 관리

PROCESS : 프로세스 관리

PRODUCT : 설치 패키지 작업 관리

QFE : QFE(Quick Fix Engineering)

QUOTASETTING : 볼륨의 디스크 할당량 정보 설정

RDACCOUNT : 원격 데스크톱 연결에 대한 사용 권한 관리.

RDNIC : 특정 네트워크 어댑터에서의 원격 데스크톱 연결 관리.

RDPERMISSIONS : 특정 원격 데스크톱 연결에 대한 사용 권한.

RDTOGGLE : 원격으로 원격 데스크톱 수신 대기자 켜기 또는 끄기.

RECOVEROS : 운영 체제에 오류가 있을 때 메모리에서 수집할 정보

REGISTRY : 컴퓨터 시스템 레지스트리 관리

SCSICONTROLLER : SCSI 컨트롤러 관리

SERVER : 서버 정보 관리

SERVICE : 서비스 응용 프로그램 관리

SHADOWCOPY : 섀도 복사본 관리.

SHADOWSTORAGE : 섀도 복사본 저장소 영역 관리.

SHARE : 공유 리소스 관리

SOFTWAREELEMENT : 시스템에 설치된 소프트웨어 제품 요소 관리

SOFTWAREFEATURE : SoftwareElement의 소프트웨어 제품 하위 집합 관리

SOUNDDEV : 사운드 장치 관리

STARTUP : 사용자가 컴퓨터에 로그온할 때 자동으로 실행할 명령 관리

SYSACCOUNT : 시스템 계정 관리

SYSDRIVER : 기본 서비스를 위한 시스템 드라이버 관리

SYSTEMENCLOSURE : 실제 시스템 엔클로저 관리

SYSTEMSLOT : 포트, 슬롯, 주변 기기 및 기타 연결점의 실제 연결점 관리

TAPEDRIVE : 테이프 드라이브 관리

TEMPERATURE : 온도 센서(전자식 온도계)의 데이터 관리

TIMEZONE : 표준 시간대 데이터 관리

UPS : UPS 관리

USERACCOUNT : 사용자 계정 관리

VOLTAGE : 전압 센서(전자식 전압계) 데이터 관리

VOLUME : 로컬 저장소 볼륨 관리.

VOLUMEQUOTASETTING : 디스크 할당량 설정을 특정 디스크 볼륨과 연결

VOLUMEUSERQUOTA : 사용자 당 저장소 볼륨 할당량 관리.

WMISET : WMI 서비스 작업 매개 변수 관리









