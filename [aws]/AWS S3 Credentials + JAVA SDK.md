# AWS S3 Credentials + JAVA SDK

## 1. 개요
AWS S3 (간단한 저장소 서비스)는 아마존 웹 서비스에서 제공하는 확장 가능한 객체 저장소 서비스 입니다. 이 서비스를 사용하면 문서, 이미지, 비디오 등 대량의 데이터를 고가용성 및 내구성을 갖춘 방식으로 저장하고 검색할 수 있습니다.  
Java를 사용하여 AWS S3와 상호작용하기 위해 AWS SDK for Java를 사용할 수 있습니다. 

- AmazonS3
    - AWS S3 서비스의 주요 진입점을 제공하는 클래스
    - S3 버킷을 만들고, 객체를 업로드하고, 다운로드하고, 삭제하는 등의 작업을 수행

- S3Client
    - AWS SDK for Java v2에서 도입된 개념
    - S3 서비스와 통신하는 데 사용되는 인스턴스를 생성하는 데 사용
    - 인스턴스를 통해 S3 버킷에 대한 접근 권한을 설정하고, 객체를 업로드하고, 다운로드하고, 삭제하는 등의 작업을 수행


## 2. 문제점
- 개발 서버 S3에서 접속시 인증 오류가 발생 
- 개발 서버는 IAM Role를 참조하여 자격 증명을 진행, KEY를 사용하면 인증
- 자격 증명에 대한 정보를 가져오지 못하는 걸로 파악됨
- AWS SDK (AmazonS3, S3Client)에 변경에 따라 자격 증명이 되는 경우가 있음



## 3. AWS 자격 증명에 대한 순서
- DefaultAWSCredentialsProviderChain
    - 아래와 같은 순서대로 자격 증명 찾음

- EnvironmentVariableCredentialsProvider
    - 환경변수 AWS_ACCESS_KEY_ID 과 AWS_SECRET_ACCESS_KEY 를 사용하여 로드

- SystemPropertiesCredentialsProvider
    - Java 시스템 속성 aws.accessKeyId 과 aws.secretKey 를 사용하여 로드

- 환경 또는 컨테이너의 웹 자격 증명 토큰 자격 증명

- ProfileCredentialsProvider
    - 일반적으로 위치 ~/.aws/credentials (플랫폼마다 다를 수 있음) 에 있는 자격증명을 로드

- ContainerCredentialsProvider
    - 환경 변수인 경우 Amazon ECS에서 AWS_CONTAINER_CREDENTIALS_RELATIVE_URI 에 있는 자격증명 로드

- InstanceProfileCredentialsProvider
    - EC2 인스턴스에서 사용되며 Amazon EC2 메타 데이터 서비스를 사용하여 로드 인스턴스 메타데이터 관련 참고
    - 단, k인스턴스 프로파일 자격 증명은 AWS_CONTAINER_CREDENTIALS_RELATIVE_URI가 설정되지 않은 경우에만 사용된다.


## 4. 테스트 코드
- 
```java
@GetMapping("/buckets-v1/DefaultAWSCredentialsProviderChain")
public ResponseEntity<List<com.amazonaws.services.s3.model.Bucket>> getBucketListV1_0() {

    AmazonS3 s3Client = AmazonS3ClientBuilder.standard()
            .withCredentials(DefaultAWSCredentialsProviderChain.getInstance())
            .withRegion(Regions.AP_NORTHEAST_2)
            .build();

    List<com.amazonaws.services.s3.model.Bucket> listBuckets =  s3Client.listBuckets();

    return ResponseEntity.ok(listBuckets);
}

// 환경 변수 자격 증명 확인
@GetMapping("/buckets-v1/EnvironmentVariableCredentialsProvider")
public ResponseEntity<List<com.amazonaws.services.s3.model.Bucket>> getBucketListV1_1() {
    AWSCredentials credentials = new BasicAWSCredentials(config.getAccessKey(), config.getSecretKey());

    AmazonS3 s3Client = AmazonS3ClientBuilder.standard()
            .withRegion(Regions.AP_NORTHEAST_2)
            .withCredentials(new AWSStaticCredentialsProvider(credentials))
            .build();

    List<com.amazonaws.services.s3.model.Bucket> listBuckets =  s3Client.listBuckets();

    return ResponseEntity.ok(listBuckets);
}

// 기본 위치 자격 증명 확인
@GetMapping("/buckets-v1/ProfileCredentialsProvider")
public ResponseEntity<List<com.amazonaws.services.s3.model.Bucket>> getBucketListV1_3() {
    AWSCredentialsProvider credentialsProvider = new ProfileCredentialsProvider();
    AWSCredentials credentials = credentialsProvider.getCredentials();

    AmazonS3 s3Client = AmazonS3ClientBuilder.standard()
            .withRegion(Regions.AP_NORTHEAST_2)
            .withCredentials(credentialsProvider)
            .build();

    List<com.amazonaws.services.s3.model.Bucket> listBuckets =  s3Client.listBuckets();

    return ResponseEntity.ok(listBuckets);
}

// AWS ECS 컨테이너 자격 증명 확인
@GetMapping("/buckets-v1/ContainerCredentialsProvider")
public ResponseEntity<List<com.amazonaws.services.s3.model.Bucket>> getBucketListV1_4() {

    AWSCredentialsProvider credentialsProvider = new EC2ContainerCredentialsProviderWrapper();
    AWSCredentials credentials = credentialsProvider.getCredentials();

    AmazonS3 s3Client = AmazonS3ClientBuilder.standard()
            .withRegion(Regions.AP_NORTHEAST_2)
            .withCredentials(credentialsProvider)
            .build();

    List<com.amazonaws.services.s3.model.Bucket> listBuckets =  s3Client.listBuckets();

    return ResponseEntity.ok(listBuckets);
}

// 인스턴스 프로필 자격 증명 확인 (v1)
@GetMapping("/buckets-v1/InstanceProfileCredentialsProvider")
public ResponseEntity<List<com.amazonaws.services.s3.model.Bucket>> getBucketListV1_5() {

    AWSCredentialsProvider credentialsProvider = new InstanceProfileCredentialsProvider();
    AWSCredentials credentials = credentialsProvider.getCredentials();

    AmazonS3 s3Client = AmazonS3ClientBuilder.standard()
            .withRegion(Regions.AP_NORTHEAST_2)
            .withCredentials(credentialsProvider)
            .build();

    List<com.amazonaws.services.s3.model.Bucket> listBuckets =  s3Client.listBuckets();

    return ResponseEntity.ok(listBuckets);
}

// 인스턴스 프로필 자격 증명 확인 (v2)
@GetMapping("/buckets-v2/InstanceProfileCredentialsProvider")
public ResponseEntity<List<software.amazon.awssdk.services.s3.model.Bucket>> getBucketListV2_5() {

    AwsCredentialsProvider credentialsProvider = software.amazon.awssdk.auth.credentials.InstanceProfileCredentialsProvider.builder()
            .build();

    S3Client s3Client = S3Client.builder()
            .region(Region.AP_NORTHEAST_2)
            .credentialsProvider(credentialsProvider)
            .build();

    List<software.amazon.awssdk.services.s3.model.Bucket> listBuckets = s3Client.listBuckets().buckets();

    return ResponseEntity.ok(listBuckets);
}

```

## 결과 분석
- 인스턴스 프로필에 자격 증명이 확인이 안되는걸로 보임
- InstanceProfileCredentialsProvider은 AWS SDK 버전 1.x에서 사용되던 클래스인데 지원이 안됨
- 개발 서버는 인스턴스 프로필로 자격 증명을 하는데 기존 AmazonS3 라이브러리로는 증명을 할 수 없음
- 환경 변수 자격 증명 확인 하던가 아니면 S3Client (AWS SDK for Java v2) 버전을 사용해서 소스를 수정해야 됨