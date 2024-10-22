# [JAVA] Optional이란?

1. Optional 개념
---

[NPE(NullPointException)]
- 개발을 할때 가장 많이 발생하는 예외 중 하나가 바로 NullPointException이에요.
- 이러한 예외를 피하려면 null 여부를 검사해야되는데, null검사 로직이 들어갈수록 코드가 복잡해지고 번거로워집니다 ㅠㅠ

아래 소스는 names가 null이면 NPE가 발생하는 소스입니다.

```java
List<String> names = getNamse();
names.sort();
```

다음 아래와 같이 null 검사를 하는 로직을 추가해서 NPE를 피할 수 있습니다.

```java
List<String> names = getNames();

// null 검사
if(names != null){
    names.sort();
}
```


[Optional이란?]
- Java 8에서는 NPE를 방지 할 수 있도록 도입된 클래스가 있습니다. 그건 바로 Optional<T> 클래스 입니다.
- Optional<T> 클래스는 null이 올 수 있는 값을 감싸는 Wrapper 클래스로, 바로 값을 참조하더라도 NPE가 발생하지 않아요.
- Wrapper 클래스는 간단히 설명하자면, Optional 클래스가 실제 값을 포장하여, 감싸는 객체 역할을 한다는 의미에요. 값이 있을 수도 없을수도 있어서, 안전하게 다루기 위해 값을 감싸는 클래스로 이해하시면 됩니다.


```java
public final class Optional<T> {
    
    private final T value;
}
```


2. Optional 활용하기
---

[Optional 생성하기]

<u>Optional.empty() - 값이 null 인 경우</u>

```java
Optional<String> optional = Optional.empty();

System.out.println(optional); // Optional.empty
System.out.println(optional.isPresent()); // false
```

Optional 클래스 내부에서 static 변수로 EMPTY 객체를 가지고 있습니다. 빈 객체를 여러개 생성해줘야 하는 경우, 1개의 Empty 객체를 공유함으로서 메모리르 절약할 수 있습니다.

```java
public final class Optional<T> {
    public static<T> Optional<T> empty() {
        Optional<T> t = (Optional<T>) EMPTY;
        return t;
    }
    private final T value;
}
```

<u>Optional.of() - 값이 null 이 아닌 경우</u>

```java
Optional<String> optional = Optional.of("test");
```

데이터가 값이 절대 null이 아니라면 Optional.of() 함수를 통해 생성할 수 있습니다.

```java
public final class Optional<T> {
    public static <T> Optional<T> of(T value) {
        return new Optional<>(value);
    }

    private final T value;

}
```


<u>Optional.ofNullable() - 값이 null 일 수도, 아닐수도 있는 경우</u>

데이터가 null 일수도 있고, 아닐 수도 있는 경우에는 Optional.ofNullable로 생성 할 수 있습니다. 그 이후에 값을 가져올 때, orElse() 또는 orElseGet()를 이용해서
값이 없을 경우 안전하게 값을 가져올 수 있습니다.

```java
Optional<String> optional = Optional.ofNullable(getName());
String name = optional.orElse("anonymous"); // 값이 없을 경우, anonymous 를 리턴
```



[Optional 정리]
- Optional 클래스는 값을 감싸서 NPE로부터 부담을 줄이기 위해 JAVA8부터 등장한 래퍼 클래스입니다.
- Optional 클래스는 값을 Wrapping 하고 다시 풀고, null일 경우에는 다른 함수를 호출하고 등 오버헤드가 존재하므로 시스템 성능이 저하될수도 있습니다.
- 그렇기 때문에 메소드의 반환 값이 절대 null이 아니라면 사용하지 않는것을 권장합니다.
- 즉, 메소드의 결과가 null이 될 수도 있고 NPE가 발생할 가능성이 높은 경우 반환값을 Optional 클래스로 사용되어야 안전한 코드를 작성 할 수 있습니다.
