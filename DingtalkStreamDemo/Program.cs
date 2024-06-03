using DingtalkStreamDemo;

using Jusoft.DingtalkStream.Core;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context , services) =>
    {
        services.AddDingtalkStream(options =>
        {
            //options.ClientId = "dingXXXXXXXXXXXXXXXXXX";
            //options.ClientSecret = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

            // appsettings.json ������
            options.ClientId = context.Configuration["ClientId"];
            options.ClientSecret = context.Configuration["ClientSecret"];

            options.AutoReplySystemMessage = true; // �Զ��ظ� SYSTEM ����Ϣ��ping,disconnect��

            // options.UA = "dingtalk-stream-demo"; // ��չ���Զ����UA

            //options.MaxQueueCount = 1000; // ������󳤶�
            //options.MaxTaskCount = Environment.ProcessorCount; // ���������������Ĭ��ȡCPU ������
            //options.RecentExecutionTimeCount = 100; // �۲������ִ��ʱ��Ĳο����ִ�е�������
            //options.TimeInterval = TimeSpan.FromMinutes(5); // ���μ�������ݵ�ʱ��
            //options.SingleExecuteTimeOut = TimeSpan.FromMinutes(5); // ��������ִ�г�ʱʱ��

            options.OnStarted = (client) =>
            {
                Console.WriteLine("���ĳ�����������");
            };
            options.OnStoped = (client , ex) =>
            {
                // ex : ֹͣ���쳣ԭ��
                Console.WriteLine("���ĳ�����ֹͣ���С�");
            };

            // options.Subscriptions.Add //  ���ģ�Ҳ��������������
        })
          .RegisterEventSubscription()  // ע���¼����� ����ѡ��
          .RegisterCardInstanceCallback()// ע�ῨƬ�ص� ����ѡ��
          .RegisterIMRobotMessageCallback()// ע���������Ϣ�ص� ����ѡ�� // ��Ҫ��� Jusoft.DingtalkStream.Robot ��
          .AddMessageHandler<DefaultMessageHandler>() //�����Ϣ�������
          .AddHostServices();// ������������������� DingtalkStreamClient

    })
    .Build();

await host.RunAsync();
