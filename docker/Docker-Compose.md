# [Server][Docker] Docker-Compose

여러 개의 컨테이너로부터 이루어진 서비스를 구축, 실행하는 순서를 자동으로 하여 관리하는 기능

## 단계

1) 각각의 컨테이너의 Dockerfile을 작성  (기존에 있는 이미지를 사용하는 경우 불필요)

2) docker-compose.yml를 작성하고 각각의 독립된 컨테이너의 실행 정의를 실시

3) “docker-compose up” 커맨드를 실행하여 docker-compose.yml으로 정의된 컨테이너를 개시

## docker-compose.yml 파일이란

yaml으로 Docker 컨테이너에 관한 실행 옵션를 기재한 파일

## docker-compose 설치

```
[root@localhost ~]# sudo curl -L "https://github.com/docker/compose/releases/download/1.27.4/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

-- 권한 부여
[root@localhost ~]# sudo chmod +x /usr/local/bin/docker-compose

-- 버전 출력
[root@localhost ~]# docker-compose --version
docker-compose version 1.27.4, build 40524192
```

## docker-compose 명령어

- up : docker-compose에 정의되어 있는 모든 서비스 컨테이너를 한 번에 생성하고 실행, -d 옵션을 통해 back ground에서 컨테이너를 띄움
- down : docker-compose에 정의되어 있는 모든 서비스 컨테이너를 한 번에 정지시키고 삭제
- start : 정지된 특정 서비스 컨테이너를 올리기 위해서 사용
- stop : 특정 서비스 컨테이너를 정지 시키기 위해서 사용
- ps : docker-compose에 정의되어 있는 모든 서비스 컨테이너 목록을 조회할 때 사용
- config : docker-compose 설정을 확인할 떄 사용

### 예시) docker-compose.yml - mariadb

```
version: "3"
services:
  db:
    image : mariadb
    ports :
      - 3306:3306
    volumes:
      - ./db/conf.d/:/etc/mysql/conf.d
      - ./db/data:/var/lib/mysl
      - ./db/initdb.d:/docker-entrypoint-initdb.d
    env_file : .env
    environment :
      TZ : Asia/Seoul
    networks:
      - backend
    restart : always

network:
  backend:

```