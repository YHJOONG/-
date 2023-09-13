# String Class 종류
- 문자열을 다루는 클래스, 비슷해 보이지만 차이가 클래스별로 각가 차이가 있음

## String
- String 클래스는 불변(immutable) => 한 번 생성된 문자열은 변경할 수 없음
- 문자열에 대한 수정이 필요한 경우, 새로운 문자열 객체가 생성되고 기존의 문자열은 변경되지 않은 채로 남음
- 멀티스레드 환경에서 안전하게 사용 가능
- 문자열 연결(Concatenation)이나 문자열 비교와 같은 작업에 사용

## StringBuilder
- StringBuilder는 가변적인(mutable) 문자열을 다루는 클래스
- 문자열을 변경할 수 있는 메서드(append, insert, delete, replace 등)를 제공
- StringBuilder는 단일 스레드 환경에서 사용되며, 동기화를 지원하지 않음
- 문자열 연결이나 수정이 자주 필요한 경우에 유용


## StringBuffer
- StringBuffer는 StringBuilder와 비슷하게 가변적인 문자열을 다루는 클래스
- StringBuilder와는 달리 StringBuffer는 동기화를 지원하여 멀티스레드 환경에서 안전하게 사용 가능
- 문자열 연결 및 수정 작업이 필요한 멀티스레드 환경에서 주로 사용
- Java 5부터는 StringBuilder가 추가되어서 동기화가 필요하지 않은 단일 스레드 환경에서는 StringBuilder를 사용하는 것이 성능상 유리


## 성능
- StringBuilder
    - StringBuilder는 동기화를 고려하지 않기 때문에 동기화 관련 오버헤드가 없음
    - 그 결과로 StringBuilder는 단일 스레드 환경에서 StringBuffer보다 더 빠르게 동작
- StringBuffer
    - StringBuffer는 멀티스레드 환경에서 안전하게 동작하기 위해 동기화를 지원
    - 하지만 동기화 관련 처리로 인해 StringBuilder에 비해 성능이 약간 떨어질 수 있음


## 예시
- String
```java
String str = "Hello";  // 불변한 문자열 객체 생성
str = str + " World";  // 새로운 문자열 객체가 생성되고 str은 변경되지 않음
System.out.println(str);  // 출력: Hello World
```

- StringBuilder
```java
StringBuilder sb = new StringBuilder();  // 가변적인 문자열 객체 생성
sb.append("Hello");  // 문자열 추가
sb.append(" World");
String result = sb.toString();  // StringBuilder를 String으로 변환
System.out.println(result);  // 출력: Hello World
```

- StringBuffer
```java
StringBuffer sb = new StringBuffer();  // 가변적인 문자열 객체 생성
sb.append("Hello");  // 문자열 추가
sb.append(" World");
String result = sb.toString();  // StringBuffer를 String으로 변환
System.out.println(result);  // 출력: Hello World
```