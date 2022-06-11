# [Server][Docker] 컨테이너 띄우기

# 1. 도커 이미지

컨테이너를 생성할 떄 필요한 요소

가상 머신을 생성할 때 사용하는 iso 파일과 비슷한 개념

## 이미지의 이름 형대

```
[저장소이름]/[이미지 이름]:[태그]
```

- 저장소 이름은 이미지가 저장된 장소를 의미
- 이미지의 이름은 주로 어떤 역할을 하는지 표시
- 태그는 이미지의 버전관리

## 도커 컨테이너

운영체제 부터 애플리케이션, 각종 도구의 이미지로 컨테이너를 생성하면 해당 목적에 맞는 독립된 공간이 생성 

# 2. 컨테이너 생성하기

## 1. docker run

```
[root@localhost rroot]# sudo docker run -it ubuntu:18.04
Unable to find image 'ubuntu:18.04' locally
18.04: Pulling from library/ubuntu
09db6f815738: Pull complete
Digest: sha256:478caf1bec1afd54a58435ec681c8755883b7eb843a8630091890130b15a79af
Status: Downloaded newer image for ubuntu:18.04
```

ubuntu:18.04는 도커 허브가 가진 공식 이미지이고 로컬에서 없으면 자동으로 내려받는다.

## 2. docker images

```
[root@localhost rroot]# sudo docker images
REPOSITORY   TAG       IMAGE ID       CREATED      SIZE
ubuntu       18.04     ad080923604a   2 days ago   63.1MB
```

현재 저장되어 있는 도커 이미지의 리스트를 출력한다.

## 3. docker pull

```
[root@localhost rroot]# sudo docker pull centos:8
```

run이랑 다르게 컨테이너를 생성하지 않고 이미지만 다운받는다.

## 4. docker create

```
[root@localhost rroot]# sudo docker create -it --name mycentos centos:7
```

다운 받은 이미지를 통해 컨테이너를 생성하는 명령어

name 옵션을 통해 컨테이너 이름 지정

## 5. docker start

```
[root@localhost rroot]# sudo docker start [컨테이너 이름 혹은 컨테이너 아이디]
```

컨테이너 실행

## 6. docker attach

```
[root@localhost rroot]# sudo docker attach [컨테이너 이름 혹은 컨테이너 아이디]
```

컨테이너 실행 → 접속

```
[root@localhost rroot]# docker exec -it [컨테이너 이름 혹은 컨테이너 아이디] /bin/bash
```

## 7. docker 컨테이너 목록

```
-- 실행중인 컨테이너 출력
[root@localhost rroot]# docker ps
CONTAINER ID   IMAGE          COMMAND   CREATED         STATUS         PORTS     NAMES
a79a76525257   ubuntu:18.04   "bash"    6 minutes ago   Up 6 minutes             nifty_hypatia

-- 정지된 컨테이너까지 출력
[root@localhost rroot]# docker ps -a
CONTAINER ID   IMAGE          COMMAND       CREATED              STATUS         PORTS     NAMES
f900abf57b84   centos:8       "/bin/bash"   About a minute ago   Created                  mycentos
a79a76525257   ubuntu:18.04   "bash"        6 minutes ago        Up 6 minutes             nifty_hypatia
```

## 8. docker 컨테이너 종료 및 삭제

```
[root@localhost rroot]# docker stop [컨테이너]
[root@localhost rroot]# docker rm [컨테이너]

//강제 삭제
[root@localhost rroot]# docker rm -f [컨테이너]
```