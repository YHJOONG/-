# [Kafka][Apache Kafka] Zookeeper 설치 및 환경

Apache Zookeeper version : 3.8.0

Apache Kafka version : 3.1.0

### Apache Zookeeper 설치

```
$ cd /usr/local

$ wget https://dlcdn.apache.org/zookeeper/zookeeper-3.8.0/apache-zookeeper-3.8.0-bin.tar.gz

#압축 풀기
$ tar xvf apache-zookeeper-3.8.0-bin.tar.gz

#심볼릭 링크 설정
$ ln -s /usr/local/apache-zookeeper-3.8.0-bin /zookeeper
```

### 설치 시 다음과 같은 에러 발생하면 —no-check-certificate 뒤에 붙여서 다시 설치 시도

```
--2022-04-01 02:18:11--  https://dlcdn.apache.org/zookeeper/zookeeper-3.8.0/apache-zookeeper-3.8.0-bin.tar.gz
Resolving dlcdn.apache.org (dlcdn.apache.org)... 151.101.2.132, 2a04:4e42::644
Connecting to dlcdn.apache.org (dlcdn.apache.org)|151.101.2.132|:443... connected.
ERROR: cannot verify dlcdn.apache.org's certificate, issued by ‘/C=US/O=Let's Encrypt/CN=R3’:
  Issued certificate has expired.
To connect to dlcdn.apache.org insecurely, use `--no-check-certificate'.
```

### 클러스터 내 주키퍼 노드를 구분하기 위한 ID를 생성

```
$mkdir /tmp/zookeeper

$echo 1> /tmp/zookeeper/myid
#cat /tmp/zookeeper/myid 
```

server1 → myid : 1로 설정

server2 → myid : 2로 설정

server3 → myid : 3로 설정

 

### zoo.cfg설정 (standalone mode)

```
$ vi /zookeeper/conf/zoo.cfg

#주키퍼가 사용하는 시간에 대한 기본 측정(ms)
tickTime=2000

팔로워가 리더와 초기에 연결하는 시간에 대한 타임 아웃(tick)
initLimit=10

#팔로워가 리더와 동기화 하는 시간에 대한 타임 아웃 tick의 수
#주키퍼에 저장된 데이터가 크면 수를 늘려야함
syncLimit=5

#주키퍼의 트랜잭션 로그와 스냅샷이 저장되는 데이터 저장 경로
dataDir=/tmp/zookeeper

#주키퍼 사용 TCP Port
clientPort=2181
```

### zoo.cfg설정 (standalone mode)

```
$ vi /zookeeper/conf/zoo.cfg

tickTime=2000
initLimit=10
syncLimit=5
dataDir=/tmp/zookeeper
clientPort=2181

#server.1에 자기 자신은 0.0.0.0로 입력, server.2에 설정에서는 server.2가 0.0.0.0
#server.{1} -> {1}은 myid이다 즉 server.myid 형식으로 되어있음
server.1=zoo1:2888:3888
server.2=zoo2:2888:3888
server.3=zoo3:2888:3888
```

server list 2888 port : peer들 간 통신이 가능하게 하기 위한필수적 연결에 사용

server list 3888 port : Service Leader 선출을 위한 Election 포트에 사용

각각 서버마다 해당 설정파일을 만들어야한다.

### 방화벽 설정

```
// 방화벽 허용
$ firewall-cmd --permanent --zone=public --add-port=2181/tcp
$ firewall-cmd --permanent --zone=public --add-port=2888/tcp
$ firewall-cmd --permanent --zone=public --add-port=3888/tcp

// 방화벽 reload
$ firewall-cmd --reload

// 만약 방화벽을 제한할 경우
$ firewall-cmd --permanent --zone=public --remove-port=2181/tcp
```

### 주키퍼 실행 및 중지

```
# 서비스 실행
$ ./zookeeper/bin/zkServer.sh start

# 서비스 중지
$ ./zookeeper/bin/zKserver.sh stop

# 주키퍼 노드 확인
$ ./zkCli.sh
```

### 주키퍼 프로세스를 systemd에 등록

- zookeeper용 systemd 파일을 생성 [zookeeper-server.service]
    
    ```
    $ sudo vi /usr/lib/systemd/system/zookeeper-server.service
    
    // 내용
    [Unit]
    # systemctl status 명령어에 표시되는 상세 설명
    Description=zookeeper-server
    # 유닛이 시작되는 순서를 조정하여 After에 지정된 유닛이 실행된 이후 시작된다.
    After=network.target
    
    [Service]
    # ExecStart에 영향을 주는 유닛 프로세스가 시작되며, simple, forking, oeshot, idle 등이 있다.
    Type=forking
    User=root
    Group=root
    # syslog에서 구분하기 위한 이름
    SyslogIdentifier=zookeeper-server
    # 실행된 프로세스의 작업 디렉토리를 설정
    WorkingDirectory=/zookeeper
    # systemctl 명령어로 인한 중지를 제외하고 프로세스가 종료된 후 재시작한다.
    Restart=always
    # Restart 옵션과 연결되어 몇 초에 실행할지 결정
    RestartSec=0s
    # 서비스가 시작될 때 실행할 명령어 또는 스크립트 작성
    ExecStart=/zookeeper/bin/zkServer.sh start
    # 서비스가 정지될 때 실행할 명령어 또는 스크립트 작성
    ExecStop=/zookeeper/bin/zkServer.sh stop
    
    [Install]
    # 서비스가 실행될 타겟 설정
    WantedBy=multi-user.target
    ```
    

- systemd 설정 적용을 위한 systemd 재시작

```
$ sudo systemctl daemon-reload
```

### 주키퍼 서비스 실행 및 중지

```
$ systemctl start zookeeper-server.service 
$ systemctl stop zookeeper-server.service 
$ systemctl restart zookeeper-server.service 
$ systemctl status zookeeper-server.service 

# 서버 재부팅 시 자동으로 주키퍼 서비스 실행하게 설정
$ systemctl enable zookeeper-server.service 
```