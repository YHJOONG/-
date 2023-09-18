# 단일 책임 원칙 (Single Responsibility Principle)
- 클래스나 모듈은 하나의 책임만 가져야 한다는 원칙을 나타냅니다.
- 자신의 책임에 집중하고 다른 관심사나 작업에 대한 처리를 다른 클래스나 모듈로 분리합니다.
- SRP를 위반한 경우
    ```java
    public class BankAccount {
        private String accountNumber;
        private double balance;
    
        public BankAccount(String accountNumber, double initialBalance) {
            this.accountNumber = accountNumber;
            this.balance = initialBalance;
        }
    
        public void deposit(double amount) {
            // 입금 로직
            balance += amount;
        }
    
        public void withdraw(double amount) {
            // 출금 로직
            balance -= amount;
        }
    
        public void transfer(BankAccount destinationAccount, double amount) {
            // 계좌 간 이체 로직
            destinationAccount.deposit(amount);
            this.withdraw(amount);
        }
    
        public void generateAccountStatement() {
            // 계좌 명세서 생성 및 출력 로직
        }
    }
    ```
위의 BankAccount 클래스는 하나의 클래스에서 다음과 같은 다양한 책임을 가지고 있습니다:

입금(deposit) 및 출금(withdraw) 로직 관리  

계좌 간 이체(transfer) 로직 관리  

계좌 명세서 생성 및 출력(generateAccountStatement) 로직 관리  

이러한 다양한 책임이 하나의 클래스에 혼재되어 있으므로 SRP를 위반하고 있습니다.

- SRP를 준수한 경우:
    ```java
    public class BankAccount {
        private String accountNumber;
        private double balance;
    
        public BankAccount(String accountNumber, double initialBalance) {
            this.accountNumber = accountNumber;
            this.balance = initialBalance;
        }
    
        public void deposit(double amount) {
            // 입금 로직
            balance += amount;
        }
    
        public void withdraw(double amount) {
            // 출금 로직
            balance -= amount;
        }
    }
    
    public class AccountTransfer {
        public static void transfer(BankAccount sourceAccount, BankAccount destinationAccount, double amount) {
            // 계좌 간 이체 로직
            destinationAccount.deposit(amount);
            sourceAccount.withdraw(amount);
        }
    }
    
    public class AccountStatement {
        public static void generateAccountStatement(BankAccount account) {
            // 계좌 명세서 생성 및 출력 로직
        }
    }
    ```

위의 코드에서는 SRP를 준수하기 위해 다음과 같은 변경이 이루어졌습니다:

BankAccount 클래스는 입금과 출금과 같은 계좌 관리 책임만을 갖고 있습니다. 

AccountTransfer 클래스는 계좌 간 이체 책임만을 갖고 있습니다.

AccountStatement 클래스는 계좌 명세서 생성 및 출력 책임만을 갖고 있습니다.