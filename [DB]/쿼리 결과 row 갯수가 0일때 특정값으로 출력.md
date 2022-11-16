# 쿼리 결과 row 갯수가 0 일때 특정값으로 출력

### 개요
- 쿼리 결과 row count == 0 일때 mybatis property mapping 되지 않음

### 해결 방안
1. 서비스 -> null 체크 후 빈값으로 넣어줌
2. 쿼리문 자체 : 빈값으로 넣어줌

- 쿼리문 자체 수정 유리 (소스 변경 필요 없음, 더 깔끔)


### 샘플 쿼리 (특정갑 0으로 출력)
```sql
select 
    test  
from
    test_table
union all
select
    '0' as test
from
    dual
```

- UNION ALL -> dummy 테이블 조인해서 사용