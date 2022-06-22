# [Server][Docker] Springboot - Postgresql

1. ssh 환경에서 spring boot default 프로젝트 clone
    
    ```
    git clone https://github.com/YHJOONG/_docker.git
    ```
    

1. dev-docker 프로젝트 구조
    
    ```
    dev-docker
    │
    └ㅡㅡ docker-compose.yml
    |
    └ㅡㅡ Dockerfile
    |
    └ㅡㅡ .env
    |
    └ㅡㅡ app/
    			|
    			└ㅡㅡdemo/ #spring boot 프로젝트
    |
    └ㅡㅡ db/
    			|
    			└ㅡㅡDockerfile-postgres
    			|
    			└ㅡㅡdata/
    			|
    			└ㅡㅡinit.d/
    						└ㅡㅡinit-user-db.sh
    ```
    

1. env 파일
    
    docker-compose.yml에서 사용될 환경 변수 작성
    
    ```
    DB_USER_ID=postgres
    DB_USER_PASSWORD=postgres
    APP_DB_USER=dev
    APP_DB_PASS=postgres
    APP_DB_NAME=dev_db
    DB_BUILD_CONTEXT=./db
    POSTGRES_HOME_DIR=./db
    APP_BUILD_CONTEXT=./app/demo
    ```
    

1. docker-compose.yml
    - dev-docker 디렉토리내에 yml을 구성 → dev-docker_db, dev-docker_app 컨테이너 생성
    - docker-compose.yml과 동일한 경로에 작성된 .env 파일을 읽어 환경변수 처리
        
        → ${.env 환경 변수 명}
        
    - 일반적으로 Image 속성을 작성 → 예시에서는 service 별로 Dockerfile 생성 → Dockerfile 로 설정
    
    ```
    version: "3" # 사용하고자 하는 docker-compose 버전 
    services:
      db:
        container_name: postgres #컨테이너 이름
        restart: always #항상 재시작 -> 명시 되어 있지 않을 경우 no가 defauilt이며 어떤 상황에도 restart 안함
        build: #build할 때 필요한 속성 지정
          context: "${DB_BUILD_CONTEXT}" #Dockerfile이 들어 있는 경로 지정
          dockerfile: Dockerfile-postgres #빌드 시에 사용될 Dockerfile 이름 -> Dockerfile 이름이 아니라 다른 이름으로 생성할 시에 명시 필요
        port:
          - "5432":"5432" #HOST_PORT : Container Port로 host의 5432 포트를 containter의 5432 포트와 매핑
        environment:
          POSTGRES_USER: "${DB_USER_ID}"  #(선택 속성) POSTGRES_USER명으로 superuser가 생성됨(작성하지 않을 경우 postgres 이름으로 superuser 생성)
          POSTGRES_PASSWORD : "${DB_USER_PASSWORD}" #(필수 속성) super user의 password 설정
          APP_DB_NAME: "${APP_DB_NAME}"
          APP_DB_USER: "${APP_DB_USER}"
          APP_DB_PASS: "${APP_DB_PASS}"
        volumes:
          - ${POSTGRES_HOME_DIR}/data:/var/lib/postgresql/data 
             #${POSTGRES_HOME_DIR}/data 부분이 실제 PostgreSQL 저장 경로로 컨테이너 종료 또는 삭제되었을 때 내부 데이터 날림 방지하기 위한 설정
    
      app:
        container_name: demo-backend
        build:
          context: "${APP_BUILD_CONTEXT}"
          dockerfile: Dockerfile-dev
        ports:
          - "8080":"8080"
        environment: # spring boot의 application-*.yml에 주입 필요한 정보
          DB_HOSTNAME : db
          DB_PORT : 5432
          DB_DBNAME : "${APP_DB_NAME}"
          DB_USERNAME : "${APP_DB_USER}"
          DB_PASSWORD : "${APP_DB_PASS}"
        depends_on:
          -db # db가 실행 된 후에 app이 실행되도록 설정
    ```
    

<aside>
💡 Dockerfile 설명
- FROM 명령 부터 시작
- FROM 명령 커맨드는 베이스 이미지를 지정
- build 속성의 context를 작성 하여 Dockerfile 경로 및 파일 설정

</aside>

1. Dockerfile-postgres
    
    PostgreSql을 띄울때, 초기 세팅을 위해 init.d 디렉토리 하위에 init-user.db.sh를 작동시켜야하기 때문
    
    init.d 디렉토리 하위 파일을 docker-entrypoint-initdb.d 디렉토리에 넣도록 COPY 작업을 진행
    
    ```
    FROM postres:12.8
    COPY ./init.d /docker-entrypoint-initdb.d
    ```
    

1. init-user-db.sh
    
    postgreSQL의 superuser가 존재하는지 체크
    
    application용 database 생성 
    
    필요한 extension 설치
    
    ```
    #! /bin/bash
    set -e
    
    echo "START INIT-USER-DB";
    
    psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
      CREATE USER $APP_DB_USER WITH PASSWORD '$APP_DB_PASS';
      CREATE DATABASE $APP_DB_NAME;
      GRANT ALL PRIVILEGES ON DATABASE $APP_DB_NAME TO $APP_DB_USER;
      \connect $APP_DB_NAME $POSTGRES_USER
      BEGIN;
        CREATE EXTENSION CUBE;
        CREATE EXTENSION EARTHDISTANCE;
      COMMIT;
    EOSQL
    ```
    

1. Dockerfile-dev
    
    springboot용 Dockerfile 작성
    
    ```
    FROM adoptopenjdk:8-hotspot
    
    ARG JAR_FILE=build/libs/*.jar # 변수 선언
    
    COPY ${JAR_FILE} app.jar 
    
    ENTRYPOINT ["java", "-jar", "-Dspring.profiles.active=application","/app.jar"]
    ```
    

1. application-dockerdev.yml 작성
    
    dev환경에 사용되는 설정 지정
    
    <aside>
    💡 java -jar -Dspring.profiles.active=application-dockerdev.yml ./app.jar 명령어로 
    profile 선택
    
    </aside>
    
    ```
    spring:
      datasource:
        url: jdbc:postgresql://${db.hostname}:${db.port}/${db.dbname}
        username: ${db.username}
        password: ${db.password}
        driver-class-name: org.postgresql.Driver
    ```
    
2. Docker-Compose 시작
    
    docker-compose.yml 파일이 존재하는 위치에서 실행
    
    ```
    1)
    # docker-compose up 
    
    2)
    # docker-compose up -d 백그라운드 실행
    
    3)
    # docker-compose up --build : 새롭게 빌드하여 실행
    ```