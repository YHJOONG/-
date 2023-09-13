# Docker 레디스 환경 구축

개발 환경 구축을 위해 도커 이미지를 이용  

ubuntu docker 설치 

```
sudo apt update #패키지 업데이트

# https를 통해 package 설치
sudo apt-get -y install \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg \
    lsb-release
```

Docker의 Official GPG Key 등록
```
 curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
```

stable repository 등록 (CPU에 맞게 명령 실행)
```
# cpu가 x86_64 또는 amd64계열일 경우
$ echo "deb [arch=amd64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

# cpu가 armhf계열일 경우
$ echo "deb [arch=armhf signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

# cpu가 arm64계열일 경우
$ echo "deb [arch=arm64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

```

apt-get list 확인
```
sudo apt-get list | grep docker-ce
```

docker 설치 
```
sudo apt-get update
sudo apt-get install docker-ce docker-ce-cli containerd.io
```

docker-compose 파일 경로
```
/redis/data/
/redis/docker-compose.yml
/redis.conf # 레디스 7.0.2에 맞는 conf 파일 
```

docker-compose.yml 파일  
레디스 버전 : 7.0.2
```
version: "3.8"
services:
    redis:
        image: redis:7.0.2
        restart: always
        volumes:
            - ./redis.conf:/usr/local/etc/redis/redis.conf
            - ./data:/data
        ports:
            - 6379:6379
        command: redis-server /usr/local/etc/redis/redis.conf

```
docker-compose 실행
```
docker-compose up -d 
```



## docker 명령어로 생성
---
docker - 레디스 간 볼륨 매칭을 위한 디렉터리 생성
```
sudo mkdir -p /data/redis
```

레디스 도커 이미지 다운로드
```
sudo docker image pull redis
```

레디스용 도커 네트워크 생성
```
sudo docker network create redis-net
```

레디스 서버 컨테이너 실행
```
sudo docker run --name redis -p 6379:6379 --network redis-net -v /data/redis:/data -d redis redis-server --appendonly yes
```

redis-cli로 해당 레디스 서버에 접속
```
sudo docker run -it --network redis-net --rm redis redis-cli -h redis
```

redis 서버 IP 주소 확인 
```
sudo docker network inspect redis-net
```