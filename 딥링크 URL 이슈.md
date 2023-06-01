# 딥링크 URL 이슈 정리
- 카카오 딥링크 개발 중 딥링크 접근 시 동작하던 패널이 뜨지 않는 이슈가 발생

## 원인 분석
- 파라미터를 암호화 해서 넘기는데 +가 있는 값들만 패널이 뜨지 않는 것을 확인함
- 검색 결과 + 기호는 URL로 넘겼을때 공백으로 인식하고 이를 URL 인코딩을 해야됨을 찾음
- 암호화 값에 + 기호 말고 여러 기호가 있는데 URLEncoder.encode를 통해 인코딩을 할지 replace("+", "%2B") 로 할지 가능

## 수정 내용
- replace를 하는것 보다 URL 인코딩을 사용하여 파라미터 값을 안전하게 전달하는 것이 좋다고 생각이 되어 URLEncoder.encode 함수를 사용
- 백엔드(Spring boot) : URLEncoder.encode('암호화값', StandardCharsets.UTF_8) 함수를 통해 URL 인코딩 적용
- 프론트엔드(Vue.js) : decodeURIComponent(인코딩된 값)  함수를 통해 URL 디코딩 적용

## 소스
```java
// 딥링크 파라미터 암호화 변수
String encryptedValue = UtilsCrypto.encryptAES('plain text');

// 딥링크 파라미터 인코딩 변수
String encodedValue = URLEncoder.encode(encryptedValue, StandardCharsets.UTF_8);
```

```javascript
decodeURIComponent(encodedValue)
```

## 결과 
- 암호화 값의 특수 기호가 URL 인코딩이 되어 딥링크 파라미터로 전달 확인
- 딥링크 클릭시 프론트 앤드에서 URL 디코딩 후 패널 동작 및 로직 정상 확인





