# Insert 후에 PK 값 얻어오기

- 테이블이 2개가 있음 (A, B)
- A의 PK = B의 FK로 참조
- A 테이블 Row 추가 -> B 테이블 Row 추가

### 예제
```
mysql> USE test;
Database changed
mysql> CREATE TABLE t (
    ->   id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
    ->   name VARCHAR(10) NOT NULL
    -> );
Query OK, 0 rows affected (0.09 sec)

mysql> INSERT INTO t VALUES (NULL, 'Bob');
Query OK, 1 row affected (0.01 sec)

mysql> SELECT * FROM t;
+----+------+
| id | name |
+----+------+
|  1 | Bob  |
+----+------+
1 row in set (0.01 sec)

mysql> SELECT LAST_INSERT_ID();
+------------------+
| LAST_INSERT_ID() |
+------------------+
|                1 |
+------------------+
1 row in set (0.00 sec)

mysql> INSERT INTO t VALUES
    -> (NULL, 'Mary'), (NULL, 'Jane'), (NULL, 'Lisa');
Query OK, 3 rows affected (0.00 sec)
Records: 3  Duplicates: 0  Warnings: 0

mysql> SELECT * FROM t;
+----+------+
| id | name |
+----+------+
|  1 | Bob  |
|  2 | Mary |
|  3 | Jane |
|  4 | Lisa |
+----+------+
4 rows in set (0.01 sec)

mysql> SELECT LAST_INSERT_ID();
+------------------+
| LAST_INSERT_ID() |
+------------------+
|                2 |
+------------------+
1 row in set (0.00 sec)
```

1. Insert Bob 추가
2. LAST_INSERT_ID()  == 1 (auto_increment column)
3. INSERT 구문으로 3개 row 추가
4. LAST_INSERT_ID() == 2 


### LAST_INSERT_ID 특징
- connection 마다 관리
  - A / B 다른 connection 가지고 동시에 Insert
  - Last_INSERT_ID() select -> 자신이 INSERT 한 ID값 반환
- keyProperty / resultClass 사용 가능

### 사용법
```
<insert id="user.insertUserAndGetId" parameterClass="user">
    <![CDATA[
        INSERT INTO t
        (
            name
        )
        VALUES
        (
            #name#
        )
    ]]>
    <selectKey keyProperty="id" resultClass="Integer">
        SELECT LAST_INSERT_ID()
    </selectKey>
</insert>
```