# [Server][MariaDB] 계정 생성 관리 및 권한 설정

```
mysql -u root -p 를 입력하여 mysql 접속

use myql; 입력

show tables; //mysql 스키마에 테이블 구성 확인

select host, user, password from user; //계정정보 조회
```

계정 추가

```
create user '계정아이디'@localhost identified by '비밀번호';   //로컬에서만 접근 가능

create user '계정아이디'@'%' identified by '비밀번호';   //외부에서 모든 아이피 허용

```

계정 삭제

```
delete from user where user='계정아이디';
```

권한 주기

```
grant all privileges on '스키마명'.'테이블명' to '계정명'@'호스트' identified by '계정비밀번호' with grant option;

flush privileges;
```

권한 제거

```
revoke all on '스키마명'.'테이블명' from '계정명'@'호스트';
```