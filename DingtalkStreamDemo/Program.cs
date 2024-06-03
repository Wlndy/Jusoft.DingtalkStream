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

            // options.UA = "dingtalk-stream-demo"; // ��չ���Զ����UA
            // options.Subscriptions.Add //  ���ģ�Ҳ��������������

            options.AutoReplySystemMessage = true; // �Զ��ظ� SYSTEM ����Ϣ��ping,disconnect��

            options.OnStarted = (client) =>
            {
                Console.WriteLine("���ĳ�����������");
            };
            options.OnStoped = (client , ex) =>
            {
                // ex : ֹͣ���쳣ԭ��
                Console.WriteLine("���ĳ�����ֹͣ���С�");
            };


        })
          //.RegisterEventSubscription()  // ע���¼����� ����ѡ��
          //.RegisterCardInstanceCallback()// ע�ῨƬ�ص� ����ѡ��
          .RegisterIMRobotMessageCallback()// ע���������Ϣ�ص� ����ѡ�� // ��Ҫ��� Jusoft.DingtalkStream.Robot ��
          .AddMessageHandler<DefaultMessageHandler>() //�����Ϣ�������
          .AddHostServices();// ������������������� DingtalkStreamClient

    })
    .Build();

await host.RunAsync();
