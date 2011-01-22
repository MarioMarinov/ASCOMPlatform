﻿using System;
using System.Windows.Forms;
using ASCOM.DeviceInterface;
using System.Collections;

namespace ASCOM.Simulator
{
    public partial class SetupDialogForm : Form
    {
        private ArrayList _switches;
        private readonly int _min;
        private const int max = 100;
        private readonly IRheostat _s1;
        private readonly IRheostat _s2;


        public SetupDialogForm()
        {
            _switches = Switches.Switches;
            InitializeComponent();
            label3.Text = Switches.Description +  @" v"+ Switches.DriverVersion;
            _min = 0;
            _s1 = (IRheostat)_switches[0];
            _s2 = (IRheostat)_switches[1];
            {
                vuMeter1.VuText = _s1.Name;
                vuMeter2.VuText = _s2.Name;
            }
            
            vuMeter1.Name = _s1.Name;
            vuMeter1.LevelMax = Convert.ToInt32(_s1.State[1]);
            trackBar1.Value =  Convert.ToInt32(_s1.State[2]);
            
            vuMeter2.Name = _s2.Name;
            vuMeter2.LevelMax = Convert.ToInt32(_s2.State[1]);
            trackBar2.Value = Convert.ToInt32(_s2.State[2]);
                   
        }

        private void Timer1Tick(object sender, EventArgs e)
        {
            vuMeter1.Level = trackBar1.Value;
            textBox2.Text = vuMeter1.PeakLevel.ToString();
            textBox1.Text = vuMeter1.Level.ToString();

            vuMeter2.Level = trackBar2.Value;
            textBox4.Text = vuMeter2.PeakLevel.ToString();
            textBox3.Text = vuMeter2.Level.ToString();

            vuMeter3.Level = vuMeter2.PeakLevel;
            label2.Text = vuMeter3.Level.ToString();
        }

        private void SetSwitch1(object sender, EventArgs e)
        {
          Switches.SetSwitch(_s1.Name, new[] { _min.ToString(), max.ToString(), vuMeter1.Level.ToString() });
          _switches = Switches.Switches;
        }

        private void SetSwitch2(object sender, EventArgs e)
        {
            Switches.SetSwitch(_s2.Name, new[] { _min.ToString(), max.ToString(), vuMeter2.Level.ToString() });
            _switches = Switches.Switches;
        }

    } 
}