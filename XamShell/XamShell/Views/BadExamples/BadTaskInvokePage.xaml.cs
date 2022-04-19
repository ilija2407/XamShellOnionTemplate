using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamShell.Views.BadExamples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BadTaskInvokePage : ContentPage
    {
        public BadTaskInvokePage()
        {
            InitializeComponent();
        }
    }
}