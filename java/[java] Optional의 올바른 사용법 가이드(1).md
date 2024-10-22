# [java] Optional의 올바른 사용법 가이드(1)

1. Optional의 orElse와 orElseGet 차이
---
<u>orElse와 orElseGet의 차이는?</u>

Optional API의 단말연산에는 orElse와 orElseGet 함수가 있습니다. 두 함수는 비슷해 보이지만 엄청난 차이가 있어요.

- orElse : 파라미터로 값을 받는다.
- orElseGet : 파라미터로 함수형 인터페이스를 받는다.

orElse는 값이, orElseGet은 함수가, 이 차이는 상당한 차이가 있습니다. 이로 인해 호출 결과가 달라질수도 있기 때문이에요.

먼저 Optional 클래스에 선언된 두 함수를 보시죠
```java
public final class Optional<T> {

    ... // 생략

    public T orElse(T other) {
        return value != null ? value : other;
    }

    public T orElseGet(Supplier<? extends T> other) {
        return value != null ? value : other.get();
    }
    
}
```

orElse와 orElseGet의 차이 예시 코드
```java
public void findUserEmailOrElse() {
    String mail = "Empty";
    String result = Optional.ofNullable(mail)
            .orElse(getUserEmail());
    
    System.out.println(result);
}

public void findUserEmailOrElseGet() {
    String mail = "Empty";
    String result = Optional.ofNullable(mail)
            .orElseGet(this::getUserEmail);
    
    System.out.println(result);
}

public String getUserEmail(){
    System.out.println("getUserEmail() Called");
    return "test@naver.com";
}
```

출력 결과
```
// 1. orElse인 경우
getUserEmail() Called
Empty

// 2. orElseGet인 경우
Empty
```

- orElse 인 경우
    - Optional.ofNullable로 "Empty" 값을 갖는 Optional 객체 생성
    - getUserEmail() 함수가 실행되어 반환값을 ofElse 파라미터 값으로 전달
    - orElse 함수가 호출 -> "Empty" 가 null이 아니므로, Empty 값을 그대로 가짐

- orElseGet 인 경우
    - Optional.ofNullable로 "Empty" 값을 갖는 Optional 객체 생성
    - getUserEmail() 함수 자체를 orElseGet 파라미터로 전달
    - orElseGet 함수 호출 -> "Empty"가 null이 아니므로 Empty 값을 그대로 가지며, getUserEmail()함수는 호출되지 않음

<u>orElse에 의한 발생가능한 장애 예시</u>

위에서 살펴보았듯 orElse와 orElseGet 함수는 중요한 차이가 있는데, 이를 제대로 인식하지 못하면 장애가 발생될 수 있습니다.
다음 아래 코드를 통해 발생가능한 장애 예시를 확인해봅시다.

```java
public void findByUserEmail(String userEmail) {
    // orElse에 의해 userEmail이 이미 존재해도 유저 생성 함수가 호출되어 에러 발생
    return userRepository.findByUserEmail(userEmail)
            .orElse(createUserWithEmail(userEmail));
}

private String createUserWithEmail(String userEmail) {
    User newUser = new User(userEmail);
    return userRepository.save(newUser);
}
```

orElse를 사용하고 있기 때문에 유저 정보를 조회 결과와 무관하게, createUserWithEmail 함수가 반드시 실행됩니다. 하지만 userEmail는 DB에서 유니크 값을 가진다고
설정되어 있어 오류가 발생할 것입니다.

다음과 같이 수정해야 오류가 발생하지 않을 것입니다.

```java
public void findByUserEmail(String userEmail) {
    // orElseGet에 의해 파라미터로 함수를 넘겨주므로 Null이 아니면 유저 생성 함수가 호출되지 않음
    return userRepository.findByUserEmail(userEmail)
           .orElseGet(this::createUserWithEmail(userEmail));
}

private String createUserWithEmail(String userEmail) {
    User newUser = new User(userEmail);
    return userRepository.save(newUser);
}
```

orElseGet 함수를 사용해서 코드를 작성하면 실제 유저가 조회 정보가 없을 경우에만 createUserWithEmail 함수가 호출되어 사용자를 생성하는 로직이 동작되게 될 것입니다.
위와 같은 상황이 실제로 발생되면 시스템에 큰 장애가 발생됩니다. orElse는 값을 생성하여 orElseGet보다 비용기 크므로 최대한 사용을 피해야합니다.

그러므로 orElse와 orElseGet의 차이점을 정확히 이해하고 사용해야됩니다.  
