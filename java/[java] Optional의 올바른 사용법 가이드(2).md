# [java] Optional의 올바른 사용법 가이드(2)

1. Optional의 올바른 사용법 가이드
---

[올바른 Optional 사용법 가이드]

<u> Optional 변수에 Null을 할당하지 말아라</u>

- Optional은 박싱 클래스일 뿐 Optional 변수에 null을 할당하는것 자체는 추천하지 않는 방법입니다. 
- 값이 없는 경우 Optional.empty()로 초기화 합시다.

```java
// AVOID
public Optional<Cart> fetchCart() {

    Optional<Cart> emptyCart = null;
    ...
}
```

<u> 값이 없을 때 Optional.orElseGet() / orElse() 함수로 기본 값을 반환해라</u>

Optional의 장점 중 하나는 함수형 인터페이스를 통해 가독성이 좋고 유연한 코드를 작성할 수 있다는 점입니다. 
isPresent()와 get() 함수를 사용하는 것 보다는 orElse 의 함수를 활용해서 처리하면 위의 장점을 살릴 수 있을 것입니다.
```java
private String findDefaultName() {
    return "test";
}

// AVOID
public String findUserName(long id) {

    Optional<String> optionalName = ... ;

    if (optionalName.isPresent()) {
        return optionalName.get();
    } else {
        return findDefaultName();
    }
}

// PREFER
public String findUserName(long id) {
    Optional<String> optionalName = ... ;
    return optionalName.orElseGet(this::findDefaultName);
}
```

- orElse 는 값이 준비되어 있는 경우
- orElseGet 은 값이 준비되어 있지 않은 경우 

<u> 단순히 값을 얻는 목적으로는 사용하지 말아라 </u>

- 단순히 값을 얻으려고 사용하는 경우는 Optional을 남용하는 대표적인 경우입니다. 이러한 경우 직접 값을 다루는게 성능상 유리합니다.

```java

// AVOID
public String findUserName(long id) {
    String name = "test";

    return Optional.ofNullable(name).orElse("Default");
}

public String findUserName(long id) {
    String name = "test";
    
    return name == null 
            ? "Default"
            : name;
}
```

<u> 생성자, 수정자, 메소드 파라미터 등으로 Optional을 넘기지 마라</u>

- 파라미터로 Optional 을 넘기는 행동은 의미가 없습니다. 넘겨온 파라미터를 위해 다시 null 체크를 해야되고, 코드도 복잡해지는 등 번거러워 집니다. 
- Optional은 반환 타입으로 대체 동작을 사용하기 위해 고안된 것임을 명심해야 합니다. 
- 앞서 설명한대로 Serializable을 구현하지 않으므로 필드 값으로 사용하지 않아야 됩니다. 

```java
// AVOID
public class User {

    private final String name;
    private final Optional<String> postcode;

    public Customer(String name, Optional<String> postcode) {
        this.name = Objects.requireNonNull(name, () -> "Cannot be null");
        this.postcode = postcode;
    }

    public Optional<String> getName() {
        return Optional.ofNullable(name);
    }
    
    public Optional<String> getPostcode() {
        return postcode;
    }
}
```

- 접근자로 사용하고 있는 경우도 마찬가지 입니다. name을 얻기 위해 Optional.ofNullable()로 반환하고 있는데, Getter에 Optional 을 얹어 반환하는 것은 남용하는 코드입니다. 


<u> 반환 타입으로 사용하라 </u>

- Optional은 반환 타입으로서 에러가 발생할 수 있는 경우에 결과 없음을 명확히 드러내기 위해 만들어졌습니다. 
- Stream API와 결합되어 유연한 체이닝 api를 만들기 위해 탄생한 것으로 정의할 수 있습니다. 
- 언어를 사용하는 사람의 입장에서는 Null을 반환하는 것보다는 값의 유무를 나타내는 객체를 반환하는 것이 합리적일 것으로 생각이 됩니다.
- 이러한 고민 끝에 설계된 것이 Optional이라고 해요.
- 설계된 목적에 맞게 반환 타입으로만 사용하는것을 추천드립니다. 


```java
// Spring JPA 예시
public interface CrudRepository<T, ID> extends Repository<T, ID> {
    Optional<T> findById(ID id);
}

public void getUserById(Long userId) {
    Optional<User> userOptional = userRepository.findById(userId);
    userOptional.orElseGet(this::getPrint(userId));
}

private void getPrint(Long userId) {
    System.out.println(userId);
}
```

