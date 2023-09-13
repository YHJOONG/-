# [Server]NGINX - Spring boot 연동 설정

운영체제 : Centos7

**Nginx와 같은 웹 서버를 사용하는 이유**

- 클라이언트의 요청 처리를 분산시킬 수 있는 로드 밸런스를 사용함으로써 효율적인 처리를 합니다.
- 정적파일을 다이렉트로 제공해주기 떄문에 백엔드 서버에 부담을 주지 않습니다.
- 클라이언트는 Nginx 포트로만 백엔드 서버에 접근 할 수 있어 보안에 도움이 됩니다.

프록시

- 클라이언트와 서버 통신 중간에서 대신 통신을 해주는 서버
- 포워드 프록시는 내부망에 함께 있는 클라이언트가 인터넷을 통해 어딘가에 있는 서버로 요청을 보내려고 하면 이 요청을 받아 연결해준다. 클라이언트 앞에서 처리
- 리버스 프록시는 내부망의 서버 앞단에서 요청을 처리 (WAS는 대부분 DB 서버와 연결되어 있으므로 , WAS가 최전방에 있으면 보안에 취약해진다.)

1. NGINX 설치 

```
# nginx 공식 저장소 추가
sudo vim /etc/yum.repos.d/nginx.repo
# 파일에 아래 내용 추가
  [nginx]
  name=nginx repo
  baseurl=http://nginx.org/packages/centos/$releasever/$basearch/
  gpgcheck=0
  enabled=1

# nginx 설치
sudo yum install nginx
```

1. NGINX 시작

```
systemctl start nginx (서비스 시작)
systemctl status nginx (서비스 상태)
systemctl stop nginx (서비스 중지)
systemctl restart nginx (서비스 재 시작)
```

1. NGINX 기본 환경 변수 

```
cd /etc/nginx
mkdir site.d
mkdir ssl
chmod 777 ssl
```

1. nginx.conf 파일 변경

```
vi nginx.conf

맨 하단에 
#include /etc/nginx/conf.d/*.conf
include /etc/nginx/site.d/*.conf

conf 파일 site.d 디렉터리로 복사
cp conf.d/default.conf site.d/test.com.conf
(도메인 주소 : stawallet.com.conf)

서비스 재시작
systemctl restart nginx
```

1. nginx.conf 설정 

```
user  nginx;
worker_processes  auto; # worket_process 설정

error_log  /var/log/nginx/error.log notice;
pid        /var/run/nginx.pid;

events {
    worker_connections  1024; #하나의 프로세스가 처리할 수 있는 커넥션 수
}

#최대 접속자 수 : worket_processes * worker_connections

http {
    include       /etc/nginx/mime.types;
    default_type  application/octet-stream;

    log_format  main  '$remote_addr - $remote_user [$time_local] "$request" '
                      '$status $body_bytes_sent "$http_referer" '
                      '"$http_user_agent" "$http_x_forwarded_for"';

    access_log  /var/log/nginx/access.log  main;

    sendfile        on;
    #tcp_nopush     on;

    keepalive_timeout  65;

    #gzip  on;

    #include /etc/nginx/conf.d/*.conf;
    include /etc/nginx/site.d/*.conf;
}
```

1. default.conf 설정

```
server
{
    listen                80;

    server_name           test.com;

    return 301 https://$host$request_uri;
}

server
{
    listen 443 ssl;
    server_name localhost;
    ssl on;
    ssl_certificate       /etc/nginx/ssl/unified.test.com.pem; #공개키 경로
    ssl_certificate_key   /etc/nginx/ssl/_wildcard_.test.com.key.pem; #개인키 경로
    location / {
      proxy_pass http://127.0.0.1:10000; # 스프링부트 포트
    }

}
```

1. open-jdk 1.8 설치

```
yum install java-1.8.0-openjdk

yum install java-1.8.0-openjdk-devel
```

1. 환경 변수 등록

```
# readlink -f /usr/bin/java
/usr/lib/jvm/java-1.8.0-openjdk-1.8.0.242.b08-0.el7_7.x86_64/jre/bin/java

//# vi /etc/profile

...

JAVA_HOME=/usr/lib/jvm/java-1.8.0-openjdk-1.8.0.242.b08-0.el7_7.x86_64
PATH=$PATH:$JAVA_HOME/bin
CLASSPATH=$JAVA_HOME/jre/lib:$JAVA_HOME/lib/tools.jar

export JAVA_HOME PATH CLASSPATH
```