﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Midi.Devices;
using Midi.Messages;

namespace MidiKeyBoardTest
{
    public static class KeyboardUtilities
    {
        private static IInputDevice midiKeyboard;
        private static object NoteOnlock=new object();
        private static object NoteOfflock = new object();
        public static string logs ="";
        public static int Connect(int index)
        {
            midiKeyboard = DeviceManager.InputDevices[index];
            if (midiKeyboard.IsOpen==true)
            {
                return -1;
            }
            else
            {
                try
                {
                    midiKeyboard.Open();
                    midiKeyboard.StartReceiving(null);
                    midiKeyboard.NoteOn += NoteOn;
                    midiKeyboard.NoteOff += NoteOff;
                }
                catch (Exception e)
                {
                    MessageBox.Show($"连接错误 \r\n {e.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            return 0;
        }

        public static void Disconnect()
        {
            if (midiKeyboard == null) return;
            if (midiKeyboard.IsOpen == true)
            {
                try
                {
                    midiKeyboard.StopReceiving();
                    midiKeyboard.Close();
                    midiKeyboard.RemoveAllEventHandlers();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"断开错误 \r\n {e.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
           

        }
        public static List<string> GetKeyboardList()
        {
            List<string> ret=new List<string>();
            DeviceManager.UpdateInputDevices();
            foreach (var device in DeviceManager.InputDevices)
            {
                ret.Add(device.Name); 
            }

            return ret;
        }

        public static void NoteOn(NoteOnMessage msg)
        {
            lock (NoteOnlock)
            {
                logs += $"Time: {DateTime.Now.ToString("HH:mm:ss:ffffff")}  Event:NoteOn Pitch:{msg.Pitch} \r\n";
            }
        }

        public static void NoteOff(NoteOffMessage msg)
        {
            lock (NoteOfflock)
            {
                logs += $"Time: {DateTime.Now.ToString("HH:mm:ss:ffffff")}  Event:NoteOff Pitch:{msg.Pitch} \r\n";
            }
        }

    }
}
