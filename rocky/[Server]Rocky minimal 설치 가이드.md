# Rocky minimal 설치 후 기본 셋팅 작업 

## 네트워크 설정
DHCP를 통해 동적 IP를 할당 받을 수 있지만 정적 IP를 통해 네트워크 구성 -> 고정된 IP 사용

- 방법
  - 네트워크 인터페이스 구성 파일 수정
  - nmtui 이용하여 네트워크 수정

### 1. 네트워크 인터페이스 구성 파일 수정
- 파일 경로 : ``` /etc/sysconfig/network-scripts ```
- 네트워크 인터 페이스 구성 파일 : ```ifcfg-ens192```
- 파일 수정
  ```
  TYPE=Ethernet         //네트워크 인터페이스 장치의 종류
  PROXY_METHOD=none     //프록시 설정 안함
  BROWSER_ONLY=no       //프록시 구성 -> 브라우저 전용 설정 안함
  BOOTPROTO=static      //static : 고정IP, DHCP : 동적 IP
  DEFROUTE=yes          //Default Routing 사용함
  IPV4_FAILURE_FATAL=no //인터페이스 구성 실패 -> 비활성화 X
  IPV6INIT=no           //IPv6 비활성화
  NAME=ens192           //네트워크 장치 이름
  UUID=####-#####     
  DEVICE=ens192         //디바이스 이름
  ONBOOT=yes            //부팅 시 네트워크 자동 연결
  IPADDR=192.168.100.253 //IPv4 주소
  PREFIX=24             //넷마스크 길이
  DNS1=8.8.8.8          //DNS 주소
  DOMAIN=8.8.4.4        //도메인 
  ```

- 설정 적용
    ```
    $ systemctl restart NetworkManager //Network Manger 재시작

    $ nmcli network off   //네트워크 비활성화

    $ nmcli network on    //네트워크 활성화
    ```


### 2. nmtui 이용하여 네트워크 수정
nmtui : Network Manger에 텍스트 사용자 인터페이스를 제공

1. nmtui 초기 화면
![**초기화면**](./image/%5BServer%5DRocky%20minimal%20%EC%84%A4%EC%B9%98%20%EA%B0%80%EC%9D%B4%EB%93%9C/1.JPG)
    Edit a connection : 네트워크 연결 편집  
    Activate a connection : 네트워크 연결 활성화  
    Set system Hostname : 네트워크 호스트명 설정  

2. [Edit a connection] 
    ![image](./image/%5BServer%5DRocky%20minimal%20%EC%84%A4%EC%B9%98%20%EA%B0%80%EC%9D%B4%EB%93%9C/3.JPG)
    - Address, Gateway, DNS servers, Search domains 정보 입력
    - IPv6 CONFIGURATION Ignore 선택 (IPv6은 사용 안함)
   
    |항목|설정값|
    |----|---|
    |Addresses|192.168.100.253/24| 
    |Gateway|192.168.100.1|
    |DNS servsers|8.8.8.8|
    |Search domains|8.8.4.4|

  
3. [Activate a connection]
    ![image](./image/%5BServer%5DRocky%20minimal%20%EC%84%A4%EC%B9%98%20%EA%B0%80%EC%9D%B4%EB%93%9C/4.JPG)
    Deactivate : 비활성화 상태  
    activate : 활성화 상태

- 설정 적용
    ```
    $ systemctl restart NetworkManager //Network Manger 재시작

    $ nmcli network off   //네트워크 비활성화

    $ nmcli network on    //네트워크 활성화
    ```

### Hostname 변경
```
    [rroot@localhost ~]# hostname bitstoa-txapi //호스트명 변경
    [rroot@bitstoa-txapi ~]$ hostname   //호스트명 조회
    bitstoa-txapi
```

### 설정된 네트워크 확인
192.168.100.253으로 IP 셋팅 완료 
```
$ ifconfig
ens192: flags=4163<UP,BROADCAST,RUNNING,MULTICAST>  mtu 1500
        inet 192.168.100.253  netmask 255.255.255.0  broadcast 192.168.100.255
        ether 00:0c:29:96:78:44  txqueuelen 1000  (Ethernet)
        RX packets 1080  bytes 83142 (81.1 KiB)
        RX errors 0  dropped 100  overruns 0  frame 0
        TX packets 478  bytes 52852 (51.6 KiB)
        TX errors 0  dropped 0 overruns 0  carrier 0  collisions 0
```


## SSH 설정

ssh 설치 후 방화벽 설정 (Rocky 리눅스 설치 시 기본으로 ssh는 설치되어 있어 방화벽 설정만 적용)
- ssh 설치 
    ```
    $ sudo dnf upgrade --refresh -y  //패키지 최신상태 업데이트
    
    $ rpm -qa | grep openssh-server  // ssh 설치 확인
    openssh-server-8.0p1-13.el8.x86_64
    $ sudo dnf install openssh-server // ssh 설치
    $ sudo systemctl enable sshd --now //ssh 활성화
    $ sudo systemctl status sshd //ssh 상태 조회
    ● sshd.service - OpenSSH server daemon
   Loaded: loaded (/usr/lib/systemd/system/sshd.service; enabled; vendor preset: enabled)
   Active: active (running) since Thu 2022-07-21 13:59:35 KST; 38min ago
     Docs: man:sshd(8)
           man:sshd_config(5)
    Main PID: 2583 (sshd)
        Tasks: 1 (limit: 11345)
    Memory: 5.3M
    CGroup: /system.slice/sshd.service
            └─2583 /usr/sbin/sshd -D -oCiphers=aes256-gcm@openssh.com,chacha20-poly1305@openssh.com,aes256-ctr,aes256-cbc,aes128-gcm@openssh.com,aes128-ctr,aes128-cbc -oMACs=hma>

    Jul 21 13:59:35 bitstoa-txapi systemd[1]: Starting OpenSSH server daemon...
    Jul 21 13:59:35 bitstoa-txapi sshd[2583]: Server listening on 0.0.0.0 port 22.
    Jul 21 13:59:35 bitstoa-txapi sshd[2583]: Server listening on :: port 22.
    Jul 21 13:59:35 bitstoa-txapi systemd[1]: Started OpenSSH server daemon.
    Jul 21 14:04:12 bitstoa-txapi sshd[2812]: Accepted password for rroot from 10.10.11.13 port 62530 ssh2
    Jul 21 14:04:12 bitstoa-txapi sshd[2812]: pam_unix(sshd:session): session opened for user rroot by (uid=0)
    Jul 21 14:04:13 bitstoa-txapi sshd[2819]: Accepted password for rroot from 10.10.11.13 port 62531 ssh2
    Jul 21 14:04:13 bitstoa-txapi sshd[2819]: pam_unix(sshd:session): session opened for user rroot by (uid=0)

    ```
- 방화벽 설정
    1. ssh 기본 포트 22 허용
        ```
        $ firewall-cmd --permanent --zone=public --add-port=22/tcp 
        ```
        또는 ssh 서비스 등록
        ```
        $ firewall-cmd --permanent --zone=public --add-service=ssh 
        ```
    2. 방화벽 다시 로드
        ```
        $ firewall-cmd --reload
        ```
    3. 외부 ssh 툴로 접속 -> 정상 접속 완료


## ICMP 차단

ping 요청을 차단 -> icmp을 이용한 공격 방어

|값|설명|
|---|--|
|0|ICMP echo 허용|
|1|ICMP echo 차단|

```
$ sudo echo 1 > /proc/sys/net/ipv4/icmp_echo_ignore_all
-bash: /proc/sys/net/ipv4/icmp_echo_ignore_all: Permission denied
```  
echo 이용하여 변경 시도 -> 퍼미션 거부 에러 -> su root 계정 변경하여 변경 시도 -> 정상 처리


## JAVA 설치
```
    sudo dnf upgrade --refresh -y

    sudo whoami

    curl --version

    curl  -O https://download.java.net/java/GA/jdk16.0.2/d4a915d82b4c4fbb9bde534da945d746/7/GPL/openjdk-16.0.2_linux-x64_bin.tar.gz

    tar -xvf openjdk-16.0.2_linux-x64_bin.tar.gz

```
