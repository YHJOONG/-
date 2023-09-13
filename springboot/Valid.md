# Valid Exception 처리 

## 문제 
- 개발 중 Valid Exception 처리에 대하여 문제가 발생
- 예전 프로젝트에서 Valid Exception 처리를 전역 컨트롤러에서 사용중이였음
- 새로운 프로젝트에서 Valid 예외 처리를 위하여 사용했지만 예외처리가 잡히지 않음
- 여러 Exception 처리는 예외가 발생했지만 MethodArgumentNotValidException 잡히지 않음
  
## 해결
- 기존 Valid 어노테이션이 Request Body로 받은 파라미터에 적용
- 새로운 소스는 Valid가 Paramter Body로 받은 파라미터에 적용 -> (ModelAttribute 어노테이션 적용)
- MethodArgumentNotValidException -> BindException 예외 클래스 변경
  

- 기존 소스
    ```
    @ControllerAdvice
    @RestController
    public class VaildExceptionController {

        @ExceptionHandler(MethodArgumentNotValidException.class)
        public ResponseEntity<ErrorResponseModel> processValidationError(MethodArgumentNotValidException exception) {
            ErrorResponseModel resMsg = new ErrorResponseModel();
            try {
                resMsg = ErrorResponseModel.builder()
                                    .code(10006)
                                    .message("Invalid data sent for a parameter.").build();
                                    
                return ResponseEntity.badRequest().body(resMsg);
            } catch (Exception e) {
                System.out.println(e.getMessage());
                resMsg = ErrorResponseModel.builder()
                                .code(10007)
                                .message("Internal server error.").build();
                return ResponseEntity.internalServerError().body(resMsg);
            }
        }

    }
    ```

- 해결 소스
  ```
    @ControllerAdvice
    @RestController
    public class VaildExceptionController {

        @ExceptionHandler(BindException.class)
        public ResponseEntity<ErrorResponseModel> processValidationError(BindException exception) {
            ErrorResponseModel resMsg = new ErrorResponseModel();
            try {
                resMsg = ErrorResponseModel.builder()
                                    .code(10006)
                                    .message("Invalid data sent for a parameter.").build();
                                    
                return ResponseEntity.badRequest().body(resMsg);
            } catch (Exception e) {
                System.out.println(e.getMessage());
                resMsg = ErrorResponseModel.builder()
                                .code(10007)
                                .message("Internal server error.").build();
                return ResponseEntity.internalServerError().body(resMsg);
            }
        }

    }
  ```



