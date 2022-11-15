# LIST null 체크 (자바)

### 리스트 null 체크
- list == null
- list.size() == 0
- list.isEmpty()
- CollectionUtils.isEmpty(list)

### list == null
- if (list 인스턴스 생성 x) == null : true  
- if (list 인스턴스 생성/ list []) == null : false

### list.size() == 0
- if (list 인스턴스 생성 x) == null : java.lang.NullPointerException
- if (list 인스턴스 생성/ list []).size() == 0 : true

### list.isEmpty() 
- if (list 인스턴스 생성 x) == null : java.lang.NullPointerException
- if (list 인스턴스 생성/ list []).isEmpty() == 0 : true

### CollectionUtils.isEmpty(list)
- null 체크 -> isEmpty() 체크
- if CollectionUtil.isEmpty(list 인스턴스 생성 x) : true
- if CollectionUtil.isEmpty(list 인스턴스 생성/ list []) : true

```java
public abstract class CollectionUtils {
  public static boolean isEmpty(@Nullable Collection<?> collection) {
    return collection == null || collection.isEmpty();
  }    
}
```

- 시간 복잡도 비교 (isEmpty 승)
  - isEmpty() : O(1)
  - size() : O(N)