# 개방폐쇄의 원칙 (Open Close Principle)
- 소프트웨어의 구성요소(컴포넌트, 클래스, 모듈, 함수)는 확장에는 열려있고, 변경에는 닫혀있어야 한다는 원리입니다.
- 관리 가능하고, 재사용 가능한 코드를 만드는 기반이며, 가장 중요한 메커니즘은 추상화, 다형성이라고 할 수 있습니다.
 
## 용어 풀이
> **확장 이란 ?**
>
>새로운 타입을 추가함으로써 새로운 기능을 추가할 수 있다. 즉 확장이란 새로운 타입을 추가함으로써 새로운 기능(요구사항)을 구현한다. **확장에는 열려 있다는 것은 새로운 타입(클래스)을 추가함으로써 기능(요구사항)을 확장하는 것이다.**

> **변경 이란 ?**
>
> 확장이 발생했을 때 상위 레벨이 영향을 미치지 않아야 한다. **확장(새로운 클래스)이 발생했을 때 해당 코드를 호출하는 쪽에서 변경이 발생하지 않다면 변경에 닫혀 있다는 것이다.**


## OCP를 준수하는 방법

인터페이스와 추상 클래스 활용: 인터페이스와 추상 클래스를 사용하여 확장 가능한 구조를 정의합니다.

상속과 다형성 활용: 상속과 다형성을 통해 새로운 기능을 추가하고 기존 코드를 수정하지 않고도 다양한 형태의 객체를 처리할 수 있게 합니다.

의존성 역전 원칙(Dependency Inversion Principle - DIP) 준수: 상위 수준 모듈이 하위 수준 모듈에 의존하면 안 되며, 둘 모두 추상화에 의존해야 합니다. 이를 통해 확장에 열려 있고 변경에 닫힌 코드를 유지할 수 있습니다.

## OCP를 위반하는 방법

기능 추가 시 기존 코드 변경: 새로운 기능을 추가하려면 기존 코드를 수정해야 하는 경우 OCP를 위반합니다.

클래스 간 강한 결합: 클래스 간의 강한 결합으로 인해 새로운 기능을 추가하면 다른 클래스에 영향을 미치는 경우 OCP를 위반합니다.

조건문과 스위치문 지나치게 사용: 조건문이나 스위치문을 지나치게 사용하여 새로운 기능을 추가할 때마다 조건을 추가하면 OCP를 위반합니다.

## 예시

- OCP를 위반한 경우
    ```java
    public class Shape {
        private String type;
    
        public Shape(String type) {
            this.type = type;
        }
    
        public double calculateArea() {
            if (type.equals("Circle")) {
                double radius = 5.0; // 원의 반지름
                return Math.PI * radius * radius;
            } else if (type.equals("Rectangle")) {
                double width = 4.0; // 사각형의 너비
                double height = 6.0; // 사각형의 높이
                return width * height;
            }
            return 0.0; // 다른 도형은 계산하지 않음
        }
    }
    ```
  
- OCP를 준수한 경우
    ```java
    public interface Shape {
        double calculateArea();
    }
    
    public class Circle implements Shape {
        private double radius;
    
        public Circle(double radius) {
            this.radius = radius;
        }
    
        @Override
        public double calculateArea() {
            return Math.PI * radius * radius;
        }
    }
    
    public class Rectangle implements Shape {
        private double width;
        private double height;
    
        public Rectangle(double width, double height) {
            this.width = width;
            this.height = height;
        }
    
        @Override
        public double calculateArea() {
            return width * height;
        }
    }
    ```