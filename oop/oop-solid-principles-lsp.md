# 리스코프 치환 원칙 (Liskov Subsitution Principle)
- 부모 객체와 이를 상속한 자식 객체가 있을 때 부모 객체를 호출하는 동작에서 자식 객체가 부모 객체를 완전히 대채할 수 있는 원칙입니다.
- 객체의 상속 과정에서 부모/자식 관계가 정의됩니다. 자식 객체는 부모 객체의 특성을 가지며, 확장가능하지만 이 과정에서 무리하거나 객체의 의의와 어긋나는 확장으로 인해 잘못된 방향으로 상속되는 경우가 생긴다.
- LSP를 위반한 경우
```java
class Rectangle {
  private int width;
  private int height;

  public void setWidth(int width) {
    this.width = width;
  }

  public void setHeight(int height) {
    this.height = height;
  }

  public int getWidth() {
    return width;
  }

  public int getHeight() {
    return height;
  }

  public int getArea() {
    return width * height;
  }
}

class Square extends Rectangle {
  public void setWidth(int side) {
    super.setWidth(side);
    super.setHeight(side);
  }

  public void setHeight(int side) {
    super.setWidth(side);
    super.setHeight(side);
  }
}

```
Rectangle 클래스는 가로와 세로의 길이를 가지고 있으며, 넓이를 계산하는 메서드를 제공합니다.
Square 클래스는 Rectangle 클래스를 확장하고, 정사각형의 특성을 갖도록 setWidth와 setHeight 메서드를 오버라이드합니다. 
그러나 이것은 LSP 위반입니다.


```java
Rectangle rectangle = new Square();
rectangle.setWidth(4);
rectangle.setHeight(5);

int area = rectangle.getArea(); // 어떤 결과가 나와야 할까요?
```
위의 코드에서 rectangle은 Square 객체를 참조하고 있으며 가로와 세로를 설정한 후에 넓이를 계산하려고 합니다. 
그러나 Square 클래스에서 가로와 세로를 같은 값으로 설정하도록 오버라이드한 것으로 인해 예상치 않은 결과가 나타납니다. 
정사각형의 넓이를 계산할 때는 가로와 세로의 값이 같아야 하지만, 위의 코드에서는 그렇지 않습니다. 이것은 LSP 위반입니다.


- LSP를 준수한 경우:
```java
class Shape {
  public int getArea() {
    return 0; // 모든 도형의 넓이는 0으로 초기화
  }
}

class Rectangle extends Shape {
  private int width;
  private int height;

  public void setWidth(int width) {
    this.width = width;
  }

  public void setHeight(int height) {
    this.height = height;
  }

  @Override
  public int getArea() {
    return width * height;
  }
}

class Square extends Shape {
  private int side;

  public void setSide(int side) {
    this.side = side;
  }

  @Override
  public int getArea() {
    return side * side;
  }
}

```

위의 코드에서 Shape 클래스는 넓이를 반환하는 기본 동작을 가지며, Rectangle 클래스와 Square 클래스는 이를 확장하여 각 도형의 넓이를 계산합니다.

```java
Shape shape = new Rectangle();
shape.setWidth(4);
shape.setHeight(5);

int area = shape.getArea(); // 올바른 결과
```
