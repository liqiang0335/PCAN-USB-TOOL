using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CANProject
{
    class CANMenuStrip : MenuStrip
    {
        public USBCAN usbCAN = null;


        public bool startDevice()
        {
            if (usbCAN == null)
                return false;

            return usbCAN.startDevice();
        }

        public bool shutDevice()
        {
            if (usbCAN == null)
                return false;

            return usbCAN.shutDevice();
        }
    }
}
