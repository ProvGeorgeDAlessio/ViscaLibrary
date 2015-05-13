using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ViscaLibrary
{
    public class ViscaService
    {
        //Serial 
        private SerialPort serialPort;
        public ViscaService()
        {
            serialPort = new SerialPort();
            //Sets up serial port
            serialPort.PortName = "Com10";
            serialPort.BaudRate = 9600;
            serialPort.Handshake = System.IO.Ports.Handshake.None;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.ReadTimeout = 200;
            serialPort.WriteTimeout = 50;
            serialPort.Open();
        }
        #region Sending

       
        public void SerialCmdSend(string data)
        {
          
                try
                {
                    // Send the binary data out the port
                    byte[] hexstring1 = new byte[] { 0x81, 0x01, 0x04, 0x19, 0x01, 0xFF };

                    //There is a intermitant problem that I came across
                    //If I write more than one byte in succesion without a 
                    //delay the PIC i'm communicating with will Crash
                    //I expect this id due to PC timing issues ad they are
                    //not directley connected to the COM port the solution
                    //Is a ver small 1 millisecound delay between chracters
                    
                    foreach (byte hexval in hexstring1)
                    {
                        byte[] _hexval = new byte[] { hexval }; // need to convert byte to byte[] to write
                        serialPort.Write(_hexval, 0, 1);
                        Thread.Sleep(1);
                    }
                    Thread.Sleep(5);
                    
                    byte[] hexstring2 = new byte[] { 0x81, 0x01, 0x04, 0x00, 0x03, 0xFF };
                    foreach (byte hexval in hexstring2)
                    {
                        byte[] _hexval = new byte[] { hexval }; // need to convert byte to byte[] to write
                        serialPort.Write(_hexval, 0, 1);
                        Thread.Sleep(1);
                    }
                }
                catch (Exception ex)
                {
                   
                }
            }
           
        

        #endregion
    }
}
