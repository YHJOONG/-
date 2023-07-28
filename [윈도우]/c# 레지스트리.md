# c# 레지스트리

 C#에서 제공하는 레지스트리 클래스를 이용한 레지스트리 읽기, 레지스트리 쓰기, 레지스트리 관리에 대해 간단히 알아보려고 합니다.

우선 레지스트리 클래스를 이용하려면 Microsoft.Win32 네임 스페이스를 사용하여야 합니다.

```c#
Using Microsoft.Win32;
```



RegistryKey 클래스의 멤버

CreateSubKey : 새 하위 키를 만들거나 기존하위키를 엽니다.

OpenSubKey : 지정된 하위 키를 검색합니다.

DeleteSubKey : 지정된 하위키를 삭제합니다.

DeleteSubKeyTree : 하위키와 자식 하위 키를 재귀적으로 삭제합니다.

GetSubKeyNames : 모든 하위 키 이름이 포함된 문자열의 배열을 검색합니다.

SetValue : 레지스트리 키에서 이름/값 쌍의 값을 설정합니다.

GetValue : 지정된 이름과 연결된 값을 검색합니다.

GetValueNames : 이 키와 관련된 모든 값 이름이 포함된 문자열의 배열을 검색합니다.

DeleteValue : 지정된 값을 이 키에서 삭제합니다.



##### 하위 키 만들기(Create Sub Key)

```c#
public RegistryKey CreateSubKey(string subkey)
```

첫번째 인자에는 새로 만들 하위 키의 이름 혹은 경로가 들어갑니다. 만약의 HKEY_CURRENT_USER에 "C#rkey"라는 하위키를 만들고 그 키에다가 또다시 "testsubkey"라는 하위키를 만드려면 어떻게 해야 할까요?

```c#
RegistryKey rkey = Registry.CurrentUser.CreateSubKey("C# rkey").CreateSubKey("testsubkey");

RegistryKey rkey = Registry.CurrentUser.CreateSubKey(@"c# rkey\testsubkey");
```



##### 하위 키 열기

```c#
public RegistryKey OpenSubKey(string name)
```

  첫번째 인자로는 열려고 하는 하위 키의 이름, 혹은 경로가 들어갑니다. 반환값으로는 요청된 하위 키가 반환되며, 하위 키를 열지 못했을경우 null을 반환합니다. 



```c#
// 예 1 
RegistryKey rkey = Registry.CurrentUser.OpenSubKey("c# rkey").OpenSubKey("testsubkey"); 
// 예 2 
RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"c# rkey\testsubkey");
```



만약 Write Access가 필요하면 두번째 인자에 true라고 설정하면 됩니다.

```c#
RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"c# rkey\testsubkey", true);
```



##### 하위 키 삭제

```c#
public void DeleteSubKey(string subkey)
```

첫번째 인자로는 삭제할 하위 키의 이르모, 혹은 경로가 들어갑니다. 반환값 x

```c#
Registry.CurrentUser.DeleteSubKey(@"c# rkey\qwd");
```

##### 

##### 값 쓰기

```c#
public void SetValue(string name, Ojbect value)
```

첫번째 인자로는 저장할 값의 이름이 옵니다. 두번째 인자로는 저장할 데이터를 말합니다.

```c#
RegistryKey rkey = Registry.CurrentUser.CreateSubKey("c# rkey").CreateSubKey("testsubkey"); rkey.SetValue("test", "테스트!"); rkey.SetValue("text", "텍스트!");
```

  SetValue은 위의 선언과 같이 void형으로, 반환값이 없습니다. 그리고 두번째 인자에 정수를 넣든, 문자열을 넣든 아래와 같이 문자열 형(REG_SZ)로 저장됩니다. 



RegistryValueKind(다른 종류로 저장하고 싶으면 사용)

```c#
public void SetValue(string name, Object value, RegistryValueKind valueKind)
```

String/ 문자열(REG_SZ)

ExpandString/ 문자열(환경변수 포함, REG_EXPAND_SZ)

Binary /이진 데이터(REG_BINARY)

DWord/  32비트 이진수(REG_DWORD) 

MultiString /문자열 배열(REG_MULTI_SZ) 

QWord /64비트 이진수(REG_QWORD) 

Unknown /지원되지 않는 레지스트리 데이터 형식(REG_RESOURCE_LIST) 



##### 값 가져오기

```c#
public Object GetValue(string name)
```

첫번째 인자값 : 가져올 값의 이름 반환값으로는 그 이름의 데이터, 없으면 null

```c#
RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"c# rkey\testsubkey"); MessageBox.Show(rkey.GetValue("test").ToString());
```



##### 값 제거하기

```c#
public void DeleteValue(string name)
```

  선언을 살펴보면, 첫번째 인자값으로는 삭제할 값의 이름이 옵니다. 반환값은 역시 void형이므로, 반환되는 값은 없습니다. 한번 DeleteValue 메서드를 이용.

```c#
// 하위 키 HKEY_CURRENT_USER\c# rkey\testsubkey에 있는 test 제거
RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"c# rkey\testsubkey", true); rkey.DeleteValue("test");
```

