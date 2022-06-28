# Quartz 스케줄러 사용

일정 시간, 주기적으로 작동되는 배치 프로그램

## 구성요소

JobDetail : 스케줄러에서 수행할 작업을 담을 JOB 생성  
JobDataMap : 스케줄러 JOB에 값을 전달하는데 사용  
Trigger : 스케줄러를 어떤 방식으로, 어떤 주기로 작동할지 결정  
- SimpleTrigger : start Time, end time, interval time, repeat times 설정  
- CronTrigger : Cron 형식으로 일정 주기를 지정 
  

### 스케줄러 Job 클래스 생성
Job Interface를 implements 하여 execute() 메소드 생성
```
    public class TestJob implement Job{
        @Override 
        public void execute(JobExecutionContext context) throws JobExecutionException{
            System.out.println("Job Run");
        }
    }
```

### 스케줄러 실행
JobDetail -> Job Class 등록  
Trigger -> 스케줄러의 주기를 설정
```
    public class Triggers{
        private SchedulerFactory schedulerFactory;
        private Scheduler scheduler;

        public void SetTrigger(){
            try{
                //스케줄 생성 및 시작         
                schedulerFactory = new StdSchedulerFactory();

                scheduler = schedulerFactory.getScheduler();

                scheduler.start();

                // Job 설정
                JobDetail jobDetail = JobBuilder.newJob(TestJob.class)
                                                .withIdentity("binance_ticker", Scheduler.DEFAULT_GROUP)
                                                .requestRecovery(true)
                                                .build();
                // Trigger 설정
                CronTrigger trigger = TriggerBuilder.newTrigger()
                                                    .withIdentity("job", Scheduler.DEFAULT_GROUP)
                                                    .withSchedule(CronScheduleBuilder.cronSchedule("0/30 * * * * ?"))
                                                    .build();
                // 스케줄 등록
                scheduler.scheduleJob(jobDetail, trigger);
                
            }catch(SchedulerException e){
                e.printStackTrace();
            }
        }
    }
```

