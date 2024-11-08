# [JAVA] 람다를 사용할 때 장단점?

1. 일급 객체 개념?

- 일급이란 뜻은 사용할 때 다른 요소들과 아무런 차별이 없다는 것을 뜻합니다.

일급 객체의 3가지 조건

1. 모든 일급 객체는 변수나 데이터에 담을 수 있어야 합니다.
2. 모든 일급 객체는 함수의 파라미터로 전달 할 수 있어야 합니다.
3. 모든 일급 객체는 함수의 리턴값으로 사용 할 수 있어야 합니다.

> Javascript, Python : 일급 객체 언어
> c, c++ : 일급 객체 언어 x (Java는 람다로 지원)


[자바의 메소드와 자바스크립트 함수의 일급 객체]
자바의 메소드도 결국 함수인데 일급 객체라고 부르지 않는것인지? 자바스크립트 함수는 왜 일급 객체인지 확인해보자!

1. 변수나 데이터에 담을 수 있어야 합니다.

- 자바의 메소드는 변수에 할당 할 수 없습니다

```java
public class Main {

    public static void hello() {
        System.out.println("hello");
    }

    public static void main(String[] args) {
        Object a = hello(); // 메서드를 변수에 할당 불가능
    }
}
```

```javascript
const hello = function () {
  console.log("hello");
}
```

2. 함수의 파라미터로 전달 할 수 있어야 합니다.

- 자바의 메소드를 입력값으로 보내는 행위는 불가능하고, 자바스크립트는 콜백 함수 형태로 전달이 가능

```java
public class Main {

    public static void hello() {
        System.out.println("hello");
    }

    public static void print(Object func) {
        func();
    }

    public static void main(String[] args) {
        print((Object) hello); // hello 메서드를 함수 매개 변수로 전달 불가능
    }
}
```

```javascript
const hello = funciton()
{
  console.log("hello");
}

function print(func) {
  func();
}

print(hello);
```

3. 함수의 리턴값으로 사용할 수 있어야 합니다.

```javascript
const hello = function () {
  console.log("hello");

  return function () {
    conosle.log("hello 2");
  }
}

const hello2 = hello();
hello2();
```

[자바의 람다 함수의 일급 객체]

1. 변수나 데이터에 담을 수 있어야 합니다.

```java
public class Main {

    public static void main(String[] args) {
        Consumer<String> c = (t) -> System.out.println(t);
        c.accept("hello world");
    }
}
```

2. 함수의 파라미터로 전달 할 수 있어야 합니다.

```java
public class Main {

    public static void print(Consumer<String> c, String str) {
        c.accept(str);
    }

    public static void main(String[] args) {
        print((t) -> System.out.println(t), "hello world");
    }
}
```

3. 함수의 리턴값으로 사용 할 수 있어야 합니다.

```java
public class Main {

    public static Consumer<String> hello() {
        // 람다 함수 자체를 리턴함
        return (t) -> {
            System.out.println(t);
        };
    }

    public static void main(String[] args) {
        Consumer<String> c = hello();
        c.accept("hello world");
    }
}
```

[람다식 사용 예시]

1. 함수형 프로그래밍 지원
- 기존 정렬 방식

```java
import java.util.ArrayList;
import java.util.Comparator;

public static void main(String[] args) {
    List<String> fruits = List.of("banana", "apple", "cherry");
    List<String> sortedData = new ArrayList<>(fruits);

    sortedData.sort(new Comparator<String>() {
        @Override
        public int compare(String s1, String s2) {
            return s1.compareTo(s2);
        }
    });

    System.out.println(sortedData);
}
```

- 람다 표현식

```java
public static void main(String[] args) {
    List<String> fruits = List.of("banana", "apple", "cherry");
    List<String> sortedData = fruits.stream().sorted(Stirng::compareTo).toList();
    System.out.println(sortedData);
}
```

2. 코드의 재사용성 (람다식 + 함수형 인터페이스를 활용)
- 기존 코드 방식
```java
public String callApi1() {
    try{
        ResponseEntity<String> response = restTemplate.getForEntity(
                "https://localhost:8081/api1", String.class
        );
        
        return response.getBody;
    }catch (RestClientException ex) { // 중복 처리
        throw new RuntimeException("외부 통신 중 예외 처리");
    } catch (Exception e) {
        throw new RuntimeException("내부 예외 처리");
    }
}

public String callApi2() {
    try{
        ResponseEntity<String> response = restTemplate.getForEntity(
                "https://localhost:8081/api2", String.class
        );

        return response.getBody;
    }catch (RestClientException ex) { // 중복 처리
        throw new RuntimeException("외부 통신 중 예외 처리");
    } catch (Exception e) {
        throw new RuntimeException("내부 예외 처리");
    }
}

```

- 람다 표현식
```java
private <T>T executeWithExceptionHandling(Supplier<T> apiCall) {
    try {
        return apiCall.get();
    }catch (RestClientException ex) { // 중복 처리
        throw new RuntimeException("외부 통신 중 예외 처리");
    } catch (Exception e) {
        throw new RuntimeException("내부 예외 처리");
    }
}

public String callApi1() {
    return executeWithExceptionHandling(() -> {
        ResponseEntity<String> response = restTemplate.getForEntity(
                "https://localhost:8081/api1", String.class
        );
        
        return response.getBody();
    });
}

public String callApi2() {
    return executeWithExceptionHandling(() -> {
        ResponseEntity<String> response = restTemplate.getForEntity(
                "https://localhost:8081/api2", String.class
        );

        return response.getBody();
    });
}
```

[람다를 사용할 때 장점]
1. 간결하고 읽기 쉬운 코드
2. 함수형 프로그래밍 지원
3. 코드이 재사용성
4. 병렬 처리 용이


[람다를 사용할 때 단점]
1. 가독성 저하
2. 디버깅의 어려움
3. 성능저하 (적은 횟수를 간단하게 반복할때는 for가 성능이 좋지만, 사실 실제 서비스 상에서는 미미한 차이이므로 상황에 맞게 사용하는게 좋다고 생각이 듭니다.)
