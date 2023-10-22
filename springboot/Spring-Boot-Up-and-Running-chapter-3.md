# 첫 번째 REST API

- 모놀리식 애플리케이션 -> 마이크로 서비스를 많이 사용
- 마이크로 서비스에서 가장 중요한건 통신임

### RREST가 무엇이며, 왜 중요할까요?
- REST란?
  - 애플리케이션 A가 애플리케이션 B 와 통신할 때, A는 B에서 통신 시점의 현재 상태를 가져옴
  - A는 B가 통신 호출간 상태를 유지하리라고 가정하지 않음
  - A는 B에 요청할 때 마다 관련 상태의 표현을 제공 -> 통신 문제가 발생하거나, B가 재시작해도 A와 상호작용한 '현재 상태'를 잃어버리지 않음
  - A가 다시 요청해 두 애플리케이션이 중단된 곳에서 통신을 이어감

### API, HTTP 메서드 스타일
```java
@springBootApplication
public class restDemoApplication{
    public static void main(String[] args){
        SpringApplication.run(restDemoApplication.class, args);
    }
}

class Coffee{
    private final String id; // 한번만 할당하고 절대 수정할 수 없게 수정
    private String name;
    
    // 매개변수를 모두 사용하는 생성자
    public Coffee(String id, String name){
        this.id = id;
        this.name = name;
    }
    
    // Coffee 인스턴스 생성 시 id 매개 변수 입력을 하지 않으면 고유 식별자 id 값을 기본으로 제공
    public Coffee(String name){
        this(UUID.randomUUID().toString(), name);
    }
    
    public String getId(){
        return id;
    }
    
    public String getName(){
        return name;
    }
    
    public void setName(String name){
        this.name = name;
    }
}``
```

- @RestController 개요
  - @Controller : 스프링 MVC는 뷰가 서버 렌더링된 웹페이지로 제공된다는 가정하에, 데이터와 데이터를 전송하는 부분과 데이터를 표현하는 부분을 분리에 도움
    - @Component의 스테레오타입
    - Model 객체를 받음
    - Model 객체로 표현 계층에 모델 기반 데이터를 제공
    - @ResponseBody를 클래스나 메서드에 추가해서 Controller 클래스에 기시할 수 있음(JSON/XML 같은 형식화된 응답을 반환)
  - @Contorller + @Responsebody를 하나의 어노테이션으로 합쳐 쓴것



### 참고
* [도서 - 처음부터 제대로 배우는 스프링부트](https://product.kyobobook.co.kr/detail/S000201866534)
