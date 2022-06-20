# [Server][Docker] NGINX 컨테이너 생성

# 1. 웹서버 설치(nginx - 1.14.0)

```
root@fdfd0775c230:/# sudo apt install nginx

-- nginx 버전 확인
root@fdfd0775c230:/# nginx -v
nginx version: nginx/1.14.0 (Ubuntu)

-- nginx start
root@fdfd0775c230:/# service nginx start

-- nginx status
root@fdfd0775c230:/# service nginx status

-- netstat port 확인
root@935e152d525c:/# netstat -antp
Active Internet connections (servers and established)
Proto Recv-Q Send-Q Local Address           Foreign Address         State       PID/Program name
tcp        0      0 0.0.0.0:80              0.0.0.0:*               LISTEN      55/nginx: master pr
tcp6       0      0 :::80                   :::*                    LISTEN      55/nginx: master pr
```

Curl로 접속 확인 (host ip 192.168.128.128)

```
C:\Users\admin>curl 192.168.128.128:80
<!DOCTYPE html>
<html>
<head>
<title>Welcome to nginx!</title>
<style>
    body {
        width: 35em;
        margin: 0 auto;
        font-family: Tahoma, Verdana, Arial, sans-serif;
    }
</style>
</head>
<body>
<h1>Welcome to nginx!</h1>
<p>If you see this page, the nginx web server is successfully installed and
working. Further configuration is required.</p>

<p>For online documentation and support please refer to
<a href="http://nginx.org/">nginx.org</a>.<br/>
Commercial support is available at
<a href="http://nginx.com/">nginx.com</a>.</p>

<p><em>Thank you for using nginx.</em></p>
</body>
</html>
```