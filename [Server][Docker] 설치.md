# [Server][Docker] 설치

# 1. 도커란?

리눅스 컨테이너를 기반으로 하여 특정한 서비스를 패키징하고 배포하는데 유용한 오픈 소스 프로그램

## 1. 가상머신과 도커 컨테이너

가상머신 : 하이퍼바이저를 이용해 Guest OS를 만들어 낸다. 

도커 컨테이너 : 꼭 필요한 것만 담겨서 구동

 → 컨테이너 안에는 애플리케이션을 구동하는데 필요한 라이브러리 및 실행 파일만 존재

## 2. 도커의 장점

1. 애플리케이션의 독립성 
2. 도커 이미지라는 패키지를 만들어서 운영 서버에 전달
3. 즉 컨테이너 하나당 하나의 기능만을 제공하는 모듈로 만들어서 부하가 많은 모듈은 여러개 만들고 하는 조정이 가능

# 2. 도커 설치 (Centos 7 설치)

## 1. 시스템 패키지 업데이트 및 필요한 종속성 설치

```
# sudo yum update
# sudo yum install yum-utils device-mapper-persistent-data lvm2
```

## 2. 도커에 안정적 리포지토리를 시스템에 추가

```
# sudo yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
```

## 3. yum을 사용하여 최신 버전의 도커 CE(Community Edition)을 설치

```
# sudo yum install docker-ce
```

## 4. Docker 데몬을 시작하고 부팅시 자동으로 시작되도록 설정

```
# sudo systemctl start docker
# sudo systemctl enable docker
# docker -v  Docker version 20.10.17, build 100c701
```