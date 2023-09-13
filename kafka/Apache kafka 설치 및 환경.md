# [Kafka][Apache kafka] 설치 및 환경

### Apache kafka 설치

```
$ cd /usr/local

$ wget https://downloads.apache.org/kafka/3.1.0/kafka_2.12-3.1.0.tgz

#압축 풀기
$ tar xvf kafka_2.12-3.1.0.tgz

#심볼릭 링크 설정
$ ln -s /usr/local/kafka_2.12-3.1.0 /kafka
```

### 서버별 구분을 위해 myid 지정

```
$ echo 1 > /tmp/kafka-logs/myid
$ cat /tmp/kafka-logs/myid
```

### Server.properties 파일 설정

```
..
    # 이전 과정에서 지정한 해당하는 myid값을 서버별로 입력
    broker.id=1
  ...
    # listeners에 현재 카프카가 설치되어 있는 서버의 자신의 IP를 입력(ex:192.168.0.203)
    listeners=PLAINTEXT://192.168.0.203:9092
  ...
    # 이전 과정에서 구성한 디스크를 입력(하나인경우 /data1, 두개인경우 /data1,/data2)
    log.dirs=/tmp/kafka-logs/data1
  ...
    # 필자는 미리 설정해놓은 고정IP로 zookeeper connect 경로를 설정
    zookeeper.connect=localhost:2181
  ...
```

### 방화벽 설정

```
//방화벽 허용
$ firewall-cmd --permanent --zone=public --add-port=9092/tcp

// 방화벽 reload
$ firewall-cmd --reload

// 만약 방화벽을 제한할 경우
$ firewall-cmd --permanent --zone=public --remove-port=9092/tcp
```

### 카프카 실행

```
// 시작
$ /kafka/bin/kafka-server-start.sh /kafka/config/server.properties -daemon

// 종료
$ /kafka/bin/kafka-server-stop.sh
```

OpenJDK 64-Bit Server VM warning 발생 할 경우 

```
OpenJDK 64-Bit Server VM warning: If the number of processors is expected to increase from one, then you should configure the number of parallel GC threads appropriately using -XX:ParallelGCThreads=N
```

카프카의 JVM 메모리가 기본적으로 1G로 할당되어서 실행, 서버 메모리가 부족하면 에러 발생

→ JVM 메모리 조정 (최소 128M, 최대 256M로 설정하여 에러 해결)

```
$ vi /kafka/bin/kafka-server-start.sh

if [ "x$KAFKA_HEAP_OPTS" = "x" ]; then
    #export KAFKA_HEAP_OPTS="-Xmx1G -Xms1G"
    export KAFKA_HEAP_OPTS="-Xmx256M -Xms128M"
fi
```

### 카프카 프로세스를 systemd에 등록

- kafka용 systemd 파일을 생성 [kafka-server.service]
    
    ```
    $ vi /usr/lib/systemd/system/kafka-server.service
    
    // 내용
    [Unit]
    Description=kafka-server
    After=network.target
    
    [Service]
    Type=simple
    User=bkjeon
    Group=bkjeon
    SyslogIdentifier=kafka-server
    WorkingDirectory=/kafka
    Restart=no
    RestartSec=0s
    ExecStart=/kafka/bin/kafka-server-start.sh /kafka/config/server.properties
    ExecStop=/kafka/bin/kafka-server-stop.sh
    
    [Install]
    WantedBy=multi-user.target
    ```
    
- systemd 설정 적용을 위한 systemd 재시작

```
$ sudo systemctl daemon-reload
```

### Kafka 서비스 실행 및 중지

```
$ systemctl start kafka-server.service 
$ systemctl stop kafka-server.service 
$ systemctl restart kafka-server.service 
$ systemctl status kafka-server.service 

# 서버 재부팅 시 자동으로 주키퍼 서비스 실행하게 설정
$ systemctl enable kafka-server.service 
```