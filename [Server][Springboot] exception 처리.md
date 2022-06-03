# [Server][Springboot] exception 처리

# Bean Validataion

---

- Bean Validation 클래스는 필드에 특정 annotaion을 적용하여 필드가 갖는 제약 조건을 정의하는 구조입니다.

<aside>
💡 @Valid, @Validated 차이
@Valid는 Java에서 지원해주는 어노테이션, @Validated는 Spring에서 지원해주는 어노테이션입니다.

</aside>

# Spring boot Validation 적용

---

### 의존성 추가 합니다.

- Gradle 의존성 추가
    
    ```
    implementation group: 'org.springframework.boot', name: 'spring-boot-starter-validation', version: '2.5.6'
    ```
    
- Maven 의존성 추가
    
    ```
    **<dependency>
      <groupId>org.springframework.boot</groupId>
      <artifactId>spring-boot-starter-validation</artifactId>
    </dependency>**
    ```
    

### Controller에서 유효성 검사를 위해 Request 객체 앞에 @Validated 어노테이션을 추가합니다.

- Controller.java
    
    ```java
    @RequestMapping(value ="/v1/user/info", method = RequestMethod.POST)
    public ResponseEntity<ResMsg> UserInfo(HttpServletRequest request, @RequestBody @Validated User body){
    		ResMsg resMsg = new ResMsg();
    		return new ResponseEntity<RedMsg>(resMeg, HttpStatus.OK);
    }
    ```
    

### Request를 핸들링할 객체를 정의할때 Validation 어노테이션을 통해 필요한 유효성을 검사합니다.

- User.java
    
    ```java
    import javax.validation.constraints.Email;
    import javax.validation.constraints.NotNull;
    
    import lombok.Getter;
    import lombok.Setter;
    import lombok.ToString;
    
    @Getter
    @Setter
    @ToString
    public class User{
    		@NotNull
        private String symbol;
        
        @NotNull
        private String apiKey;
    
        @NotNull
        private String timestamp;
    
        @NotNull
        private String version;
        
        @NotNull
        private String signature;
    
        @Email
        @NotNull
        private String email;
    }
    ```
    

### Request를 핸들링 후 유효하지 않은 Paramter에 대한 Exception 처리

- @ExceptionHandler 어노테이션을 이용해 예외를 처리

```java
@ControllerAdvice   // 전역 설정을 위한 annotaion
@RestController

public class ExceptionAdvisor{
	@ExceptionHandler(MethodArgumentNotValidException.class)
	public ResponseEntity<ResMsg> processValidationError(MethodArgumentNotValidException exception) {
		ResMsg resMsg = new ResMsg();
		try {
	            String msg  = "Invalid data sent for a parameter.";
	            resMsg = ResMsg.builder()
	                                .code(1)
	                                .status("error")
	                                .message(msg).build();
	            return new ResponseEntity<ResMsg>(resMsg, HttpStatus.BAD_REQUEST);
      } catch (Exception e) {
          //TODO: handle exception
          System.out.println(e.getMessage());
          resMsg = ResMsg.builder()
                          .code(2)
                          .status("error")
                          .message("Internal server error.").build();
          return new ResponseEntity<ResMsg>(resMsg, HttpStatus.INTERNAL_SERVER_ERROR);
      }
	}
}
```

```
<dependency>

<groupId>com.github.vladimir-bukhtoyarov</groupId>

<artifactId>bucket4j-core</artifactId>

<version>4.0.1</version>

</dependency>
```