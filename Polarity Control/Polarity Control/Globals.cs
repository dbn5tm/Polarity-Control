using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polarity_Control;

namespace Polarity_Control
{
    public static class GlobalVar
    {
        static AntennaPolarityControl _Main;

        public static AntennaPolarityControl Main
        {
            get
            {
                return _Main;
            }
            set
            {
                _Main = value;
            }
        }

        static SetupForm _SetupForm;

        public static SetupForm SetupForm
        {
            get
            {
                return _SetupForm;
            }
            set
            {
                _SetupForm = value;
            }
        }

        static AzElform _AzElForm;
        public static AzElform AzElForm
        {
            get
            {
                return _AzElForm;
            }
            set
            {
                _AzElForm = value;
            }
        }
    }
}
