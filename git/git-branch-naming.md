# Git branch naming 규칙

## Branch
- 사전적인 의미 : 나무의 가지라는 뜻
- development 상태와 production 상태일 때를 나누거나, bug fix를 해야할 때 유용하게 사용
- 메인 프로젝트를 손상시키지 않고 여러 버전을 관리하기 위해 git branch를 사용


## Supporting branches 
supporting 브랜치를 사용하여 구성원간 평행 개발이 가능, 기능 쉽게 추적, 여러가지 문제도 해결 쉬워짐, 제한된 수명을 가짐
- feature branch
  - 기능을 개발하는 브랜치
  - develop 브랜치로부터 분기
  - 자신의 로컬 브랜치에서 관리

- release branch
  - 이번 출시 버전을 준비하는 브랜치
  - 배포를 위한 전용 브랜치를 사용함으로써 한 팀이 해당 배포를 준비하는 동안 다른 팀은 다음 배포를 위한 기능 개발을 계속할 수 있음
  - 새로운 기능을 추가로 merge 하지 않음
  
- hotfix branch
  - 출시 버전에서 발생한 버그를 수정하는 브랜치
  - 긴급히 수정을 해야 할 경우, master 브랜치에서 분기하는 브랜치
  - 배포 순서 
    1. 배포한 버전에 긴급하게 수정할 부분 발생 => master브랜치에서 hotfix브랜치 분기
    2. 문제가 되는 부분 수정후, master브랜치에 merge하고 배포
    3. hotfix 브랜치에서의 변경 사항은 develop 브랜치에도 merge한다.


## Branch 네이밍 규칙
- master branch, develop branch
  - master와 develop 브랜치는 본래 이름 그대로 사용함
  
- feature branch
  - feature/기능요약 : 형식을 추천
  - feature/{issue-number}-{feature-name} : 이슈추적을 사용한다면 이와 같은 형식을 따른다.
  
- release branch
  - release-RB_... 또는 release-... 또는 release/...같은 이름이 일반적이다.
  - release-... 형식을 추천한다. ex) release-1.2

- hotfix branch
  - hotfix-... 형식을 추천한다. ex) hotfix-1.2.1