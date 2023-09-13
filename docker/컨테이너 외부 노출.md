# [Server][Docker] 컨테이너 외부 노출

# 1. 컨테이너 외부 노출

## 1. 컨테이너 ip 확인 (Docker Container - Ubuntu 18.04)

ipconfig를 사용하기 위해 net-tools 설치

```
root@a79a76525257:/# apt-get update
root@a79a76525257:/# apt-get install net-tools
root@a79a76525257:/# ifconfig

eth0: flags=4163<UP,BROADCAST,RUNNING,MULTICAST>  mtu 1500
        inet 172.17.0.2  netmask 255.255.0.0  broadcast 172.17.255.255
        ether 02:42:ac:11:00:02  txqueuelen 0  (Ethernet)
        RX packets 9395  bytes 26058229 (26.0 MB)
        RX errors 0  dropped 0  overruns 0  frame 0
        TX packets 8804  bytes 480734 (480.7 KB)
        TX errors 0  dropped 0 overruns 0  carrier 0  collisions 0

lo: flags=73<UP,LOOPBACK,RUNNING>  mtu 65536
        inet 127.0.0.1  netmask 255.0.0.0
        loop  txqueuelen 1000  (Local Loopback)
        RX packets 0  bytes 0 (0.0 B)
        RX errors 0  dropped 0  overruns 0  frame 0
        TX packets 0  bytes 0 (0.0 B)
        TX errors 0  dropped 0 overruns 0  carrier 0  collisions 0
```

도커 컨테이너는 172.17.0.x 의 IP를 순차적으로 할당, lo는 로컬 호스트를 의미

## 2. 컨테이너 포트 포워딩

외부에 컨테이너의 애플리케이션을 노출하기 위해서는 eth0의 ip와 포트를 호스트이 IP와 포트에 바인딩을 해야한다. 

```
-- 도커 컨테이너 8080:8080 포트 포워딩
docker run -it --name mywebserver **-p 8080:8080** ubuntu:14.04

-- 호스트 IP의 8080번 포트로 접근 -> 호스트의 80번 포트는 컨테이너의 80번 포트로 포워딩 -> 웹서버 접근
```

실행중인 컨테이너일 경우에는 docker를 다시 만들거나 현재 컨테이너 이미지를 복사해서 포트 포워딩을 해야한다. (불편..)

```
1. stop running container
docker stop ubuntu

2. commit the container
docker commit ubuntu ubuntu_02

3. re-run the commited image
docker run -p 8080:8080 -p 80:80 -p 7922:22 -td ubuntu_02

4. prv-contaier delete
docker rm ubuntu

5. new-container rename
docker rename ubuntu_02 ubuntu
```

포트 설정 확인

```
[root@localhost ~]# docker ps
CONTAINER ID   IMAGE       COMMAND   CREATED         STATUS         PORTS                                                                                                                 NAMES
935e152d525c   ubuntu_03   "bash"    5 minutes ago   Up 5 minutes   0.0.0.0:80->80/tcp, :::80->80/tcp, 0.0.0.0:8080->8080/tcp, :::8080->8080/tcp, 0.0.0.0:7922->22/tcp, :::7922->22/tcp   ubuntu
```