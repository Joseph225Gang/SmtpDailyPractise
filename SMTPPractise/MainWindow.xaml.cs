using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SMTPPractise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int aDaySecond = 60 * 60 * 24;
            int aWeek = 7;
            //1.0創建調度工廠
            ISchedulerFactory factory = new StdSchedulerFactory();
            //2.0通過工廠獲取調度器實例
            IScheduler scheduler = factory.GetScheduler();
            //3.0通過JobBuilder構建Job
            IJobDetail job = JobBuilder.Create<JobSendEmail>().UsingJobData("emailAddress", tboHotmail.Text).UsingJobData("password", tboPassword.Text).UsingJobData("content", tboContent.Text).Build();
            //4.0通過TriggerBuilder構建Trigger
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithSimpleSchedule(a => a.WithIntervalInSeconds(aDaySecond).WithRepeatCount(aWeek))
                .Build();
            //5.0組裝各個組件<Job,Trigger>
            scheduler.ScheduleJob(job, trigger);
            //6.0啟動
            scheduler.Start();
            Thread.Sleep(10000);
            //7.0銷毀內置的Job和Trigger
            scheduler.Shutdown(true);
        }
    }
    
}
