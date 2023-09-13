# git-tutorial
깃허브에서 clone한 프로젝트는 어느 경로에 놓든 상관이 없음

## 깃 명령어
    다운로드 할때 그 repository 경로로 다운받기 때문에 push 할때도 깃 허브에서 다운받은 repository로 push됨

## git 명령어

1. github: git을 저장하는 원격저장소

2. git status : 변경사항뭔지 볼수 있음

3. git clone : 해당 git repository에 있는 파일을 컴퓨터 해당 폴더로 복사,(새로운 폴더로 만들어짐) 
    ex)git clone http://~~

4. git add: 깃 추가 Staging Area로 감
   ex) git add document.txt   
   ex) git add . ->모든 수정된 파일이 staging area로 감
   
5. git reset : git add 한걸 취소하고싶을때 사용 다시 Staging Area에서 없어짐
   ex) git reset document.txt
   
6. git checkout : 변경사항을 무시함 -> 이러면 코드가 다시 변경하기전으로 돌아감
   ex)git checkout --document.txt
   
7. git commit:깃 추가당시를 스냇샷처럼 찍어 저장(이 기능을 통해 다시 원상태로 돌아올수 있음)
    커밋까지 하면 깃에 파일이 저장된것(그러나 깃허브에는 X)
    ex)git commit -m "add text file [document.txt]"
    
8. git push:깃허브에도 넣음

9. git fetch:remote repository에 있는 다른사람이 수정한 코드를 내 local repository로 다운로드

10. git merge:서로 수정한 내용 합침,다른사람과 내가 동일한 파일을 수정해서 conflict가 일어날경우 merge 해서 내 working directory의 내용을 변경 즉, 내 컴퓨터 소스코드와 원격저장소 코드를 맞춤
    - merge 할때 충돌이 일어날수 있음 ->특정 부분에서 서로 각자 다른내용을 가지고 있기때문에 일어남->파일로 다시가보면 충돌난 부분이 둘다 write되어있는걸 확인할수있음->수동으로 원하는코드만 남기고 삭제함->다시 add,commit,merge하면 성공적으로 됨

11. git log : 로그값,커밋,커밋해쉬값 볼수 있음

12. git reset --hard 커밋해쉬값 : 저 커밋했던때로 코드가 돌아감
    cf)코드가 예전으로 돌아갔을때 rocal repo와 remote repo 구성이 완전 다르기 때문에 push 해도 거절됨 이때는 git push -f 해서 강제로 푸쉬하면 가능
13. git commit --amend 한후 특정에디터로 들어가짐 a 를 누르면 에디터내용변경후 esc 누르고 :wq! 라고 쓰면 자동으로 저장되고 빠져나옴 : 커밋메세지 변경하는법

14. git remote : 현재 원격저장소(git)로 어떤게 저장되어있는지확인가능

15. git remote show origin : origin 원격 저장소의 상세정보불러옴

16. git remote add test https://github.com/git-study.git : test라고 깃을 만들고 그 주소로 원래있던 레포짓토리를 넣음

17. git remote rename test temp:test를 temp로 변경함

18. git log: 깃로그확인 가능 (q로 빠져나옴),깃허브에서 그냥 볼수있음

19. git log --stat : 통계정보확인가능(해당파일에 코드가 얼마나 추가,제거됐나)

20. git log --graph :브랜치와 병합정보를 그래프로 출력

21. git log -p -3:커밋에 적용된 구체적 항목 출력(-3은 위에서부터 3개 까지만 출력)

22. git log --pretty=oneline:커밋정보를 내가 지정한 형식으로 출력(=oneline은 한줄로 출력)

## git 용어
    - Branch : 비선형적개발가능,프로젝트의 가지치기 가능 각자 다른 기능을 개발 할수 있도록 함,브랜치끼리 서로 영향을 주지 않음
    - master branch -> 깃을 생성하면 자동으로 생성되는 브랜치, 배포가능한 수준의 안정화된 버전을 넣음
    - 통합브랜치:배포가능한 안정화된 브랜치 ex)master branch
    - 토픽브랜치:특정기능을 위해 만든 브랜치
        ex)git branch : 브랜치 몇개있는지 확인가능
        ex)git branch develop : develop브랜치 만듦
        ex)git checkout develop : develop 브랜치로 이동
    - 커밋에서 origin/master,orgin/Head 이거는 여기까지가 깃허브에 반영된 부분
        ex)master브런치에서 다른 브런치 작업을 master 브랜치로 가져와서 합치고 싶다면
    - master 브랜치로 이동후 git merge develop 하면 합쳐짐
        ex)git branch -d develop 로 develop브랜치 삭제 
    - commit : 스냅샷(작업변경내역이 따로 저장됨)
    - repository(저장소) : repo라고도 말함,실제 소스코드가 담겨있고, commit 내역 등 모든 작업이력이 담겨있는 공간, 메타데이터 등 각종 데이터는 .git 폴더에 담김

## 깃허브에서 별 1000개만 넘어도 굉장히 유익한 코드
    contributors:해당 코드를 수정하거나,보완,기여한사람(수천명이될수도있음)
    committer: 컨트리뷰션한것을 프로젝트에 반영할지 결정하는사람

## git 프로젝트의 세가지 구성요소
    1.Working Directory : 작업할 파일이 있는 디렉토리
    2.Staging Area : commit을 수행할 파일이 올라가는 영역( git add 명령을 했을때 .git 폴더안에 add 한 파일이 올라가고 git commit을 하면 여기 있던 파일이 rocal repository에 반영됨)
        cf)rocal repository: 내 컴퓨터에 있는 repository의미 이건 .git 폴더안에 있음
        cf)remote repository: github를 말함
    3.git Directory : git 프로젝트의 메타 데이터와 데이터 정보가 저장되는 디렉토리 (.git 폴더를 의미)

## 깃허브에서 다른사람이 수정한 내용 가져올때 순서
    - git fetch->git merge
    - git fetch와 git merge를 한번에 사용하는 명령어 : git pull(주로 많이 사용)
    - 깃허브에 내용 넣을때 순서
        git add->git commit->git push

## 깃허브 commit 탭에서 개발자들이 언제 무엇을 변경했는지 확인 가능
commit 내역을 수정한다는것은 commit 할 때 코멘트뿐만아니라 이때 커밋했을때로 코드를 변경할수있다는것

## 깃 로그을 본다는것
    커밋을 확인한다는것
