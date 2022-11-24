# VUEX의 Store란??

- Vuex 
  - vue 개발에서 상태를 관리해 주는 기능을 제공
  - 상태관리 패턴 + 라이브러리

- Store
  - 어플리케이션의 모든 컴포넌트들에 대한 중앙 집중식 저장소의 역할

- Vuex가 없다면? 컴포넌트간의 데이터를 주고 받기 위해서는 --> 복잡
  - 부모 컴포넌트 -> 자식 컴포넌트 : props 이용
  - 자식 컴포넌트 -> 부모 컴포넌트 : Emit event 방식 사용
  - 형제 컴포넌트 -> 형제 컴포넌트 : Event Bus를 사용

- Vuex의 핵심 구성 요소
  - state, Mutations, Actions, Getters 

- State : 데이터 객체
  - 공통으로 참조하기 위한 변수를 정의 
  - 프로젝트 모든 곳 참조 및 사용 가능
  - 모든 컴포넌트들 공통된 값 사용

```js
export const state = () => ({
  account: null,
  isAdmin: null,
  item: null
})
```

- Mutations : 동기형 State 변경 처리기
  - State 변경을 담당 (반드시 Mutation 통해 변경)
  - 동기 처리 방식
  - commit('함수명', '전달인자') 방식 호출
  - mutations 내에 함수 작성

```js
export const mutations = {
  currentUser(state, account){
    state.account = account
  }
}
```

- Actions : Mutation 트리거
  - Mutation 실행시키는 역할
  - 비동기 처기 방식
  - dispatch('함수명', '전달인자') 방식 호출
  - Actions 내에 함수 작성
  - 콜백 함수 작성

```js
// 일반적인 호출 호출 시
dispatch('setAccount', account);

// action 정의 - store.js
export const actions = {
  setAccount({commit, dispatch}, account){
    //mutation 실행
    commit('currentUser', account); 
    dispatch('setIsAdmin', account.uid);
  }
}
```

```js
// 컴포넌트에서 콜백 실행 방식
dispatch(('setAccount', account).then() => {});

// action 정의 - store.js
export const actions = {
  setAccount({commit}, account){
    return new Promise((resolve, reject)=>{
      setTimeout(()=> {
        //Mutation 실행
        commit('currentUser', account);
        //콜백처리
        resolve();  
      }, 1000)
    })
  }
}
```

- Getters : 공통 속성
  - 각 컴포넌트의 계산된 속성(computed) 공통 속정으로 정의
  - 여러 컴포넌트에서 동일한 computed 사용될 경우 Getters 정의해서 공통으로 쉽게 사용
  - 하위 모듈 Getters 호출 : this.$store.getter['경로명/함수명']

```js
export const getters = {
  // 현재 로그인 상태 여부 확인 (user 정보 설정 여부로 true/false)
  isAuthenticated(state) {
    return !!state.user;
  },
  // 회원정보 불러오기
  getAccount(state) {
    return state.account;
  }
}
```

- 데이터 반영 흐름
  - Dispatch -> Actions(commit) -> Mutations(State)

- 데이터 조회 흐름
  - State -> Getters