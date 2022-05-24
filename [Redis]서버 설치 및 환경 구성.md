# [Redis]서버 설치 및 환경 구성

- Redis(Remote Dictionary Server)는 메모리 기반의 key-value 저장소
- 다양한 자료구조(문자열, 리스트, 해시, 셋, 정렬된 셋 등)을 지님
- publish / subscribe 구조 가능

설치 환경 : Window Server 2016

설치 경로 :   C:\Program Files\Redis (기본)

레디스 서버 버전 : 64bits, 5.0.14

### Redis 서비스 등록

1. 서비스 등록
    
    ```
    redis-server.exe --service-install "C:\Program Files\Redis\redis.windows-service.conf" --service-name redis
    ```
    

1. 서비스 시작
    
    ```
    redis-server.exe --service-start --service-name redis
    ```
    
2. 서비스 중지
    
    ```
    redis-server.exe --service-stop --service-name redis
    ```
    
3. 서비스 제거
    
    ```
    redis-server.exe --service-uninstall --service-name redis
    ```
    

### redis.windows-service.conf 설정

```
bind 0.0.0.0
protected-mode yes
port 6383
tcp-backlog 511
timeout 0
tcp-keepalive 5
loglevel notice
logfile "server_log.txt"
syslog-enabled yes
syslog-ident redis
databases 3
always-show-logo yes
#save 900 1
#save 300 10
#save 60 10000
stop-writes-on-bgsave-error yes
rdbcompression yes
rdbchecksum yes
dbfilename dump_imc.rdb
dir ./
replica-serve-stale-data yes
replica-read-only yes
repl-diskless-sync no
repl-diskless-sync-delay 5
repl-disable-tcp-nodelay no
replica-priority 100
lazyfree-lazy-eviction no
lazyfree-lazy-expire no
lazyfree-lazy-server-del no
replica-lazy-flush no
appendonly yes
appendfilename "appendonly_imc.aof"
appendfsync no
no-appendfsync-on-rewrite no
auto-aof-rewrite-percentage 100
auto-aof-rewrite-min-size 512mb
aof-load-truncated yes
aof-use-rdb-preamble yes
lua-time-limit 5000
slowlog-log-slower-than 10000
slowlog-max-len 128
latency-monitor-threshold 0
notify-keyspace-events ""
hash-max-ziplist-entries 512
hash-max-ziplist-value 64
list-max-ziplist-size -2
list-compress-depth 0
set-max-intset-entries 512
zset-max-ziplist-entries 128
zset-max-ziplist-value 64
hll-sparse-max-bytes 3000
stream-node-max-bytes 4096
stream-node-max-entries 100
activerehashing yes
client-output-buffer-limit normal 0 0 0
client-output-buffer-limit replica 256mb 64mb 60
client-output-buffer-limit pubsub 32mb 8mb 60
hz 10
dynamic-hz yes
aof-rewrite-incremental-fsync yes
rdb-save-incremental-fsync yes
maxmemory 10gb
requirepass test #패스워드
```