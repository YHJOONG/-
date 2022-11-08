# AWS CI/CD 솔루션

## CodeCommit 
- AWS 출시한 코드 관리 서비스 == private git repository
- 소스 s3 암호화 저장

## CodeBuild
- 소스 코드 컴파일 / 테스트 실행
- 배포 준비된 소프트웨어 패키지 생성

## CodeDeploy
- EC2 서버에 배포 

## CodePipeline
- 지속석 제공하는 파이프 라인 구축
- 소프트웨어 배포에 필요한 단계를 모델링, 시각화 및 자동화하는데 사용할 수 있는 지속적인 제공 서비스


## 1. aws 환경 세팅
- 기본 프로그램 설치
  - Git 설치 
    - https://taewow.tistory.com/13
  - CLI 설치 (CLI version2) - SSO 로그인 방식 이용 (하단 상세 첨부)
    - https://docs.aws.amazon.com/ko_kr/cli/latest/userguide/getting-started-install.html
    ```
    msiexec.exe /i https://awscli.amazonaws.com/AWSCLIV2.msi
    aws --version
    ```
    
  - Git Remote Codecommit(GRC) 설치
    - https://docs.aws.amazon.com/ko_kr/codecommit/latest/userguide/setting-up-git-remote-codecommit.html
    - Python 버전 3 이상 및 pip 설치 
      - Add Python 3.10 to Path 체크
      - Customize install -> environment variables ... 체크
      - 설치 확인 
        ```
        > git --version
        git version 2.37.2.windows.2
        > aws --version
        aws-cli/2.8.3 Python/3.9.11 Windows/10 exe/AMD64 prompt/off
        ```
  - aws codecommit repository - 로컬 연동
    - aws profile 생성 (aws sso 방식)
      - aws sso 로그인을 통해 소스 레포지터리 접근
      
      - git credential helper 설정 (최초 1회) 
        - git config --global credential.helper “!aws codecommit credential-helper $@”
        - git config --global credential.UseHttpPath true
      
      - Git 유저 이름 및 이메일 설정 (최초 1회)
        - git config --global user.name "<USER_NAME>"  
        - git config --global user.email "<USER_EMAIL>"  
      - SSO profile 설정
        - aws configure sso --profile <PROFILE NAME>
        ```
        > aws configure sso --profile devops-devloper
        SSO start URL : https://<URL 정보>
        SSO region : 레퍼지토리 지역
        ```
        - aws 로그인 창 팝업 -> 로그인 -> allow 선택
        - SSO 계정 선택
      - 로컬 aws profile 환경 변수 추가
        ```
        -- Windows
        setx AWS_PROFILE <PROFILE_NAME>
        
        -- Mac/Linux
        export AWS_PROFILE=<PROFILE_NAME>
        ```

      - 세션 끊긴 경우
        - 로컬 aws profile 환경 변수 수정 (위에 참고)
        - cmd > aws sso login
      
      - 로그인 Profile 확인
        - aws sts get-caller-identity

      - Codecommit Repository Clone
        - IDEA 이용
        - CLI 이용 
          - git clone  codecommit::<SSO REGION>://<REPO_NAME>
  



