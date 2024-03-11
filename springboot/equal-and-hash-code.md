## Equal and HashCode Method

#### 객체 비교 Equals() 와 == 

- 동등성 : 객체 주소값이 다르더라도 내용(필드 값)이 같으면 같다고 보는것 (equals)
- 동일성 : 객체 주소값이 다르면 내용(필드 값이)이 같아도 같지 않다고 보는것 (==)

- VO 비교 시, 변수 내용(필드)이 같으면 되기 때문에(동등성을 위해) equals 기능을 재 정의해준다

- String 타입의 변수 비교
```java
String s1 = "Hello";
String s2 = "Hello";

System.out.println(s1 == s2);       // false (주소 비교)
System.out.println(s1.equals(s2));  // true (값 비교)
```

- 객체 타입의 변수 비교
```java
class Person {
    String name;

    public Person(String name) {
        this.name = name;
    }

public class Example {
    public static void main(String[] args) {
        Person person1 = new Person("홍길동");
        Person person2 = new Person("홍길동");

        System.out.println(person1 == person2); // == 은 객체타입인경우 주소값을 비교한다. 서로다른 객체는 다른 주소를 가지고 있기 때문에 false가 출력됨

        System.out.println(person1.equals(person2)); // equals 또한 객체타입인경우 주소값을 비교하기 때문에 false가 출력된다.
    }
}
```

- 최상위 클래스 Object 클래스의 equals 형태
  - 원래 equals 매소드는 값, 주소값 모두 비교하는 기능
    ```java
    public boolean equals(Object o){
        return (this == o);
    }
    ```

- 요즘 많이 쓰는 equals는 String에서 재 정의 된것
  - 두 VO의 객체의 논리적으로 동등 하면 true리턴 그렇지 않으면 false를 리턴
  - 논리적 동등 == 객체 내부 value값 동일
  - String class 
    - equals() : Object equals() 메소드를 재 정의하여 주소값 비교가 아닌 문자열 '값'만을 비교
    - == : 주소값을 비교
    
```java
public class Student {

    private String name;
    private int age;

    public Student(String name, int age) {
        this.name = name;
        this.age = age;
    }

    @Override
    public boolean equals(Object obj) {
        // return super.equals(obj); 기존 Object클래스 구현 내용
        if (this == obj) return true; //같은 객체를 참조하여 참조값이 같은경우 true를 바로 리턴해준다.
        if (obj == null || getClass() != obj.getClass()) return false; //비교하는 객체가 null인지 클래스가 같은지 체크한다.
        Student student = (Student) obj;
        return age == student.age &&
                Objects.equals(name, student.name); //객체 내부의 값들을 비교하여 리턴한다.
    }
}
```

### HashCode
- 객체를 식별하는 하나의 고유 정숫값
- 객체의 메모리 번지를 이용하여 hashcode을 리턴하여 객체 마다 고유의 번호가 부여됨
```java
    @Override
    public int hashCode() {
    //  return super.hashCode();  기존 Object클래스 구현 내용
        return Objects.hash(name, age);
    }
```