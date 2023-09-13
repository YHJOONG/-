# VMware ESXi Rocky8 설치

회사 내에 구축된 VMware ESXi을 가상화 서버에 Rocky8 서버 설치

## ESXi란
- VMware에서 만든 가상화 OS
- vSphere Hypervisor 제품군에 속함
- 무료
- PC 리소스 효율적으로 배분, 비용 줄임 -> 가상화 관리용 OS 


## ESXI 접속
- 로그인 창 -> 부여받은 계정, 패스워드 로그인
![image](./image//%5BServer%5DVmware%20ESXi%20Rocky%20%EC%84%A4%EC%B9%98/dashboard.JPG)



## VM 생성 / 등록
- 메인 대시보드에 [VM 생성/등록] 버튼 클릭 -> VM 생성
![image](./image/%5BServer%5DVmware%20ESXi%20Rocky%20%EC%84%A4%EC%B9%98/2.JPG)  


## VM 생성 유형 선택
- [새 가상 시스템 생성] 선택
![image](./image/%5BServer%5DVmware%20ESXi%20Rocky%20%EC%84%A4%EC%B9%98/3.JPG)

## VM 이름 및 게스트 운영 체제 선택
- VM 이름 선택 (인스턴스내에 고유한 이름으로 설정)  
- 게스트 운영 체제 제품군 : Windows, Linux, Mac, 기타 중에 Linux 선택
- 게스트 운영 체제 버전 : Vmware Rocky 지원 x -> 기타 Linux 4.0 이상 (64비트) 선택
![image](./image/%5BServer%5DVmware%20ESXi%20Rocky%20%EC%84%A4%EC%B9%98/4.JPG)

## VM 스토리지 선택
- 가상 디스크의 데이터 스토어 선택
- Storage1 선택
![image](./image/%5BServer%5DVmware%20ESXi%20Rocky%20%EC%84%A4%EC%B9%98/5.JPG)


## VM 세부 설정
- CPU, memory, Disk 사용 목적에 맞춰 설정
- CD/DVD 드라이브 1 -> 데이터스토어 ISO 파일 선택 -> CD/DVD 미디어 [찾아보기] -> 준비된 Rocky8 이미지 선택
![image](./image/%5BServer%5DVmware%20ESXi%20Rocky%20%EC%84%A4%EC%B9%98/6.JPG)


## VM 설정 확인
- VM 설정된 정보 확인하여 체크 -> [완료] 클릭 -> VM 생성 완료
![image](./image/%5BServer%5DVmware%20ESXi%20Rocky%20%EC%84%A4%EC%B9%98/7.JPG)

## VM 실행
- 대시보드 왼쪽 메뉴에서 [가상시스템] 선택 -> 생성된 가상 머신 선택 -> [전원 켜기]
![image](./image/%5BServer%5DVmware%20ESXi%20Rocky%20%EC%84%A4%EC%B9%98/8.JPG)


## Rocky8 설치
- Install Rocky Linux 8 
![image](./image/%5BServer%5DVmware%20ESXi%20Rocky%20%EC%84%A4%EC%B9%98/9.JPG)

- 언어 선택
![image](./image/%5BServer%5DVmware%20ESXi%20Rocky%20%EC%84%A4%EC%B9%98/10.JPG)

- 서버 설정  
    패스워드, 시간대, 등등 설정 후 [Begin Installation] 클릭
    필수! [System] Install Destination -> DISK 선택
    Network & Host Name 선택 -> ens192 ON으로 변경
    설치 설정
    - [SOFTWARE SELECTION] -> [Base Environment] -> Minimal Install 선택  
    - [SOFTWARE SELECTION] -> [Additional Software Selected Environment] -> Standard 선택   

    ![image](./image/%5BServer%5DVmware%20ESXi%20Rocky%20%EC%84%A4%EC%B9%98/11.JPG)
 

