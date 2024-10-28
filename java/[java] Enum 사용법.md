# [JAVA] Enum 사용법

이번 시간에는 Enum 클래스에 올바른 사용법에 대해서 공부해보겠습니다.

1. Enum 클래스 개념
---

- 일반적으로 상수를 정의할 때, public static final String 으로 상수를 정의합니다.

```java
// 일반 상수 방식
public static final String PROCEEDING = "진행중";
public static final String COMPLETE = "진행완료";

// Enum 클래스 방식
enum Status {
    PROCEEDING, COMPLETE;
}
```

- 일반 상수 방식은 기존 상수를 정의하는 방식으로 정의하였습니다.
- 상수가 무수히 많고 복잡한 시스템의 경우, 런타임 에러가 발생할 수 있고, 프로그래밍의 치명적인 오작동을 일으킬수 있습니다. 
- Enum 클래스 방식은 다음과 같이 풀어 해석 할 수 있습니다. 

```java
class Status {
    public static final Status PROCEEDING = new Status();
    public static final Status COMPLETE = new Status();
}
```


[Enum 클래스의 장점]
1. 코드가 단순해지며, 가독성이 좋아진다.
2. 인스턴스 상곡과 생성을 방지하여 컴파일 시에 상수값의 타입 안정성이 보장됩니다.
3. Enum 키워드를 통해 구현의 의도가 열거임을 분명하게 알 수 있습니다. 



2. Enum 클래스 사용 고급
---
[기존의 코드 관리 방법]

과거 상수와 관련된 코드들을 별도의 테이블과 클래스로 관리한다고 가정합니다. 

```java
@Entity
@Table("codeVO")
@Getter
@NoArgsConstructor
public class CodeVO extends CommonVO implements Serializable {

    private String codeSn;					// 코드일련번호
    @Setter
    private String lvl1;					// 레벨1코드
    @Setter
    private String lvl2;					// 레벨2코드
    private String codeName;				// 코드한글명
    private String codeDesc;				// 코드설명
    @Setter
    private String selType;					// 조회유형

    @Builder
    public CodeVO(String lvl1, String selType){
        this.lvl1 = lvl1;
        this.selType = selType;
    }

}
// 출처: https://mangkyu.tistory.com/74 [MangKyu's Diary:티스토리]
```

```java
public class BoardVO extends CodeVO implements Serializable {
    private String title;					// 제목
    private String contents;				// 내용
    private String status;					// 상태
}

@RestController
@RequestMapping("/board")
@Log4j2
public class BoardController {

    @Resource(name = "boardService")
    private BoardService boardService;

    @GetMapping(value = "/list")
    public ResponseEntity<List<BoardVO>> list() {
        BoardVO boardVO = new BoardVO();
        boardVO.setLvl1("001");
        boardVO.setLvl2("002");
        List<BoardVO> boardList = boardService.findAll(boardVO);
        return new ResponseEntity.ok(boardList);
    }
}
// 출처: https://mangkyu.tistory.com/74 [MangKyu's Diary:티스토리]
```

이러한 경우 게시물의 Code Level1 이 어떤값인지 계속 확인해주어야 합니다. 

[Enum 을 활용한 코드 리팩토링]

일반적으로 Enum과 같이 열거형이 필요한 타입들의 경우 서버에서 데이터를 제공합니다. Enum 데이터들이 공통으로 갖는 이름(Code), 
설명(Title)이 있다고 가정합시다!

그리고 이러한 내용을 담는 인터페이스를 아래와 같이 구성해봅시다
```java
public interface EnumMapperType {
    // 해당 Enum의 이름을 조회하는 변수
    String getCode();
    
    // 해당 Enum의 설명을 조회하는 변수
    String getTitle();
}
```

Status Enum 클래스를 기준으로 설명하면 PROCEEDING 과 COMPLETE이 code에 해당되고 getCode()를 통해 조회가능합니다. 
'진행중', '진행완료'는 title에 해당합니다. 
기존의 status를 EnumMapperType를 implements 하여 재작성하면 다음과 같습니다. 
```java
@RequiredArgsConstructor
public enum Status implements EnumMapperType {
    PROCEEDING("진행중"),
    COMPLETE("진행완료");
    
    @Getter
    private final String title;
    
    @Override
    public String getCode(){
        return name();
    }
}
```

- Enum 변수들은 불변이기 때문에 final 키워드를 붙여줄수 있고 RequiredArgsConstructor 어노테이션을 활용하면 용이합니다.

- Enum 종류들을 관리하기 위한 EnumMapperFactory()를 생성합니다.
```java
@Getter
@AllArgsConstructor
public class EnumMapperFactory {
	// 다양한 종류의 Enum을 생성 및 관리하는 factory
    private Map<String, List<EnumMapperValue>> factory;

    // 새로운 Enum 종류를 추가하는 함수
    public void put(String key, Class<? extends EnumMapperType> e) {
        factory.put(key, toEnumValues(e));
    }

    // 특정 Enum의 항목들을 조회하는 함수
    public List<EnumMapperValue> get(String key) {
        return factory.get(key);
    }

    // Enum의 내용들을 List로 바꾸어주는 함수
    private List<EnumMapperValue> toEnumValues(Class<? extends EnumMapperType> e) {
        return Arrays.stream(e.getEnumConstants()).map(EnumMapperValue::new)
                .collect(Collectors.toList());
    }

}
```

EnumMapperType을 implement한 구현체에 대해 실제 값을 갖는 EnumMapperValue는 아래와 같습니다.
```java
@Getter
public class EnumMapperValue {
    private String code;
    private String title;
    
    public EnumMapperValue(EnumMapperType enumMapperType) {
        code = enumMapperType.getCode();
        title = enumMapperType.getTitle();
    }
}
```

이렇게 생성한 Enum status를 아래와 같이 Factory에 등록하여 활용합니다.

```java
@Configuration
public class EnumMapper {

    @Bean
    public EnumMapperFactory createEnumMapperFactory() {
        EnumMapperFactory enumMapperFactory = new EnumMapperFactory(new LinkedHashMap<>());
        enumMapperFactory.put("Status", Status.class);
        return enumMapperFactory;
    }
}
```

Controller 에서 호출 방식입니다.

```java
@ReqruiedArgsConstructor
@RestController
public class EnumsController {

    private final EnumMapperFactory enumMapperFactory;

    @GetMapping("/status")
    public ResponseEntity status(){
        return ResponseEntity.ok(enumMapperFactory.get("Status"));
    }
}
```




==========================================================================
위의 Enum을 활용한 코드 리팩토링의 대부분은 우아한 형제들의 글에서부터 참고하여 작성한 것입니다!
