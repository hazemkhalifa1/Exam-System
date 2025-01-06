using static Exam_System.AccountPages.LoginViewModel;

namespace Exam_System
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            CheckUserRole();
            InitializeComponent();
        }

        private async void CheckUserRole()
        {
            // افترض أن لديك متغير يحدد إذا كان المستخدم Admin أم لا

            if (UserSession.IsAdmin)
            {
                // إذا لم يكن المستخدم Admin، يتم تعطيل الـ Flyout
                Shell.SetFlyoutBehavior(this, FlyoutBehavior.Flyout);
            }
            else
            {
                Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
                // إذا كان المستخدم Admin، يتم تمكين الـ Flyout
            }
        }
    }
}
