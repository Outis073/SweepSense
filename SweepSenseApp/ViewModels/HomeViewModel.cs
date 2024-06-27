using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SweepSenseApp.Services;

namespace SweepSenseApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly UserService _userService;

        public HomeViewModel(UserService userService)
        {
            
        }

    }
}
